using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;

    private bool sceneStarting = true;
    private RawImage rawImage = null;

    public void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    public void ClearToFade()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    public void StartScene()
    {
        FadeToClear();
        if (rawImage.color.a <= 0.05f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }

    public void SwitchScene(string sceneName)
    {
        rawImage.enabled = true;
        ClearToFade();
        if (rawImage.color.a >= 0.95f)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }
    }
}
