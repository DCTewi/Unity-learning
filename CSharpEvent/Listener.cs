using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnMouseDownEvent += OnMouseDownEventInvoked;
    }

    private void OnDisable()
    {
        EventManager.OnMouseDownEvent -= OnMouseDownEventInvoked;
    }

    private void OnMouseDownEventInvoked(object sender, EventManager.MouseDownEventArgs e)
    {
        Debug.Log(string.Format("Mouse click! {0}, {1}", sender, e), gameObject);
    }
}
