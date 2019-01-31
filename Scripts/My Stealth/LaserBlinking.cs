using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour
{
    public float onTime = 1.5f;
    public float offTime = 1.55f;

    private float timer;
    private Renderer laserRenderer;
    private Light laserLight;

    private void Awake()
    {
        laserRenderer = this.GetComponent<Renderer>();
        laserLight = this.GetComponent<Light>();
        timer = 0f;
    }

    private void SwitchBeam()
    {
        timer = 0f;
        laserRenderer.enabled = !laserRenderer.enabled;
        laserLight.enabled = !laserLight.enabled;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (laserRenderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }
        if (!laserRenderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }
    }
}
