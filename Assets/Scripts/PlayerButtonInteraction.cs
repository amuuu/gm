using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonInteraction : MonoBehaviour
{

    public bool isJetpackActive;
    private bool isCollidingWithButton;
    private bool isCollidingWithJetpack;


    // Start is called before the first frame update
    void Start()
    {
        isCollidingWithButton = false;
        isCollidingWithJetpack = false;
        isJetpackActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollidingWithButton && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("SHIT");

        }
        if (isCollidingWithJetpack && Input.GetKeyDown(KeyCode.E))
        {
            isJetpackActive = true;
            Debug.Log("JETPACK ACTIVE");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Button")
        {
            isCollidingWithButton = true;
        }
        if(collision.gameObject.tag == "Jetpack")
        {
            isCollidingWithJetpack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCollidingWithButton)
        {
            isCollidingWithButton = false;
        }
        if (isCollidingWithJetpack)
        {
            isCollidingWithJetpack = false;

            if(isJetpackActive)
            {
                collision.gameObject.SetActive(false);
                
            }
        }
    }
}
