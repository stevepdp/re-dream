using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkHandler : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.ShowCursorConfined();
    }

    public void OpenURL(string url) => Application.OpenURL(url);
}
