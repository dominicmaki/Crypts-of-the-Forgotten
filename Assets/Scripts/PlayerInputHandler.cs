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
    public ProjectileLauncher launcher;

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
            isFacingRight = false;
            FlipSprite(false); // Facing left
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1, 0, 0);
            isFacingRight = true;
            FlipSprite(true); // Facing right
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(0, -1, 0);
        }

        if (Input.GetMouseButton(0))  // Left click (0 is the left mouse button)
        {
            Attack(); // Handle attack
        }

        // Determine the direction of movement and set sprite accordingly
        SetSpriteForDirection(movement);

        // If no movement keys are pressed and not attacking, revert to normal sprite
        if (movement == Vector3.zero && !Input.GetMouseButton(0))
        {
            SetSpriteForDirection(Vector3.zero); // Reset sprite based on facing direction
        }

        character.Move(movement);
    }

    // Set sprite based on direction (up, down, left, right)
    void SetSpriteForDirection(Vector3 movement)
    {
        if (movement == Vector3.zero && !Input.GetMouseButton(0)) 
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

        // Launch the projectile in the correct direction
        launcher.SetFacingDirection(isFacingRight); // Pass the facing direction
        launcher.Launch();

        // Start the attack animation and then reset the sprite smoothly
        StartCoroutine(ResetSpriteAfterDelay(0.2f)); // Adjust the delay as needed (0.2 seconds for example)
    }

    // Coroutine to reset sprite after a delay (smooth transition)
    IEnumerator ResetSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the duration of the attack animation
        ResetSprite(); // Call ResetSprite after the delay
    }

    void ResetSprite()
    {
        // Reset to the normal idle sprite after the attack
        SetSpriteForDirection(Vector3.zero); // Revert to facing sprite when idle
    }

    void FlipSprite(bool isFacingRight)
    {
        if (this.isFacingRight != isFacingRight)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = isFacingRight ? 1 : -1; // Flip the character's scale
            transform.localScale = localScale;

            this.isFacingRight = isFacingRight;

            // Notify the ProjectileLauncher to flip the spawn point
            launcher.SetFacingDirection(isFacingRight);
        }
    }
}
