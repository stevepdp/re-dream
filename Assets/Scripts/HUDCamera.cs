using UnityEngine;

public class HUDCamera : MonoBehaviour
{
    Camera canvasCam;

    void Awake()
    {
        canvasCam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        HUDInput.OnToggleHUD += ToggleHUD;
    }

    void OnDisable()
    {
        HUDInput.OnToggleHUD -= ToggleHUD;
    }

    void ToggleHUD()
    {
        if (canvasCam != null)
            canvasCam.enabled = !canvasCam.enabled;
    }
}
