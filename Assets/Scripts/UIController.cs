using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform summonPosition;
    [SerializeField] private GameObject summonSelectButton;
    [SerializeField] GameObject summonButtonParent;
    [SerializeField] List<GameObject> summons;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < summons.Count; i++)
        {
            var go = Instantiate(summonSelectButton, summonButtonParent.transform);
            go.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = summons[i].name;
            var image = go.GetComponentsInChildren<Image>()[1];
            image.sprite = summons[i].GetComponent<SpriteRenderer>().sprite;
            var index = i;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                var go1 = Instantiate(summons[index], summonPosition.position, Quaternion.identity);
                go1.tag = "Friendly";
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
