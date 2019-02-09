using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffecter : MonoBehaviour
{
    public Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    public List<string> names;
    public List<AudioClip> audios;

    private AudioSource audioPlayer;

    public void PlaySE(string name)
    {
        if (sounds.ContainsKey(name))
        {
            if (audioPlayer.isPlaying)
            {
                audioPlayer.Pause();
            }
            audioPlayer.clip = sounds[name];
            audioPlayer.Play();
        }
        else
        {
            Debug.LogWarning("Can't Find SE named " + name);
        }
    }

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        
        int len = Mathf.Min(names.Count, audios.Count);
        for (int i = 0; i < len; i++)
        {
            if (names[i] != null)
            {
                Debug.Log("Added " + names[i] + " \"" + audios[i].name + "\" to sounds!");
                sounds.Add(names[i], audios[i]);
            }
        }
    }
}
