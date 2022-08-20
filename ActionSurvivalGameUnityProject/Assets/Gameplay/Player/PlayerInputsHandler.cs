using UnityEngine;

namespace ASG.Gameplay.Player
{
    public class PlayerInputsHandler : Character.AInputsHandler
    {
        private PlayerInputsActions m_actions = null;

        private void Start()
        {
            SetUpActions();
            RegisterInputsEvents();
        }

        private void SetUpActions()
        {
            m_actions = new PlayerInputsActions();
            m_actions.Enable();
        }

        private void RegisterInputsEvents()
        {
            
        }

        private void UnregisterInputsEvents()
        {

        }

        private void read_inputs()
        {
            if(m_actions != null)
            {
                MovementInputs = m_actions.Movement.Movement.ReadValue<Vector2>();
                if(MovementInputs.sqrMagnitude > 1f)
                {
                    MovementInputs.Normalize();
                }
                Vector2 lookAround = m_actions.Movement.LookAround.ReadValue<Vector2>();
                if(lookAround.sqrMagnitude > 0.3f)
                {
                    LookAroundInputs = lookAround;
                }
            }
            else
            {
                MovementInputs = default;
                LookAroundInputs = default;
            }
        }

        private void Update()
        {
            read_inputs();
        }


        private void OnDestroy()
        {
            UnregisterInputsEvents();
            CleanUpActions();
        }

        private void CleanUpActions()
        {
            m_actions.Disable();
        }
    }
}