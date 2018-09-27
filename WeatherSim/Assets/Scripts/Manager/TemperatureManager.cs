using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureManager : MonoBehaviour
{
    public static TemperatureManager TManager = null;
    public float min = 1.5f;
    public float max = 4;
    public float tempmin = -10f;
    public float tempmax = 50f;
    private void Awake()
    {
        if (TManager == null)
        {
            TManager = new TemperatureManager();
        }
    }
    // Use this for initialization
    public float ClampToC(float value)
    {
        return (value - min) / (max - min) * (tempmax - tempmin) + tempmin; ;
    }
    public float CToF(float value)
    {
        return 9.0f / 5.0f * value + 32f;
    }
    public float CToF(int value)
    {
        return 9.0f / 5.0f * value + 32f;
    }
}
