using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatRecieve : MonoBehaviour {
    public static float temperatureconst = 100;
    public static float speadconst = 0.1f;
    public float temperature =1;
    public float cunrrenttemp = 1;
    public float heatspeed = 1;
	// Use this for initialization
	void Start () {
        Getexpand();
        cunrrenttemp = temperatureconst * HeatSource.HeatLevel * 1 / DistanceSquare(this.gameObject.transform.position.x, this.gameObject.transform.position.y, HeatSource.sun.transform.position.x, HeatSource.sun.transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        float deltatemp = 0;
        temperature = temperatureconst*HeatSource.HeatLevel* 1/DistanceSquare(this.gameObject.transform.position.x, this.gameObject.transform.position.y, HeatSource.sun.transform.position.x, HeatSource.sun.transform.position.y);
        if (cunrrenttemp != temperature) {
            deltatemp = temperature * speadconst * ((temperature - cunrrenttemp)/Mathf.Abs(temperature-cunrrenttemp))*Time.deltaTime*heatspeed;
            cunrrenttemp += deltatemp;
        }
	}
    private void Getexpand() {
        this.heatspeed = 1/gameObject.transform.localScale.y;
    }
    public static float DistanceSquare(float x1, float y1, float x2, float y2) {
        return Mathf.Sqrt(Mathf.Pow((x2 - x1), 2)) + Mathf.Pow((y2 - y1), 2);
    }
}
