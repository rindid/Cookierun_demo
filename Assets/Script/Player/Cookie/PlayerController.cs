using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Solved : Slide키를 누르면서 동시에 Jump키를 누르면 Idle상태로 점프하는 버그가 있다...(점프 취급 X)
//아마 Idle상태로 만드는 코드가 jump이후에 바로 끼어들기 하는게 아닌가 싶다. 아예 Jump함수에 안들어가는건 아닌걸 확인했으니 Animator쪽에서 읽기도 전에 Idle상태로 복귀하는게 문제가 아닌가 싶다.
//Jump상태를 유지하는 의도적 딜레이를 주는 방식 생각중. 
//-> 점프 할 때 살짝 띄워줌으로써 collision 안생기도록 함
public class PlayerController : MonoBehaviour
{
    public float jumpPower = 3.3f;
    public float jumpIntervalTime = 0.1f;
    public SpriteRenderer cookieSpriteRenderer = null;
    //public Sprite slideUpSprite = null;
    //public Sprite slideDownSprite = null;
    public BoxCollider2D slideUpBoxCollider2D = null;
    public BoxCollider2D slideDownBoxCollider2D = null;
    public float abilityTimeElipsed = 0;
    float jumpTimeElipsed = 0;
    private Rigidbody2D rb = null;
    bool isOnGround = false;
    bool isDoubleJump = false;
    bool isAbilityOn = false;
    public bool isSlideKeyDown = false;
    bool isHurtMotionPlayed = true;

    public enum PlayerState
    {
        Idle,
        Jump,
        Slide,
        DoubleJump,
        Hurt,
        Dead,
        Ability
    }
    public PlayerState state = PlayerState.Idle;
    private PlayerState GetState()
    {
        return state;
    }
    private void SetState(PlayerState newState)
    {
        if(!isAbilityOn)
        {
            state = newState;
        }
        else
        {
            state = PlayerState.Ability;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimeElipsed = 0;
        slideUpBoxCollider2D.enabled =true;
        slideDownBoxCollider2D.enabled =false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.isKinematic = false;
        jumpTimeElipsed += Time.deltaTime; 
        if(GameManager.Instance.IsAbilityOn())
        {
            AbilityOn();
            
            abilityTimeElipsed += Time.deltaTime;
            if(abilityTimeElipsed >= GameManager.Instance.GetAbilityDurationTime())
            {
                AbilityOff();
            }
        }
        if(GameManager.Instance.IsGameOver())
        {
            SetState(PlayerState.Dead);
        }
        if(Input.GetKeyDown(KeyCode.F) && !isDoubleJump && jumpTimeElipsed>jumpIntervalTime)
        {
            Jump();
            jumpTimeElipsed = 0;
        }
        else if(Input.GetKeyDown(KeyCode.J) && isOnGround)
        {
            SlideDown();
        }
        else if(Input.GetKeyUp(KeyCode.J) && isOnGround)
        {
            SlideUp();
        }
        if(!isAbilityOn && isOnGround && !isSlideKeyDown && isHurtMotionPlayed && !GameManager.Instance.IsGameOver())
        {
                //Debug.Log("Idle1");
                SetState(PlayerState.Idle);
        }
        
    }
    void OnCollisionEnter2D(Collision2D coll) 
    {
        if(coll.collider.tag == "Ground")
        {
            //Debug.Log("Ground Collison");
            if(!isOnGround)
            {
                rb.isKinematic = true;
            }
            isOnGround = true;
            isDoubleJump = false;
            /*if(!isSlideKeyDown && isHurtMotionPlayed && !GameManager.Instance.IsGameOver())
            {
                Debug.Log("Idle1");
                SetState(PlayerState.Idle);
            }*/
            
        }
        else
        {
            isOnGround = false;
        } 
    }
    void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.tag == "Platform")
        {
            SetState(PlayerState.Hurt);
            GameManager.Instance.Hurt();
            isHurtMotionPlayed = false;
            Invoke("HurtPlayed",0.5f);
        }
    }
    //TODO? : 미묘한 바운스 해결 방법 https://answers.unity.com/questions/462907/how-do-i-stop-a-projectile-cold-when-colliding-wit.html
    public void Jump()
    {
        if(!isDoubleJump)
        {
            if(isSlideKeyDown)
            {
                //Debug.Log("Jump3");
                slideUpBoxCollider2D.enabled =true;
                slideDownBoxCollider2D.enabled =false;
                //cookieSpriteRenderer.sprite = slideUpSprite;
                isSlideKeyDown = false;
            }
            if(!isOnGround)
            {
                //Debug.Log("Jump1");
                isDoubleJump = true;
                SetState(PlayerState.DoubleJump);
            }
            else
            {
                //Debug.Log("Jump2");
                SetState(PlayerState.Jump);
            }
            isOnGround = false;
            //rb.AddForce(Vector2.up * jumpPower);//init 180
            rb.position = rb.position + Vector2.up * 0.01f;
            rb.velocity = Vector2.up * jumpPower;//init 3.3
        }
    }
    public void SlideDown()
    {
        if(!isSlideKeyDown)
        {
            slideUpBoxCollider2D.enabled =false;
            slideDownBoxCollider2D.enabled =true;
            //cookieSpriteRenderer.sprite = slideDownSprite;
            isSlideKeyDown = true;
            SetState(PlayerState.Slide);
        }
    }
    public void SlideUp()
    {
        if(isSlideKeyDown)
        {
            slideUpBoxCollider2D.enabled =true;
            slideDownBoxCollider2D.enabled =false;
            //cookieSpriteRenderer.sprite = slideUpSprite;
            isSlideKeyDown = false;
            if(isOnGround)
            {
                //Debug.Log("Idle2");
                SetState(PlayerState.Idle);
            }
        }
    }
    public void HurtPlayed()
    {
        isHurtMotionPlayed = true;
        //Debug.Log("Idle3");
        SetState(PlayerState.Idle);
    }
    public void AbilityOn()
    {
        isAbilityOn = true;
        SetState(PlayerState.Ability);
    }
    public void AbilityOff()
    {
        GameManager.Instance.SetAbility(false);
        abilityTimeElipsed = 0;
        isAbilityOn = false;
        SetState(PlayerState.Idle);
    }
}
