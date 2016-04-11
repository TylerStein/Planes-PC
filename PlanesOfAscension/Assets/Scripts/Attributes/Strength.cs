using UnityEngine;
using System.Collections;

public class Strength : Attributes {

    public float strengthModifier = 0.05f;
    const float modifier = 0.1f;
    public float physicalDamage = 10;
    public float staminaDepletionModifier = 0;
    public float throwingSpeed = 10;
    public float grabControl = 20;

    public float weightDebuffValue;

    Controls player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
        calculateWeightDebuff();
	}

    public void increaseStrengthModifier()
    {
        strengthModifier += 0.01f;
    }

    public void increaseDamage()
    {
        physicalDamage += 2;
    }

    public void increaseStaminaDepletionModifier()
    {
        staminaDepletionModifier += 0.02f;
    }

    public void increaseThrowingSpeed()
    {
        throwingSpeed += 0.04f;
    }

    public void IncreaseGrabControlLevel()
    {
        grabControl += 1;
    }

    public void calculateWeightDebuff()
    {
        float difference = player.maxCarryWeight - player.currentCarryWeight;
        weightDebuffValue = (player.maxCarryWeight - difference - 5) * (modifier - (difference * strengthModifier)) * (1 + strengthModifier * difference);
    }

    //calls all methods once for easy level up. each needs their own because they all have different amounts of methods
    public void levelUp()
    {
        if (currentLevel != maxLevel)
        {
            increaseStrengthModifier();
            increaseDamage();
            increaseStaminaDepletionModifier();
            increaseThrowingSpeed();
            IncreaseGrabControlLevel();

            currentLevel++;
        }
    }
}
