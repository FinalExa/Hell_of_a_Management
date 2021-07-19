using UnityEngine;
using UnityEngine.UI;

public class MiniDialogueWithText : MonoBehaviour
{
    [SerializeField] private Text textObj;
    [SerializeField] private GameObject graphicObj;
    [SerializeField] private float fixRotationX;
    [SerializeField] private float fixRotationY;
    [HideInInspector] public bool activated;

    public void SetupPosition()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(fixRotationX, fixRotationY, 0f);
    }

    public void SetupDialogue(string text)
    {
        graphicObj.SetActive(true);
        textObj.text = text;
        activated = true;
    }

    public void DeactivateDialogue()
    {
        textObj.text = string.Empty;
        graphicObj.SetActive(false);
        activated = false;
    }
}
