using UnityEngine;

<<<<<<< Updated upstream:Assets/Scripts/EnemySpawner.cs
public class EnemySpawner : MonoBehaviour
=======
public class Spawner : MonoBehaviour
>>>>>>> Stashed changes:Assets/Scripts/spawner.cs
{
    // Start is called before the first frame update
    [SerializeField] private float spawnInterwal = 1f;
    [SerializeField] private GameObject tegelane;
    [SerializeField] bool isEnemySpawner = false;
    
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
            var go = Instantiate(tegelane, transform.position , Quaternion.identity);
            // Debug.Log("Spawned minion");
            if (isEnemySpawner)
            {
                // Set enemy tag
                go.gameObject.tag = "Enemy";
                // Flip sprite
                go.GetComponent<SpriteRenderer>().flipX = true;
                
            }
            else
            {
                // Set friendly tag
                go.gameObject.tag = "Friendly";
            }

            currentInterwal = spawnInterwal;
        }
    }
}
