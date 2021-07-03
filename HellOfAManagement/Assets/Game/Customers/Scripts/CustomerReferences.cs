using UnityEngine;

public class CustomerReferences : MonoBehaviour
{
    [HideInInspector] public CustomerVignette customerVignette;
    [HideInInspector] public Highlightable highlightable;
    private void Awake()
    {
        customerVignette = this.gameObject.GetComponent<CustomerVignette>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
    }
}
