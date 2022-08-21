using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASG.Gameplay.Flow
{
    public class GameplayFlowState : Global.Flow.LocalFlowState
    {
        [SerializeField]
        private GameplayFlowMachine m_flowMachine = null;

    }
}