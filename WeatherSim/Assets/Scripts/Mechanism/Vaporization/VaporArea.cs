using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporArea : MonoBehaviour {

    ParticleSystem particleVFX;
    ParticleSystem.EmissionModule _main;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    // Use this for initialization
    void Start() {
        particleVFX = GetComponent<ParticleSystem>();
        _main = particleVFX.emission;

    }

    // Update is called once per frame
    void Update() {

    }
    public void SetEnegry(float _partLife_percent)
    {
        _main.rateOverTime = _partLife_percent;
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleVFX.GetCollisionEvents(other, collisionEvents);
        
        ParticleReceiver receiver = other.GetComponent<ParticleReceiver>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (receiver)
            {
                if (receiver.OnParticleCollided != null 
                    && receiver.name.Substring(receiver.name.Length-1).Equals(transform.parent.parent.name.Substring(transform.parent.parent.name.Length-1)))
                    receiver.OnParticleCollided.Invoke();
            }
            i++;
        }
    }

}
