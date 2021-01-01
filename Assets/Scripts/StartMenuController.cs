using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("STARTING GAME!");
        SceneManager.LoadScene("Level1");
    }
}
