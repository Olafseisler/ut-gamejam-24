using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class RangedSummon : MeeleSummon
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float range;
    [SerializeField] private bool inRange = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        moveSpeed *= direction;
        _friendlyTag = gameObject.tag;
        //_height2 = new Vector3(0, gameObject.GetComponent<Collider2D>().bounds.size.y / 2);
        _height2 = new Vector3(0, 0.1f);
        _width2 = new Vector3(gameObject.GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, 0) * direction;
    }

    private void FixedUpdate()
    {
        //InCombat
        if (Physics2D.Raycast(transform.position + _height2 + _width2, _width2, vahe, 1 << LayerMask.NameToLayer(enemyTag))) inCombat = true;
        else
        {
            inCombat = false;
            //Waiting
            if (Physics2D.Raycast(transform.position + _height2 +
                                  _width2, _width2, vahe, 1 << LayerMask.NameToLayer(_friendlyTag))) waiting = true;
            else waiting = false;
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (inCombat)
        {
            closeCombat(Time.deltaTime);
        }
        else
        {
            //InRange
            if (Physics2D.Raycast(transform.position + _height2 + _width2, _width2, range, 1 << LayerMask.NameToLayer(enemyTag)).collider) inRange = true;
            else inRange = false;
            if(inRange){rangedCombat(Time.deltaTime);}
            else if (waiting) idle(Time.deltaTime);
            else move(Time.deltaTime);
        }
        
    }

    private void closeCombat(float deltaTime)
    {
        _animator.SetBool("isMoving", false);
    }
    
    private void rangedCombat(float deltaTime)
    {
        if (!waiting) move(deltaTime);
        
        // Check if the no friendlies in the way
        if (Physics2D.Raycast(transform.position + _height2 + _width2, _width2, range, 1 << LayerMask.NameToLayer(_friendlyTag))) return;
        
        // Launch projectile
        var go = Instantiate(projectile, transform.position, Quaternion.identity);
        var projectileScript = go.GetComponent<Projectile>();
        projectileScript.direction = direction;
        // Set the projectile to friendly
        go.tag = _friendlyTag;
    }

    private void OnDrawGizmos()
    {
        
    }
}