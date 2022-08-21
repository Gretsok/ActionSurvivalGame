using ASG.MainMenu.UI;
using UnityEngine;

namespace ASG.MainMenu
{
    public class MainScreenFlowState : Global.Flow.LocalFlowState
    {
        [SerializeField]
        private MainMenuFlowMachine m_flowMachine = null;

        private MainScreenPanel m_panel = null;

        protected override void SetUpReferences()
        {
            base.SetUpReferences();
            m_panel = GetFirstAssociatedObjectComponent<MainScreenPanel>();
        }
        protected override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.PlayButton.onClick.AddListener(HandlePlayButtonClicked);
            m_panel.QuitButton.onClick.AddListener(HandleQuitButtonClicked);
        }

        private void HandleQuitButtonClicked()
        {
            Application.Quit();
        }

        private void HandlePlayButtonClicked()
        {
            m_flowMachine.SwitchToPlayScreen();
        }

        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();
            m_panel.PlayButton.onClick.RemoveListener(HandlePlayButtonClicked);
            m_panel.QuitButton.onClick.RemoveListener(HandleQuitButtonClicked);
        }
    }
}