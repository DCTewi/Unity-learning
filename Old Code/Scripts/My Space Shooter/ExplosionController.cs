using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float lifeTime = 3.0f;

    private float startTime;

	void Start () 
	{
        startTime = Time.time;
	}

	void Update () 
	{
		if (Time.time - startTime > lifeTime)
        {
            Destroy(this.gameObject);
        }
	}
}
