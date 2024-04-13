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
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.gameObject.CompareTag(enemyTag)) inCombat = true;
        else if (other.gameObject.CompareTag(gameObject.tag)) waiting = true;
    }
    
    // private void OnCollisionExit2D()
    // {
    //     waiting = false;
    // }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (!Physics2D.Raycast(
                transform.position + Vector3.up +
                new Vector3(gameObject.GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, 0),
                transform.right * direction,
                0.01f).collider) waiting = false;
        if (Physics2D.Raycast(transform.position + Vector3.up, transform.right * direction, range, 1 << LayerMask.NameToLayer(enemyTag)).collider) inRange = true;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(range, 0) * direction);
    }
}