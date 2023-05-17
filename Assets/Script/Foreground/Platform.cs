using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.tag == "Ability")
        {
            DestroyPlatform();
            Debug.Log("PlatformDestoryed"); 
        }
    }
    public void DestroyPlatform()
    {
        //TODO : 아래 active off하는 부분을 모션 이펙트로 변경
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
