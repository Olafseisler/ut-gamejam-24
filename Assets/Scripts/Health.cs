using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    [SerializeField] private float elud = 10f;

    public static event Action<float> OnEnemyDeath;
    

    public void TakeDamage(float damage)
    {
        elud -= damage;
        if (elud <= 0)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Enemy"))
                OnEnemyDeath?.Invoke(10f);
        }
    }
}
