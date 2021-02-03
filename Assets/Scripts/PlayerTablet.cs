using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class PlayerTablet: MonoBehaviour
{
    private PlayerButtonInteraction buttonScript;
    private TabletModel model;
    private TabletView view;

    private void Start()
    {
        buttonScript = this.GetComponent<PlayerButtonInteraction>();
        model = new TabletModel();
        view = new TabletView();
    }

    void Update()
    {
        if (buttonScript.isTabletActive && !model.isActive)
        {
            model.isActive = buttonScript.isTabletActive;
        }

        if (model.isActive && !model.isBeingUsed && Input.GetKeyDown(KeyCode.M))
        {
            model.isBeingUsed = true;
            model.ToggleTabletCameraObjects(true);
            Debug.Log("USING TABLET");
            
        }
        else if (model.isActive && model.isBeingUsed && Input.GetKeyDown(KeyCode.M))
        {
            model.isBeingUsed = false;
            model.ToggleTabletCameraObjects(false);
            Debug.Log("STOPPED USING TABLET");

            model.tabletCharge -= 20f;
        }

        if (model.tabletCharge <= 0.0f)
        {
            model.isActive = false;
            buttonScript.isTabletActive = false;
            view.UpdateTabletChargeTextUI(-1);
        }

        if (model.isActive)
            view.UpdateTabletChargeTextUI((int)model.tabletCharge);
    }

    public float GetTabletCharge()
    {
        return model.tabletCharge;
    }

    public void SetTabletCharge(float charge)
    {
        model.tabletCharge = charge;
    }
}

class TabletModel
{

    private GameObject[] tabletCameraObjects;
    
    public bool isActive;
    public float tabletCharge;
    public bool isBeingUsed;

    public TabletModel()
    {
        isActive = false;
        tabletCharge = 100.0f;
        isBeingUsed = false;

        tabletCameraObjects = GameObject.FindGameObjectsWithTag("TabletCamera");

        ToggleTabletCameraObjects(false);
    }

    public void ToggleTabletCameraObjects(bool on)
    {
        foreach (GameObject cameraObject in tabletCameraObjects)
        {
            if (on)
            {
                cameraObject.SetActive(true);
            }
            else
            {
                cameraObject.SetActive(false);
            }
        }
    }
}

class TabletView
{
    GameObject textUI;

    public TabletView()
    {
        textUI = GameObject.FindGameObjectsWithTag("TabletChargeText")[0];
    }

    public void UpdateTabletChargeTextUI(int value)
    {
        var textComponent = textUI.GetComponent<Text>();

        if (value != -1)
            textComponent.text = "TABLET CHARGE: " + value + "%";
        else
            textComponent.text = "";
    }
}