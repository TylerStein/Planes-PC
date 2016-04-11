using UnityEngine;
using System.Collections;

public class HUDHandler : MonoBehaviour {

    public GameObject healthBar;
    public GameObject strainBar;
    public GameObject stamBar;

    public GameObject inventoryScreen;
    bool inventoryOn;


    public float m_HP = 100f;
    public float m_SP = 100f;
    public float m_MSP = 100f;

    public float c_HP = 0;
    public float c_SP = 0;
    public float c_MSP = 0;
    // Use this for initialization
    void Start () {
        c_HP = m_HP;
        c_SP = m_SP;
        c_MSP = m_MSP;

        inventoryScreen.SetActive(false);
        inventoryOn = false;
	}
	
	// Update is called once per frame
	void Update () {
        c_HP -= 0.1f;
        c_SP -= 0.1f;
        c_MSP -= 0.1f;

        decreaseHP(c_HP);
        decreaseSP(c_SP);
        decreaseMSP(c_MSP);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventoryOn)
            {
                inventoryScreen.SetActive(false);
                inventoryOn = false;
            }
            else
            {
                inventoryScreen.SetActive(true);
                inventoryOn = true;
            }
        }


    }

    float convertToPercentage(float val)
    {
        float percent = val / 100;

        return percent;
    }

    void decreaseHP(float hp)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(convertToPercentage(hp), 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    void decreaseSP(float sp)
    {
        stamBar.transform.localScale = new Vector3(Mathf.Clamp(convertToPercentage(sp), 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
    void decreaseMSP(float msp)
    {
        strainBar.transform.localScale = new Vector3(Mathf.Clamp(convertToPercentage(msp), 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

}
