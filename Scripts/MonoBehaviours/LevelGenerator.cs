using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Used to determine which level is being generated.
    private int levelsGenerated = 1;

    //levelNumber is the number of the level on which the player is currently playing.
    public int levelNumber = 1;

    //stripsGenPerLevel is the number of strips which make up a level with the exception of the grass strips
    //at the beginning and end of each level.
    private int stripsGenPerLevel = 8;

    //numOfLevels is the number of levels which will be generated at the start of the game.
    private int numOfLevels = 50;

    //levelStrips contains all of the prefabs which will be used to create strips for each level.
    public GameObject[] levelStrips;

    //levelArray will store all of the Level GameObjects which are generated. It will be accessed
    //in selecting which level comes next.
    private GameObject[] levelArray = new GameObject[51];

    //inPlayLevels is used to store the indices of which levels should not be selected because they are already selected
    //inPlayLevels[0] represents the previous Level which the player just played.
    //inPlayLevels[1] represents the current level which the player is on.
    private int[] inPlayLevels = { 0, 1 };

    //nextLevel is used each time to store everything for a new level until the level is stored.
    private GameObject nextLevel;

    //unselectCoroutine is used to call the Unselect() Coroutine
    private IEnumerator unselectCoroutine;

    //Setups the game.
    private void Start()
    {
        SetupGame();
    }

    //PickStripCategory()
    //will return a string which can be used to select different strips options.
    public string PickStripCategory()
    {
        int randomNumber = Random.Range(1, 100);
        if (randomNumber >= 1 && randomNumber <= 20)
        {
            return "Safe";
        }
        else if (randomNumber > 20 && randomNumber <= 55)
        {
            return "Danger";
        }
        else
        {
            return "Water";
        }
    }

    //GenerateStrips()
    //generates a (stripsGenPerLevel + 2) number of strips which are made children GameObjects of nextLevel.
    public void GenerateStrips()
    {
        GameObject currentStrip = Instantiate(levelStrips[0], nextLevel.transform, false);
        currentStrip.transform.localPosition = new Vector3(-39, 0, 10);
        string previousStripCategory = "Safe";
        for (int i = 0; i < stripsGenPerLevel; i++)
        {
            string stripCategory = PickStripCategory();
            if (stripCategory == "Safe")
            {
                currentStrip = Instantiate(levelStrips[1], nextLevel.transform, false);
                currentStrip.transform.localPosition = new Vector3(-29 + 10 * i, 0, 10);
            }
            else if (stripCategory == "Danger")
            {
                currentStrip = Instantiate(levelStrips[Random.Range(6, 15)], nextLevel.transform, false);
                currentStrip.transform.localPosition = new Vector3(-29 + 10 * i, 0, 10);
            }
            else if (stripCategory == "Water")
            {
                if (previousStripCategory != "Water")
                {
                    currentStrip = Instantiate(levelStrips[Random.Range(2, 5)], nextLevel.transform, false);
                    currentStrip.transform.localPosition = new Vector3(-29 + 10 * i, 0, 10);
                }
                else
                {
                    currentStrip = Instantiate(levelStrips[Random.Range(6, 15)], nextLevel.transform, false);
                    currentStrip.transform.localPosition = new Vector3(-29 + 10 * i, 0, 10);
                }
            }
            else
            {
                Debug.Log("No strip was chosen error.");
            }
            previousStripCategory = stripCategory;

        }
        currentStrip = Instantiate(levelStrips[1], nextLevel.transform, false);
        currentStrip.transform.localPosition = new Vector3(-29 + 10 * stripsGenPerLevel, 0, 10);
    }

    //UnselectLevel()
    //is used to deactivate and move a level which is found at index oldLevel of levelArray to an off screen location.
    public IEnumerator UnselectLevel(int oldLevel)
    {
        yield return new WaitForSeconds(2.0f);
        levelArray[oldLevel].transform.position = new Vector3(levelArray[oldLevel].transform.position.x, 0, 100);
        levelArray[oldLevel].SetActive(false);
    }

    //SelectLevel()
    //is used to activate and move a level which is found at index newLevel of levelArra to the next level position.
    //placement is an int representing for which level's position this selected level should appear.
    public void SelectLevel(int newLevel, int placement)
    {
        levelArray[newLevel].transform.position = new Vector3((placement - 1) * 100, 0, 0);
        levelArray[newLevel].transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
        levelArray[newLevel].SetActive(true);
    }

    //GenerateLevel()
    //will create a GameObject called Level+x where x is levelsGenerated + 1.
    //It will also move this new level to its appropriate position off screen.
    //It will generate the level using prefabs and make these prefabs children
    //objects to the Level+x GameObject.
    //It will finally store the Level+x GameObject in the levelArray at index x.
    public void GenerateLevel()
    {
        nextLevel = new GameObject("Level" + (levelsGenerated + 1).ToString());
        nextLevel.transform.position = new Vector3(100 * levelsGenerated, 0, -100);
        GenerateStrips();
        nextLevel.SetActive(false);
        levelArray[levelsGenerated + 1] = nextLevel;
        this.levelsGenerated += 1;
    }

    //GenerateLevels()
    //will generate numOfLevels and will store them in the levelArray.
    public void GenerateLevels()
    {
        for (int i = 0; i < numOfLevels - 1; i++)
        {
            GenerateLevel();
        }
    }

    //SelectLevelNumber()
    //will choose a randomly selected level to place as the next level. It ensures that the level selected
    //is not currently in play.
    public int SelectLevelNumber()
    {
        int selectLevelNumber = Random.Range(2, numOfLevels);
        while (selectLevelNumber == inPlayLevels[0] || selectLevelNumber == inPlayLevels[1])
        {
            selectLevelNumber = Random.Range(2, numOfLevels);
        }
        if (inPlayLevels[0] != 0)
        {
            unselectCoroutine = UnselectLevel(inPlayLevels[0]);
            StartCoroutine(unselectCoroutine);
        }
        inPlayLevels[0] = inPlayLevels[1];
        inPlayLevels[1] = selectLevelNumber;
        return selectLevelNumber;
    }


    //SetupGame()
    //will generate all of the levels used for the rest of the game.
    //will store these levels in LevelArray to be accessed for the rest of the game.
    //will deactivate these levels as to not affect gameplay.
    //will randomly select the second level, activate it, and place it in its appropriate play position.
    public void SetupGame()
    {
        levelArray[1] = GameObject.Find("Level1");
        GenerateLevels();
        SelectLevel(SelectLevelNumber(), levelNumber + 1);
    }
}
