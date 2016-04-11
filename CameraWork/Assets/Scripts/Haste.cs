using UnityEngine;
using System.Collections;

public class Haste : Attributes {

    public float preAttackTimeModifier = 0;
    public float stumbleRecoveryTimeModifier = 0;
    public float itemPrepTimeModifier = 0;
    public float itemUseTimeModifier = 0;

    Controls player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increasePreAttackTimeModifier()
    {
        preAttackTimeModifier += 0.05f;
    }

    public void increaseStumbleRecoveryTimeModifier()
    {
        stumbleRecoveryTimeModifier += 0.05f;
    }

    public void increaseMaxStamina()
    {
        player.maxStamina += 5;
    }

    public void increaseMoveSpeed()
    {
        player.speed += 7 * 0.02f;
    }

    public void increaseItemPrepModifier()
    {
        itemPrepTimeModifier += 0.02f;
    }

    public void increaseItemUseModifier()
    {
        itemUseTimeModifier += 0.02f;
    }

    public void levelUp()
    {
        if (currentLevel != maxLevel)
        {
            increasePreAttackTimeModifier();
            increaseStumbleRecoveryTimeModifier();
            increaseMaxStamina();
            increaseMoveSpeed();
            increaseItemPrepModifier();
            increaseItemUseModifier();

            currentLevel++;
        }
    }

}
