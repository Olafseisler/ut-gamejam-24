using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeeleSummon : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float dmg;
    [SerializeField] private float speed;
    [SerializeField] private int direction; 
    [SerializeField] private bool inCombat = false;
    [SerializeField] private bool waiting = false;
    [SerializeField] private string enemyTag;
    // Start is called before the first frame update
    void Start()
    {
        speed *= direction;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(enemyTag)) inCombat = true;
    }

    private void OnCollisionExit2D()
    {
        inCombat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (inCombat)
        {
            combat(Time.deltaTime);
        }

        else if (waiting)
        {
            idle(Time.deltaTime);
        }
        else
        {
            move(Time.deltaTime);
        }
    }

    private void move(float deltaTime)
    {
        transform.position += Vector3.right * (speed * deltaTime);
    }
    
    private void combat(float deltaTime)
    {
        
    }
    
    private void idle(float deltaTime)
    {
        
    }
}
