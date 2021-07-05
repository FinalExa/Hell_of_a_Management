using UnityEngine;

[CreateAssetMenu(fileName = "OutlineData", menuName = "ScriptableObjects/OutlineData", order = 5)]
public class OutlineData : ScriptableObject
{
    public Color highlightColor;
    [SerializeField, Range(0f, 10f)] public float outlineWidth;
}
