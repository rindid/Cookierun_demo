using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AdditionalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClickFunc;
    public UnityEvent buttonReleasedFunc;
    private Button _button;
    void Awake() 
    {
        _button = GetComponent<Button>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonClickFunc.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonReleasedFunc.Invoke();
    }
}
