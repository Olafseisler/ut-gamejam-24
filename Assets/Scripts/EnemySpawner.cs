using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour

{
    // Start is called before the first frame update
    [SerializeField] private float spawnInterwal = 1f;
    [SerializeField] bool isEnemySpawner = false;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject spawnParticles;
    
    private float currentInterwal;
    
    void Start()
    {
        currentInterwal = spawnInterwal;
    }

    // Update is called once per frame
    void Update()
    {
        currentInterwal -= Time.deltaTime;

        if (currentInterwal <= 0)
        {
            // Show spawn particles
            Instantiate(spawnParticles, transform.position, Quaternion.identity);
            
            var enemy = getRandomEnemy();
            enemy.gameObject.tag = isEnemySpawner ? "Enemy" : "Player";
            var go = Instantiate(enemy, transform.position, Quaternion.identity);
            
            // Get the sprite and flip it
            var sprite = go.GetComponent<SpriteRenderer>();
            sprite.flipX = isEnemySpawner;

            currentInterwal = spawnInterwal;
        }
    }
    
    private GameObject getRandomEnemy()
    {
        var randomIndex = Random.Range(0, enemies.Count);
        return enemies[randomIndex];
    }
}
