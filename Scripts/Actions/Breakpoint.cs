using UnityEngine;
using MultiplayerARPG.KitBT;

public class Breakpoint : ActionNode
{
    protected override void OnStart() {
        Debug.Log("Trigging Breakpoint");
        Debug.Break();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
