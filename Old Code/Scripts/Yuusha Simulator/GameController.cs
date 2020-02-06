using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Animator animFader;
    public GameObject gameoverCanvas;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Substring(0, 6) == "_Level")
        {
            gameoverCanvas = GameObject.FindGameObjectWithTag(Tags.GameOverCanvas);
            gameoverCanvas = gameoverCanvas.transform.Find(Tags.GameOverCanvas).gameObject;
        }
        gameoverCanvas.SetActive(false);
    }

    private IEnumerator CoroLoadScene(string scene)
    {
        Debug.Log("Loading " + scene);
        animFader.SetTrigger("Fade");
        yield return new WaitForSeconds(0.8f);

        GC.Collect();
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(CoroLoadScene(scene));
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void GameOver()
    {
        gameoverCanvas.SetActive(true);
        Pause();
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    private void Update()
    {
        if (Time.timeScale == 0.0f)
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1.0f;
            }
        }
    }
}
