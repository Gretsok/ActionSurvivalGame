using UnityEngine;
using UnityEngine.UI;

namespace ASG.MainMenu.UI
{
    public class MainScreenPanel : MonoBehaviour
    {
        [SerializeField]
        private Button m_playButton = null;
        [SerializeField]
        private Button m_quitButton = null;
        public Button PlayButton => m_playButton;
        public Button QuitButton => m_quitButton;

    }
}