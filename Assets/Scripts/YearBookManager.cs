using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class YearBookManager : MonoBehaviour
{
    [SerializeField] private YearBookScript book;
    [SerializeField] private TextMeshProUGUI[] Text;
    [SerializeField] private Image[] Images;
    [SerializeField] private List<ScriptableInteractions> Interactions;
    [SerializeField] private RectTransform[] CharPosition;
    [SerializeField] private GameObject CharPortrait;
    [SerializeField] private GameObject CharPortraitPosition;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private RectTransform graphImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button3D bookButton;

    [SerializeField] private TextMeshProUGUI tutText;
    [SerializeField] private Image tutimage;

    [SerializeField] private GameObject endPanel;

    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private Image endLogo1;
    [SerializeField] private Image endLogo2;

    [SerializeField] private Button endYearButton;

    private RectTransform currentSelectedChar;

    private int currentSelectedCharIndex = 0;

    private void Awake() {
        panel.gameObject.SetActive(true);
        
    }

    private void Start() {        

        Interactions = GameData.Instance.Interactions;
        
        foreach(TextMeshProUGUI objet in Text)
        {
            objet.DOFade(0f, 0.1f);
        }
        foreach(Image objet in Images)
        {
            objet.DOFade(0f, 0.1f);
        }

        endText.DOFade(0f,0.1f);
        endLogo1.DOFade(0f,0.1f);
        endLogo2.DOFade(0f,0.1f);

        for (int i = 0; i < CharPosition.Count(); i++)
        {
            if (i < Interactions.Count())
            {
                Debug.Log(Interactions[i]);
                GameObject newCharPortrait = Instantiate(CharPortrait, Vector3.zero, Quaternion.identity);
                newCharPortrait.GetComponent<Image>().sprite = Interactions[i].portraitOfCharacter;
                newCharPortrait.transform.SetParent(CharPosition[i], false);  
                newCharPortrait.GetComponent<CharacterInteractionPortrait>().graphImage = graphImage;
                newCharPortrait.GetComponent<CharacterInteractionPortrait>().ybManager = this;       
            }
            else CharPosition[i].gameObject.SetActive(false);
     
        }

        Invoke("FadeIn",0.5f);
    }

    private void FadeIn()
    {
        panel.GetComponent<Image>().DOFade(0.0f,1.0f).OnComplete(DesactivatePanel);
        bookButton.canBookBePressed = true;
    }

    private void DesactivatePanel()
    {
        panel.gameObject.SetActive(false);
    }

    private void loadInteractions()
    {
        for (int i = 0; i < Interactions.Count(); i++)
        {
            CharPosition[i].GetComponentInChildren<CharacterInteractionPortrait>().MakeAppear();
        }

        Invoke("LoadNextInteraction",1.5f);
    }

    public void LoadNextInteraction()
    {
        if (currentSelectedCharIndex == 1 ||  Interactions.Count() == 0)
        {
            tutText.DOFade(0f, 0.5f);
            tutimage.DOFade(0f, 0.5f).OnComplete(DesactivateTurtorial);
        }
        if (currentSelectedCharIndex < Interactions.Count())
        {
            questionText.text = Interactions[currentSelectedCharIndex].question;
            currentSelectedChar = CharPosition[currentSelectedCharIndex];
            currentSelectedChar.transform.DORotate(CharPortraitPosition.transform.localRotation.eulerAngles, 0.5f);
            currentSelectedChar.transform.DOScale(CharPortraitPosition.transform.localScale, 0.5f);
            currentSelectedChar.transform.DOMove(CharPortraitPosition.transform.position, 0.5f).OnComplete(setActive);
            currentSelectedCharIndex ++;
        }
        else
        {
            endYearButton.gameObject.SetActive(true);
            endYearButton.transform.DOScale(1.0f, 0.3f);
        }
    }

    private void DesactivateTurtorial()
    {
        tutText.gameObject.SetActive(false);
        tutimage.gameObject.SetActive(false);

    }
    private void setActive()
    {
        currentSelectedChar.GetComponentInChildren<CharacterInteractionPortrait>().isSelected = true;
        currentSelectedChar.GetComponentInChildren<CharacterInteractionPortrait>().canBeDragged = true;

    }

    public void OpenBook()
    {
        book.OpenBook();
        Invoke("BookFinishedOpening", 4f);

    }

    private void BookFinishedOpening()
    {
        loadInteractions();
        
        foreach(TextMeshProUGUI objet in Text)
        {
            objet.DOFade(1f, 1f);
        }
        foreach(Image objet in Images)
        {
            objet.DOFade(1f, 1f);
        }
        
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ScreenCapture.CaptureScreenshot("YearBook.png");
            Debug.Log("A screenshot was taken!");
        }
    }

    public void EndYear() {
        panel.gameObject.SetActive(true);
        panel.GetComponent<Image>().DOFade(1.0f,1.0f).OnComplete(ShowEndText);
    }

    private void ShowEndText() {
        endPanel.SetActive(true);
        endText.DOFade(1f,0.5f);
        endLogo1.DOFade(1f,0.5f);
        endLogo2.DOFade(1f,0.5f);
    }

    public void QuitGame() {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void RestartGame() {
        LevelLoader.Load(LevelLoader.Scene.MainMenu);
    }
}
