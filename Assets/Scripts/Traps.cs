using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Traps : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("Character")){
            SceneManager.LoadScene("MainMenu");
            //health.TakeDamge(20);
            //Debug.Log("-20 health");
        }
    }
}
