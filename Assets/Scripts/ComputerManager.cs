using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hacker.Computer;
using Hacker.Computer.Core;

namespace Hacker.Game {
    public class ComputerManager : MonoBehaviour
    {
        ComputerAndMonitorBridge m_bridge;

        public string m_playerName = "Tawesome07";
        public HackerLevel m_playerHackingLevel = HackerLevel.NOOB;
        public GameObject m_gameObject;


        // Start is called before the first frame update
        void Start()
        {
            m_bridge = FindObjectOfType<ComputerAndMonitorBridge>();
          //  m_bridge.CreateHackingComputer(m_playerName, m_gameObject, m_playerHackingLevel);
           // print("The Game Manager has successfully created the Computer.");
        }

        // Update is called once per frame
        void Update()
        {
           
        }
    }
}
