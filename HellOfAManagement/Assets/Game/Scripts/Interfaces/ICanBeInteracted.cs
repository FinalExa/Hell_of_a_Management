using UnityEngine;
public interface ICanBeInteracted
{
    GameObject Self { get; set; }
    void Interaction();
}
