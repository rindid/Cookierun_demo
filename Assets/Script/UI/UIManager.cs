using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public UnityEvent UIUpdateEvents;
    void OnEnable()
    { 
        if (GameManager.Instance != null && GameManager.Instance.GameUIManager == null)
        {
            GameManager.Instance.GameUIManager = this;
        }
    }
    public void UpdateUI()
    {
        UIUpdateEvents.Invoke();
    }
    
}
