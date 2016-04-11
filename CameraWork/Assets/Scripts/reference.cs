using UnityEngine;
using System.Collections;

public class reference : MonoBehaviour {

    private GameObject playerReference;

	// Use this for initialization
	void Start () {
        playerReference = GameObject.FindWithTag("ReferenceTarget");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerReference.transform.position;
	}
}
