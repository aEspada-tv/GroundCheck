using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_4 : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 4f;
    
    [SerializeField]
    private float _jumpForce = 8f;
    
    [SerializeField]
    private LayerMask platformLayerMask;

    [SerializeField]
    private float _canJump = 0.0f;

    [SerializeField]
    private float _jumpRate = 0.25f;


    private BoxCollider2D boxCollider2d;
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    
    public bool isGrounded;


    
    private void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Move()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * _speed;

        
    }
    
    void Bounds()
    {
        if (transform.position.y < -2.64f)
        {
            transform.position = new Vector3(transform.position.x, -2.64f, 0);
        }
        else if (transform.position.y > 10f)
        {
            transform.position = new Vector3(transform.position.x, 10f, 0);
        }
        if (transform.position.x > 10f)
        {
            transform.position = new Vector3(10f, transform.position.y, 0);
        }
        else if (transform.position.x < -10f)
            transform.position = new Vector3(-10f, transform.position.y, 0);
    }
    
    void groundCheck()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.9f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), platformLayerMask);
    }
    
    void Jump()
    {
        if (Time.time > _canJump)
        {
            
            
           var movement = Input.GetAxis("Horizontal");
           if (Input.GetButton("Jump") && isGrounded)
           {
               rigidbody2d.velocity = Vector2.up * _jumpForce;
               anim.SetBool("isJumping", true);
           }

            _canJump = Time.time + _jumpRate;
        }
    }
    

    void SpriteDirection()
    {
        var movement = Input.GetAxis("Horizontal");
        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    
    void animations()
    {
        if (rigidbody2d.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        if (rigidbody2d.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }
        if (rigidbody2d.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }



        var movement = Input.GetAxis("Horizontal");
        if (movement == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

    }


    void Update()
    {

        animations();
        SpriteDirection();
        Jump();
        groundCheck();
        Bounds();

        
    }
    
    void FixedUpdate()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        Move();

    }

    
}
