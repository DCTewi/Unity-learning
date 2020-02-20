using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private AsyncOperation loadingOperation;
    private Slider loadingSlider;

    public float LoadingProgress
    {
        get
        {
            if (loadingOperation == null) return 0f;
            return loadingOperation.progress;
        }
    }

    [ContextMenu("Reload")]
    public void Reload()
    {
        string current = SceneManager.GetActiveScene().name;

        LoadSceneAsync(current);
    }

    public void LoadSceneAsync(string scene)
    {
        StartCoroutine(AsyncLoading(scene));
    }

    private IEnumerator AsyncLoading(string scene)
    {
        yield return SceneManager.LoadSceneAsync("Loader");

        loadingSlider = GameObject.FindWithTag("LoadingSlider").GetComponent<Slider>();
        if (loadingSlider == null)
        {
            Debug.LogError("Can't find loading slider!!");
        }

        loadingSlider.value = 0f;

        loadingOperation = SceneManager.LoadSceneAsync(scene);
        loadingOperation.allowSceneActivation = true;

        yield return loadingOperation;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Should be divided to another script.
        if (loadingSlider != null)
        {
            loadingSlider.value = LoadingProgress;
        }
    }
}

