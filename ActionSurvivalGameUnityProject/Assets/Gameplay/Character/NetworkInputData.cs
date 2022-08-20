using Fusion;
using UnityEngine;

namespace ASG.Gameplay.Character
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector2 lookAround;
        public Vector2 movement;
    }
}