using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Hacker.Computer
{
    public class ComputerCreator : NetworkBehaviour
    {

        [SerializeField] string m_computerOwner;
        [SerializeField] int m_computerExitSceneIndex;
        [SerializeField] bool m_isPlayerComputer = true;
        private ComputerAndMonitorBridge m_Bridge;

        void Start()
        {
            m_Bridge = GetComponentInChildren<ComputerAndMonitorBridge>();
            m_Bridge.isPlayerComputer = m_isPlayerComputer;
            m_Bridge.ComputerName = m_computerOwner;
            m_Bridge.ExitSceneIndex = m_computerExitSceneIndex;

        }
    }
}