using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeeleSummon : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float dmg;
    [SerializeField] private bool inCombat = false;
    [SerializeField] private bool waiting = false;
    [SerializeField] private string enemyTag;
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(enemyTag)) inCombat = true;
        else waiting = true;
    }

    private void OnCollisionExit2D()
    {
        inCombat = waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat)
        {
            combat();
        }

        if (!waiting)
        {
            move(Time.deltaTime); 
        }
        else
        {
            idle();
        }
    }

    private void move(float deltaTime)
    {
//         rb.MovePosition(rb.position + (Vector2) transform.forward * (speed * deltaTime));
    }
    
    private void combat()
    {
        
    }
    
    private void idle()
    {
        
    }
}
