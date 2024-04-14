using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int baseHP = 100;
    [SerializeField] private int currentHP;
    [SerializeField] private int baseMana = 100;
    [SerializeField] private int currentMana;
    [SerializeField] private UIController uiController;
    [SerializeField] private FriendlySpawner friendlySpawner;
    
    // Define instance
    public static GameController instance;
    float spawnCooldown = 0.5f;

    private void OnEnable()
    {
        Health.OnEnemyDeath += IncreaseMana;
        UIController.OnSummonSelected += TrySpawnFriendly;
        Base.OnEnemyEnterBase += DecreaseHP;
        Base.OnWinGame += WinGame;
    }
    
    private void OnDisable()
    {
        Health.OnEnemyDeath -= IncreaseMana;
        UIController.OnSummonSelected -= TrySpawnFriendly;
        Base.OnEnemyEnterBase -= DecreaseHP;
        Base.OnWinGame -= WinGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHP = baseHP;
        currentMana = baseMana;    
        uiController.setHealthText(currentHP);
        uiController.setManaText(currentMana);
        // Periodically regenerate mana
        InvokeRepeating("regenMana", 1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldown -= Time.deltaTime;
        
    }

    void regenMana()
    {
        if (currentMana < baseMana)
        {
            IncreaseMana(10);
        }
    }
    
    public void TrySpawnFriendly(GameObject unit, int cost)
    {
        if (currentMana < cost)
        {
            Debug.Log("Not enough mana!");
            uiController.showCannotSpawn("Not enough mana!");
        }
        else if (spawnCooldown > 0)
        {
            Debug.Log("Cooldown!");
            // uiController.showCannotSpawn("Cooldown " + spawnCooldown.ToString("0.0") + "s");
            
        }
        else
        {
            friendlySpawner.SpawnFriendly(unit);
            DecreaseMana(cost);
            spawnCooldown = 0.5f;
        }
    }

    void DecreaseHP(int damage)
    {
        Debug.Log("HP decreased by " + damage);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("You lose!");
            uiController.showGameEnd(false);
        }
        uiController.setHealthText(currentHP);
    }
    
    void DecreaseMana(int mana)
    {
        currentMana -= mana;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
        uiController.setManaText(currentMana);
    }
    
    void IncreaseMana(int mana)
    {
        Debug.Log("Mana increased by " + mana);
        currentMana += mana;
        uiController.setManaText(currentMana);
    }
    
    void IncreaseHP(int hp)
    {
        currentHP += hp;
        if (currentHP >= baseHP)
        {
            currentHP = baseHP;
        }
        uiController.setHealthText(currentHP);
    }
    
    public int GetMana()
    {
        return currentMana;
    }
    
    public int GetHP()
    {
        return currentHP;
    }
    
    void WinGame()
    {
        Debug.Log("You win!");
        Time.timeScale = 0;
        uiController.showGameEnd(true);
    }
}
