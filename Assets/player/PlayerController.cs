using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;

    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onground = false;

    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerClear";
    public string deadAnime = "PlayerOver";
    public string nowAnime = "";
    public string oldAnime = "";

    public static string gameState = "playing";

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        gameState = "playing";
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
}

    // Update is called once per frame
    void Update()
    {
        if(gameState != "playing")
        {
            return;
        }
        axisH = Input.GetAxisRaw("Horizontal");

        if(axisH > 0.0f)
        {
            /*Debug.Log("오른쪽 이동");*/
            transform.localScale = new Vector3(1, 1, 0);
        }else if(axisH < 0.0f)
        {
           /* Debug.Log("왼쪽 이동");*/
            transform.localScale = new Vector3(-1, 1, 0);
        }

        if(Input.GetKeyDown/*("Jump")*/(KeyCode.Space))
        {
            Jump();
        }
    }
    void FixedUpdate()
    {

        if(gameState != "playing")
        {
            return;
        }
        onground = Physics.Linecast(transform.position,
                                    transform.position - (transform.up * 0.1f), groundLayer);
        
     if(onground ||axisH !=0)
    {
        rbody.velocity = new Vector3(speed * axisH, rbody.velocity.y);
    }
        if(/*onground/*&&*/ goJump)
        {
            Debug.Log("점프");
            Vector3 jumpPw = new Vector3(0,jump,0);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        if(onground)
        {
            if(axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            nowAnime = jumpAnime;
        }
        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }

    }

    
    public void Jump()
    {
        goJump = true;
        Debug.Log("점프 버튼 눌림!");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal();
        }else if(collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        else if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score = item.value;
            Destroy(collision.gameObject);
        }
    }

    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "gameclear";
        GameStop();
    }
    public void GameOver()
    {
        animator.Play(deadAnime);
        gameState = "gameover";
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.AddForce(new Vector3(0, 5, 0), ForceMode2D.Impulse);
    }
    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector3(0,0,0);
    }
}
