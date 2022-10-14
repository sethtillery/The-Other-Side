using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class spawner : MonoBehaviour
{
    // Any prefab object that you want to spawn (player or enemy in our game)
    public GameObject prefabToSpawn;

    // used to determine repeatInterval
    public float baseRepeat;

    // Used to spawn multiple copies at a set interval (primarily for enemy objects)
    public float repeatInterval;

    //Gets the level manager game object
    public GameObject LM;

    public void OnEnable()
    {
        LM = GameObject.FindGameObjectWithTag("LevelGenerator");
        if(LM != null)
        {
            repeatInterval = (baseRepeat - .03f * LM.GetComponent<LevelGenerator>().levelNumber);
        }
        StartCoroutine(SpawnTimer());
    }



    // Used to spawn a new game object
    public void SpawnObject()
    {
        // Instantiate the prefab at the location of the current SpawnPoint object
        // Quaternion is a data structure used to represent rotations; identity = no rotation
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }

    //  Used as a timer for spawning animals
    public IEnumerator SpawnTimer()
    {
        while(true)
        {
            SpawnObject();
            yield return new WaitForSeconds(repeatInterval);
        }
    }



}
