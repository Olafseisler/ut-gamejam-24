using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    [SerializeField] private int attackPower = 2;
    [SerializeField] private float attackCooldown = 1f;

    private Animator _animator;
    private float _currentAttackCooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        _currentAttackCooldown = 0;
        _animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_currentAttackCooldown > 0)
        {
            _currentAttackCooldown -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.CompareTag(gameObject.tag) && _currentAttackCooldown <= 0)
        {
            Health health = collision2D.gameObject.GetComponent<Health>();
            if (health == null) return;
            
            health.TakeDamage(attackPower);
            _currentAttackCooldown = attackCooldown;
            _animator.SetTrigger("Attack");
        }
    }
}
