using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public bool shouldLoad;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        shouldLoad = false;
    }

    public void StartGame()
    {
        Debug.Log("STARTING GAME!");
        SceneManager.LoadScene("Level1");
    }

    public void LoadGame()
    {
        shouldLoad = true;
        StartGame();
    }
}
