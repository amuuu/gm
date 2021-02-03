using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager: MonoBehaviour
{
    GameObject player;
    
    PlayerJetpack playerJetpack;
    PlayerTablet playerTablet;
    PlayerButtonInteraction playerInteractions;
    PlayerGoggles playerGoggles;

    GameObject nofilter;
    GameObject red;
    GameObject green;
    GameObject blue;

    [SerializeField] Save saveInstance;
    [SerializeField] Save loadInstance;

    private void Start()
    {
        /*if (GameObject.Find("LoadPayload").GetComponent<StartMenuController>().shouldLoad)
        {
            LoadGame();
        }*/
    }


    public void SaveGame()
    {
        Debug.Log("Saving the game...");

        saveInstance = CreateSaveGameObject();
        string json = JsonUtility.ToJson(saveInstance);
        
        System.IO.File.WriteAllText("GAME_SAVE.json", json);
    }

    public void LoadGame()
    {
        ReadSaveFile();

        player.transform.position = new Vector2(loadInstance.playerPosition.Item1, loadInstance.playerPosition.Item2);

        playerInteractions.isJetpackActive = loadInstance.isJetpackActive;
        playerJetpack.SetJetpackCharge(loadInstance.jetpackCharge);
        
        loadInstance.isTabletActive = playerInteractions.isTabletActive;
        playerTablet.SetTabletCharge(loadInstance.tabletCharge);

        playerGoggles.SetCurrentGoggleFilterCursor(loadInstance.currentGoggleFilterCursor);

        DeactiveChildsByIndex(ref nofilter, ref saveInstance.nofilterDeactivatedObjects);
        DeactiveChildsByIndex(ref red, ref saveInstance.redDeactivatedObjects);
        DeactiveChildsByIndex(ref green, ref saveInstance.greenDeactivatedObjects);
        DeactiveChildsByIndex(ref blue, ref saveInstance.blueDeactivatedObjects);

    }
    
    public void ReadSaveFile()
    {
        Debug.Log("Loading the game...");
        if (File.Exists("GAME_SAVE.json"))
        {
            string json = System.IO.File.ReadAllText("GAME_SAVE.json");
            loadInstance = JsonUtility.FromJson<Save>(json);
        }
        else
        {
            Debug.Log("ERROR: Save file not found.");
        }
    }

    public Save CreateSaveGameObject()
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


        Save saveInstance = new Save();

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

    private void DeactiveChildsByIndex(ref GameObject gameObj, ref List<int> targetList)
    {
        int counter = 0;
        foreach (Transform child in gameObj.transform)
        {
            if (targetList.Contains(counter))
            {
                child.gameObject.SetActive(false);
            }
            counter++;
        }
    }
}
