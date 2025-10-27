using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class BlockController : MonoBehaviour
{
    private bool hasLanded = false;
    private bool scoreGiven = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLanded)
        {
            hasLanded = true;
            Debug.Log("ºí·Ï ÂøÁö!");

            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }

            if (collision.gameObject.CompareTag("Block") ||
                collision.gameObject.name.Contains("Block"))
            {
                if (!scoreGiven)
                {
                    scoreGiven = true;
                    StackGameManager manager = FindObjectOfType<StackGameManager>();
                    if (manager != null)
                    {
                        manager.AddScore();
                    }
                }
            }
        }
    }
}