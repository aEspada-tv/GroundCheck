using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_1 : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    
    [SerializeField]
    private LayerMask platformLayerMask;
    
    [SerializeField]
    private float jumpForce = 8f;
    
    [SerializeField]
    private float _speed = 4f;
    
    [SerializeField]
    private float extraHeightText = .05f;
    
    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(isGrounded() && Input.GetKeyDown(KeyCode.Space))
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

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, + extraHeightText, platformLayerMask);

        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x + extraHeightText), rayColor);
        return raycastHit.collider != null;
    }
}
