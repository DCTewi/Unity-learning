using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;
    public Text tutorialText;
    public GameObject[] asteriods;
    public Vector3 spawnValues;
    public float napSpawn = 0.5f;
    public float napWave = 1.5f;
    [Range (3, 20)]
    public int maxCountAsteriods = 10;

    private Vector3 spawnPosition = Vector3.zero;
    private Quaternion spawnRotation;
    private bool inWave = false;
    private int score = 0;

    private IEnumerator SpawnWaves()
    {
        inWave = true;
        yield return new WaitForSeconds(napWave);

        int countWave = Random.Range(3, maxCountAsteriods);
        for (int i = 0; i < countWave; i++)
        {
            spawnPosition.x = Random.Range(-spawnValues.x, spawnValues.x);
            spawnPosition.z = spawnValues.z;
            spawnRotation = Quaternion.identity;
            int objID = Random.Range(0, asteriods.Length);
            Instantiate(asteriods[objID], spawnPosition, spawnRotation);

            yield return new WaitForSeconds(napSpawn);
        }

        inWave = false;
    }

    private IEnumerator Tutorial()
    {
        tutorialText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.75f);
        tutorialText.gameObject.SetActive(false);
    }

    private void UpdateScore()
    {
        scoreText.text = "得分:" + score.ToString();
    }

    public void AddScore(int delta)
    {
        score += delta;
        UpdateScore();
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
    }

    private void Start()
    {
        StartCoroutine(Tutorial());
        score = 0;
        UpdateScore();
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!inWave)
        {
            StartCoroutine(SpawnWaves());
        }

        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 0.0f)
        {
            Restart();
        }
    }
}
