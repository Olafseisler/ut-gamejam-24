using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    [SerializeField] private int elud = 10;

    public static event Action<int> OnEnemyDeath;
    

    public void TakeDamage(int damage)
    {
        elud -= damage;
        if (elud <= 0)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Enemy"))
                OnEnemyDeath?.Invoke(10);
        }
    }
}
