using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableObjectData", menuName = "ScriptableObjects/ThrowableObjectData", order = 2)]
public class ThrowableObjectData : ScriptableObject
{
    public float objectWeight;
    public Color highlightColor;
    [HideInInspector] public Color baseColor;
}
