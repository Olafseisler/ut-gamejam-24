using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private float selfDestructTime = 5f;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            // TestScript1 playerScript = col.collider.gameObject.GetComponent<TestScript1>();
            // if (!playerScript)
            // {
            //     return;
            // }

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        selfDestructTime -= Time.deltaTime;
    }
}
