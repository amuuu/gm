using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonInteraction : MonoBehaviour
{
    private bool isCollidingWithButton;

    // Start is called before the first frame update
    void Start()
    {
        isCollidingWithButton = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollidingWithButton && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("SHIT");

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Button")
        {
            isCollidingWithButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        isCollidingWithButton = false;
    }
}
