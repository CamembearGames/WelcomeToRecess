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


public class DialogManager : MonoBehaviour
{


    [Header("GM")]
    public GameManager gameManagerReference;

    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private Story currentStory;
    public bool dialogIsPlaying{get; private set;}

    private static DialogManager instance;

    public PlayerInputActions playerControls;
    private InputAction continueTalk;

    public NonPlayableCharacter currentNPC;

    public GameObject dialogTextPrefab;
    public GameObject content;

    private bool reverseText = true;



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

    public static DialogManager GetInstance()   
    {
        return instance;
    }

    private void Start() {
        dialogIsPlaying = false;
        dialogPanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponent<TextMeshProUGUI>();
            index++;
        }
        //EventSystem.current.SetSelectedGameObject(null);
    }


    public void EnterDialogMode( TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;
        dialogPanel.SetActive(true);

        currentStory.BindExternalFunction("PerformActivity", () => {
            gameManagerReference.PeformActivity();
            StartCoroutine(ExitDialogMode());
        });

        /*currentStory.BindExternalFunction("PlayCards", () => {
            gameManagerReference.PlayCards();
            StartCoroutine(ExitDialogMode());
        });*/

        currentStory.BindExternalFunction("ChangeRelashionship", (string name, int value) => {
            gameManagerReference.ChangeRelationship(name, value);
        });

        currentStory.BindExternalFunction("GoBackToClass", () => {
            gameManagerReference.GoBackToClass();
            StartCoroutine(ExitDialogMode());
        });

        ContinueStory();
    }
    private IEnumerator ExitDialogMode()
    {
        Debug.Log("Stop dialog");
        yield return new WaitForSeconds(0.2f);
        dialogIsPlaying = false;
        dialogPanel.SetActive(false);   
        dialogText.text = "";

        //currentStory.UnbindExternalFunction("PlayCards");
        currentStory.UnbindExternalFunction("ChangeRelashionship");
        currentStory.UnbindExternalFunction("PerformActivity");
    } 

    private void ContinueStory(){
        if (currentStory.canContinue)
        {
            Debug.Log("Printing Text");
            dialogText.text = currentStory.Continue();
            DisplayChoices();
        }
        else{
            StartCoroutine(ExitDialogMode());
        }
    }

    private void DisplayChoices()
    {
        Debug.Log("Loading Choices");
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            UnityEngine.Debug.LogError("More choices in text than in UI");
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        //StartCoroutine(SelectFirstChoice());

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
        //EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {

        currentStory.ChooseChoiceIndex(choiceIndex);
        GameObject dialogBox =  Instantiate(dialogTextPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        dialogBox.transform.SetParent(content.transform, false);
        dialogBox.GetComponent<HorizontalLayoutGroup>().reverseArrangement = reverseText;
        reverseText = !reverseText;
        dialogBox.GetComponentInChildren<TextMeshProUGUI>().text = choicesText[choiceIndex].text;
        dialogBox =  Instantiate(dialogTextPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        dialogBox.transform.SetParent(content.transform, false);
        dialogText = dialogBox.GetComponentInChildren<TextMeshProUGUI>();


        ContinueStory();
    }


}
