using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float movementTime;

    private Vector2 newPosition;
    private SpriteRenderer spriteRenderer;
 
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        newPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition = new Vector2(transform.position.x + movementSpeed, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition = new Vector2(transform.position.x + -movementSpeed, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

            spriteRenderer.flipX = true;
        }
    }
}



