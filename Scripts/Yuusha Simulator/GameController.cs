using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Animator animFader;

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

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }
}
