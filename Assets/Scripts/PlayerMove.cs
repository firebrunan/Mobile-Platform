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
    public BoxCollider2D boxCollider2D;
    public LayerMask floorMask;
    public LayerMask wallMask;
    public int direcao; //0 = parado, 1 = direita, -1 = esquerda
    public bool paredeEsq;
    public bool paredeDir;
    AudioSource audioSource;
    public AudioClip Jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direcao = 0;
    }

    private void Update()
    {
        Jumping();


    }

    private void FixedUpdate()
    {
        //movimento.x = moveSpeed;
        if (direcao >= 0)
        {
            movimento.x = moveSpeed;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            spriteRenderer.flipX = false;
            if (IsWallOnRight())
            {
                paredeDir=true;
                direcao = -1;
            }
        }
        else
        {
            movimento.x = -moveSpeed;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            spriteRenderer.flipX = true;
            if (IsWallOnLeft())
            {
                paredeEsq=true;
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

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && IsGrounded())
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                // construct a ray from the current touch coordinates Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                    audioSource.clip = Jump;
                    audioSource.Play();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            audioSource.clip = Jump;
            audioSource.Play();
        }

        if (!IsGrounded())
         {
             anim.SetBool("taPulando", true);
         }
         else
         {
             anim.SetBool("taPulando", false);
         }
    }

    public bool IsGrounded()
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
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right, boxCollider2D.bounds.extents.x + 0.5f, wallMask);
        return hit.collider != null;
    }
}
