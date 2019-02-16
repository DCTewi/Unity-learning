using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPLampController : MonoBehaviour
{
    public Color healthColor;
    public Color normalColor;
    public Color dangourousColor;

    private RawImage lamp;
    private RectTransform trans;
    private PlayerController player;

    private bool shined = false;

    private IEnumerator shining()
    {
        shined = true;
        trans.localScale = new Vector3(1.2f, 1.2f, 1f);
        trans.position = new Vector3(31f, 31f);
        yield return new WaitForSeconds(0.3f);

        trans.localScale = new Vector3(1f, 1f, 1f);
        trans.position = new Vector3(35f, 35f);
        yield return new WaitForSeconds(0.3f);
        shined = false;
    }

    private void Awake()
    {
        lamp = GetComponent<RawImage>();
        trans = GetComponent<RectTransform>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.healthPoint >= 60 && lamp.color != healthColor)
        {
            lamp.color = healthColor;
        }
        if (player.healthPoint < 60 && player.healthPoint >= 40 && lamp.color != normalColor)
        {
            lamp.color = normalColor;
        }
        if (player.healthPoint <= 40)
        {
            if (lamp.color != dangourousColor)
            {
                lamp.color = dangourousColor;
            }
            if (!shined)
            {
                StartCoroutine(shining());
            }
        }
    }
}
