using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //references the LevelGenerator script which is found on the levelGenerator GameObject in the scene
    private LevelGenerator lg;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        lg = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Level: " + lg.levelNumber.ToString();
    }

}
