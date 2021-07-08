using UnityEngine;

public class CustomerReferences : MonoBehaviour
{
    public CustomerData customerData;
    [HideInInspector] public CustomerVignette customerVignette;
    [HideInInspector] public Highlightable highlightable;
    [HideInInspector] public Animations animations;
    private void Awake()
    {
        customerVignette = this.gameObject.GetComponent<CustomerVignette>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
        animations = this.gameObject.GetComponent<Animations>();
    }
}
