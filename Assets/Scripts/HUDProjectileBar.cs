using UnityEngine;
using UnityEngine.UI;

public class HUDProjectileBar : MonoBehaviour
{
    [SerializeField] Image hudBurnoutFillImage;
    [SerializeField] Color hudBurnoutColourLocked;
    [SerializeField] Color hudBurnoutColourNormal;

    PlayerProjectile playerProjectile;
    Slider hudBurnoutSlider;

    void Awake()
    {
        hudBurnoutSlider = GetComponent<Slider>();
        playerProjectile = FindObjectOfType<PlayerProjectile>();
    }

    void Start()
    {
        if (hudBurnoutSlider != null && playerProjectile != null)
        {
            hudBurnoutFillImage = hudBurnoutSlider.fillRect.GetComponent<Image>();
            hudBurnoutSlider.minValue = playerProjectile.FireCooldownMin;
            hudBurnoutSlider.maxValue = playerProjectile.FireCooldownMax;
        }
    }

    void Update()
    {
        FillBurnoutBar();
    }

    void OnEnable()
    {
        Challenge.OnEnableProjectile += SetProjectileReady;
        PlayerProjectile.OnPlayerProjectileReady += SetProjectileReady;
    }

    void OnDisable()
    {
        Challenge.OnEnableProjectile -= SetProjectileReady;
        PlayerProjectile.OnPlayerProjectileReady -= SetProjectileReady;
    }

    void FillBurnoutBar()
    {
        if (hudBurnoutFillImage != null && hudBurnoutSlider != null && playerProjectile != null)
        {
            float cooldownTime = playerProjectile.FireCooldownTime;
            hudBurnoutFillImage.enabled = true;
            hudBurnoutSlider.value = cooldownTime;
            if (cooldownTime == 0) hudBurnoutFillImage.enabled = false;
        }
    }

    void SetProjectileReady()
    {
        if (hudBurnoutFillImage != null && playerProjectile?.FireCooldownTime == 0)
            hudBurnoutFillImage.enabled = false;
    }
}
