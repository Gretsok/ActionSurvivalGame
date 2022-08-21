using UnityEngine;

namespace ASG.MainMenu
{
    public class MainMenuFlowMachine : Global.Flow.LocalFlowMachine
    {
        [SerializeField]
        private MainScreenFlowState m_mainScreenState = null;
        [SerializeField]
        private PlayScreenFlowState m_playScreenState = null;

        public void SwitchToPlayScreen()
        {
            SwitchToState(m_playScreenState);
        }

        public void SwitchToMainScreen()
        {
            SwitchToState(m_mainScreenState);
        }
    }
}