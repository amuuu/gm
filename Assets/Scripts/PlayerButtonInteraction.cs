using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonInteraction : MonoBehaviour
{
    private bool isCollidingWithButtonGameObject;

    private bool isCollidingWithJetpackGameObject;
    public bool isJetpackActive;

    private bool isCollidingWithTabletGameObject;
    public bool isTabletActive;

    private GameObject wonMenu;

    void Start()
    {
        isCollidingWithButtonGameObject = false;
        
        isCollidingWithJetpackGameObject = false;
        isJetpackActive = false;

        isCollidingWithTabletGameObject = false;
        isTabletActive = false;

        wonMenu = GameObject.FindGameObjectsWithTag("WonMenu")[0];
        wonMenu.SetActive(false);
    }

    void Update()
    {
        if (isCollidingWithButtonGameObject && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("BUTTON PRESSED");
            wonMenu.SetActive(true);
        }
        if (isCollidingWithJetpackGameObject && Input.GetKeyDown(KeyCode.E))
        {
            isJetpackActive = true;
            Debug.Log("JETPACK ACTIVATED");
        }
        if (isCollidingWithTabletGameObject && Input.GetKeyDown(KeyCode.E))
        {
            isTabletActive = true;
            Debug.Log("TABLET ACTIVATED");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            isCollidingWithButtonGameObject = true;
        }
        if (collision.gameObject.tag == "Jetpack")
        {
            isCollidingWithJetpackGameObject = true;
        }
        if (collision.gameObject.tag == "Tablet")
        {
            isCollidingWithTabletGameObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCollidingWithButtonGameObject)
        {
            isCollidingWithButtonGameObject = false;
        }
        
        if (isCollidingWithJetpackGameObject)
        {
            isCollidingWithJetpackGameObject = false;

            if(isJetpackActive)
            {
                collision.gameObject.SetActive(false);
            }
        }
        
        if (isCollidingWithTabletGameObject)
        {
            isCollidingWithTabletGameObject = false;

            if(isTabletActive)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
