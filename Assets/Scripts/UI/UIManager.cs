using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Health")]
    [SerializeField] private Image playerHealthBar;
    private float newValue = 1;
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [Header("Score //Energy")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [Header("Coin")]
    [SerializeField] private TextMeshProUGUI coinText;
    [Header("Power Bar")]
    [SerializeField] private TextMeshProUGUI powerAmountText;
    public int PowerBooster = 2;
    [HideInInspector] public float PowerAmount;
    [Header("Pause Screen")]
    [SerializeField] private AnimatedPanel pauseScreen;
    private bool isOpen;

    private void OnEnable()
    {
        PlayerHealth.HealthAmount += Health;
        PlayerHealth.PlayerDeath += PlayerDeath;
        GameInputManager.PlayerPower += Power;
    }
    private void OnDisable()
    {
        PlayerHealth.HealthAmount -= Health;
        PlayerHealth.PlayerDeath -= PlayerDeath;
        GameInputManager.PlayerPower += Power;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void ScoreControl()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        coinText.text = GameManager.Instance.Coin.ToString();
    }
    private void Health(int amount, bool isIncrease)
    {
        if (isIncrease)
            newValue = newValue + (float)amount / 100;
        else
            newValue = newValue - (float)amount / 100;

        playerHealthBar.DOFillAmount(newValue, 3f);
    }
    private void Power(InputAction.CallbackContext context, float input)
    {
        if (PowerAmount > 99f)
            PowerAmount = 0;
    }
    private void PlayerPower()
    {
        if (PowerAmount < 100f)
            PowerAmount += PowerBooster * Time.deltaTime;

        powerAmountText.text = ((int)PowerAmount).ToString();
    }
    private void PlayerDeath()
    {
        gameOverScreen.SetActive(true);
    }
    private void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;
            if (isOpen)
                pauseScreen.Show();
            else
                pauseScreen.Hide();

            GameManager.Instance.GamePaused = isOpen;
        }
    }
    private void Update()
    {
        if (PlayerController.Instance.IsDead && GameManager.Instance.GamePaused)
            return;

        ScoreControl();
        PlayerPower();
        Pause();
    }
}
