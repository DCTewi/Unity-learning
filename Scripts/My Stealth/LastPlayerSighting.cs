using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour
{
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    public Vector3 resetPos = new Vector3(1000f, 1000f, 1000f);
    public float lightHighIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;
    public float fadeSpeedMusic = 1f;

    private AlarmLight alarmScript;
    private Light mainLight;
    private AudioSource music;
    private AudioSource panicMusic;
    private AudioSource[] sirens;
    private const float muteVolume = 0f;
    private const float normalVolume = 0.8f;

    private void MusicFading()
    {
        if (position != resetPos)
        {
            music.volume = Mathf.Lerp(music.volume, muteVolume, fadeSpeedMusic * Time.deltaTime);
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, normalVolume, fadeSpeedMusic * Time.deltaTime);
        }
        else
        {
            music.volume = Mathf.Lerp(music.volume, normalVolume, fadeSpeedMusic * Time.deltaTime);
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, muteVolume, fadeSpeedMusic * Time.deltaTime);
        }
    }

    private void SwitchAlarms()
    {
        alarmScript.alarmOn = (position != resetPos);
        float newIntensity;
        if (position != resetPos)
        {
            newIntensity = lightLowIntensity;
        }
        else
        {
            newIntensity = lightHighIntensity;
        }
        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

        for (int i = 0; i < sirens.Length; i++)
        {
            if (position != resetPos && !sirens[i].isPlaying)
            {
                sirens[i].Play();
            }
            else if (position == resetPos)
            {
                sirens[i].Stop();
            }
        }
    }

    private void Awake()
    {
        alarmScript = GameObject.FindGameObjectWithTag(Tags.AlarmLight).GetComponent<AlarmLight>();
        mainLight = GameObject.FindGameObjectWithTag(Tags.MainLight).GetComponent<Light>();

        music = this.GetComponent<AudioSource>();
        panicMusic = this.transform.Find("secondary_music").GetComponent<AudioSource>();
        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.Siren);
        sirens = new AudioSource[sirenGameObjects.Length];

        for (int i = 0; i < sirenGameObjects.Length; i++)
        {
            sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        SwitchAlarms();
        MusicFading();
    }
}
