using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : Interactable {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (b_Touched)
        {
            
            Vector3 pos = Camera.main.ScreenToWorldPoint(v_onScreenPos[0]);
            pos.z = 0;
            transform.position = pos;
        }
        
    }
}
