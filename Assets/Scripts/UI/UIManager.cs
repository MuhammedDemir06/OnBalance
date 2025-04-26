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
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    [Header("Coin")]
    [SerializeField] private TextMeshProUGUI coinText;
    [Header("Power Bar")]
    [SerializeField] private TextMeshProUGUI powerAmountText;
    public int PowerBooster = 2;
    [HideInInspector] public float PowerAmount;
    [Header("Pause Screen")]
    [SerializeField] private AnimatedPanel pauseScreen;
    private bool isOpen;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private Rigidbody2D player;
    [Header("Sounds")]
    [SerializeField] private AudioSource gameOverSound;

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
        GameInputManager.PlayerPower -= Power;
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
        Invoke(nameof(ResetPower), 0.9f);
    }
    private void ResetPower()
    {
        if (PowerAmount >= 100f)
            PowerAmount = 0;
    }
    private void PlayerPower()
    {
        if (PowerAmount < 100f)
            PowerAmount += PowerBooster * Time.deltaTime;

        if (PowerAmount > 99)
            powerAmountText.color = Color.blue;
        else
            powerAmountText.color = Color.white;

        powerAmountText.text = ((int)PowerAmount).ToString();
    }
    private void PlayerDeath()
    {
        GameManager.Instance.CursorActive(true);
        gameOverScreen.SetActive(true);
        if (gameOverSound.enabled)
            gameOverSound.Play();

        pauseScreen.Hide();

        if (GameManager.Instance.Score > GameManager.Instance.MaxScore)
        {
            GameManager.Instance.MaxScore = GameManager.Instance.Score;
            PlayerPrefs.SetInt("Max Score", GameManager.Instance.MaxScore);
        }

        maxScoreText.text = $"Max Score: {GameManager.Instance.MaxScore.ToString()}";
        lastScoreText.text = $"Score: {GameManager.Instance.Score.ToString()}";
    }
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerController.Instance.IsDead)
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                GameManager.Instance.CursorActive(true);

                player.bodyType = RigidbodyType2D.Static;

                pauseScreen.Show();
            }
            else
            {
                GameManager.Instance.CursorActive(false);

                player.bodyType = RigidbodyType2D.Dynamic;

                pauseScreen.Hide();
            }

            GameManager.Instance.GamePaused = isOpen;
        }
    }
    private void Update()
    {
        Pause();

        if (PlayerController.Instance.IsDead || GameManager.Instance.GamePaused)
            return;

        ScoreControl();
        PlayerPower();
    }
}
