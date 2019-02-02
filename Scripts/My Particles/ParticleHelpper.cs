using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHelpper : MonoBehaviour
{
    public ParticleSystem.MainModule part;
    public bool varyAlpha;
    public float minAlpha;
    public float maxAlpha;
    public float alphaIncreaseRate;
    public float alphaDecreaseRate;
    public float alphaVariation;

    public void IncreaseAlpha()
    {
        if (part.startColor.color.a < maxAlpha)
        {
            Color adjColor = part.startColor.color;
            adjColor.a += alphaIncreaseRate * Time.deltaTime;
            part.startColor = adjColor;
        }
    }

    public void DecreaseAlpha()
    {
        if (part.startColor.color.a > minAlpha)
        {
            Color adjColor = part.startColor.color;
            adjColor.a -= alphaDecreaseRate * Time.deltaTime;
            part.startColor = adjColor;
        }
    }

    //etc.
	
}
