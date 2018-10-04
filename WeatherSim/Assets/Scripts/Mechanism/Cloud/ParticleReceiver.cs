using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleReceiver : MonoBehaviour {
    public UnityEvent OnParticleCollided;
    // Use this for initialization
    static bool b_mountainRain;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // arrive die area
        if (collision.transform.name.Equals("CloudDieArea"))
        {
            transform.GetChild(0).GetComponent<Cloud>().CloudDie();
        }
        if (collision.transform.name.Equals("CloudRainArea"))
        {
            Cloud temp = transform.GetChild(0).GetComponent<Cloud>();
            if (temp.f_WaterVolume > temp.f_CloudFullVolume && !b_mountainRain)
            {
                b_mountainRain = true;
                PopUpManager.Instance.SetContent(2);
            }

                temp.CanRain();
        }
    }
}
