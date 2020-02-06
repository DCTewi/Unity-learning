using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Slider HPBar;
    public Canvas GameOverCanvas;
    public Canvas GameSeccessCanvas;

    private void OnEnable()
    {
        Player.OnPlayerHealthChanged += UpdateHPBar;
        Player.OnPlayerHealthChanged += GameOver;
        Enemy.OnEnemyDead += GameSuccess;
    }

    private void OnDisable()
    {
        Player.OnPlayerHealthChanged -= UpdateHPBar;
        Player.OnPlayerHealthChanged -= GameOver;
        Enemy.OnEnemyDead -= GameSuccess;
    }

    private void Awake()
    {
        GameOverCanvas.enabled = false;
    }

    private void UpdateHPBar(float percent)
    {
        HPBar.value = Mathf.Clamp01(percent);
    }

    private void GameOver(float percent)
    {
        if (Mathf.Approximately(percent, 0))
        {
            GameOverCanvas.enabled = true;
        }
    }

    private void GameSuccess(object sender, System.EventArgs e)
    {
        GameSeccessCanvas.enabled = true;
    }
}
