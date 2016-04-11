using UnityEngine;
using System.Collections;

public class Intelligence : Attributes {

    public float magicDamage = 10;
    public float globalCooldownModifier = 0;

    Controls player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increaseMagicDamage()
    {
        magicDamage += 2;
    }

    public void increaseGlobalCooldownModifier()
    {
        globalCooldownModifier += 0.025f;
    }

    public void increaseCritChance()
    {
        player.critChance += 0.02f;
    }

    public void increaseSkillSlots()
    {
        if(currentLevel <=9 && currentLevel >=6)
        {
            player.skillSlots++;
        }
    }

    public void increaseMaxMentalStrain()
    {
        player.maxMS += 5;
    }

    public void levelUp()
    {
        if (currentLevel != maxLevel)
        {
            increaseMagicDamage();
            increaseGlobalCooldownModifier();
            increaseCritChance();
            increaseSkillSlots();
            increaseMaxMentalStrain();

            currentLevel++;
        }
    }
}
