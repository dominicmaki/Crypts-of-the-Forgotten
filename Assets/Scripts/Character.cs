using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Movement")]
    [SerializeField] float speed = 5f;
    [Header("Movement")]
    [SerializeField] ProjectileLauncher projectileLauncher;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Move(Vector3 movement)
    {
        rb.velocity = movement * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("RedPortal")){
            SceneManager.LoadScene("HellScene");
        }
        else if(other.CompareTag("BluePortal")){
            SceneManager.LoadScene("LobbyScene");
        }
        else if(other.CompareTag("GamePortal")){
            SceneManager.LoadScene("GameScene");
        }
    }

    public void LaunchWithCharacter()
    {
        projectileLauncher.Launch();
    }
    
}
