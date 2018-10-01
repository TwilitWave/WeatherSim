using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporArea : MonoBehaviour {

    float f_energy;
    ParticleSystem particleVFX;
	// Use this for initialization
	void Start () {
        particleVFX = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleVFX.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
