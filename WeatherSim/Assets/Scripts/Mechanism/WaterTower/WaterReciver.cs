using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReciver : MonoBehaviour {
    public Material FillShader;
    private float CurrentFill;
    public float fillrate;
    private bool accomplished = false;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (CurrentFill >= 1 && !accomplished) {
            PopUpManager.Instance.SetContent(3);
            accomplished = true;
        }
	}
    private void OnParticleCollision(GameObject other)
    {
        if (CurrentFill < 1) {
            CurrentFill += fillrate * Time.deltaTime;
            FillShader.SetFloat("_Fill", CurrentFill);
        }
    }
}
