using UnityEngine;

public class CustomerReferences : MonoBehaviour
{
    public CustomerScoreData customerScoreData;
    [HideInInspector] public CustomerVignette customerVignette;
    [HideInInspector] public Highlightable highlightable;
    private void Awake()
    {
        customerVignette = this.gameObject.GetComponent<CustomerVignette>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
    }
}
