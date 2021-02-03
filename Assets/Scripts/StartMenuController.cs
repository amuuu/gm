using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    GameObject loadPayloadObject;
    void Start()
    {
        loadPayloadObject = GameObject.FindGameObjectsWithTag("LoadPayload")[0];
    }

    public void StartGame()
    {
        Debug.Log("STARTING GAME!");
        SceneManager.LoadScene("Level1");
    }

    public void LoadGame()
    {
        DontDestroyOnLoad(loadPayloadObject);
        loadPayloadObject.GetComponent<LoadPayload>().shouldLoad = true;
        StartGame();
    }
}
