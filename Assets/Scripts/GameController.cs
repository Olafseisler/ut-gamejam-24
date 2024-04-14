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
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldown -= Time.deltaTime;
    }
    
    public void TrySpawnFriendly(GameObject unit, int cost)
    {
        if (currentMana < cost)
        {
            Debug.Log("Not enough mana!");
        }
        if (spawnCooldown > 0)
        {
            Debug.Log("Cooldown!");
        }
        else
        {
            friendlySpawner.SpawnFriendly(unit);
            DecreaseMana(cost);
            spawnCooldown = 1f;
        }
    }

    void DecreaseHP(int damage)
    {
        Debug.Log("HP decreased by " + damage);
        currentHP -= damage;
        uiController.setHealthText(currentHP);
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("You lose!");
            uiController.showGameEnd(false);
        }
    }
    
    void DecreaseMana(int mana)
    {
        currentMana -= mana;
        uiController.setManaText(currentMana);
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }
    
    void IncreaseMana(int mana)
    {
        Debug.Log("Mana increased by " + mana);
        uiController.setManaText(currentMana);
        currentMana += mana;
    }
    
    void IncreaseHP(int hp)
    {
        currentHP += hp;
        uiController.setHealthText(currentHP);
        if (currentHP >= baseHP)
        {
            currentHP = baseHP;
        }
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
        // TODO: Add win screen
    }
}
