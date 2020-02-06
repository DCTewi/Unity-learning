using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject explosion;

    private void Start()
    {
        Destroy(this.gameObject, 2f);
    }

    private void OnExplode()
    {
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randomRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.Enemy)
        {
            collision.GetComponent<EnemyController>().Hurt();
            OnExplode();
            Destroy(this.gameObject);
        }
        else if (collision.tag == Tags.BombPickUp)
        {
            collision.GetComponent<BombController>().Explode();
            Destroy(collision.transform.root.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.tag != Tags.Player)
        {
            OnExplode();
            Destroy(this.gameObject);
        }
    }
}
