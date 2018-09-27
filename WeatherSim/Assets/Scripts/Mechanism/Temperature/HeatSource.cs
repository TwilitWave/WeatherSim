using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSource : MonoBehaviour {
    public static HeatSource sun;
    public static float HeatLevel =1;
    // Use this for initialization
    private void Awake()
    {
        sun = this;
    }
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
