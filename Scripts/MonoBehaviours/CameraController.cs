using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    //store references to both of the used virtualCameras
    [SerializeField]
    private CinemachineVirtualCamera vcam1;
    [SerializeField]
    private CinemachineVirtualCamera vcam2;

    //usingCam1 will be set to true only if the game is rendering using virtual camera 1
    private bool usingCam1 = true;

    //private IEnumerator moveCamCoroutine is used for the MoveCamera coroutine
    private IEnumerator moveCamCoroutine;

    //SwitchCameras()
    //will switch which virtual camera is currently being used to render.
    public void SwitchCameras()
    {
        if (usingCam1)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            moveCamCoroutine = MoveCamera(vcam1);
            StartCoroutine(moveCamCoroutine);
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
            moveCamCoroutine = MoveCamera(vcam2);
            StartCoroutine(moveCamCoroutine);
        }
        usingCam1 = !usingCam1;
    }

    //MoveCamera()
    //will move the camera up
    public IEnumerator MoveCamera(CinemachineVirtualCamera camera)
    {
        yield return new WaitForSeconds(1.0f);
        camera.transform.localPosition = new Vector3(camera.transform.localPosition.x + 200, 215.1f, 27.6f);
    }


}
