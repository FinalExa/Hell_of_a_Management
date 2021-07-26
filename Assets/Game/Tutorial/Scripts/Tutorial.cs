using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Action<float> tutorialAddScore;
    private PlayerInputs playerInputs;
    private Timer timer;
    private int tutorialIndex;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private Text tutorialTextBox;
    [SerializeField] private string[] tutorialTexts;
    private bool waitForClickToContinue;

    private void Awake()
    {
        playerInputs = FindObjectOfType<PlayerInputs>();
        timer = FindObjectOfType<Timer>();
        SpecificTrigger.tutorialAdvance += ShowTutorialScreen;
        CustomerHighlightable.continueTutorial += ShowTutorialScreen;
    }

    private void Start()
    {
        tutorialIndex = 0;
        waitForClickToContinue = false;
        ShowTutorialScreen();
    }

    private void Update()
    {
        if (waitForClickToContinue && (Input.GetButtonDown("Fire1") == true || Input.GetButtonDown("Fire2") == true)) ClickToContinue();
    }

    public void ShowTutorialScreen()
    {
        playerInputs.TutorialStop();
        playerInputs.enabled = false;
        tutorialScreen.SetActive(true);
        tutorialTextBox.text = tutorialTexts[tutorialIndex];
        waitForClickToContinue = true;
    }

    private void ClickToContinue()
    {
        tutorialScreen.SetActive(false);
        tutorialTextBox.text = string.Empty;
        waitForClickToContinue = false;
        playerInputs.enabled = true;
        tutorialIndex++;
        if (tutorialIndex == tutorialTexts.Length) EndTutorial();
    }

    private void EndTutorial()
    {
        tutorialAddScore(10000);
        timer.currentTime = 0;
        timer.TimerFinish();
    }
}
