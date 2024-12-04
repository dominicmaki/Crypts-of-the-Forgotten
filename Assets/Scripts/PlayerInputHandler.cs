using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float moveAmount = 1.0f;
    private Vector3 targetPosition; // Target position sprite will move to
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
    private bool isFacingUp = false;  // Track if the character is facing up
    private bool isFacingDown = false; // Track if the character is facing down

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        spriteRenderer = character.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        // Handle movement inputs
        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(-1, 0, 0);
            SetFacingDirection(false, false, false); // Facing left
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1, 0, 0);
            SetFacingDirection(true, false, false); // Facing right
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(0, 1, 0);
            SetFacingDirection(false, true, false); // Facing up
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(0, -1, 0);
            SetFacingDirection(false, false, true); // Facing down
        }

        if (Input.GetMouseButton(0)) // Left click (0 is the left mouse button)
        {
            Attack(); // Handle attack
        }

        // Update the character's movement
        character.Move(movement);
    }

    void Attack()
    {
        // Launch the projectile in the current direction
        launcher.Launch();

        // Change to attack sprite based on facing direction during attack
        if (isFacingRight)
            spriteRenderer.sprite = attackRightSprite;
        else if (isFacingUp)
            spriteRenderer.sprite = normalBackSprite; // Example for attacking up
        else if (isFacingDown)
            spriteRenderer.sprite = normalSprite; // Example for attacking down
        else
            spriteRenderer.sprite = attackLeftSprite;

        // Keep the attack sprite until next movement input
    }

    void SetFacingDirection(bool facingRight, bool facingUp, bool facingDown)
    {
        isFacingRight = facingRight;
        isFacingUp = facingUp;
        isFacingDown = facingDown;

        // Update the launcher with the current facing direction
        launcher.SetFacingDirection(facingRight, facingUp, facingDown);

        // Flip sprite horizontally for left/right
        if (facingRight || !facingRight)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = facingRight ? 1 : -1;
            transform.localScale = localScale;
        }

        // Set sprite based on facing direction
        SetFacingSprite();
    }

    void SetFacingSprite()
    {
        // Use the appropriate sprite based on the current facing direction
        if (isFacingUp)
        {
            spriteRenderer.sprite = normalBackSprite;
        }
        else if (isFacingDown)
        {
            spriteRenderer.sprite = normalSprite;
        }
        else if (isFacingRight)
        {
            spriteRenderer.sprite = defaultRightSprite;
        }
        else
        {
            spriteRenderer.sprite = defaultLeftSprite;
        }
    }
}
