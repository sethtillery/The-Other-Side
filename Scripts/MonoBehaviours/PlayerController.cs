using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    public float negZRange = -13.0f;
    public float zRange = 33.0f;
    public float stamina = 0.0f;
    public float startInterval = 1.0f;
    public float stamDecAmount = 0.5f;
    public float speedDecAmount = 0.03f;
    public float negXRange = -42.0f;


    //Used to get a reference to the staminaUI prefab
    public staminaUI staminaUIPrefab;

    // a copy of the staminaUI prefab
    staminaUI staminaUI;

    public void Start()
    {
        // Get a copy of the staminaUI prefab and store a reference to it
        //staminaUI = Instantiate(staminaUIPrefab);
    }
     
    //calls decrease method at a fixed rate to stabilize depreciation across systems
    public void FixedUpdate()
    {
        Invoke("decrease", startInterval);
    }
    // Update is called once per frame
    void Update()
    {
        if(speed > 0){
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.left * verticalInput * Time.deltaTime * speed);
            transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
        }
        

        if (transform.position.z < negZRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, negZRange);
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.x < negXRange)
        {
            transform.position = new Vector3(negXRange, transform.position.y, transform.position.z);
        }
        if((Vector3.Distance(transform.position,(new Vector3(negXRange, transform.position.y, transform.position.z)))) > 95)
        {
            negXRange += 100;
        }
    }


    void decrease()
    {
        if(speed > 10)
        {
            speed = speed - speedDecAmount;
        }
        if (stamina > 0)
        {
            stamina -= stamDecAmount;
        }

    }

    void increaseSpeed(int newSpeed)
    {
        if (speed < 25)
        {
            speed += newSpeed;
        }

    }

    void increaseStamina(int newStamina)
    {
        if (stamina < 100)
        {
            stamina += newStamina;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Retrieve the game object that the player collided with, and check the tag
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            // Grab a reference to the Item (scriptable object) inside the Consumable class and assign it to hitObject
            // Note: at this point it is a coin, but later may be other types of CanBePickedUp objects
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            // Check for null to make sure it was successfully retrieved, and avoid potential errors
            if (hitObject != null)
            {
                // indicates if the collision object should disappear
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.Banana:
                        increaseSpeed(hitObject.speedToBeAdded);
                        increaseStamina(hitObject.staminaToBeAdded);
                        shouldDisappear = true;
                        break;

                    case Item.ItemType.Cookie:
                        increaseStamina(hitObject.staminaToBeAdded);
                        increaseSpeed(hitObject.speedToBeAdded);
                        shouldDisappear = true;
                        break;

                    case Item.ItemType.Steak:
                        increaseStamina(hitObject.staminaToBeAdded);
                        increaseSpeed(hitObject.speedToBeAdded);
                        shouldDisappear = true;
                        break;

                    case Item.ItemType.EnergyDrink:
                        increaseStamina(hitObject.staminaToBeAdded);
                        increaseSpeed(hitObject.speedToBeAdded);
                        shouldDisappear = true;
                        break;
                }
                // Hide the game object in the scene to give the illusion of picking up
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(GameObject.FindWithTag("Player"));
            SceneManager.LoadScene("Death Screen");
        }
    }



}
