using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public GameObject target;
    private GameObject playerCharacter;

    private void Awake()
    {
        playerCharacter = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        FollowAndLook();
    }

    private void FollowAndLook()
    {
        this.gameObject.transform.position = playerCharacter.transform.position;
        this.gameObject.transform.LookAt(target.transform.position);
    }
}
