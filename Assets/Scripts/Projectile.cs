using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;  // Damage the projectile will deal

    void Start()
{
    damage = PlayerStats.Instance.playerAttackDamage; // Set the damage when the projectile is initialized
}

    // Detect collision with enemy
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            Mob enemy = collision.gameObject.GetComponent<Mob>();
            if (enemy != null)
            {

                // Deal damage to the enemy
                enemy.TakeDamage(damage);  

                // Destroy the projectile after hitting the enemy
                Destroy(gameObject);       
            }
        }
    }
}
