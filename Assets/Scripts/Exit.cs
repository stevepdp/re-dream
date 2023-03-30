using System;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public static event Action OnPlayerEnteredExit;
    public static event Action OnPlayerLeftExit;

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnteredExit?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnPlayerLeftExit?.Invoke();
    }
}
