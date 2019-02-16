using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private PlayerController player;
    private Slider bar;

    private void Awake()
    {
        bar = GetComponent<Slider>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (bar.value != player.healthPoint)
        {
            Debug.Log("bar set to " + player.healthPoint.ToString());
            bar.value = player.healthPoint;
        }
    }
}
