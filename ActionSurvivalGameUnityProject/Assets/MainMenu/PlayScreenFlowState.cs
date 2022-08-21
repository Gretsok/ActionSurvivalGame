using ASG.MainMenu.UI;
using Fusion;
using UnityEngine;

namespace ASG.MainMenu
{
    public class PlayScreenFlowState : Global.Flow.LocalFlowState
    {
        [SerializeField]
        private MainMenuFlowMachine m_flowMachine = null;
        [SerializeField]
        private NetworkCallbacksManager m_callbacksManager = null;

        private PlayScreenPanel m_panel = null;
        private NetworkRunner m_runner;

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
            if (!IsPassingCommonChecks()) return;

            m_panel.Log($"Joining Game {m_panel.RoomNameField.text}");
            StartGame(GameMode.Client, m_panel.RoomNameField.text);
        }

        private void HandleStartAGameAsHostButtonClicked()
        {
            if (!IsPassingCommonChecks()) return;

            m_panel.Log($"Starting Game {m_panel.RoomNameField.text}");
            StartGame(GameMode.Host, m_panel.RoomNameField.text);
        }

        private bool IsPassingCommonChecks()
        {
            if (m_panel.RoomNameField.text.Length == 0)
            {
                m_panel.Log("Don't let the room name empty");
                return false;
            }
            return true;
        }

        async void StartGame(GameMode mode, string a_roomName)
        {
            if(m_runner)
            {
                Destroy(m_runner);
            }
            m_runner = m_callbacksManager.gameObject.AddComponent<NetworkRunner>();
            m_runner.ProvideInput = true;

            // Start or join (depends on gamemode) a session with a specific name
            await m_runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = a_roomName,
                Scene = 1,
                SceneManager = m_callbacksManager.gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
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