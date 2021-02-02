using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private MovementView movementView;
    public MovementConfigData movementConfig;
    
    private Vector2 newPosition;

    void Start()
    {
        movementView = new MovementView(gameObject);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition = new Vector2(transform.position.x + movementConfig.movementSpeed, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * movementConfig.movementTime);

            movementView.SetSpriteRendererFlipX(false);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition = new Vector2(transform.position.x + -movementConfig.movementSpeed, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * movementConfig.movementTime);

            movementView.SetSpriteRendererFlipX(true);

        }
    }
}

public class MovementView
{
    private SpriteRenderer spriteRenderer;
    private GameObject gameObject;

    public MovementView(GameObject obj)
    {
        gameObject = obj;
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetSpriteRendererFlipX(bool b)
    {
        spriteRenderer.flipX = b;
    }
}


[CreateAssetMenu(menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class MovementConfigData : ScriptableObject
{
    public float movementSpeed;
    public float movementTime;
}
