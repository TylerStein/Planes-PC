using UnityEngine;
using System.Collections;

public class Agility : Attributes {

    public float critDamageModifier = 0.5f;
    public float attackRecoveryTime = 0;
    public float physicalSkillCooldownModifier = 0;
    public float knockdownRecoveryTimeModifier = 0;
    public float balancePenalty = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increaseCritDamageModifier()
    {
        critDamageModifier += 0.025f;
    }

    public void increaseAttackRecoveryTime()
    {
        attackRecoveryTime += 0.05f;
    }

    public void increasePhysicalSkillCooldownModifier()
    {
        physicalSkillCooldownModifier += 0.025f;
    }

    public void decreaseKnockdownRecovery()
    {
        knockdownRecoveryTimeModifier += 0.2f;
    }

    public void increaseBalancePenalty()
    {
        balancePenalty += 0.02f;
    }

    public void levelUp()
    {
        if (currentLevel != maxLevel)
        {
            increaseCritDamageModifier();
            increaseAttackRecoveryTime();
            increasePhysicalSkillCooldownModifier();
            decreaseKnockdownRecovery();
            increaseBalancePenalty();

            currentLevel++;
        }
    }
}
