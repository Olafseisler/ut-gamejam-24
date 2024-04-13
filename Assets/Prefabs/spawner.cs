using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float spawnInterwal = 1f;
    [SerializeField] private GameObject tegelane;
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
            Instantiate(tegelane, transform.position , Quaternion.identity);
            currentInterwal = spawnInterwal;
        }
    }
}
