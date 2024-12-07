using UnityEngine;


[CreateAssetMenu(fileName = "NewMob", menuName = "Mob/New Mob Variant")]
public class MobSO : ScriptableObject
{
    public GameObject mobPrefab;  // The specific prefab for this mob
    public int maxHealth;
    public int attackDamage;
    public float moveSpeed;
    public AudioClip deathSound;  // Optional: death sound for the mob
}

