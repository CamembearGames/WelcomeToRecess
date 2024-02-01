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
using NUnit.Framework;


public class DialogManager : MonoBehaviour
{
    [Header("GM")]
    public GameManager gameManagerReference;

    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject slider;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject char1Portrait;
    [SerializeField] private GameObject char2Portrait;
    [SerializeField] private GameObject containerAnswers;


    public bool dialogIsPlaying{get; private set;}
    public static DialogManager instance;
    public PlayerInputActions playerControls;
    public NonPlayableCharacter currentNPC;
    public PlayerController player;

    private TextMeshProUGUI[] choicesText;
    private bool isTutorial;
    private Story currentStory;
    private int lastChoice = 0;
    private bool textFinishedLoading = false;
    private bool privateTalk = true;
    private float exitTime;
    private InputAction continueTalk;


    //private InputAction navAnswer;
    //private bool reverseText = true;
    //public GameObject playerBubble;
    //public GameObject npcBubble;
    //private int selectedAnswer = 0;
    //private int oldSelectedAnswer = 0;

    private void Awake() {
        playerControls = new PlayerInputActions();

        continueTalk = playerControls.UI.ContinueDialog;
        continueTalk.performed += ContinuePressed;
        continueTalk.Enable();


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

    public void EnterDialogMode(TextAsset inkJSON, ScriptableCharacter char1, ScriptableCharacter char2, bool isInTutorial, float exitTimeVar)
    {

        exitTime = exitTimeVar;
        // Char1 is the person the plkayer talks to
        if (player!=null) player.OnDisable();
        isTutorial = isInTutorial;

        // Update portraits of dialog mode and show them
        if (char1 != null) 
        {
            slider.SetActive(true);
            privateTalk = false;
            if (GameData.Instance)slider.GetComponent<SliderController>().UpdateProgress(GameData.Instance.relationshipDatabase[char1.nameOfCharacter]);
            char1Portrait.GetComponent<Image>().sprite = char1.portraitOfCharacter;
            char1Portrait.GetComponent<Image>().color = Color.white;
        }
        else privateTalk = true;
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

        if (GameData.Instance)if(char1 != null && !isTutorial) currentStory.variablesState[char1.nameOfCharacter+"Friendship"] = GameData.Instance.relationshipDatabase[char1.nameOfCharacter];
        if (GameData.Instance)if(char1 != null && !isTutorial) currentStory.variablesState["talkAlready"] = GameData.Instance.talkAlreadyDatabase[char1.nameOfCharacter];

        

        currentStory.BindExternalFunction("UpdateRelashionship", (string name, int value) => {
            gameManagerReference.UpdateRelashionship(name, value);
        });

        currentStory.BindExternalFunction("UpdateTalkAlready", (string name, bool value) => {
            gameManagerReference.UpdateTalkAlready(name, value);
        });

        currentStory.BindExternalFunction("ChangeRelashionship", (string name, int value) => {
            gameManagerReference.ChangeRelationship(name, value);
        });

        currentStory.BindExternalFunction("GoBackToClass", () => {
            gameManagerReference.GoBackToClass();
        });

        currentStory.BindExternalFunction("GoBackToRecess", () => {
            gameManagerReference.GoBackToRecess();
        });

        currentStory.BindExternalFunction("ContinueTutorial", () => {
            gameManagerReference.ContinueTutorial();
        });

        currentStory.BindExternalFunction("CancelTutorial", () => {
            gameManagerReference.CancelTutorial();
        });

        currentStory.BindExternalFunction("UseTimeSlot", (int numberOfTimeSlots) => {
            gameManagerReference.UseTimeSlot(numberOfTimeSlots);
        });

        currentStory.BindExternalFunction("StartMiniGame", (int miniGameNumber) => {
            gameManagerReference.StartMiniGame(miniGameNumber);
        });

        currentStory.BindExternalFunction("StartPong", () => {
            gameManagerReference.StartPongMatch();
        });

        dialogPanel.GetComponent<DialogAnimatedV2>().ShowDialogBox();

        ContinueStory();

    }
    private IEnumerator ExitDialogMode()
    {
        yield return new WaitForSeconds(0.2f);

        containerAnswers.transform.DOScale(0.0f, 0.4f);

        currentStory.UnbindExternalFunction("GoBackToClass");
        currentStory.UnbindExternalFunction("GoBackToRecess");
        currentStory.UnbindExternalFunction("ContinueTutorial");
        currentStory.UnbindExternalFunction("CancelTutorial");
        currentStory.UnbindExternalFunction("UpdateRelashionship");
        currentStory.UnbindExternalFunction("UpdateTalkAlready");
        currentStory.UnbindExternalFunction("UseTimeSlot");
        currentStory.UnbindExternalFunction("StartPong");

        if (privateTalk) Invoke("HideBox", 0.1f);
        else Invoke("HideBox", exitTime);

    } 

    public void RemoveBindings()
    {
        continueTalk.ChangeBinding(0).Erase();
    }

    private void HideBox()
    {
        dialogIsPlaying = false;
        dialogPanel.GetComponent<DialogAnimatedV2>().HideDialogBox(); 
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
                textFinishedLoading = false;
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
                if (index > 0)
                {
                    Button currentButton = choices[index].gameObject.GetComponent<Button>();
                    choices[index].gameObject.GetComponent<RectTransform>().DOScale(1.0f, 0.4f).OnComplete(()=>ActivateButton(currentButton));
                }
                
                 
                //choices[index].gameObject.GetComponent<Button>().interactable = true;
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

    private void ActivateButton(Button button)
    {
        choices[0].gameObject.GetComponent<Button>().interactable = true;
        button.interactable = true;
    }

    private void ContinuePressed (InputAction.CallbackContext context)
    {               
        if(!dialogIsPlaying)
        {
            return;
        }
        if ( currentStory.currentChoices.Count == 0)
        {
            if (textFinishedLoading) ContinueStory();
            else if (!textFinishedLoading) 
            {
                textFinishedLoading = true;
                dialogPanel.GetComponent<DialogAnimatedV2>().ShowAllText();
            }

        }
        else if (currentStory.currentChoices.Count > 0)
        {
            if (!textFinishedLoading)
            {
                textFinishedLoading = true;
                dialogPanel.GetComponent<DialogAnimatedV2>().ShowAllText();
            }

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
        DOTween.KillAll();
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.GetComponent<Button>().interactable = false;
            if (i != choiceIndex) choices[i].gameObject.GetComponent<RectTransform>().DOScale(0.0f, 0.2f); //choices[i].gameObject.SetActive(false);
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
        textFinishedLoading = true;
        DisplayChoices();
    }
}
