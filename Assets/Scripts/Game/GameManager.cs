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
    private void Awake()
    {
        Instance = this;
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
            Coin += 1;
    }
    private void Update()
    {
        UpdateScore();
    }
}
