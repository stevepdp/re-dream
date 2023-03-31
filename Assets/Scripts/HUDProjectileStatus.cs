using TMPro;
using UnityEngine;

public class HUDProjectileStatus : MonoBehaviour
{
    TMP_Text hudProjectileStatusText;

    void Awake()
    {
        hudProjectileStatusText = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        Challenge.OnDisableProjectile += SetProjectileBurnedOut;
        Challenge.OnEnableProjectile += SetProjectileReady;
        PlayerProjectile.OnPlayerProjectileBurnout += SetProjectileBurnedOut;
        PlayerProjectile.OnPlayerProjectileReady += SetProjectileReady;
    }

    void OnDisable()
    {
        Challenge.OnDisableProjectile -= SetProjectileBurnedOut;
        Challenge.OnEnableProjectile -= SetProjectileReady;
        PlayerProjectile.OnPlayerProjectileBurnout -= SetProjectileBurnedOut;
        PlayerProjectile.OnPlayerProjectileReady -= SetProjectileReady;
    }

    void SetProjectileBurnedOut()
    {
        if (hudProjectileStatusText != null)
            hudProjectileStatusText.text = "X";

    }

    void SetProjectileReady()
    {
        if (hudProjectileStatusText != null)
            hudProjectileStatusText.text = "";
    }
}
