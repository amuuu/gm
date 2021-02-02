using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager: MonoBehaviour
{
    Save saveInstance;

    GameObject player;
    
    PlayerJetpack playerJetpack;
    PlayerTablet playerTablet;
    PlayerButtonInteraction playerInteractions;
    PlayerGoggles playerGoggles;

    GameObject nofilter;
    GameObject red;
    GameObject green;
    GameObject blue;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        
        playerJetpack = player.GetComponent<PlayerJetpack>();
        playerTablet = player.GetComponent<PlayerTablet>();
        playerInteractions = player.GetComponent<PlayerButtonInteraction>();
        playerGoggles = player.GetComponent<PlayerGoggles>();

        nofilter = GameObject.FindGameObjectsWithTag("NoFilter")[0];
        red = GameObject.FindGameObjectsWithTag("Red")[0];
        green = GameObject.FindGameObjectsWithTag("Green")[0];
        blue = GameObject.FindGameObjectsWithTag("Blue")[0];

    }

    public Save CreateSaveGameObject()
    {
        saveInstance = new Save();

        saveInstance.playerPosition = (player.transform.position.x, player.transform.position.y);

        saveInstance.isJetpackActive = playerInteractions.isJetpackActive;
        saveInstance.jetpackCharge = playerJetpack.GetJetpackCharge();
        saveInstance.isTabletActive = playerInteractions.isTabletActive;
        saveInstance.tabletCharge = playerTablet.GetTabletCharge();

        saveInstance.currentGoggleFilterCursor = playerGoggles.GetCurrentGoggleFilterCursor();

        saveInstance.nofilterDeactivatedObjects = new List<int>();
        saveInstance.redDeactivatedObjects = new List<int>();
        saveInstance.greenDeactivatedObjects = new List<int>();
        saveInstance.blueDeactivatedObjects = new List<int>();

        FillChildsDeactiveIndexesList(ref nofilter, ref saveInstance.nofilterDeactivatedObjects);
        FillChildsDeactiveIndexesList(ref red, ref saveInstance.redDeactivatedObjects);
        FillChildsDeactiveIndexesList(ref green, ref saveInstance.greenDeactivatedObjects);
        FillChildsDeactiveIndexesList(ref blue, ref saveInstance.blueDeactivatedObjects);

        return saveInstance;
    }

    private void FillChildsDeactiveIndexesList(ref GameObject gameObj, ref List<int> targetList)
    {
        int counter = 0;
        foreach (Transform child in gameObj.transform)
        {
            if (!child.gameObject.activeSelf)
                targetList.Add(counter);
            counter++;
        }
    }
}
