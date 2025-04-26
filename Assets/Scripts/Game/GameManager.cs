using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public int Score;
    public int MaxScore;
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

        GetData();
        CursorActive(false);
    }
    private void GetData()
    {
        MaxScore = PlayerPrefs.GetInt("Max Score");
    }
    public void CursorActive(bool active)
    {
        if (active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
            
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Game/Delete Current Data")]
    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted All Data");
    }
#endif
}
