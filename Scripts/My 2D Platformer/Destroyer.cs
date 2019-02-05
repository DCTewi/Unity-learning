using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public bool DestroyOnAwake;
    public float DestoryDelayTime;
    public bool DestroyChild = false;
    public string ChildName;

    private void Awake()
    {
        if (DestroyOnAwake)
        {
            if (DestroyChild)
            {
                Destroy(transform.Find(ChildName).gameObject);
            }
            else
            {
                Destroy(this.gameObject, DestoryDelayTime);
            }
        }
    }
}
