using UnityEngine;

public class Mop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            SurfaceController sc = other.gameObject.GetComponent<SurfaceController>();
            if (sc != null) SurfaceManager.DeactivateSurface(ref sc);
        }
    }
}
