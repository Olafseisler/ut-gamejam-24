using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voitle : MonoBehaviour
{
    [SerializeField] private float attackPower = 2f;
    [SerializeField] private GameObject tegelane;
    private float currentAttackPower;
    
    

    // Start is called before the first frame update
    void Start()
    {
        currentAttackPower = attackPower;
    }

    // Update is called once per frame
    void Update()
    {
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Läksid pihta:" + collision2D.gameObject.name);
        GetComponent<liikumine>();
    }

    private void OnCollisionExit(Collision other)
    {
        GetComponent<liikumine>().isMoving = true;
    }
}
