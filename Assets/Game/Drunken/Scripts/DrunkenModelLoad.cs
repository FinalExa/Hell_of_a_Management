using UnityEngine;

public class DrunkenModelLoad : MonoBehaviour
{
    private DrunkenController drunkenController;
    private int selectedCustomerIndex;
    [System.Serializable]
    public struct DrunkenGraphics
    {
        public GameObject customerModel;
        public Outline customerOutline;
        public Animator customerAnimator;
    }

    private void Awake()
    {
        drunkenController = this.gameObject.GetComponent<DrunkenController>();
    }

    [SerializeField] private DrunkenGraphics[] drunkenGraphics;

    private void OnEnable()
    {
        RandomizeModel();
    }

    private void OnDisable()
    {
        TurnOffCustomerModels();
    }

    public void RandomizeModel()
    {
        selectedCustomerIndex = Random.Range(0, drunkenGraphics.Length);
        drunkenGraphics[selectedCustomerIndex].customerModel.SetActive(true);
        drunkenController.drunkenReferences.animations.animator = drunkenGraphics[selectedCustomerIndex].customerAnimator;
        drunkenController.drunkenReferences.highlightable.outline = drunkenGraphics[selectedCustomerIndex].customerOutline;
        drunkenController.drunkenReferences.highlightable.outline.OutlineColor = drunkenController.drunkenReferences.highlightable.outlineData.highlightColor;
        drunkenController.drunkenReferences.highlightable.outline.OutlineWidth = drunkenController.drunkenReferences.highlightable.outlineData.outlineWidth;
    }

    private void TurnOffCustomerModels()
    {
        drunkenGraphics[selectedCustomerIndex].customerModel.SetActive(false);
    }
}
