using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Elud : MonoBehaviour
{

    [SerializeField] private float elud = 10f;
    [SerializeField] private GameObject tegelane;
    private float currentElud;
    
    // Start is called before the first frame update
    void Start()
    {
        currentElud = elud;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentElud <= 0)
        {
            Destroy(tegelane);
        }
    }
}
