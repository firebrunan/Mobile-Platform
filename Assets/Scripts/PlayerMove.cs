using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movimento;
    public float moveSpeed;
    public float jumpHeight;
    public Animator anim;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    public LayerMask floorMask;
    public LayerMask wallMask;
    public int direcao = 0; //0 = parado, 1 = direita, -1 = esquerda
    public bool paredeEsq;
    public bool paredeDir;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }


    }

    private void FixedUpdate()
    {
        movimento.x = moveSpeed;
        Walking();
        Jumping();


    }

    private void Walking()
    {
        if (direcao >= 0)
        {
            rb.velocity = new Vector2(moveSpeed, -3f);
            spriteRenderer.flipX = false;
            if (IsWallOnRight())
            {
                direcao = -1;
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, -3f);
            spriteRenderer.flipX = true;
            if (IsWallOnLeft())
            {
                direcao = 1;
            }
        }
        if (rb.velocity.x != 0)
        {
            anim.SetBool("taCorrendo", true);
        }
        else
        {
            anim.SetBool("taCorrendo", false);
        }
        
        
    }


    public void Jumping()
    {
        if (!IsGrounded())
        {
            anim.SetBool("taPulando", true);
        }
        else
        {
            anim.SetBool("taPulando", false);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + 0.5f, floorMask);
        return hit.collider != null;
    }

    public bool IsWallOnLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.left, boxCollider2D.bounds.extents.x + 0.5f, wallMask);
        return hit.collider != null;
    }

    public bool IsWallOnRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right, boxCollider2D.bounds.extents.x - 0.5f, wallMask);
        return hit.collider != null;
    }
}
