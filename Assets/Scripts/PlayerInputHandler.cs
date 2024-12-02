using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float moveAmount = 1.0f;
    private Vector3 targetPosition; // Target position sprite will move to
    private Vector3 mousePosition;
    [SerializeField] Character character;
    public float rotationSpeed = 700f; // Speed of rotation when moving
    public Sprite normalSprite; // The normal idle sprite
    public Sprite attackRightSprite; // The sprite when attacking right
    public Sprite attackLeftSprite;  // The sprite when attacking left
    public Sprite normalBackSprite;  // Sprite when pressing W (moving up)
    public Sprite defaultRightSprite; // Sprite when pressing D (moving right)
    public Sprite defaultLeftSprite;  // Sprite when pressing A (moving left)
    private SpriteRenderer spriteRenderer;

    private bool isFacingRight = true; // Track if the character is facing right

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        spriteRenderer = character.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the character towards the mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the Z position remains the same

        // Move character towards the mouse position smoothly
        targetPosition = Vector3.MoveTowards(transform.position, mousePosition, moveAmount * Time.deltaTime);
        transform.position = targetPosition;

        Vector3 movement = Vector3.zero;
        // Handle movement inputs
        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(-1, 0, 0);
            isFacingRight = false; // Facing left
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1, 0, 0);
            isFacingRight = true; // Facing right
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Attack(); // Handle attack
        }

        // Determine the direction of movement and set sprite accordingly
        SetSpriteForDirection(movement);

        // If no movement keys are pressed and not attacking, revert to normal sprite
        if (movement == Vector3.zero && !Input.GetKey(KeyCode.Space))
        {
            SetSpriteForDirection(Vector3.zero); // Reset sprite based on facing direction
        }

        character.Move(movement);
    }

    // Set sprite based on direction (up, down, left, right)
    void SetSpriteForDirection(Vector3 movement)
    {
        if (movement == Vector3.zero && !Input.GetKey(KeyCode.Space)) 
        {
            // Idle state: Use facing sprite
            if (isFacingRight)
                spriteRenderer.sprite = normalSprite;
            else
                spriteRenderer.sprite = normalSprite; // (optional: a different idle sprite for left-facing)
        }
        else if (movement.y > 0) // Moving up (W)
        {
            spriteRenderer.sprite = normalBackSprite;
        }
        else if (movement.y < 0) // Moving down (S)
        {
            spriteRenderer.sprite = normalSprite;
        }
        else if (movement.x != 0) // Moving left or right (A/D)
        {
            if (isFacingRight)
                spriteRenderer.sprite = defaultRightSprite;
            else
                spriteRenderer.sprite = defaultLeftSprite;
        }
    }

    void Attack()
    {
        // Change to attack sprite based on facing direction
        if (isFacingRight)
            spriteRenderer.sprite = attackRightSprite;
        else
            spriteRenderer.sprite = attackLeftSprite;

        // Optionally, reset to normal sprite after a short time to simulate an attack animation
        Invoke("ResetSprite", 0.2f); // Reset sprite after 0.2 seconds (adjust as needed)
    }

    void ResetSprite()
    {
        // Reset to the normal idle sprite
        SetSpriteForDirection(Vector3.zero); // Revert to facing sprite when idle
    }
}
