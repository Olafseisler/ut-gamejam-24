using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liikumine : MonoBehaviour
{
    [SerializeField] private float movespeed = 2.0f;

    public bool isMoving = true;
    
    // Start is called before the first frame update
    void Start() // kui mäng algas siis tehakse kohe, ühekordne
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() //iga frame update kävtatakse
    {
        if (isMoving)
        {
            transform.position += new Vector3(movespeed*Time.deltaTime, 0);
        }
        
    }
}
