using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private BoxCollider2D abilityBc = null;
    void Awake()
    {
        abilityBc = GetComponent<BoxCollider2D>();
        abilityBc.enabled = false;
    }
    void Update()
    {
        if(GameManager.Instance.IsAbilityOn())
        {
            Invoke("BoxColliderOn", 0.3f);
        }
        else
        {
            BoxColliderOff();
        }
    }
    void BoxColliderOn()
    {
        abilityBc.enabled = true;
    }
    void BoxColliderOff()
    {
        abilityBc.enabled = false;
    }
}
