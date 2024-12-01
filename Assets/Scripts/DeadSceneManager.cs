using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeadSceneManager : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("LobbyScene");
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
