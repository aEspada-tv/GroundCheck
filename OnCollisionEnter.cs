using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_2 : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;


    
    [SerializeField]
    private float jumpForce = 8f;

    [SerializeField]
    private float _speed = 4f;

    public bool isGrounded;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rigidbody2d.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;

        }
    }


}
