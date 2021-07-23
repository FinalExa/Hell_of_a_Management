using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;
    private Image star1Image;
    private Image star2Image;
    private Image star3Image;
    [SerializeField] Image fillImg;
    [SerializeField] GameObject scoreNumber;
    [SerializeField] GameObject scoreBar;
    private bool isActive = false;
    private bool isPaused = false;

    #region Unity Callbacks
    public void Awake()
    {
        Init();
    }
    void OnDisable()
    {
        Score.OnProgressChanged -= UpdateGraphics;
    }

    void Update()
    {
        Activate();
    }
    #endregion

    void Init()
    {
        /* RESETS GFX */
        fillImg.fillAmount = 0;
        scoreText.text = $"Score: {0}";
        star1Image = star1.GetComponent<Image>();
        star2Image = star2.GetComponent<Image>();
        star3Image = star3.GetComponent<Image>();
        Score.OnProgressChanged += UpdateGraphics;
    }

    void Activate()
    {
        if (LevelManager.levelManagerInstance != null)
        {
            if (!LevelManager.levelManagerInstance.isLoading && LevelManager.levelManagerInstance.currentIndex == 1 && isActive == false)
            {
                scoreBar.SetActive(true);
                scoreNumber.SetActive(true);
                isActive = true;
            }
            if (isPaused == false)
            {
                scoreBar.SetActive(false);
                scoreNumber.SetActive(false);
                isPaused = true;
            }
            if (isPaused == true)
            {
                scoreBar.SetActive(true);
                scoreNumber.SetActive(true);
                isPaused = false;
            }
        }
    }

    public void UpdateGraphics(Score sender, float score)
    {
        scoreText.text = "Score: " + score;
        fillImg.fillAmount = score / (float)levelData.GetLevel(0).thirdStarScore;

        if (score < levelData.GetLevel(0).firstStarScore)
        {
            return;
        }

        else if (score < levelData.GetLevel(0).secondStarScore && score >= levelData.GetLevel(0).firstStarScore)
        {
            if (star1Image.color != Color.white)
            {
                LightStar(ref star1Image, Color.white);
                star1.GetComponent<Animator>().SetTrigger("StarSpin");
            }

            return;
        }

        else if (score < levelData.GetLevel(0).thirdStarScore && score >= levelData.GetLevel(0).secondStarScore)
        {
            if (star2Image.color != Color.white)
            {
                LightStar(ref star2Image, Color.white);
                star2.GetComponent<Animator>().SetTrigger("StarSpin");
            }

            return;
        }

        else if (score >= levelData.GetLevel(0).thirdStarScore)
        {
            if (star3Image.color != Color.white)
            {
                LightStar(ref star3Image, Color.white);
                star3.GetComponent<Animator>().SetTrigger("StarSpin");
            }

            return;
        }
    }

    public void LightStar(ref Image star, Color color) => star.color = color;
}
