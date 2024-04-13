using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float baseHP = 100f;
    [SerializeField] private float currentHP;
    [SerializeField] private float baseMana = 100f;
    [SerializeField] private float currentMana;

    private void OnEnable()
    {
        Health.OnEnemyDeath += IncreaseMana;
        Base.OnEnemyEnterBase += DecreaseHP;
        Base.OnWinGame += WinGame;
    }
    
    private void OnDisable()
    {
        Health.OnEnemyDeath -= IncreaseMana;
        Base.OnEnemyEnterBase -= DecreaseHP;
        Base.OnWinGame -= WinGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = baseHP;
        currentMana = baseMana;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void DecreaseHP(float damage)
    {
        Debug.Log("HP decreased by " + damage);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("You lose!");
            Time.timeScale = 0;
            // TODO: Add lose screen or go back to main menu
        }
    }
    
    void DecreaseMana(float mana)
    {
        currentMana -= mana;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }
    
    void IncreaseMana(float mana)
    {
        Debug.Log("Mana increased by " + mana);
        currentMana += mana;
    }
    
    void IncreaseHP(float hp)
    {
        currentHP += hp;
        if (currentHP >= baseHP)
        {
            currentHP = baseHP;
        }
    }
    
    void WinGame()
    {
        Debug.Log("You win!");
        Time.timeScale = 0;
        // TODO: Add win screen
    }
}
