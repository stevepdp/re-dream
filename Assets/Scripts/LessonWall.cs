using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonWall : MonoBehaviour
{
    void OnEnable() => Crystal.OnCrystalCollected += HideWall;

    void OnDisable() => Crystal.OnCrystalCollected -= HideWall;

    void HideWall() => gameObject.SetActive(false);
}
