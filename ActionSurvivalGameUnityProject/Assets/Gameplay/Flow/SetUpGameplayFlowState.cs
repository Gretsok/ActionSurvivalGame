using Fusion;
using System.Collections;
using UnityEngine;

namespace ASG.Gameplay.Flow
{
    public class SetUpGameplayFlowState : Global.Flow.LocalFlowState
    {
        [SerializeField]
        private GameplayFlowMachine m_flowMachine = null;
        [SerializeField]
        private Player.Camera.CameraController m_cameraController = null;
        protected override void SetUpReferences()
        {
            base.SetUpReferences();
            StartCoroutine(SettingUpRoutine());
        }

        private IEnumerator SettingUpRoutine()
        {
            NetworkCallbacksManager callbacksManager = null;

            while(callbacksManager == null)
            {
                callbacksManager = FindObjectOfType<NetworkCallbacksManager>();
                yield return null;
            }

            NetworkObject localPlayerCharacter = null;
            while(localPlayerCharacter == null)
            {
                localPlayerCharacter = callbacksManager.GetLocalPlayerCharacter();
                /*
                 * Does not work, maybe get all players and try 
                 * to look for the one to which we have input authority over
                 */
                yield return null;
            }

            m_cameraController.SetTarget(localPlayerCharacter.transform);
            m_flowMachine.SwitchToGameplayState();
        }
    }
}