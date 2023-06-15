using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Jump;
    public AudioClip Music;

    public BoxCollider2D boxCollider2D;
    public LayerMask floorMask;

    private void Awake()
    {
        Audio[] gameObjects = FindObjectsOfType<Audio>();
        if (gameObjects.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Music;
        audioSource.loop = true;
        audioSource.Play();

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && IsGrounded())
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                // construct a ray from the current touch coordinates Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    audioSource.clip = Jump;
                    audioSource.Play();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            audioSource.clip = Jump;
            audioSource.Play();
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + 0.5f, floorMask);
        return hit.collider != null;
    }
}
