using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime) * Vector2.right);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Friendly")) ||
            (col.gameObject.CompareTag("Friendly") && gameObject.CompareTag("Enemy")))
        {
            col.gameObject.GetComponent<MeeleSummon>().TakeDamage(damage);
        }
    }
}
