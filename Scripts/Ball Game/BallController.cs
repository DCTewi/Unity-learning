/*
 * This .cs file is used in Ball-Game.
 * A game that moves the plate to prevent the ball from falling out of the screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [Range (0.0f, 100.0f)]
    public float thrust = 40.0f; //The thrust when ball hit the plate
    public Text gameOverText;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("AnyKeyDown");
        }

        if (Input.anyKeyDown && Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            Debug.Log("Reload");
            SceneManager.LoadScene("Main");
        }

        if (transform.position.y <= -10.0f)
        {
            rb.useGravity = false;
            gameOverText.gameObject.SetActive(true);

            Time.timeScale = 0.0f;
            Debug.Log("The World!!!Toki yo tomare!!!!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(new Vector3(Random.Range(-0.3f, 0.3f), 1.0f, 0) * thrust);
        Debug.Log("Duang");
    }
}
