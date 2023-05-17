using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    // Start is called before the first frame update

    public int jellyScore = 1;
    void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.tag == "Player")
        {
            GameManager.Instance.AddScore(jellyScore);
            gameObject.SetActive(false);
            Destroy(gameObject);    
        }
    }
}
