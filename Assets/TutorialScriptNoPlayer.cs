using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TutorialScriptNoPlayer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera []tutorialCameras;
    [SerializeField] private TextAsset []tutorialText;
    [SerializeField] private DialogManager diagManager;
    [SerializeField] private ScriptableCharacter tutorialCharacter;
    [SerializeField] private GameManager GM;


    private int iteration = 1;

    public void SwitchCamera()
    {
        CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.Priority = 10;
        tutorialCameras[iteration].Priority = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.Priority+1;
        iteration ++;
    }

    public void StartDialog()
    {
        Invoke("StartNextDialog", 2f);
    }    
    public void StartNextDialog()
    {
        diagManager.EnterDialogMode(tutorialText[iteration-1], tutorialCharacter, true, 0.5f);
    }

    public void EndCamera()
    {
        CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.Priority = 10;
        tutorialCameras[tutorialCameras.Length-1].Priority = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.Priority+1;
        GM.canInteract = true;
        GM.canRotate = true;
    }
    
}
