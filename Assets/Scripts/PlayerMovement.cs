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

    GameObject particleSystemGameObject;
    PlayerButtonInteraction butttonScript;
    ParticleSystem particleSystem;

    public MovementView(GameObject obj)
    {
        gameObject = obj;
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        butttonScript = gameObject.GetComponent<PlayerButtonInteraction>();

        particleSystemGameObject = GameObject.FindGameObjectsWithTag("JetpackParticleSystem")[0];
        particleSystem = particleSystemGameObject.GetComponent<ParticleSystem>();
    }

    public void SetSpriteRendererFlipX(bool b)
    {
        spriteRenderer.flipX = b;

        if(butttonScript.isJetpackActive)
            particleSystem.transform.position = new Vector3(particleSystem.transform.position.x * -1, particleSystem.transform.position.y, particleSystem.transform.position.z);
    }
}


[CreateAssetMenu(menuName = "ScriptableObjects/MovementConfig", order = 1)]
public class MovementConfigData : ScriptableObject
{
    public float movementSpeed = 4.0f;
    public float movementTime = 2.5f;
}
