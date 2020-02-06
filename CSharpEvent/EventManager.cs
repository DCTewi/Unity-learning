using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public class MouseDownEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MouseDownEventArgs(string message)
        {
            Message = message;
        }
    }
    public delegate void MouseDownEventHandler(object sender, MouseDownEventArgs e);

    static public event MouseDownEventHandler OnMouseDownEvent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDownEvent.Invoke(this, new MouseDownEventArgs("Hello!"));
        }
    }
}
