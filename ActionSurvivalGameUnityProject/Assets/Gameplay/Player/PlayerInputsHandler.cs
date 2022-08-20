using ASG.Gameplay.Character;
using Fusion;
using System;
using UnityEngine;

namespace ASG.Gameplay.Player
{
    public class PlayerInputsHandler : Character.AInputsHandler
    {
        private PlayerInputsActions m_actions = null;

        public virtual Vector2 LocalMovementInputs { get; protected set; } = default;
        public virtual Vector2 LocalLookAroundInputs { get; protected set; } = default;

        public override void Spawned()
        {
            base.Spawned();
            SetUpActions();
            RegisterInputsEvents();
        }


        private void SetUpActions()
        {
            if (!HasInputAuthority) return;
            m_actions = new PlayerInputsActions();
            m_actions.Enable();
        }

        private void RegisterInputsEvents()
        {
            if (!HasInputAuthority) return;
        }

        private void UnregisterInputsEvents()
        {
            if (!HasInputAuthority) return;
        }

        private void read_local_inputs()
        {
            if (!HasInputAuthority) return;

            if (m_actions != null)
            {
                LocalMovementInputs = m_actions.Movement.Movement.ReadValue<Vector2>();
                if(MovementInputs.sqrMagnitude > 1f)
                {
                    LocalMovementInputs.Normalize();
                }
                Vector2 lookAround = m_actions.Movement.LookAround.ReadValue<Vector2>();
                if(lookAround.sqrMagnitude > 0.3f)
                {
                    LocalLookAroundInputs = lookAround;
                }
            }
            else
            {
                LocalMovementInputs = default;
                LocalLookAroundInputs = default;
            }
        }

        private void read_networked_inputs()
        {
            if(GetInput(out NetworkInputData l_inputData))
            {
                MovementInputs = l_inputData.movement;
                LookAroundInputs = l_inputData.lookAround;

                if(MovementInputs.sqrMagnitude > 1)
                {
                    MovementInputs.Normalize();
                }
            }
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            read_local_inputs();
            read_networked_inputs();
        }



        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            base.Despawned(runner, hasState);
            UnregisterInputsEvents();
            CleanUpActions();
        }

        private void CleanUpActions()
        {
            if (!HasInputAuthority) return;
            m_actions.Disable();
        }
    }
}