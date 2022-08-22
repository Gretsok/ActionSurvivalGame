using ASG.Gameplay.Character;
using Fusion;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

            m_actions.Building.PlaceCurrentBuilding.started += HandlePlaceCurrentBuildingStarted;
            m_actions.Building.ToggleBuildingMode.started += HandleToggleBuildingModeStarted;
            m_actions.Building.SwitchToNextBuilding.started += HandleSwitchToNextBuildingStarted;
            m_actions.Building.SwitchToPreviousBuilding.started += HandleSwitchToPreviousBuildingStarted;
        }


        private void UnregisterInputsEvents()
        {
            if (!HasInputAuthority) return;

            m_actions.Building.PlaceCurrentBuilding.started -= HandlePlaceCurrentBuildingStarted;
            m_actions.Building.ToggleBuildingMode.started -= HandleToggleBuildingModeStarted;
            m_actions.Building.SwitchToNextBuilding.started -= HandleSwitchToNextBuildingStarted;
            m_actions.Building.SwitchToPreviousBuilding.started -= HandleSwitchToPreviousBuildingStarted;
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

        #region Inputs Events Handling
        private void HandleSwitchToPreviousBuildingStarted(InputAction.CallbackContext obj)
        {
            RPC_SwitchToPreviousBuilding();
        }

        private void HandleSwitchToNextBuildingStarted(InputAction.CallbackContext obj)
        {
            RPC_SwitchToNextBuilding();
        }

        private void HandleToggleBuildingModeStarted(InputAction.CallbackContext obj)
        {
            RPC_ToggleBuildingModeStarted();
        }

        private void HandlePlaceCurrentBuildingStarted(InputAction.CallbackContext obj)
        {
            RPC_PlaceCurrentBuilding();
        }

        #region RPCs
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SwitchToPreviousBuilding()
        {
            OnSwitchToPreviousBuildingAsked?.Invoke();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SwitchToNextBuilding()
        {
            OnSwitchToNextBuildingAsked?.Invoke();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_ToggleBuildingModeStarted()
        {
            OnToggleBuildingModeAsked?.Invoke();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_PlaceCurrentBuilding()
        {
            OnPlaceCurrentBuildingAsked?.Invoke();
        }
        #endregion

        #endregion
    }
}