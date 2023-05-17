using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    void Awake()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    public void DisplayTimerUpdate()
    {
        float nowTime = GameManager.Instance.GetTime() + Time.deltaTime;
        if((int)nowTime/60 > 0)
        {
            if((int)nowTime%60 >= 10)
            {
                timerText.text = ((int)nowTime/60).ToString()+":"+((int)nowTime%60).ToString();
            }
            else
            {
                timerText.text = ((int)nowTime/60).ToString()+":0"+((int)nowTime%60).ToString();
            }
        }
        else
        {
            timerText.text = ((int)nowTime%60).ToString();
        }   
    }
    public void UpdateUI()
    {
        DisplayTimerUpdate();
    }
}
