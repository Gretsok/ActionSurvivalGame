using UnityEngine;

namespace ASG.Gameplay.Character
{
    public class CharacterAnimationsHandler : MonoBehaviour
    {
        #region Animations Keys
        private int HORIZONTAL_MOVEMENT = Animator.StringToHash("HorizontalMovement");
        private int VERTICAL_MOVEMENT = Animator.StringToHash("VerticalMovement");
        #endregion
        [Header("Refs")]
        [SerializeField]
        private Animator m_animator = null;
        private NetworkCharacterControllerPrototype m_characterController = null;

        private Vector3 m_movementInputs;
        [Header("Params")]
        [SerializeField]
        private float m_movementSmoothValue;

        private Vector3 m_lastFlatPosition = default;

        private void Awake()
        {
            m_characterController = GetComponent<NetworkCharacterControllerPrototype>();
            m_lastFlatPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        }

        private void get_inputs()
        {
            /*m_movementInputs = Vector3.Slerp(m_movementInputs,
                transform.InverseTransformDirection(new Vector3(m_inputsHandler.MovementInputs.x, 0f, m_inputsHandler.MovementInputs.y)),
                m_movementSmoothValue * Time.deltaTime);*/
            Vector3 flatPosition = new Vector3(transform.position.x, 0f, transform.position.z);
            m_movementInputs = Vector3.Slerp(m_movementInputs,
                transform.InverseTransformDirection(((flatPosition - m_lastFlatPosition) / Time.deltaTime) / 5f),
                m_movementSmoothValue * Time.deltaTime);
            m_lastFlatPosition = flatPosition;
        }

        private void LateUpdate()
        {
            get_inputs();
            apply_animations();
        }

        private void apply_animations()
        {
            m_animator.SetFloat(HORIZONTAL_MOVEMENT, m_movementInputs.x);
            m_animator.SetFloat(VERTICAL_MOVEMENT, m_movementInputs.z);
        }

    }
}