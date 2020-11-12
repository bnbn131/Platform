using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    public Transform groundCheck;
    bool isGrounded;
    Animator anim;
    int curHP;
    int maxHP = 3;

    public Main main;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
       
        

        if (Input.GetAxis("Horizontal") == 0 && (isGrounded))
        {
            anim.SetInteger("State", 1);
        }
        else {
            Flip();
            if (isGrounded) {
                anim.SetInteger("State", 2);
            }
        }

      

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);

    }

    void Flip() {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckGround() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded) {
            anim.SetInteger("State", 3);
        }
    }

    public void RecountHp(int deltaHP) {

        curHP = curHP + deltaHP;
      //  if(deltaHP < 0)
            //anim.SetInteger("State", 4);
        if (curHP <= 0) {
           
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Lose", 1f); 
            

        }

    }

    void Lose() {
        main.GetComponent<Main>().Lose();
    }

   
}
