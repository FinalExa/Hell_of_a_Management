using UnityEngine;
public interface ICanBeInteracted
{
    GameObject Self { get; set; }
    bool IsInsidePlayerRange { get; set; }
    void Interaction();
}
