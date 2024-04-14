using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySpawner : MonoBehaviour
{

    [SerializeField] private GameObject spawnParticles;
    [SerializeField] private Animator priestAnimator;

    public void SpawnFriendly(GameObject unit)
    {
        // Show spawn particles
        Instantiate(spawnParticles, transform.position, Quaternion.identity);
        priestAnimator.SetTrigger("Trigger");
        
        var friendly = Instantiate(unit, transform.position, Quaternion.identity);
        friendly.gameObject.tag = "Friendly";
        
    }
}
