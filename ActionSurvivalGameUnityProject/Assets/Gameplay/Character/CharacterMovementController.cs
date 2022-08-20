using Fusion;
using UnityEngine;

namespace ASG.Gameplay.Character
{
    [RequireComponent(typeof(NetworkCharacterControllerPrototype))]
    public class CharacterMovementController : NetworkBehaviour
    {
        [SerializeField, HideInInspector]
        private AInputsHandler m_inputsHandler = null;
        private NetworkCharacterControllerPrototype m_characterController = null;

        private Vector2 m_movementInputs = default;
        private Vector2 m_lookAroundInputs = default;

        [Header("Params")]
        [SerializeField]
        private float m_movementSpeed = 5f;
        [SerializeField]
        private float m_rotationSmoothValue = 5f;

        private float SpeedToUse => m_movementSpeed;

        private void Awake()
        {
            m_characterController = GetComponent<NetworkCharacterControllerPrototype>();
        }

        private void get_inputs()
        {
            m_movementInputs = m_inputsHandler.MovementInputs;
            m_lookAroundInputs = m_inputsHandler.LookAroundInputs;
        }

        private void apply_movement()
        {
            m_characterController.Move((Vector3.forward * m_movementInputs.y + Vector3.right * m_movementInputs.x) * SpeedToUse * Runner.DeltaTime);
        }

        private void apply_rotation()
        {
            transform.forward = Vector3.Slerp(transform.forward, new Vector3(m_lookAroundInputs.x, 0f, m_lookAroundInputs.y), m_rotationSmoothValue * Runner.DeltaTime);
            // m_characterController.WriteRotation(transform.rotation);
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            get_inputs();
            apply_rotation();
            apply_movement();
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