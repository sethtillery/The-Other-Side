using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public GameObject LM;
    Vector3 down = new Vector3(0, 180, 0);

    

    // Start is called before the first frame update
    void Start()
    {

        LM = GameObject.FindGameObjectWithTag("LevelGenerator");
        // Settig speed based off of current level as well as rotating the animal rotatint the animal if it is in the botom of the screen
        if (transform.position.z <= 0)
        {
            if(LM != null)
            {
                speed += (.01f * LM.GetComponent<LevelGenerator>().levelNumber);
            }
        }
        else
        {
            if(LM != null)
            {
                speed += (.01f * LM.GetComponent<LevelGenerator>().levelNumber);

            }
            transform.Rotate(down);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
        // move the animal forward at a constant rate
        transform.Translate(Vector3.forward * speed);

        DestroyGameObject();
    }

    
    void DestroyGameObject()
    {
        // removing the game object if it goes above or below the map
        if (transform.position.z >= 60 || transform.position.z <= -30)
        {
            Destroy(gameObject);
        }
    }
}
