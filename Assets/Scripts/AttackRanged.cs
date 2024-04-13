using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanged : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] float maxRange = 10f;
    [SerializeField] float minRange = 2f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] string enemyLayer = "Enemy";
    [SerializeField] float attackCooldown = 1.5f;

    private float _currentAttackCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentAttackCooldown = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the enemy in range 
        Vector3 enemyPosition = getEnemyInRange();
        if (enemyPosition == Vector3.zero)
        {
            return;
        }
        // Check if too close 
        if (enemyPosition == Vector3.negativeInfinity)
        {
            return;
        }
        // Do not shoot if friendlies are in the way
        RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyPosition - transform.position, maxRange, 1 << LayerMask.NameToLayer(enemyLayer));
        if (hit.collider != null && hit.collider.CompareTag(gameObject.tag))
        {
            return;
        }
        
        ShootProjectile(enemyPosition);
    }
    
    Vector3 getEnemyInRange()
    {
        // Use OverlapCircleAll to get all colliders within the range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, maxRange, 1 << LayerMask.NameToLayer(enemyLayer));

        if (hits.Length == 0)
        {
            return Vector3.zero; // No enemy in range
        }

        foreach (var hit in hits)
        {
            float distance = Vector3.Distance(hit.transform.position, transform.position);
            if (distance >= minRange)
            {
                return hit.transform.position; // Return the position of the first enemy that is not too close
            }
        }

        return Vector3.negativeInfinity; // All enemies are too close
    }

    public void ShootProjectile(Vector2 enemyPosition)
    {
        Vector2 direction = enemyPosition - (Vector2)transform.position;
        
        if (_currentAttackCooldown > 0)
        {
            return;
        }
        
        GameObject projectile = Instantiate(this.projectile.gameObject, transform.position + (Vector3)direction.normalized * 0.5f,
             Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        projectile.tag = gameObject.tag;
        _currentAttackCooldown = attackCooldown;
        // Debug.Log("Shot projectile");
    }
}