using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float projectileSpeed = 3f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnTransform;
    [SerializeField] float despawnTime = 4f;

    void Update()
    {
        
    }

    public void Launch()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0, 0);
        Destroy(newProjectile, despawnTime);
    }
}
