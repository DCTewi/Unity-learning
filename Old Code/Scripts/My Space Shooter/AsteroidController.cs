using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public float speed = -5.0f;
    public int scoreValue = 1;

    private Rigidbody rb;
    private GameController gameController;
    private float tumble;

    private void Start()
    {
        tumble = Random.Range(5.0f, 15.0f);

        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = new Vector3(0.0f, 0.0f, speed);

        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if (gcObj != null)
        {
            gameController = gcObj.GetComponent<GameController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Boundary") && !other.CompareTag("Asteroid"))
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            if (other.CompareTag("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.StartCoroutine("GameOver");
            }

            if (other.CompareTag("Bolt"))
            {
                gameController.AddScore(scoreValue);
            }

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
