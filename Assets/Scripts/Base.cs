using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private bool isEnemyBase = false;
    
    public static event Action<float> OnEnemyEnterBase;
    public static event Action OnWinGame;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            OnEnemyEnterBase?.Invoke(10f);
            Destroy(col.gameObject);
        }    
        
        if (col.gameObject.CompareTag("Friendly"))
        {
            OnWinGame?.Invoke();
        }
    }
}
