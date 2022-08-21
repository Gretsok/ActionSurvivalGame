using ASG.MainMenu.UI;
using UnityEngine;

namespace ASG.MainMenu
{
    public class PlayScreenFlowState : Global.Flow.LocalFlowState
    {
        [SerializeField]
        private MainMenuFlowMachine m_flowMachine = null;

        private PlayScreenPanel m_panel = null;

        protected override void SetUpReferences()
        {
            base.SetUpReferences();
            m_panel = GetFirstAssociatedObjectComponent<PlayScreenPanel>();
        }

        protected override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.StartAGameAsHostButton.onClick.AddListener(HandleStartAGameAsHostButtonClicked);
            m_panel.JoinAGameButton.onClick.AddListener(HandleJoinAGameButtonClicked);
            m_panel.BackButton.onClick.AddListener(HandleBackButtonClicked);
        }

        private void HandleBackButtonClicked()
        {
            m_flowMachine.SwitchToMainScreen();
        }

        private void HandleJoinAGameButtonClicked()
        {
            m_panel.Log("Join A Game Button Clicked");
        }

        private void HandleStartAGameAsHostButtonClicked()
        {
            m_panel.Log("Start A Game As Host Button Clicked");
        }

        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();
            m_panel.StartAGameAsHostButton.onClick.RemoveListener(HandleStartAGameAsHostButtonClicked);
            m_panel.JoinAGameButton.onClick.RemoveListener(HandleJoinAGameButtonClicked);
            m_panel.BackButton.onClick.RemoveListener(HandleBackButtonClicked);
        }
    }
}