using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    RectTransform healthBar;
    private float maxSize=0;
    private float minRight = 0;
    void Awake()
    {
        healthBar = GetComponent<RectTransform>();
        maxSize = 0;
    }
    public void DisplayHealthUpdate()
    {
        if(maxSize==0)
        {
            maxSize = healthBar.rect.width;
            minRight = -healthBar.offsetMax[0];
        }
        healthBar.offsetMax =  Vector2.left * (minRight + (1-GameManager.Instance.HealthPercentage()) * maxSize) + Vector2.up* healthBar.offsetMax[1];
        //healthBar.sizeDelta = Vector2.right * (GameManager.Instance.HealthPercentage() * maxSize) + Vector2.up * healthBar.sizeDelta[1];
    }
    public void UpdateUI()
    {
        DisplayHealthUpdate();
    }
}
