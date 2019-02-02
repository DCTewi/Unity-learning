using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFire : MonoBehaviour
{
    public List<ParticleHelpper> particles;

    private void Update()
    {
        foreach (ParticleHelpper ph in particles)
        {
            if (ph.varyAlpha)
            {
                ph.IncreaseAlpha();
            }
            //...
        }
    }
}
