using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporManager : MonoBehaviour {
    public static VaporManager instance;
    bool b_sunHigh;
    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    int[] ia_enegryClamp = { 10, 100 };
    float[] fa_EmmisionRate = { 2f, 20f };
    float[] fa_sunRayLifeTime = { 1f, 2f };
    [Range(10,100)]
    public float f_energy = 10;
    [SerializeField] ParticleSystem sunRay;

    ParticleSystem.MainModule sun_main;
    List<VaporArea> lists = new List<VaporArea>();
	// Use this for initialization
	void Start () {
        sun_main = sunRay.main;
        for (int i = 0; i < transform.childCount; i++)
        {
            lists.Add(transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<VaporArea>());
        }
        Debug.Log(lists.Count);

    }
	
	// Update is called once per frame
	void Update () {
        float ratio = (f_energy - ia_enegryClamp[0]) / (ia_enegryClamp[1] - ia_enegryClamp[0]);

        if (!b_sunHigh && ratio > 0.9f)
        {
            b_sunHigh = true;
            PopUpManager.Instance.SetContent(0);
        }
        // vapor area
        foreach (var item in lists)
        {
            item.SetEnegry(ratio*(fa_EmmisionRate[1] - fa_EmmisionRate[0]) + fa_EmmisionRate[0]);
        }
        // sun ray
        sun_main.startLifetime = ratio * (fa_sunRayLifeTime[1] - fa_sunRayLifeTime[0]) + fa_sunRayLifeTime[0];

    }

    public void ModifyEnergy(float _ratio)
    {

        f_energy = (ia_enegryClamp[1] - ia_enegryClamp[0]) * _ratio + ia_enegryClamp[0];
    }
}
