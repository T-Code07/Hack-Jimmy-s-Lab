using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;

namespace Hacker.Computer
{
    public class ComputerCreator : NetworkBehaviour
    {
        private string m_id = Guid.NewGuid().ToString();
        public string m_computerOwner;
        [SerializeField] int m_computerExitSceneIndex;
        [SerializeField] bool m_isPlayerComputer = true;
        private ComputerAndMonitorBridge m_Bridge;

        public string ID
        {
            get {return m_id; }

        }

      public void createComputer()
        {
            //This tries to create a unique id tag for the computer for later use. 

            print(m_id);
            m_Bridge = GetComponentInChildren<ComputerAndMonitorBridge>();
            m_Bridge.isPlayerComputer = m_isPlayerComputer;
            m_Bridge.ComputerName = m_computerOwner;
            m_Bridge.ExitSceneIndex = m_computerExitSceneIndex;

        }
    }
}