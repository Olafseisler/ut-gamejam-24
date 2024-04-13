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
    [SerializeField]private float vahe = 0.2f;

    private Animator _animator;
    private string _friendlyTag;
    private Vector3 _height2;
    private Vector3 _width2;
    // Start is called before the first frame update
    
    void Start()
    {
        speed *= direction;
        _animator = GetComponent<Animator>();
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


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        if (inCombat) combat(Time.deltaTime);
        else if (waiting)idle(Time.deltaTime);
        else move(Time.deltaTime);
    }

    private void move(float deltaTime)
    {
        transform.position += Vector3.right * (speed * deltaTime);
        _animator.SetBool("isMoving", true);        
    }
    
    private void combat(float deltaTime)
    {
        _animator.SetBool("isMoving", false);
    }
    
    private void idle(float deltaTime)
    {
        _animator.SetBool("isMoving", false);
    }
    
    public void SetupFriendly() {
        enemyTag = "Enemy";
        direction = 1;
        // Set own tag to friendly
        gameObject.tag = "Friendly";
        // Set layer
        gameObject.layer = LayerMask.NameToLayer("Friendly");
    }
    
    public void SetupEnemy() {
        enemyTag = "Friendly";
        direction = -1;
        // Flip the sprite
        GetComponent<SpriteRenderer>().flipX = true;
        // Set own tag to enemy
        gameObject.tag = "Enemy";
        // Set layer
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
}
