using UnityEngine;
using System.Collections;

public class Wisdom : Attributes {

    public float castTimeModifier = 0;
    public float mentalStrainConsumptionModifier = 0;
    public float magicSkillCooldownModifier = 0;

    Controls player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increaseCritAffectChance()
    {
        player.critAffectChance += 0.025f;
    }

    public void increaseCastTimeModifier()
    {
        castTimeModifier += 0.03f;
    }

    public void increasementailStrainConsumptionModifier()
    {
        mentalStrainConsumptionModifier += 0.02f;
    }

    public void increaseMentalStrainRestoreRate()
    {
        player.mentalStrainRestoreRate += 0.25f;
    }

    public void increaseMagicSkillCooldownModifier()
    {
        magicSkillCooldownModifier += 0.025f;
    }

    public void levelUp()
    {
        if (currentLevel != maxLevel)
        {
            increaseCritAffectChance();
            increaseCastTimeModifier();
            increasementailStrainConsumptionModifier();
            increaseMentalStrainRestoreRate();
            increaseMagicSkillCooldownModifier();

            currentLevel++;
        }
    }
}
