using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleReceiver : MonoBehaviour {
    public UnityEvent OnParticleCollided;
    // Use this for initialization

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // arrive die area
        if (collision.transform.name.Equals("CloudDieArea"))
        {
            transform.GetChild(0).GetComponent<Cloud>().CloudDie();
        }
    }
}
