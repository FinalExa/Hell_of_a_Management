using UnityEngine;

public class DrunkenRange : MonoBehaviour
{
    private DrunkenController drunkenController;
    private GameObject player;

    private void Awake()
    {
        drunkenController = GetComponentInParent<DrunkenController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Range();
    }

    private void Range()
    {
        float distance = Vector2.Distance(new Vector2(drunkenController.gameObject.transform.position.x, drunkenController.gameObject.transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z));
        if (distance >= drunkenController.drunkenReferences.drunkenData.rangeSize) drunkenController.playerInRange = false;
        else drunkenController.playerInRange = true;
    }
}
