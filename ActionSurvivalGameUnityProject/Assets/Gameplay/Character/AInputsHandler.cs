using UnityEngine;

namespace ASG.Gameplay.Character
{
    public abstract class AInputsHandler : MonoBehaviour
    {
        public virtual Vector2 MovementInputs { get; protected set; } = default;
        public virtual Vector2 LookAroundInputs { get; protected set; } = default;
    }
}