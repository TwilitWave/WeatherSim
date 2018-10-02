using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporManager : MonoBehaviour {
    int[] ia_enegryClamp = { 10, 100 };
    float[] fa_particleLifeTime = { 1f, 5f };
    [Range(10,100)]
    public float f_energy;
    List<VaporArea> lists = new List<VaporArea>();
	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            lists.Add(transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<VaporArea>());
        }
        Debug.Log(lists.Count);
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var item in lists)
        {

            float ratio = (f_energy - ia_enegryClamp[0]) / (ia_enegryClamp[1] - ia_enegryClamp[0]);

            item.SetEnegry(ratio*(fa_particleLifeTime[1] - fa_particleLifeTime[0]) + fa_particleLifeTime[0]);
        }
	}
}
