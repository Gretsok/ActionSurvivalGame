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
        [SerializeField, HideInInspector]
        private AInputsHandler m_inputsHandler = null;
        [SerializeField]
        private Animator m_animator = null;

        private Vector3 m_movementInputs;
        [Header("Params")]
        [SerializeField]
        private float m_movementSmoothValue;

        private void get_inputs()
        {
            m_movementInputs = transform.InverseTransformDirection(new Vector3(m_inputsHandler.MovementInputs.x, 0f, m_inputsHandler.MovementInputs.y));
        }

        private void Update()
        {
            get_inputs();
            apply_animations();
        }

        private void apply_animations()
        {
            m_animator.SetFloat(HORIZONTAL_MOVEMENT, m_movementInputs.x);
            m_animator.SetFloat(VERTICAL_MOVEMENT, m_movementInputs.z);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_inputsHandler == null)
            {
                if (TryGetComponent(out AInputsHandler l_inputsHandler))
                {
                    m_inputsHandler = l_inputsHandler;
                    UnityEditor.EditorUtility.SetDirty(this);
                }
                else
                {
                    Debug.LogError($"Could not find an Inputs Handler on gameobject {gameObject.name} while it must have one");
                }
            }
        }
#endif
    }
}