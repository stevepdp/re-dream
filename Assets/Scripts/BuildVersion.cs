using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildVersion : MonoBehaviour
{
    [SerializeField] TMP_Text buildVersionText;

    void Start()
    {
        if (buildVersionText != null)
        {
            buildVersionText.text = "v" + Application.version;
        }
    }
}
