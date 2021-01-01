using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonInteraction : MonoBehaviour
{
    private bool isCollidingWithButton;

    private bool isCollidingWithJetpack;
    public bool isJetpackActive;

    private bool isCollidingWithTablet;
    public bool isTabletActive;

    private GameObject wonMenu;

    // Start is called before the first frame update
    void Start()
    {
        isCollidingWithButton = false;
        
        isCollidingWithJetpack = false;
        isJetpackActive = false;

        isCollidingWithTablet = false;
        isTabletActive = false;

        wonMenu = GameObject.FindGameObjectsWithTag("WonMenu")[0];
        wonMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollidingWithButton && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("BUTTON PRESSED");
            wonMenu.SetActive(true);
           

        }
        if (isCollidingWithJetpack && Input.GetKeyDown(KeyCode.E))
        {
            isJetpackActive = true;
            Debug.Log("JETPACK ACTIVATED");
        }
        if (isCollidingWithTablet && Input.GetKeyDown(KeyCode.E))
        {
            isTabletActive = true;
            Debug.Log("TABLET ACTIVATED");
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
        if(collision.gameObject.tag == "Tablet")
        {
            isCollidingWithTablet = true;
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
        if (isCollidingWithTablet)
        {
            isCollidingWithTablet = false;

            if(isTabletActive)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
