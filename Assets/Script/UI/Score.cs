using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;
    void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    public void DisplayScoreUpdate()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();  
    }
    public void UpdateUI()
    {
        DisplayScoreUpdate();
    }
}
