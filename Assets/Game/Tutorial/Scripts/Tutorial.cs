using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    [SerializeField] private CustomerData customerDataTutorial;
    public static Action<float> tutorialAddScore;
    [SerializeField] private PlayerInputs playerInputs;
    private Timer timer;
    private TutorialArrow tutorialArrow;
    [HideInInspector] public int tutorialIndex;
    [SerializeField] private GameObject drunken;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private Text tutorialTextBox;
    [SerializeField] private string[] tutorialTexts;
    [SerializeField] private string tutorialWrongOrder;
    [SerializeField] private int[] followingIndexes;
    [SerializeField] private int[] arrowIndexes;
    [SerializeField] private GameObject[] arrowTargets;
    private int arrowIndex;
    [SerializeField] private KeyCode showHintAgainKey;
    private bool waitForClickToContinue;
    private bool openPrevious;
    private bool arrowActive;
    private bool specialCase;
    private bool drunkenActivate;
    [SerializeField] private int doubleIndex;

    private void Awake()
    {
        instance = this;
        playerInputs = FindObjectOfType<PlayerInputs>();
        timer = FindObjectOfType<Timer>();
        tutorialArrow = this.gameObject.GetComponentInChildren<TutorialArrow>();
    }

    private void Start()
    {
        customerDataTutorial.activeOrders = 0;
        tutorialArrow.gameObject.SetActive(false);
        tutorialIndex = 0;
        arrowIndex = 0;
        arrowActive = false;
        openPrevious = false;
        waitForClickToContinue = false;
        ShowTutorialScreen();
    }

    private void Update()
    {
        if (waitForClickToContinue && (Input.GetButtonDown("Fire1") == true || Input.GetButtonDown("Fire2") == true))
        {
            if (!specialCase) ClickToContinue();
            else ClickToContinueSpecial();
        }
        if (Input.GetKeyDown(showHintAgainKey)) ShowTutorialScreen(tutorialIndex - 1);
    }

    public void ShowTutorialScreen()
    {
        specialCase = false;
        playerInputs.TutorialStop();
        playerInputs.enabled = false;
        tutorialScreen.SetActive(true);
        tutorialTextBox.text = tutorialTexts[tutorialIndex];
        waitForClickToContinue = true;
    }
    public void ShowTutorialScreen(int index)
    {
        specialCase = false;
        openPrevious = true;
        playerInputs.TutorialStop();
        playerInputs.enabled = false;
        tutorialScreen.SetActive(true);
        tutorialTextBox.text = tutorialTexts[index];
        waitForClickToContinue = true;
    }

    public void CheckIndex()
    {
        if (tutorialIndex == doubleIndex)
        {
            tutorialIndex++;
        }
        ShowTutorialScreen();
    }

    private void ClickToContinue()
    {
        playerInputs.enabled = true;
        waitForClickToContinue = false;
        bool consecutive = CheckForFollowingIndexes();
        if (CheckForArrowIndexes()) ArrowActivate();
        else ArrowDeactivate();
        tutorialScreen.SetActive(false);
        tutorialTextBox.text = string.Empty;
        if (!openPrevious) tutorialIndex++;
        else openPrevious = false;
        if (consecutive) ShowTutorialScreen();
        if (drunkenActivate)
        {
            drunken.SetActive(true);
            drunkenActivate = false;
        }
        if (tutorialIndex == tutorialTexts.Length) EndTutorial();
    }

    private void EndTutorial()
    {
        tutorialAddScore(10000);
        timer.currentTime = 0;
        timer.TimerFinish();
    }

    private bool CheckForFollowingIndexes()
    {
        bool isAFollowingIndex = false;
        for (int i = 0; i < followingIndexes.Length; i++)
        {
            if (tutorialIndex == followingIndexes[i])
            {
                isAFollowingIndex = true;
                break;
            }
        }
        return isAFollowingIndex;
    }

    private bool CheckForArrowIndexes()
    {
        bool isAnArrowIndex = false;
        if (arrowIndex < arrowIndexes.Length)
        {
            for (int i = 0; i < arrowIndexes.Length; i++)
            {
                if (tutorialIndex == arrowIndexes[i])
                {
                    isAnArrowIndex = true;
                    break;
                }
            }
        }
        return isAnArrowIndex;
    }
    private void ArrowActivate()
    {
        tutorialArrow.gameObject.SetActive(true);
        tutorialArrow.target = arrowTargets[arrowIndex];
        arrowActive = true;
        arrowIndex++;
    }
    public void ArrowDeactivate()
    {
        if (arrowActive)
        {
            arrowTargets[arrowIndex - 1].SetActive(false);
            tutorialArrow.gameObject.SetActive(false);
            arrowActive = false;
        }
    }

    public void WrongOrder()
    {
        playerInputs.TutorialStop();
        playerInputs.enabled = false;
        tutorialScreen.SetActive(true);
        tutorialTextBox.text = tutorialWrongOrder;
        waitForClickToContinue = true;
        specialCase = true;
    }

    private void ClickToContinueSpecial()
    {
        playerInputs.enabled = true;
        waitForClickToContinue = false;
        tutorialScreen.SetActive(false);
        tutorialTextBox.text = string.Empty;
        specialCase = false;
    }

    public void FinalTutorialSetup()
    {
        drunkenActivate = true;
        ShowTutorialScreen();
    }
}
