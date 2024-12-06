using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // The audio clip you want to play as background music
    private AudioSource audioSource; // The AudioSource component to play the music

    void Start()
    {
        // Get or add an AudioSource component if not already attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource if none exists
        }

        // Set up the AudioSource
        audioSource.clip = backgroundMusic;  // Set the audio clip
        audioSource.loop = true;              // Set the loop property to true
        audioSource.playOnAwake = true;       // Play automatically when the game starts
        audioSource.volume = 0.5f;            // Adjust the volume (optional)

        // Play the music
        audioSource.Play();
    }
}
