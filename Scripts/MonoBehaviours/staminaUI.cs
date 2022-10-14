using UnityEngine.UI;
using UnityEngine;

public class staminaUI : MonoBehaviour
{
    // Reference to the current Player object to get stamina field
    public PlayerController character;

    // For convenience, a direct reference to the health bar meter; set through the Unity Editor
    public Image meterImage;

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            meterImage.fillAmount = character.stamina / 100;
        }
    }
}
