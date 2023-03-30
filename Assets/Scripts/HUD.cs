using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] PlayerProjectile playerProjectile;
    [SerializeField] TMP_Text hudCrystalCountText;
    [SerializeField] TMP_Text hudPuzzlePieceCountText;
    [SerializeField] MeshRenderer hudJournalNotifierMesh;
    [SerializeField] TMP_Text hudProjectileStatusText;
    [SerializeField] Slider hudBurnoutSlider;
    [SerializeField] Image hudBurnoutFillImage;
    [SerializeField] Color hudBurnoutColourLocked;
    [SerializeField] Color hudBurnoutColourNormal;
    PlayerIdle player;

    void Awake()
    {
        player = FindObjectOfType<PlayerIdle>();
        playerProjectile = FindObjectOfType<PlayerProjectile>();
    }

    void Start()
    {
        SetCrystalCountText();
        SetPuzzlePieceCountText();

        hudBurnoutFillImage = hudBurnoutSlider.fillRect.GetComponent<Image>();
        hudBurnoutSlider.minValue = (float) playerProjectile?.FireCooldownMin;
        hudBurnoutSlider.maxValue = (float) playerProjectile?.FireCooldownMax;
    }

    void Update()
    {
        SetProjectileText();
    }

    void OnEnable()
    {
        Challenge.OnDisableProjectile += SetProjectileBurnedOut;
        Challenge.OnEnableProjectile += SetProjectileReady;
        GameManager.OnRoomCrystalsCounted += SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated += SetCrystalCountText;
        GameManager.OnRoomPuzzlePiecesCounted += SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated += SetPuzzlePieceCountText;
        JournalViewer.OnJournalOpened += HideJournalNotification;
        PlayerProjectile.OnPlayerProjectileBurnout += SetProjectileBurnedOut;
        PlayerProjectile.OnPlayerProjectileReady += SetProjectileReady;
        PuzzlePiece.OnPuzzlePieceCollected += ShowJournalNotification;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected += ShowJournalNotification;
    }

    void OnDisable()
    {
        Challenge.OnDisableProjectile -= SetProjectileBurnedOut;
        Challenge.OnEnableProjectile -= SetProjectileReady;
        GameManager.OnRoomCrystalsCounted -= SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated -= SetCrystalCountText;
        GameManager.OnRoomPuzzlePiecesCounted -= SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated -= SetPuzzlePieceCountText;
        JournalViewer.OnJournalOpened -= HideJournalNotification;
        PlayerProjectile.OnPlayerProjectileBurnout -= SetProjectileBurnedOut;
        PlayerProjectile.OnPlayerProjectileReady -= SetProjectileReady;
        PuzzlePiece.OnPuzzlePieceCollected -= ShowJournalNotification;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected -= ShowJournalNotification;
    }

    void HideJournalNotification()
    {
        if (hudJournalNotifierMesh != null)
            hudJournalNotifierMesh.enabled = false;
    }

    void SetCrystalCountText()
    {
        if (hudCrystalCountText != null)
            hudCrystalCountText.text = $"<size=150%>{GameManager.instance?.PlayerCrystalsCount}</size>/<size=75%>{GameManager.instance?.RoomCrystalsTotal}</size>";
    }

    void SetProjectileBurnedOut()
    {
        hudProjectileStatusText.text = "X";
    }

    void SetProjectileReady()
    {
        hudProjectileStatusText.text = "";
        SetProjectileText();
        if ((float) playerProjectile?.FireCooldownTime == 0)
            hudBurnoutFillImage.enabled = false;
    }

    void SetProjectileText()
    {
        if (player != null)
        {
            float cooldownTime = (float) playerProjectile?.FireCooldownTime;
            hudBurnoutFillImage.enabled = true;
            hudBurnoutSlider.value = cooldownTime;
            if (cooldownTime == 0) hudBurnoutFillImage.enabled = false;
        }
    }

    void SetPuzzlePieceCountText()
    {
        if (hudPuzzlePieceCountText != null)
            hudPuzzlePieceCountText.text = $"<size=150%>{GameManager.instance?.PlayerPuzzlePiecesCount}</size>/<size=75%>{GameManager.instance?.RoomPuzzlePiecesTotal}*</size>";
    }

    void ShowJournalNotification(Object puzzlePiece)
    {
        if (hudJournalNotifierMesh != null)
            hudJournalNotifierMesh.enabled = true;
    }
}
