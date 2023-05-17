using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : 배경 바뀔때는 여기서 isObjectRespawn false로 전환하고, 필요한 위치에 새로운 맵 로딩하면 될듯? 대신 싱크 신경써...주진 않아도 되겠지? 해봐야 알듯
public class ObjectMove : MonoBehaviour
{
    public Sprite bgSprite = null;
    public float speed =2.0f;
    public bool isObjectRespawn = true;
    public float respawnInterval = 0.0f;
    public float respawnPosX = 0.0f;
    public float destroyPosX = -10.0f;
    float bgTimer = 0;
    bool flag = true;

    // Start is called before the first frame update
    void Awake()
    {
        flag = true;
        bgTimer = 0;
        if(transform.position.x <= respawnPosX && bgSprite != null && flag && isObjectRespawn)
        {
            Instantiate(gameObject, transform.position + Vector3.right* respawnInterval+ Vector3.right * bgSprite.bounds.size.x * transform.localScale.x, transform.rotation, transform.parent);
            flag = false;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bgTimer += Time.deltaTime;
        if(!GameManager.Instance.IsGameOver())
        {
            transform.Translate(Vector3.left * bgTimer * speed);
            bgTimer = 0;
        }
        if(transform.position.x <= respawnPosX && bgSprite != null && flag && isObjectRespawn)
        {
            Instantiate(gameObject, transform.position + Vector3.right* respawnInterval + Vector3.right * bgSprite.bounds.size.x * transform.localScale.x, transform.rotation, transform.parent);
            flag = false;
        }
        if(transform.position.x < destroyPosX)
        {
            Destroy(gameObject);
        }
    }
}
