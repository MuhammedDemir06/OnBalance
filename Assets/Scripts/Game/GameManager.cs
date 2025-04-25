using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public int Score;
    public float ScoreBooster = 2;
    private float scoreValue;
    [Header("Coin")]
    public int Coin;
    [HideInInspector] public int CoinBooster = 1;
    [Header("Game")]
    public bool GamePaused;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CoinBooster = 1;
    }
    public void UpdateScore()
    {
        if(!PlayerController.Instance.IsDead)
        {
            scoreValue += ScoreBooster * Time.deltaTime;
            Score = (int)scoreValue;
        }
    }
    public void UpdateCoin()
    {
        if (!PlayerController.Instance.IsDead)
            Coin += 1 * CoinBooster;
    }
    public void StoreCoin(int amount)
    {
        Coin -= amount;
    }
    private void Update()
    {
        if (GamePaused)
            return;
        UpdateScore();
    }
}
