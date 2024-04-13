using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RangedSummon : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float dmg;
    [SerializeField] private float speed;
    [SerializeField] private int direction;
    [SerializeField] private float range;
    [SerializeField] private bool inRange = false;
    [SerializeField] private bool inCombat = false;
    [SerializeField] private bool waiting = false;
    [SerializeField] private string enemyTag;
    // Start is called before the first frame update
    void Start()
    {
        speed *= direction;
        //ContactFilter2D filter = new ContactFilter2D();
        //filter.layerMask =  1 << LayerMask.NameToLayer("Enemy");
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.gameObject.CompareTag(enemyTag)) inCombat = true;
        else if (other.gameObject.CompareTag(gameObject.tag)) waiting = true;
    }

    private void OnCollisionExit2D()
    {
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right , range, 1 << LayerMask.NameToLayer(enemyTag));
        if (hit.collider) inRange = true;
        else inRange = false;
        
        if (inCombat)
        {
            closeCombat(Time.deltaTime);
        }
        else if (inRange)
        {
            rangedCombat(Time.deltaTime);
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
    
    private void closeCombat(float deltaTime)
    {
        
    }
    
    private void rangedCombat(float deltaTime)
    {
        
    }
    
    private void idle(float deltaTime)
    {
        
    }
}