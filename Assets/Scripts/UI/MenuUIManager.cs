using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [Header("Max Score")]
    [SerializeField] private int maxScore;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        maxScore = PlayerPrefs.GetInt("Max Score");
        maxScoreText.text = $"Max Score: {maxScore.ToString()}";
    }
    //Buttons
    //Social Media
    public void Social(string link)
    {
        Application.OpenURL(link);
    }
}
