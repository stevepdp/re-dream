using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
