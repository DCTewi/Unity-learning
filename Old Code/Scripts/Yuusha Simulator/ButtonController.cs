using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private AudioSource audioClick;

    public void PlayClickAudio()
    {
        Debug.Log("Clicked " + gameObject.name);
        audioClick.Play();
    }

    private void Awake()
    {
        audioClick = GetComponent<AudioSource>();
    }
}
