using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 0.25f;

    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
