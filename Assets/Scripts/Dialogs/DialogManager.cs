using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using System.Linq.Expressions;
using System.Diagnostics;
using UnityEngine.EventSystems;


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
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
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
    
        ContinueStory();
    }
    private IEnumerator ExitDialogMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogIsPlaying = false;
        dialogPanel.SetActive(false);   
        dialogText.text = "";

        currentStory.UnbindExternalFunction("PerformActivity");
    } 

    private void ContinueStory(){
        if (currentStory.canContinue)
        {
            dialogText.text = currentStory.Continue();
            DisplayChoices();
        }else{
            StartCoroutine(ExitDialogMode());
        }
    }

    private void DisplayChoices()
    {
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

        StartCoroutine(SelectFirstChoice());

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
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }


}
