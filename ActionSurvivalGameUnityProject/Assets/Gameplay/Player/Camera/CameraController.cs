using UnityEngine;

namespace ASG.Gameplay.Player.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Cinemachine.CinemachineVirtualCamera m_virtualCamera = null;
        public void SetTarget(Transform a_target)
        {
            m_virtualCamera.Follow = a_target;
        }
    }
}
