using UnityEngine;
using System.Collections;

public class Perception : Attributes {

    public float sight = 10;
    public int enemyInfoLevel = 10;
    public int notice = 10;

    Controls player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increaseSight()
    {
        sight++;
    }

    public void increaseBalance()
    {
        player.balance++;
    }

    public void increaseEnemyInfoLevel()
    {
        enemyInfoLevel++;
    }

    public void increaseGrabControl()
    {
        player.grabControl++;
    }

    public void levelUp()
    {
        if (currentLevel != maxLevel)
        {
            increaseSight();
            increaseBalance();
            increaseEnemyInfoLevel();
            increaseGrabControl();

            currentLevel++;
        }
    }

}
