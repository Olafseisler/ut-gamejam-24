using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnableObject;
    [SerializeField] private float _spawnCooldown = 1f;

    private float _currentCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentCooldown = _currentCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown <= 0)
        {
            // Spawn object and reset cooldown
            GameObject go = Instantiate(spawnableObject, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
            if (go == null)
            {
                return;
            }
            Rigidbody2D go_rb = go.GetComponent<Rigidbody2D>();
            go_rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            _currentCooldown = _spawnCooldown;
        }
    }
}
