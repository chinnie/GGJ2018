using UnityEngine;

public interface IRobot
{
    Vector3 StartPosition { get; set; }
    Vector3 EndPosition { get; set; }
    bool AltBehavior { get; set; }
    bool IsInteracting { get; set; }
    void TriggerAction();
    void Toggle();
}