using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public BoxCollider2D boxColliderPlayer;
    public BoxCollider2D boxColliderHouse;

    void Update()
    {
        if (boxColliderPlayer.IsTouching(boxColliderHouse))
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}

