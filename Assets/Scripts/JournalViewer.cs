using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class JournalViewer : MonoBehaviour
{
    public static event Action OnJournalOpened;
    public static event Action OnJournalClosed;

    [SerializeField] GameObject journalCanvas;
    [SerializeField] GameObject journalPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] TMP_Text pageContentText;
    [SerializeField] TMP_Text pageNumberText;

    [SerializeField] JournalController journalController;
    [SerializeField] JournalPage pageScriptableObject;

    PlayerControls playerControls;
    InputAction journalOpen;
    InputAction journalPageLeft;
    InputAction journalPageRight;
    InputAction cancel;
    
    int firstPageIndex = 0;
    int currentPageNumber = 0;
    string currentPageContent;
    string pageLockedStr = "Locked<br><br>Hint: Journal pages will reveal themselves with each puzzle piece that you find.";

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        RenderPage();
    }

    void OnEnable()
    {
        GameManager.OnGameLostFocus += DisableJournalPanel;

        journalOpen = playerControls.Player.Journal;
        journalOpen?.Enable();
        journalOpen.performed += ShowJournal;

        journalPageLeft = playerControls.Player.JournalPageLeft;
        journalPageLeft?.Enable();
        journalPageLeft.performed += PageLeft;

        journalPageRight = playerControls.Player.JournalPageRight;
        journalPageRight?.Enable();
        journalPageRight.performed += PageRight;
    }

    void OnDisable()
    {
        GameManager.OnGameLostFocus -= DisableJournalPanel;

        journalOpen.Disable();
        journalPageLeft.Disable();
        journalPageRight.Disable();
    }

    void GetPageScriptableObject()
    {
        if (pageScriptableObject != null)
        {
            currentPageNumber = pageScriptableObject.pageNumber;
            currentPageContent = pageScriptableObject.pageContent;
        }       
    }

    void DisableJournalPanel()
    {
        if (journalPanel != null && pausePanel != null)
        {
            if (journalPanel.activeInHierarchy && !pausePanel.activeInHierarchy)
            {
                OnJournalClosed?.Invoke();
                Time.timeScale = 1;
                journalPanel?.SetActive(false);
            }
        }
    }

    void EnableJournalPanel()
    {
        if (journalPanel != null && pausePanel != null)
        {
            if (!journalPanel.activeInHierarchy && !pausePanel.activeInHierarchy)
            {
                OnJournalOpened?.Invoke();
                Time.timeScale = 0;
                journalPanel?.SetActive(true);
            }
        }
    }

    void ShowJournal(InputAction.CallbackContext context)
    {
        if (!journalPanel.activeInHierarchy)
            EnableJournalPanel();
        else
            DisableJournalPanel();
    }

    void PageRight(InputAction.CallbackContext context)
    {
        if (currentPageNumber >= 0 && currentPageNumber < journalController.pageCount)
            currentPageNumber++;

        RenderPage();
    }

    void PageLeft(InputAction.CallbackContext context)
    {
        if (currentPageNumber > 0 && currentPageNumber <= journalController.pageCount)
            currentPageNumber--;

        RenderPage();
    }

    void RenderPage()
    {
        if (pageNumberText != null)
            pageNumberText.text = currentPageNumber.ToString();

        if (journalController != null && pageContentText != null)
        {
            object journalPageScriptableObject = journalController.CanViewPage(currentPageNumber);

            if (journalPageScriptableObject is JournalPage)
            {
                JournalPage thePage = (JournalPage) journalPageScriptableObject;
                pageNumberText.text = thePage.pageNumber.ToString();
                pageContentText.text = thePage.pageContent;
            }
            else
            {
                pageNumberText.text = currentPageNumber.ToString();
                pageContentText.text = pageLockedStr;
            }
        }
    }
}
