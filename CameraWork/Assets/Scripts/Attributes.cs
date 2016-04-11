using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

    public int currentLevel;
    public const int maxLevel = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    int getStatLevel()
    {
        return currentLevel;
    }

}
