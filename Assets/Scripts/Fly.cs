using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fly : MonoBehaviour
{
    
    [SerializeField] private float flyPower = 5f;
    [SerializeField] private float flycooldown = 10f;
    private Rigidbody2D Rb2D;

    private float currentCooldown;
    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        currentCooldown = flycooldown;
    }

   
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            float randomPower = Random.Range(0.8f,1.5f);
            Rb2D.AddForce(randomPower * flyPower * Vector2.up, ForceMode2D.Impulse);
            float randomCooldown = Random.Range(0.5f,3f);
            currentCooldown = randomCooldown * flycooldown;
        }
    }
}
