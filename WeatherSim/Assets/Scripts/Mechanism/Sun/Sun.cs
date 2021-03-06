﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun : MonoBehaviour {

    [SerializeField] Text buttonText;

   [SerializeField] Sprite[] sun_moon;
    SpriteRenderer sun_sprite;
    float f_angle = 53;
    // There are 5 levels
    [SerializeField] float[] fa_rotSpdLevel = {0.1f ,0.5f,1f,2f,3f,10f };
   [SerializeField] float f_rotSpeed;
    public bool b_IsPaused;
    public bool b_AtNight;
    int i_currentSpd = 3;
    [SerializeField] float f_baseSpeed = 1;
    [SerializeField] DayNightEffect effect;
    // Use this for initialization
    void Start () {
        sun_sprite = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        f_rotSpeed = fa_rotSpdLevel[i_currentSpd];
        UpdateButtonText();

    }
	
	// Update is called once per frame
	void Update () {
        if (!b_IsPaused)
        {
            
            transform.Rotate(new Vector3(0, 0, 1), f_baseSpeed * f_rotSpeed * Time.deltaTime);
            float deltaAngle = transform.eulerAngles.z;
            if(deltaAngle > 180)
            {
                deltaAngle = 360 -deltaAngle;
            }
            float ratio = 1- deltaAngle / f_angle;
            
            if (!b_AtNight)
            {
                VaporManager.instance.ModifyEnergy(ratio);
                effect.SunPosition = ratio * 3 > 1 ? 1 : ratio * 3;
            }
            else
            {
                VaporManager.instance.f_energy = 10;
                effect.SunPosition = 0;
            }
            // from left to right
            if (transform.rotation.eulerAngles.z >= f_angle && transform.rotation.eulerAngles.z < 360- f_angle)
            {
               // Debug.Log(deltaAngle);
                transform.rotation = Quaternion.Euler(0, 0, -f_angle);
                b_AtNight = !b_AtNight;

                sun_sprite.sprite = sun_moon[b_AtNight ? 1 : 0];
                sun_sprite.transform.parent.GetChild(b_AtNight ? 1 : 2).gameObject.SetActive(false);
                sun_sprite.transform.parent.GetChild(b_AtNight ? 2 : 1).gameObject.SetActive(true);

            }
        }
    }

    public void SpeedUp()
    {
        if (!b_IsPaused)
        {
            i_currentSpd++;
            if (i_currentSpd >= fa_rotSpdLevel.Length)
                i_currentSpd = fa_rotSpdLevel.Length - 1;
            f_rotSpeed = fa_rotSpdLevel[i_currentSpd];

            UpdateButtonText();
        }
    }

    public void StartAndPause()
    {
        if (!b_IsPaused)
            buttonText.text = "||";
        else
            UpdateButtonText();
        b_IsPaused = !b_IsPaused;


    }
    public void SpeedDown()
    {
        if (!b_IsPaused)
        {
            i_currentSpd--;
            if (i_currentSpd < 0)
                i_currentSpd = 0;
            f_rotSpeed = fa_rotSpdLevel[i_currentSpd];
            UpdateButtonText();
        }

    }

    void UpdateButtonText()
    {

        buttonText.text = "x" + fa_rotSpdLevel[i_currentSpd];
    }
}
