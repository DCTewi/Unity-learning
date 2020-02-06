using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanSpawner : MonoBehaviour
{
    public Rigidbody2D prop;
    public float leftSpawnPosX;
    public float rightSpawnPosX;
    public float minSpawnPosY;
    public float maxSpawnPosY;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public float minSpeed;
    public float maxSpeed;

    private float nextTime;
    private bool inWave;

    private IEnumerator SpawnSwan()
    {
        inWave = true;

        bool facingLeft = Random.Range(0, 2) == 0;
        float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;
        float posY = Random.Range(minSpawnPosY, maxSpawnPosY);
        Vector3 spawnPos = new Vector3(posX, posY, this.transform.position.z);
        Rigidbody2D propInstance = Instantiate(prop, spawnPos, Quaternion.identity) as Rigidbody2D;
        Debug.Log("Swan Instaniated.");

        if (!facingLeft) //If Facing Left Then Mirror It.
        {
            Vector3 scale = propInstance.transform.localScale;
            scale.x *= -1;
            propInstance.transform.localScale = scale;
        }

        float speed = Random.Range(minSpeed, maxSpeed);
        speed *= facingLeft ? -1f : 1f;
        propInstance.velocity = new Vector2(speed, 0);

        while(propInstance != null)
        {
            if (facingLeft)
            {
                if (propInstance.transform.position.x < leftSpawnPosX - 0.5f)
                {
                    Destroy(propInstance.gameObject);
                    Debug.Log("Swan Destroyed.");
                }
                else if(propInstance.transform.position.x > rightSpawnPosX + 0.5f)
                {
                    Destroy(propInstance.gameObject);
                    Debug.Log("Swan Destroyed.");
                }
                yield return null;
            }
        }
        inWave = false;
    }

    private void Start()
    {
        Random.InitState(System.DateTime.Today.Millisecond);
        nextTime = 0.0f;
        inWave = false;
    }

    private void Update()
    {
        if (!inWave)
        {
            if (nextTime > 0)
            {
                nextTime -= Time.deltaTime;
            }
            else
            {
                nextTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
                StartCoroutine(SpawnSwan());
            }
        }
    }
}
