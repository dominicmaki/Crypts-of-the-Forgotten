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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.tag);  // Log the tag
        if (other.CompareTag("Character"))
        {
            Debug.Log("Character triggered");
            HealthBar health = other.GetComponent<HealthBar>();
            health.UpdateHealth(-20);
            Debug.Log("-20 health");
        }
    }

}
