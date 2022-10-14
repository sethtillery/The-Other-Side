using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromSky : MonoBehaviour
{
    //used to represent the current position of the spawner
    Vector3 position;

    //itemArray is used to store references to all of the food prefabs
    [SerializeField]
    private GameObject[] itemArray;

    //SpawnItems
    //will take a levelNumber as a parameter and will spawn items for that level
    public void SpawnItems(int levelNum, int numOfItems)
    {
        for(int i = 0; i < numOfItems; i++)
        {
            Instantiate(itemArray[Random.Range(0, 3)], new Vector3(Random.Range(-42 + (levelNum - 1) * 100, -52 + (levelNum * 100)), 3, Random.Range(-13.0f, 33.0f)), Quaternion.identity);
        }
    }

}
