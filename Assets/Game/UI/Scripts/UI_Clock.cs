using UnityEngine;
using UnityEngine.UI;

public class UI_Clock : MonoBehaviour
{
    public GameObject clock;
    private Image circle;
    public Color green;
    public Color yellow;
    public Color red;
    public int animationPercent = 25;

    private void Start()
    {
        circle = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        UITimerGoingDown();
        UITimerChangeColor();
        UITimerStartAnimations();
    }

    void UITimerGoingDown()
    {
        if (Timer.self.runTimer)
            circle.fillAmount = Mathf.Clamp(Timer.self.currentTime / Timer.self.totaltime, 0, 1);

    }

    void UITimerChangeColor()
    {
        if(Timer.self.currentTime >= (Timer.self.totaltime * 50) / 100)
            circle.color = green;
        else if(Timer.self.currentTime >= (Timer.self.totaltime * 25) / 100 && Timer.self.currentTime <= (Timer.self.totaltime * 67) / 100)
            circle.color = yellow;
        else if (Timer.self.currentTime <= (Timer.self.totaltime * 24) / 100)
            circle.color = red;
    }

    void UITimerStartAnimations()
    {
        if (Timer.self.currentTime <= (Timer.self.totaltime * animationPercent) / 100)
        {
            gameObject.GetComponent<Animator>().SetTrigger("StartFlashing");
            clock.GetComponent<Animator>().SetTrigger("StartPulsing");
        }
    }
}
