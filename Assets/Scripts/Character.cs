using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public static Character Instance;  // Declare the static instance of the character
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float speed = 5f;

    [Header("Projectile")]
    [SerializeField] ProjectileLauncher projectileLauncher;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Singleton pattern: Ensure only one instance of the character persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps player across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Add movement or other gameplay logic here
    }

    public void Move(Vector3 movement)
    {
        rb.velocity = movement * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RedPortal"))
        {
            SceneManager.LoadScene("HellScene");
        }
        else if (other.CompareTag("BluePortal"))
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else if (other.CompareTag("GamePortal"))
        {
            SceneManager.LoadScene("GameScene");
        }
        else if (other.CompareTag("Mob"))
        {
            // Take damage logic here
        }
    }

    public void LaunchWithCharacter()
    {
        projectileLauncher.Launch();
    }
}

