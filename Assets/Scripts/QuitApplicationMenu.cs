using System.Collections;
using System.Collections.Generic;
#if UNITY_STANDALONE
using TMPro;
#endif
using UnityEngine;
using UnityEngine.UI;

public class QuitApplicationMenu : QuitApplication
{
#if UNITY_EDITOR || UNITY_WEBGL
    [SerializeField] Button buttonStart;
#endif
#if UNITY_STANDALONE
    [SerializeField] private TMP_Text exitButtonText;
#endif

    public void Awake()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        if (buttonStart != null) buttonStart.transform.position = transform.position;
        gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
        exitButtonText.text = "Exit to Windows";
#elif UNITY_STANDALONE_OSX
        exitButtonText.text = "Exit to macOS";
#elif UNITY_STANDALONE_LINUX
        exitButtonText.text = "Exit to Linux";
#endif
    }
}
