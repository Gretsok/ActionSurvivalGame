using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ASG.MainMenu.UI
{
    public class PlayScreenPanel : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField]
        private Button m_startAGameAsHostButton = null;
        [SerializeField]
        private Button m_joinAGameButton = null;
        [SerializeField]
        private Button m_backButton = null;
        [SerializeField]
        private TMP_InputField m_roomNameField = null;
        [SerializeField]
        private TextMeshProUGUI m_logsText = null;
        public Button StartAGameAsHostButton => m_startAGameAsHostButton;
        public Button JoinAGameButton => m_joinAGameButton;
        public Button BackButton => m_backButton;
        public TMP_InputField RoomNameField => m_roomNameField;

        [Header("Params")]
        [SerializeField]
        private float m_logDuration = 10f;

        [System.Serializable]
        private struct LogData
        {
            public string LogText;
            public float timeOfEmission;
        }
        private List<LogData> m_logs = new List<LogData>();

        private void Update()
        {
            for(int i = m_logs.Count - 1; i >= 0; i--)
            {
                if(Time.time - m_logs[i].timeOfEmission > m_logDuration)
                {
                    m_logs.RemoveAt(i);
                }
            }
            print_logs();
        }

        public void Log(string a_newLog)
        {
            LogData newLog;
            newLog.LogText = a_newLog;
            newLog.timeOfEmission = Time.time;
            m_logs.Add(newLog);
            print_logs();
        }

        private void print_logs()
        {
            string textToDisplay = "";
            for(int i = 0; i < m_logs.Count; ++i)
            {
                textToDisplay += m_logs[i].LogText + "\n";
            }
            if(m_logsText.text != textToDisplay)
                m_logsText.text = textToDisplay;
        }
    }
}