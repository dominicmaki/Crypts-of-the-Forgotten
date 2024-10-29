using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void StartGame(){
        SceneManager.LoadScene("LobbyScene");
    }

    public void Settings(){
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame(){
        Debug.Log("QUIT");
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
