using UnityEngine;

public class CustomerModelLoad : MonoBehaviour
{
    private CustomerController customerController;
    private int selectedCustomerIndex;
    [System.Serializable]
    public struct CustomerGraphics
    {
        public GameObject customerModel;
        public Outline customerOutline;
        public Animator customerAnimator;
    }

    private void Awake()
    {
        customerController = this.gameObject.GetComponent<CustomerController>();
    }

    [SerializeField] private CustomerGraphics[] customerGraphics;

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
        selectedCustomerIndex = Random.Range(0, customerGraphics.Length);
        customerGraphics[selectedCustomerIndex].customerModel.SetActive(true);
        customerController.customerReferences.animations.animator = customerGraphics[selectedCustomerIndex].customerAnimator;
        customerController.customerReferences.highlightable.outline = customerGraphics[selectedCustomerIndex].customerOutline;
        customerController.customerReferences.highlightable.outline.OutlineColor = customerController.customerReferences.highlightable.outlineData.highlightColor;
        customerController.customerReferences.highlightable.outline.OutlineWidth = customerController.customerReferences.highlightable.outlineData.outlineWidth;
    }

    private void TurnOffCustomerModels()
    {
        customerGraphics[selectedCustomerIndex].customerModel.SetActive(false);
    }
}
