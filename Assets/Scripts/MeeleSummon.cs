using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MeeleSummon : MonoBehaviour
{
    [SerializeField] protected float health; 
    [SerializeField] protected float meleeDamage;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int direction; 
    [SerializeField] protected bool inCombat = false;
    [SerializeField] protected bool waiting = false;
    [SerializeField] protected string enemyTag;
    [SerializeField] protected float vahe = 0.2f;

    protected Animator _animator;
    protected string _friendlyTag;
    protected Vector3 _height2;
    protected Vector3 _width2;

    private RaycastHit2D _enemy;
    // Start is called before the first frame update
    protected float Health;

    void Start()
    {
        moveSpeed *= direction;
        _animator = GetComponent<Animator>();
        _friendlyTag = gameObject.tag;
        //_height2 = new Vector3(0, gameObject.GetComponent<Collider2D>().bounds.size.y / 2);
        _height2 = new Vector3(0, 0.1f);
        _width2 = new Vector3(gameObject.GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, 0) * direction;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void FixedUpdate()
    {
        //InCombat
        _enemy = Physics2D.Raycast(transform.position + _height2 + _width2, _width2, vahe, 1 << LayerMask.NameToLayer(enemyTag));
        if (_enemy)inCombat = true;
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
        if (inCombat) combat(Time.deltaTime, _enemy);
        else if (waiting)idle(Time.deltaTime);
        else move(Time.deltaTime);
    }

    protected void move(float deltaTime)
    {
        transform.position += Vector3.right * (moveSpeed * deltaTime);
        _animator.SetBool("isMoving", true);        
    }
    
    private void combat(float deltaTime, RaycastHit2D enemyHit2D)
    {
        if (enemyHit2D.collider == null) return;
        var enemy = enemyHit2D.collider.gameObject;
        _animator.SetBool("isMoving", false);
        enemy.GetComponent<MeeleSummon>().Health = enemy.GetComponent<MeeleSummon>().Health - meleeDamage;
    }
    
    protected void idle(float deltaTime)
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
