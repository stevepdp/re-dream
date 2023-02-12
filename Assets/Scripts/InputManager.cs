using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    public static event Action OnPlayerToggleHUD;

    void Awake() => EnforceSingleInstance();

    void Update() => GetKeyboardInputs();

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void GetKeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.H))
            OnPlayerToggleHUD?.Invoke();
    }
}
