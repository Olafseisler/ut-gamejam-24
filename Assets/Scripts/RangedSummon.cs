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
    private string _friendlyTag;
    private float _vahe = 0.1f;
    private Vector3 _height_2;
    private Vector3 _width_2;
    // Start is called before the first frame update
    void Start()
    {
        speed *= direction;
        _vahe *= direction;
        _friendlyTag = gameObject.tag;
        _height_2 = new Vector3(0, gameObject.GetComponent<Collider2D>().bounds.size.y / 2);
        _width_2 = new Vector3(gameObject.GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, 0) * direction;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // { 
    //     if (other.gameObject.CompareTag(enemyTag)) inCombat = true;
    // }

    private void FixedUpdate()
    {
        // //Waiting
        // if (Physics2D.Raycast(transform.position + Vector3.up +
        //                       new Vector3(gameObject.GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, 0), transform.position * direction, _vahe, 1 << LayerMask.NameToLayer(_friendlyTag))) waiting = true;
        // else waiting = false;
        
        //InCOmbat
        if (Physics2D.Raycast(transform.position + _height_2 + _width_2, _width_2 * direction, _vahe, 1 << LayerMask.NameToLayer(enemyTag))) inCombat = true;
        else
        {
            inCombat = false;
            //Waiting
            if (Physics2D.Raycast(transform.position + _height_2 +
                                  _width_2, _width_2 * direction, _vahe, 1 << LayerMask.NameToLayer(_friendlyTag))) waiting = true;
            else waiting = false;
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        //InRange
        // if (Physics2D.Raycast(transform.position + _height_2 + _width_2, _width_2, range, 1 << LayerMask.NameToLayer(enemyTag)).collider) inRange = true;
        // else inRange = false;
        
        if (inCombat) closeCombat(Time.deltaTime);
        else
        {
            //InRange
            if (Physics2D.Raycast(transform.position + _height_2 + _width_2, _width_2, range, 1 << LayerMask.NameToLayer(enemyTag)).collider) inRange = true;
            else inRange = false;
            if(inRange){rangedCombat(Time.deltaTime);}
            else if (waiting)idle(Time.deltaTime);
            else move(Time.deltaTime);
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
        if (!waiting) move(deltaTime);
    }
    
    private void idle(float deltaTime)
    {
        
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawLine(transform.position + _height_2, transform.position + _height_2 + new Vector3(range, 0) * direction);
        Gizmos.DrawLine(transform.position + _height_2 +
                        _width_2, transform.position + _height_2 + new Vector3(_vahe, 0));
    }
}