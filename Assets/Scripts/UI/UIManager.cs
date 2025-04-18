using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] private Image playerHealthBar;
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [Header("Score //Energy")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [Header("Coin")]
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        PlayerHealth.HealthAmount += Health;
        PlayerHealth.PlayerDeath += PlayerDeath;
    }
    private void OnDisable()
    {
        PlayerHealth.HealthAmount -= Health;
        PlayerHealth.PlayerDeath -= PlayerDeath;
    }
    private void ScoreControl()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        coinText.text = GameManager.Instance.Coin.ToString();
    }
    private void Health(int amount)
    {
        float newValue = (float)amount / 100;
        if(newValue == 0.1f)
            newValue = 1.1f - newValue;
        else
            newValue = 1f - newValue;
        playerHealthBar.DOFillAmount(newValue, 3f);
    }
    private void PlayerDeath()
    {
        gameOverScreen.SetActive(true);
    }
    private void Update()
    {
        ScoreControl();
    }
}
