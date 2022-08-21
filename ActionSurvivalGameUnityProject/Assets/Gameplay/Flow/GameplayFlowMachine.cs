using ASG.Global.Flow;
using UnityEngine;

namespace ASG.Gameplay.Flow
{
    public class GameplayFlowMachine : LocalFlowMachine
    {
        [SerializeField]
        private GameplayFlowState m_gameplayState = null;

        public void SwitchToGameplayState()
        {
            SwitchToState(m_gameplayState);
        }
    }
}