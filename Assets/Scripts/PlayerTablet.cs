using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTablet : MonoBehaviour
{
    
    private PlayerButtonInteraction buttonScript;
    private bool isActive;

    GameObject[] tabletCameraObjects;

    private float tabletCharge;
    private bool usingTablet;

    void Start()
    {
        isActive = false;
        buttonScript = this.GetComponent<PlayerButtonInteraction>();
        tabletCharge = 100.0f;
        usingTablet = false;

        //tabletCameraObjects = new GameObject[3];
        tabletCameraObjects = GameObject.FindGameObjectsWithTag("TabletCamera");

        ToggleTabletCameraObjects(false);
    }

    void Update()
    {
        if (buttonScript.isTabletActive && !isActive)
        {
            isActive = buttonScript.isTabletActive;
        }
        
        if (isActive && !usingTablet && Input.GetKeyDown(KeyCode.M))
        {
            usingTablet = true;
            ToggleTabletCameraObjects(true);
            Debug.Log("USING TABLET");
        }
        else if (isActive && usingTablet && Input.GetKeyDown(KeyCode.M))
        {
            usingTablet = false;
            ToggleTabletCameraObjects(false);
            Debug.Log("STOPPED USING TABLET");
        }
    }

    void ToggleTabletCameraObjects(bool on)
    {
        foreach(GameObject obj in tabletCameraObjects)
        {
            if (on)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}
