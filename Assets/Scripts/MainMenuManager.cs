using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playerPrefab;  // Reference to the player prefab
    public Transform spawnPoint;     // Reference to where the player should spawn

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        // Instantiate the player at the spawn point after loading the scene
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
