using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    public float fadeSpeed = 2f;        //亮度渐变速度
    public float highIntensity = 4f;    //亮度最大值
    public float lowIntensity = 0.5f;   //亮度最小值
    public float changeMargin = 0.2f;   //亮度阈值
    public bool alarmOn;   

    private float targetIntensity;      //目标亮度值
    private Light alarmLight;

    public void CheckTargetIntensity()
    {
        if (Mathf.Abs(highIntensity - alarmLight.intensity) < changeMargin)
        {
            targetIntensity = lowIntensity;
        }
        else if (Mathf.Abs(lowIntensity - alarmLight.intensity) < changeMargin)
        {
            targetIntensity = highIntensity;
        }
    }

    private void Awake()
    {
        alarmLight = GetComponent<Light>();
        alarmLight.intensity = 0f;
        targetIntensity = highIntensity;
    }

    private void Update()
    {
        if (alarmOn)
        {
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }
}
