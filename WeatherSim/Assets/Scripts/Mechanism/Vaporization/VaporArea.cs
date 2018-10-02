using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporArea : MonoBehaviour {

    ParticleSystem particleVFX;
    ParticleSystem.MainModule _main;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    // Use this for initialization
    void Start() {
        particleVFX = GetComponent<ParticleSystem>();
        _main = particleVFX.main;

    }

    // Update is called once per frame
    void Update() {

    }
    public void SetEnegry(float _partLife_percent)
    {
        _main.startLifetime = _partLife_percent;
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
                if (receiver.OnParticleCollided != null)
                    receiver.OnParticleCollided.Invoke();
            }
            i++;
        }
    }

}
