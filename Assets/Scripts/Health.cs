using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float elud = 10f;
    
    public void TakeDamage(float damage)
    {
        elud -= damage;
        if (elud <= 0)
        {
            Destroy(gameObject);
        }
    }
}
