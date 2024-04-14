using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private bool isMoving = true;

    [SerializeField] private Animator animator;
    
    private int moveDirection = 1;
    
    // Start is called before the first frame update
    void Start() // kui mäng algas siis tehakse kohe, ühekordne
    {
        moveDirection = gameObject.tag == "Enemy" ? -1 : 1;
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void FixedUpdate() //iga frame update käivitatakse
    {
        if (isMoving)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime * moveDirection, 0, 0);
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }
        
    }
    
    // // If colliding with friendly, stop moving
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Friendly"))
    //     {
    //         isMoving = false;
    //     }
    // }
    //
    // // If no longer colliding with friendly, start moving
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Friendly"))
    //     {
    //         isMoving = true;
    //     }
    // }
    
}
