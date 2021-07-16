using System.Collections.Generic;
using UnityEngine;
public class PlayerOcclusionCheck : MonoBehaviour
{
    private GameObject shader;
    [SerializeField] private List<BoxCollider> occlusionTagsDetected;
    private void Awake()
    {
        shader = GameObject.FindGameObjectWithTag("OcclusionShader");
    }
    private void Start()
    {
        shader.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Occlusion")) if (!occlusionTagsDetected.Contains((BoxCollider)other)) occlusionTagsDetected.Add((BoxCollider)other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Occlusion")) if (occlusionTagsDetected.Contains((BoxCollider)other)) occlusionTagsDetected.Remove((BoxCollider)other);
    }
    private void Update()
    {
        if (occlusionTagsDetected.Count > 0) CheckForOcclusionPosition();
        else if (occlusionTagsDetected.Count == 0 && shader.activeSelf) shader.SetActive(false);
    }
    private void CheckForOcclusionPosition()
    {
        Vector3 playerPos = this.gameObject.transform.position;
        bool success = false;
        for (int i = 0; i < occlusionTagsDetected.Count; i++)
        {
            bool xCheck;
            bool zCheck;
            Vector3 curObjPos = occlusionTagsDetected[i].gameObject.transform.position + occlusionTagsDetected[i].center;
            Vector3 curObjSize = occlusionTagsDetected[i].size;
            if (playerPos.x >= curObjPos.x - curObjSize.x / 2 && playerPos.x <= curObjPos.x + curObjSize.x / 2) xCheck = true;
            else xCheck = false;
            if (playerPos.z >= curObjPos.z - curObjSize.z / 2 && playerPos.z <= curObjPos.z + curObjSize.z / 2) zCheck = true;
            else zCheck = false;
            if (xCheck && zCheck)
            {
                success = true;
                shader.SetActive(true);
                break;
            }
        }
        if (!success) shader.SetActive(false);
    }
}
