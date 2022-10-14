using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevelFunction : MonoBehaviour
{
    //references the LevelGenerator script which is found on the levelGenerator GameObject in the scene
    private LevelGenerator lg;

    //references the virtualCamera object
    private GameObject virtualCameras;

    //references the SkySpawner
    private GameObject skySpawner;


    private void Start()
    {
        lg = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
        virtualCameras = GameObject.FindGameObjectWithTag("VirtualCameras");
        skySpawner = GameObject.FindGameObjectWithTag("SkySpawner");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lg.levelNumber += 1;
            skySpawner.GetComponent<SpawnFromSky>().SpawnItems(lg.levelNumber, 3);
            virtualCameras.GetComponent<CameraController>().SwitchCameras();
            lg.SelectLevel(lg.SelectLevelNumber(), lg.levelNumber + 1);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
