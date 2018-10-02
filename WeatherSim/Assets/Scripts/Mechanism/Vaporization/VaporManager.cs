﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporManager : MonoBehaviour {
    public static VaporManager instance;
    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    int[] ia_enegryClamp = { 10, 100 };
    float[] fa_particleLifeTime = { 1f, 5f };
    float[] fa_sunRayLifeTime = { 1f, 2f };
    [Range(10,100)]
    public float f_energy = 10;
    [SerializeField] ParticleSystem sunRay;
    [SerializeField] Draggable theSun;
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
        theSun.OnScaleUp += ModifyEnergy;
        theSun.OnScaleDown += ModifyEnergy;
    }
	
	// Update is called once per frame
	void Update () {
        float ratio = (f_energy - ia_enegryClamp[0]) / (ia_enegryClamp[1] - ia_enegryClamp[0]);

        // vapor area
        foreach (var item in lists)
        {
            item.SetEnegry(ratio*(fa_particleLifeTime[1] - fa_particleLifeTime[0]) + fa_particleLifeTime[0]);
        }
        // sun ray
        sun_main.startLifetime = ratio * (fa_sunRayLifeTime[1] - fa_sunRayLifeTime[0]) + fa_sunRayLifeTime[0];

    }

    public void ModifyEnergy(float _speed)
    {

        f_energy += _speed * Time.deltaTime;
        if (f_energy > 100)
            f_energy = 100;
        if (f_energy < 10)
            f_energy = 10;
    }
}
