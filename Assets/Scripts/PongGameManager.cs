using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PongGameManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject playerPaddle;
    [SerializeField] private GameObject playerGoal;
    [SerializeField] private GameObject computerPaddle;
    [SerializeField] private GameObject computerGoal;
    [SerializeField] private GameObject playerScoreText;
    [SerializeField] private GameObject computerScoreText;
    [SerializeField] private TextMeshProUGUI passesText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private DialogManager diagManager;
    [SerializeField] private TextAsset pongInkFile;
    [SerializeField] private TextAsset passesFailedInkFile;
    [SerializeField] private TextAsset passesCompleteInkFile;
    [SerializeField] private ScriptableCharacter char1;
    [SerializeField] private ScriptableCharacter char2;

    [SerializeField] private TextMeshProUGUI ExchangeText;
    [SerializeField] private TextMeshProUGUI ExchangeNumber;

    private int playerScore = 0;
    private int computerScore = 0;
    private int numberOfPasses = 0;
    private int countdown = 3;

    private Color transparent = Color.white;


    private void Start() 
    {
        Invoke("StartTalk",1f);
    }
    public void PlayerScored()
    {
        playerScore++;
        playerScoreText.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
        ResetPosition();
        
    }

    public void StartTalk()
    {
        DialogManager.GetInstance().EnterDialogMode(pongInkFile, char1, false, 0.1f);
    }

    public void ComputerScored()
    {

        computerScore++;
        computerScoreText.GetComponent<TextMeshProUGUI>().text = computerScore.ToString();
        ResetPosition();
    }

    public void ResetPosition()
    {
        ball.GetComponent<BallScript>().Reset();
        //playerPaddle.GetComponent<PlayerPaddle>().Reset();

        numberOfPasses = 0;
        passesText.text = numberOfPasses.ToString();

    }

    public void AddPass()
    {
        numberOfPasses++;
        passesText.text = numberOfPasses.ToString();

        if(numberOfPasses == 3)
        {
            FreezeGame();
            GameData.Instance.miniGameWon = false;
            DialogManager.GetInstance().EnterDialogMode(passesCompleteInkFile, char1, false, 0.1f);
            //LevelLoader.Load(LevelLoader.Scene.Recess);
        }
    }

    public void StartGame()
    {
        if (countdown > -1)
        {
            countDownText.text = countdown.ToString();
            countDownText.transform.localScale = Vector3.zero;
            transparent.a = 1.0f;

            countDownText.color = transparent;
  
            countdown --;
            countDownText.transform.DOScale(2, 0.7f).OnComplete(MakeTransparent);
        }
        else
        {
            launchBall();
        }
        
    }

    void MakeTransparent()
    {
                    
        transparent.a = 0.0f;
        countDownText.DOColor(transparent, 0.5f).OnComplete(StartGame);
        
    }

    void MakeTransparentFinal()
    {      
        transparent.a = 0.0f;
        countDownText.DOColor(transparent, 0.5f);
        ExchangeNumber.DOFade(1f,0.5f);
        ExchangeText.DOFade(1f,0.5f);
    }
    private void launchBall()
    {

        countDownText.text = "Go";
        countDownText.transform.localScale = Vector3.zero;
        transparent.a = 1.0f;
        countDownText.color = transparent;

        countDownText.transform.DOScale(2, 0.7f).OnComplete(MakeTransparentFinal);

        
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        ResetPosition();
    }

    private void FreezeGame()
    {
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //ResetPosition();
    }
}
