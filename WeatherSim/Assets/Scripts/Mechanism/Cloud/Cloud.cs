using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    // mass could be the size of the could
    public float f_Mass;
    public float f_AirDrag = 1f;
    public float f_MaxSpeed;
    [SerializeField] float f_vertSpd;
    [SerializeField] SpriteRenderer sr_FillCloud;
    [Header("Cloud Grow")]
    public bool b_IsCloudFull;
    public bool b_CanRain;
    // the water volume required to full the cloud
    public float f_CloudFullVolume;
    // the water volume allowed the cloud to rain
    public float f_RainFullVolume;
    // current water volume for the cloud
    [SerializeField] private float f_WaterVolume;

    void Start()
    {

        sr_FillCloud.material.SetFloat("_Fill", 0f);
        if (!b_IsCloudFull)
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int dir = WindManager.instance.direction;
        if (WindManager.instance.b_blowing)
        {
            f_vertSpd += dir * (WindManager.instance.Scale) * Time.deltaTime;
        }

        if (f_vertSpd > 0.01f || f_vertSpd < -0.01f)
        {
            f_vertSpd += -dir * f_AirDrag * Time.deltaTime;

            if (f_vertSpd > f_MaxSpeed)
                f_vertSpd = f_MaxSpeed;
            if (f_vertSpd < -f_MaxSpeed)
                f_vertSpd = -f_MaxSpeed;
            if (dir > 0 && f_vertSpd < 0)
                f_vertSpd = 0;
            if (dir < 0 && f_vertSpd > 0)
                f_vertSpd = 0;

            transform.position += new Vector3(f_vertSpd * Time.deltaTime, 0, 0);
        }
        else
        {
            f_vertSpd = 0;
        }

        if (!b_IsCloudFull || !b_CanRain)
            GrowUp(1);

    }
    public void InstantiateCloud()
    {

    }

    public void GrowUp(float f_speed)
    {
        // increase size of cloud
        if (!b_IsCloudFull && f_WaterVolume < f_CloudFullVolume)
        {
            f_WaterVolume += f_speed;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), f_speed * Time.deltaTime);
        }
        else
        {
            b_IsCloudFull = true;
        }
        if (b_IsCloudFull && !b_CanRain)
        {

            if (f_WaterVolume < f_RainFullVolume)
            {
                f_WaterVolume += f_speed;
                float ratio = 1 - (f_RainFullVolume - f_WaterVolume) / (f_RainFullVolume - f_CloudFullVolume);
                sr_FillCloud.material.SetFloat("_Fill", ratio);
            }
            else
            {
                b_CanRain = true;
            }
        }
    }
    public void Rain()
    {

    }
}
