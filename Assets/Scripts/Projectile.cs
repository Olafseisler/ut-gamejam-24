using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    
    public int direction;
    float startCooldown = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    
    // Update is called once per frame
    void Update()
    {
        startCooldown -= Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (startCooldown > 0) return;
        Health health = col.gameObject.GetComponent<Health>();
        if (health == null) return;
        
        health.TakeDamage(damage);

        Destroy(gameObject);
    }
}
