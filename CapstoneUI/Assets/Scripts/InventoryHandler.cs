using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryHandler : MonoBehaviour {
    public GameObject slots;
    int x = -850;
    int y = 385;

    int items = 40;

    // Use this for initialization
    void Start()
    {
        for (int i = 1; i < items; i++)
        {
            GameObject slot = (GameObject)Instantiate(slots);
            //slot.transform.parent = this.gameObject.transform;
            
            slot.transform.SetParent(this.gameObject.transform, false);
            //slot.transform.localPosition = new Vector3(x, y, 0);
            
            slot.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
            
            x += 240;

            if ((i%8) == 0)
            {
                x = -850;
                y -= 240;
            }

        }

    }

	// Update is called once per frame
	void Update () {
	
	}
}
