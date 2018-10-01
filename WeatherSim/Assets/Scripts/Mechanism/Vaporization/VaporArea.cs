using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporArea : MonoBehaviour {

    float f_energy;
    ParticleSystem particleVFX;
    List<ParticleCollisionEvent> collisionEvents= new List<ParticleCollisionEvent>();

    // Use this for initialization
    void Start () {
        particleVFX = GetComponent<ParticleSystem>();
        particleVFX.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleVFX.GetCollisionEvents(other, collisionEvents);
        Debug.Log(numCollisionEvents);
        ParticleReceiver receiver = other.GetComponent<ParticleReceiver>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (receiver)
            {
                if (receiver.OnParticleCollided != null)
                    receiver.OnParticleCollided.Invoke();
            }
            i++;
        }
    }

}
