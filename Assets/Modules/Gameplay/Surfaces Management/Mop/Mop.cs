using UnityEngine;

public class Mop : MonoBehaviour
{
    [SerializeField] private MopData mopData;
    private float scoreGiven;
    private SurfaceController lastTerrainTouched;
    private void Start()
    {
        scoreGiven = mopData.scoreGivenByRemovingTerrains;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            SurfaceController sc = other.gameObject.GetComponent<SurfaceController>();
            if (sc != null && sc != lastTerrainTouched)
            {
                lastTerrainTouched = sc;
                SurfaceManager.DeactivateSurface(ref sc);
                Score.self.AddScore(scoreGiven);
                lastTerrainTouched = null;
            }
        }
    }
}
