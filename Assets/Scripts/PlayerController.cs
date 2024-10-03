using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player_Rb;
    private bool grounded;
    [SerializeField] private float jumpForce;

    void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        player_Rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
