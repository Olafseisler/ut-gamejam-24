using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private bool isEnemyBase = false;
    [SerializeField] private int enemyBaseHealth = 100;
    
    public static event Action<int> OnEnemyEnterBase;
    public static event Action OnWinGame;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isEnemyBase && col.gameObject.CompareTag("Enemy"))
        {
            OnEnemyEnterBase?.Invoke(10);
            Destroy(col.gameObject);
        }    
        
        if (isEnemyBase && col.gameObject.CompareTag("Friendly"))
        {
            enemyBaseHealth -= col.gameObject.GetComponent<ManaCost>().manaCost;
            if (enemyBaseHealth <= 0)
                OnWinGame?.Invoke();
        }
    }
}
