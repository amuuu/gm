using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{


    public float jumpTime;

    private Rigidbody2D rb;
    
    private bool isJumpPressed = false;
    private bool isOnGround = true;
    //private bool twoTimesPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /*if (isJumpPressed && !twoTimesPressed && Input.GetKey(KeyCode.Space))
        {
            twoTimesPressed = true;
        }
        else if (!isJumpPressed && !twoTimesPressed && Input.GetKey(KeyCode.Space))
        {
            isJumpPressed = true;
        }*/

        if (!isJumpPressed && Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Floor")
        {
            isOnGround = true;
            isJumpPressed = false;
            //twoTimesPressed = false;
        }

    }
    void FixedUpdate()
    {
        if (isJumpPressed && isOnGround)
        {
                Debug.Log("ONE TIME");
            rb.AddForce(new Vector2(0, 15*jumpTime), ForceMode2D.Impulse);

            isOnGround = false;
        }

        /*else if (isJumpPressed && !isOnGround && twoTimesPressed)
        {
            Debug.Log("TWO TIMES");
            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }*/

    }
}
