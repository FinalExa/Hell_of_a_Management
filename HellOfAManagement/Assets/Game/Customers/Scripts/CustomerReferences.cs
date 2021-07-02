using UnityEngine;

public class CustomerReferences : MonoBehaviour
{
    [HideInInspector] public CustomerVignette customerVignette;
    private void Awake()
    {
        customerVignette = this.gameObject.GetComponent<CustomerVignette>();
    }
}
