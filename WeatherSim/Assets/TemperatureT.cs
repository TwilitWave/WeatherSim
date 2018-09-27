using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TemperatureT : MonoBehaviour {

    public static string Celsius = " °C ";
    public static string Fahrenheit = " F ";
    public Text text;
    public HeatRecieve Mysensor;
    public static float min = 1.5f;
    public static float max = 4;
    public static float tempmin = -10f;
    public static float tempmax = 50f;
    private int Celsiustemp = 35;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Celsiustemp = (int)(TemperatureManager.TManager.ClampToC(Mysensor.cunrrenttemp));
        text.text = "Temperature: " + Celsiustemp + Celsius + TemperatureManager.TManager.CToF(Celsiustemp) + Fahrenheit;
    }
    // this function clamps game temperature to Celsiustemp
}
