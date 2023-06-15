using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movimento;
    public float moveSpeed;
    public float jumpHeight;
    //public Animator anim;
    private SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public LayerMask floorMask;
    public LayerMask wallMask;
    public LayerMask playerMask;
    public int direcao; //0 = parado, 1 = direita, -1 = esquerda
    public bool paredeEsq;
    public bool paredeDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direcao = 0;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        //movimento.x = moveSpeed;
        if (direcao >= 0)
        {
            movimento.x = moveSpeed;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            spriteRenderer.flipX = false;
            if (IsWallOnRight() || IsPlayerOnRight())
            {
                paredeDir = true;
                direcao = -1;
            }
        }
        else
        {
            movimento.x = -moveSpeed;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            spriteRenderer.flipX = true;
            if (IsWallOnLeft() || IsPlayerOnLeft())
            {
                paredeEsq = true;
                direcao = 1;
            }
        }
        /*if (rb.velocity.x != 0)
        {
            anim.SetBool("taCorrendo", true);
        }
        else
        {
            anim.SetBool("taCorrendo", false);
        }*/


    }



    public bool IsWallOnLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.left, boxCollider2D.bounds.extents.x + 0.5f, wallMask);
        return hit.collider != null;
    }
    public bool IsPlayerOnLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.left, boxCollider2D.bounds.extents.x + 0.5f, playerMask);
        return hit.collider != null;
    }

    public bool IsWallOnRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right, boxCollider2D.bounds.extents.x + 0.5f, wallMask);
        return hit.collider != null;
    }
    public bool IsPlayerOnRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right, boxCollider2D.bounds.extents.x + 0.5f, playerMask);
        return hit.collider != null;
    }
}

