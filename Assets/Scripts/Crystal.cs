using System;
using UnityEngine;

public class Crystal : Item
{
    public static event Action OnCrystalCollected;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            OnCrystalCollected?.Invoke();
    }
}
