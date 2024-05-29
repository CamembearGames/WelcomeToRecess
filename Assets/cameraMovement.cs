using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class cameraMovement : MonoBehaviour
{

    public Vector2 yaw = Vector2.zero;
    public Vector2 pitch = Vector2.zero;

    public Vector2 x_mov = Vector2.zero;
    public Vector2 y_mov = Vector2.zero;

    public float threshold = 0.0f;

    [SerializeField] private GameManager GM;

    [SerializeField] private CinemachineVirtualCamera currentCamera;

    private CinemachineVirtualCamera oldCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if (GM.canRotate & GM.canInteract)
        {

            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 viewportSize = new Vector2(Screen.width, Screen.height);
            Vector2 screenRatio = mousePos/viewportSize;
            Vector2 targetRotation = new Vector2(Mathf.Lerp(yaw.x,yaw.y, screenRatio.x), Mathf.Lerp(pitch.x,pitch.y, screenRatio.y));
            float timeRatio = 2.5f;
            //currentCamera.transform.eulerAngles = new Vector3(Mathf.LerpAngle(currentCamera.transform.eulerAngles.x,targetRotation.y, Time.deltaTime*timeRatio),Mathf.LerpAngle(currentCamera.transform.eulerAngles.y,targetRotation.x,Time.deltaTime*timeRatio),0);  //Debug.Log(Vector3.Angle(target_rotation,  currentCamera.transform.eulerAngles));
            //currentCamera.transform.DOKill();

            //if (Vector3.Distance(target_rotation,  currentCamera.transform.eulerAngles)> threshold) 
            //currentCamera.transform.DORotate(target_rotation, 1.5f);
        }

        
    }*/

    public void switchCamera(CinemachineVirtualCamera newCamera)
    { 
        newCamera.Priority = 20;
        currentCamera.Priority = 10;
        oldCamera = newCamera;
    }

    public void resetCamera()
    {
        if (oldCamera != null)
        {
            oldCamera.Priority = 10;
            currentCamera.Priority = 20;
        }
    }
}
