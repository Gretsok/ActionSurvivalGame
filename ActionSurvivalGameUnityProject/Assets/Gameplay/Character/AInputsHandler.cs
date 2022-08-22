using Fusion;
using System;
using UnityEngine;

namespace ASG.Gameplay.Character
{
    public abstract class AInputsHandler : NetworkBehaviour
    {
        public virtual Vector2 MovementInputs { get; protected set; } = default;
        public virtual Vector2 LookAroundInputs { get; protected set; } = default;

        public Action OnSwitchToNextBuildingAsked = null;
        public Action OnSwitchToPreviousBuildingAsked = null;
        public Action OnToggleBuildingModeAsked = null;
        public Action OnPlaceCurrentBuildingAsked = null;
    }
}