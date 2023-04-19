using UnityEngine;
using Fusion;

public struct InputData : INetworkInput
{
    public Vector2 MoveInput;
    public Vector2 LookInput;
    public Vector2 FollowTargetEulerAngles;
}
