using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Animator animator;
    private bool fullscreen;
    public AudioMixer audioMixer;
    public Text fullScreenText;

    private const string RESOLUTION_PREF_KEY = "resolution";
    private const string QUALITY_PREF_KEY = "quality";

    [SerializeField]
    private Text resolutionText;

    private Resolution[] resolutions;

    private int currentResolutionIndex = 0;

    [SerializeField]
    private Text qualityText;

    public string[] qualities;

    private int currentQualityIndex = 0;


    public void Start()
    {
        animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        resolutions = Screen.resolutions;
        qualities = QualitySettings.names;

        currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, 0);
        currentQualityIndex = PlayerPrefs.GetInt(QUALITY_PREF_KEY, 0);

        SetQualityText(qualities[currentQualityIndex]);
        SetResolutionText(resolutions[currentResolutionIndex]);
    }

    public void SetQualityText (string qualities)
    {
        qualityText.text = qualities;
    }

    public void SetNextQuality()
    {
        currentQualityIndex = QualityGetNextWrappedIndex(qualities, currentQualityIndex);
        SetQualityText(qualities[currentQualityIndex]);
        SetAndApplyQuality(currentQualityIndex);
    }

    public void SetPreviosQuality()
    {
        currentQualityIndex = QualityGetPreviosWrappedIndex(qualities, currentQualityIndex);
        SetQualityText(qualities[currentQualityIndex]);
        SetAndApplyQuality(currentQualityIndex);
    }

    private void SetAndApplyQuality(int newQualityIndex)
    {
        currentQualityIndex = newQualityIndex;
        ApplyCurrentQuality();
    }

    private void ApplyCurrentQuality()
    {
        ApplyQuality(qualities[currentQualityIndex]);
    }

    private void ApplyQuality(string qualities)
    {
        SetQualityText(qualities);

        if(qualities == "Low")
            QualitySettings.SetQualityLevel(0, true);
        else if(qualities == "Medium")
            QualitySettings.SetQualityLevel(1, true);
        else if(qualities == "High")
            QualitySettings.SetQualityLevel(2, true);

        Debug.Log(QualitySettings.GetQualityLevel());
    }

    private int QualityGetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        return (currentIndex + 1) % collection.Count;
    }

    private int QualityGetPreviosWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currentIndex - 1) < 0) return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;
    }

    private void SetResolutionText(Resolution resolution)
    {
        resolutionText.text = resolution.width + "x" + resolution.height;
    }

    public void SetNextResolution()
    {
        currentResolutionIndex = ResolutionGetNextWrappedIndex(resolutions, currentResolutionIndex);
        SetResolutionText(resolutions[currentResolutionIndex]);
        SetAndApplyResolution(currentResolutionIndex);
    }

    public void SetPreviosResolution()
    {
        currentResolutionIndex = ResolutionGetPreviosWrappedIndex(resolutions, currentResolutionIndex);
        SetResolutionText(resolutions [currentResolutionIndex]);
        SetAndApplyResolution(currentResolutionIndex);
    }

    private void SetAndApplyResolution(int newResolutionIndex)
    {
        currentResolutionIndex = newResolutionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyResolution(resolutions[currentResolutionIndex]);
    }

    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);

        if(!fullscreen)
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        else
            Screen.SetResolution(resolution.width, resolution.height, !Screen.fullScreen);

        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);
    }

    private int ResolutionGetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        return (currentIndex + 1) % collection.Count;
    }

    private int ResolutionGetPreviosWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currentIndex - 1) < 0) return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;
    }

    public void SetFullscreen()
    {
        if (!fullscreen)
        {
            fullScreenText.text = "FullScreen";
            Screen.fullScreen = true;
            fullscreen = true;
        }
        else
        {
            fullScreenText.text = "Windowed Bordeless";
            Screen.fullScreen = false;
            fullscreen = false;
        }
    }

    public void SetBrightness(float brightness)
    {
        Screen.brightness = brightness;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }   

    public void ResetDefault()
    {
        if (!fullscreen)
            SetFullscreen();

        currentResolutionIndex = 0;
        currentQualityIndex = 0;
        QualitySettings.SetQualityLevel(0, true);
        SetQualityText(qualities[currentQualityIndex]);
        SetResolutionText(resolutions[currentResolutionIndex]);
        PlayerPrefs.SetInt(QUALITY_PREF_KEY, currentQualityIndex);
        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);

        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void Back()
    {
        animator.SetTrigger("Back");
    }
}
