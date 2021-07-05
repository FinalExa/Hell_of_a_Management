using UnityEngine;

public class CustomerReferences : MonoBehaviour
{
    public CustomerData customerData;
    [HideInInspector] public CustomerVignette customerVignette;
    [HideInInspector] public Highlightable highlightable;
    private void Awake()
    {
        customerVignette = this.gameObject.GetComponent<CustomerVignette>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
    }
}
