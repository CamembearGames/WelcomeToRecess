using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using System.Linq.Expressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using DG.Tweening;


public class DialogManager : MonoBehaviour
{


    [Header("GM")]
    public GameManager gameManagerReference;

    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI textBox;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] private GameObject char1Portrait;
    [SerializeField] private GameObject char2Portrait;

    [SerializeField] private GameObject containerAnswers;

    private bool isTutorial;

    private Story currentStory;
    public bool dialogIsPlaying{get; private set;}

    public static DialogManager instance;

    public PlayerInputActions playerControls;
    private InputAction continueTalk;
        
    private InputAction navAnswer;

    public NonPlayableCharacter currentNPC;

    //private bool reverseText = true;

    //public GameObject playerBubble;
    //public GameObject npcBubble;

    public PlayerController player;


    private int lastChoice = 0;
    //private int selectedAnswer = 0;
    //private int oldSelectedAnswer = 0;


    private void Awake() {
        playerControls = new PlayerInputActions();

        continueTalk = playerControls.UI.ContinueDialog;
        continueTalk.Enable();
        continueTalk.performed += ContinuePressed;


        if (instance != null)
        {
            UnityEngine.Debug.LogWarning("Too many dialog managers");
        }
        instance = this;
    }

    /*private void Update()
    {
        delay -= Time.deltaTime;
        if (delay<0 && dialogIsPlaying)
        {
            int navVector = Mathf.RoundToInt(navAnswer.ReadValue<Vector2>().y);
            selectedAnswer -= navVector;
            if (selectedAnswer>choices.Length-1) selectedAnswer = 0;
            if (selectedAnswer<0) selectedAnswer = choices.Length-1;

            if (selectedAnswer!=oldSelectedAnswer)
            {
                choices[selectedAnswer].GetComponent<AnswerButton>().OnHighlightEnter();
                choices[oldSelectedAnswer].GetComponent<AnswerButton>().OnHighlightExit();
                oldSelectedAnswer = selectedAnswer;
            }
            delay = delayMax;
        }

    }*/

    public static DialogManager GetInstance()   
    {
        return instance;
    }

    private void Start() {
        dialogIsPlaying = false;

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        //EventSystem.current.SetSelectedGameObject(null);
    }

    public void EnterDialogMode(TextAsset inkJSON, ScriptableCharacter char1, ScriptableCharacter char2, bool isInTutorial)
    {

        if (player!=null) player.OnDisable();
        isTutorial = isInTutorial;

        // Update portraits of dialog mode and show them
        if (char1 != null) 
        {
            char1Portrait.GetComponent<Image>().sprite = char1.portraitOfCharacter;
            char1Portrait.GetComponent<Image>().color = Color.white;
        }
        if (char2 != null) 
        {
            char2Portrait.GetComponent<Image>().sprite = char2.portraitOfCharacter;
            if (char1 != null) char2Portrait.GetComponent<Image>().color = Color.grey;
            else char2Portrait.GetComponent<Image>().color = Color.white;
        }

        char1Portrait.SetActive(char1 != null);
        char2Portrait.SetActive(char2 != null);

        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;
        //selectedAnswer = 0;
        //oldSelectedAnswer = 0;

        if(char1 != null && !isTutorial) currentStory.variablesState[char1.nameOfCharacter+"Friendship"] = GameData.Instance.relationshipDatabase[char1.nameOfCharacter];
        if(char1 != null && !isTutorial) currentStory.variablesState["talkAlready"] = GameData.Instance.talkAlreadyDatabase[char1.nameOfCharacter];

        dialogPanel.GetComponent<DialogAnimatedV2>().ShowDialogBox();

        currentStory.BindExternalFunction("UpdateRelashionship", (string name, int value) => {
            gameManagerReference.UpdateRelashionship(name, value);
        });

        currentStory.BindExternalFunction("UpdateTalkAlready", (string name, bool value) => {
            gameManagerReference.UpdateTalkAlready(name, value);
        });

        currentStory.BindExternalFunction("PerformActivity", () => {
            gameManagerReference.PeformActivity();
        });

        currentStory.BindExternalFunction("ChangeRelashionship", (string name, int value) => {
            gameManagerReference.ChangeRelationship(name, value);
        });

        currentStory.BindExternalFunction("GoBackToClass", () => {
            gameManagerReference.GoBackToClass();
            //StartCoroutine(ExitDialogMode());
        });

        currentStory.BindExternalFunction("GoBackToRecess", () => {
            gameManagerReference.GoBackToRecess();
            //StartCoroutine(ExitDialogMode());
        });

        currentStory.BindExternalFunction("ContinueTutorial", () => {
            gameManagerReference.ContinueTutorial();
            //StartCoroutine(ExitDialogMode());
        });

        currentStory.BindExternalFunction("CancelTutorial", () => {
            gameManagerReference.CancelTutorial();
        });



        ContinueStory();
    }
    private IEnumerator ExitDialogMode()
    {
        yield return new WaitForSeconds(0.2f);

        containerAnswers.transform.DOScale(0.0f, 0.4f);

        //currentNPC.HideDialogBox();
        //npcBubble.SetActive(false);
        //player.FinishDialog();

        dialogIsPlaying = false;
        dialogPanel.GetComponent<DialogAnimatedV2>().HideDialogBox(); 
        //dialogText.text = "";

        //currentStory.UnbindExternalFunction("PlayCards");
        currentStory.UnbindExternalFunction("ChangeRelashionship");
        currentStory.UnbindExternalFunction("GoBackToClass");
        currentStory.UnbindExternalFunction("GoBackToRecess");
        currentStory.UnbindExternalFunction("ContinueTutorial");
        currentStory.UnbindExternalFunction("CancelTutorial");
        currentStory.UnbindExternalFunction("UpdateRelashionship");
        currentStory.UnbindExternalFunction("UpdateTalkAlready");

        if (!isTutorial & player!=null) player.OnEnable();

    } 

    private void ContinueStory(){
        if (currentStory.canContinue)
        {
            //dialogText.text = currentStory.Continue();

            String newtext = currentStory.Continue();

            if(newtext != "")
            {
                //npcBubble.GetComponentInChildren<TextMeshPro>().text = newtext;
                if (char1Portrait.activeSelf) char1Portrait.GetComponent<Image>().DOColor(Color.white, 0.3f);
                if (char2Portrait.activeSelf){
                    if(char1Portrait.activeSelf) char2Portrait.GetComponent<Image>().DOColor(Color.grey, 0.3f);
                    else char2Portrait.GetComponent<Image>().DOColor(Color.white, 0.3f);
                } 
                dialogPanel.GetComponent<DialogAnimatedV2>().AddWriter(textBox,newtext, 0.04f, true);
                
            }
            else
            {
                if (gameObject  != null) StartCoroutine(ExitDialogMode());
            }
        }
        else{
            if (gameObject  != null) StartCoroutine(ExitDialogMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;


        if (currentChoices.Count > choices.Length)
        {
            UnityEngine.Debug.LogError("More choices in text than in UI");
        }

        if (currentChoices.Count > 0)
        
        {
            //choices[lastChoice].gameObject.GetComponent<RectTransform>().DOScale(0.0f, 0.4f);

            containerAnswers.transform.DOScale(1.0f, 0.4f);

            int index = 0;

            foreach (Choice choice in currentChoices)
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                if (index > 0) choices[index].gameObject.GetComponent<RectTransform>().DOScale(1.0f, 0.4f);
                choices[index].gameObject.GetComponent<Button>().interactable = true;
                index++;
            }

            for (int i = index; i < choices.Length; i++)
            {
                choices[index].gameObject.GetComponent<Button>().interactable = false;
                choices[index].gameObject.GetComponent<RectTransform>().DOScale(0.0f, 0.4f);

                //choices[i].gameObject.SetActive(false);
            }

            if (char1Portrait.activeSelf) char1Portrait.GetComponent<Image>().DOColor(Color.grey, 0.3f);
            if (char2Portrait.activeSelf) char2Portrait.GetComponent<Image>().DOColor(Color.white, 0.3f);

            //Invoke("SelectFirstChoice", 0.4f);

            StartCoroutine(SelectFirstChoice());
        } 

        

    }

    private void ContinuePressed (InputAction.CallbackContext context)
    {       
        if(!dialogIsPlaying)
        {
            return;
        }
        if ( currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
        
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        //choices[0].GetComponent<AnswerButton>().OnHighlightEnter();
    }

    public void MakeChoice(int choiceIndex)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.GetComponent<Button>().interactable = false;
            //if (i != choiceIndex) choices[i].gameObject.GetComponent<RectTransform>().DOScale(0.0f, 0.2f); //choices[i].gameObject.SetActive(false);
        }

        currentStory.ChooseChoiceIndex(choiceIndex);
        lastChoice = choiceIndex;
        //GameObject dialogBox =  Instantiate(dialogTextPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //dialogBox.transform.SetParent(content.transform, false);
        //dialogBox.GetComponent<HorizontalLayoutGroup>().reverseArrangement = reverseText;
        //reverseText = !reverseText;
        //dialogBox.GetComponentInChildren<TextMeshProUGUI>().text = choicesText[choiceIndex].text;
        //dialogBox =  Instantiate(dialogTextPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        ///dialogBox.transform.SetParent(content.transform, false);
        //dialogText = dialogBox.GetComponentInChildren<TextMeshProUGUI>();


        ContinueStory();
    }

    public void TextFinishedLoading()
    {
        DisplayChoices();
    }
}
