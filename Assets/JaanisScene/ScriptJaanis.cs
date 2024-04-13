using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeeleSummonJaanis : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float dmg;
    [SerializeField] private bool inCombat = false;
    [SerializeField] private bool waiting = false;
    [SerializeField] private string enemyTag;
    [SerializeField] private AudioSource combatAudio;
    

    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        combatAudio = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            inCombat = true;
            waiting = true;
            Debug.Log("Waiting" + waiting);
        }
        else
        {
            
        }
    }

    private void OnCollisionExit2D()
    {
        inCombat = waiting = false;
    }

    // Update is called once per frame
    void Update() {
        
        if (inCombat){ combat(); }

        if (!waiting) { move(Time.deltaTime); }
        else { idle(); }
    }

    private void move(float deltaTime) {
        transform.position += Vector3.right * (speed * deltaTime);
    }
    
    private void combat()
    {
        Debug.Log("Combat");
    }
    
    private void idle()
    {
        Debug.Log("Idle");
        
    }
}
