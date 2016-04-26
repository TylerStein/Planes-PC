using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour {

    public Slider healthBar;
    public Slider strainBar;
    public Slider stamBar;

    public GameObject inventoryScreen;
    public GameObject UIScreen;
    bool inventoryOn;
    bool UIScreenOn;

    // Use this for initialization
    void Start () {
        healthBar = GameObject.FindWithTag("Healthbar").GetComponent<Slider>();
        stamBar = GameObject.FindWithTag("StaminaBar").GetComponent<Slider>();
        strainBar = GameObject.FindWithTag("MSBar").GetComponent<Slider>();

        inventoryScreen.SetActive(false);
        inventoryOn = false;
        UIScreen.SetActive(true);
        UIScreenOn = true;
    }
	
	// Update is called once per frame
	void Update () {
        //call methods
        updateHealth();
        updateStamina();
        updateMS();
        inventoryCheck();
        itemCheck();
        skillCheck();
    }

    void updateHealth()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            healthBar.value += 10;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            healthBar.value -= 10;
        }
    }

    void updateStamina()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            stamBar.value += 10;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            stamBar.value -= 10;
        }
    }

    void updateMS()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            strainBar.value += 10;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            strainBar.value -= 10;
        }
    }

    void inventoryCheck()
    {
        if (Input.GetAxis("Pause") > 0.5f)
        {
            if (inventoryOn)
            {
                inventoryScreen.SetActive(false);
                inventoryOn = false;
                UIScreen.SetActive(true);
                UIScreenOn = true;
            }
            else
            {
                inventoryScreen.SetActive(true);
                inventoryOn = true;
                UIScreen.SetActive(false);
                UIScreenOn = false;
            }
        }
    }

    void itemCheck()
    {
        //TODO::here we will set the images in the boxes based on the item that is there.
    }

    void skillCheck()
    {
        //TODO::here we will set the images of the boxes based on the skills that will be in that position.
    }
}
