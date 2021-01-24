using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_3 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private float _jumpForce = 8f;
    [SerializeField]
    private LayerMask platformLayerMask;

    private BoxCollider2D boxCollider2d;
    private Animator anim;
    
    public bool isGrounded;

    [SerializeField]
    private Rigidbody2D rigidbody2d;
    [SerializeField]
    private float extraHeightText = .01f;


    private void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 1.32f, transform.position.y - 0.6f),
            new Vector2(transform.position.x + 1.32f, transform.position.y - 1.28f), platformLayerMask);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody2d.velocity = Vector2.up * _jumpForce;
            anim.SetBool("isJumping", true);
        }

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

    void FixedUpdate()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * _speed;

    }
}
