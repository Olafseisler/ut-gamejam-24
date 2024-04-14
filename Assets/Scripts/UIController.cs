using System;
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
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private TMPro.TextMeshProUGUI manaText;
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private GameObject cannotSpawnText;

    public static event Action<GameObject, int> OnSummonSelected;

    private float bannerTimer = 0f;
    private bool bannerActive = false;
    
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
            var unitCost = summons[i].GetComponent<ManaCost>().manaCost;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnSummonSelected?.Invoke(summons[index], unitCost);
            });
        }
    }
    
    public void setManaText(int mana)
    {
        manaText.text = "Mana: " + mana;
    }
    
    public void setHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }
    
    public void showCannotSpawn(string message)
    {
        if (bannerActive) return;
        cannotSpawnText.SetActive(true);
        cannotSpawnText.GetComponent<TMPro.TextMeshProUGUI>().text = message;
        bannerActive = true;
        bannerTimer = 1.5f;
    }

    
    public void showGameEnd(bool win)
    {
        Time.timeScale = 0;
        gameEndPanel.SetActive(true);
        // Get the EndText object
        var endText = gameEndPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        // Set the text based on win or lose
        endText.text = win ? "You win!" : "You lose!";
    }
    
    public void goToMainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (bannerActive)
        {
            bannerTimer -= Time.deltaTime;
            if (bannerTimer <= 0)
            {
                cannotSpawnText.SetActive(false);
                bannerActive = false;
            }
        }
        
        
    }
}
