using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Item
{
    public static event Action OnCrystalCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCrystalCollected?.Invoke();
        }
    }
}
