using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hacker.Computer.UI;
using Hacker.Computer.Core;

namespace Hacker.Computer
{


    /// <summary>
    /// This bridges the gap between the UI controls and the hard computer data.
    /// </summary>
 
    public class ComputerAndMonitorBridge : MonoBehaviour
    {
        private int m_exitSceneIndex;
        private AbstractComputer m_computerBrain;
        private InteractWithMonitor m_monitor;
        private bool m_computerCreated = false;
        private bool m_isPlayerComputer = true;
        private string m_computerOwner = "Tawesome07";
        private HackerLevel m_hackerlevel = HackerLevel.NOOB;
      

        public int ExitSceneIndex
        {
            get { return m_exitSceneIndex; }
            set { m_exitSceneIndex = value; }
        }

        public string ComputerName
        {
            get { return m_computerOwner; }
            set { m_computerOwner = value; }
        }

        public bool isPlayerComputer
        {
            get { return m_isPlayerComputer; }
            set { m_isPlayerComputer = value; }
        }

        private void Start()
        {
            if (isPlayerComputer)
            {
                print("created");
                m_computerBrain = new AbstractHackerComputer(m_computerOwner, m_hackerlevel, gameObject, m_exitSceneIndex);
                m_computerCreated = true;
            }
            else
            {
                m_computerBrain = new CivilianComputer(m_computerOwner, gameObject, m_exitSceneIndex);
                m_computerCreated = true;
            }
            m_monitor = GetComponentInChildren<InteractWithMonitor>();

            m_monitor.SetOwnerLabel(m_computerBrain.Owner);
            
        }


        //Public so that there can be a unity message broadcasted to it from the computer class.
        /// <summary>
        /// This will put the text on the monitor from the computer script. 
        /// </summary>
        /// <param name="textData">Must always be in the format 0 = [Text] 1 = [addText?]</param>
        public void ShowTextBridge(object[] textData)
        {
            string text = (string)textData[0];
            bool addText = (bool)textData[1];
            
            m_monitor.ShowText(text, addText);
        }

        public void RunCommand(string command)
        {
            bool foundCommand = false; 
            
            foreach(ConsoleCommand consoleCommand in m_computerBrain.ConsoleCommandList)
            {
                
                if(command == consoleCommand.ToString().ToLower())
                {
                    
                   
                    m_computerBrain.ImplementCommand(consoleCommand);
                    foundCommand = true; 
                    break;
                }
                
            }
            if (!foundCommand)
            {
                bool addToMonitor = false;
                if(m_computerBrain.ComputerState == ComputerStates.RUNNING_COMMAND)
                {
                    addToMonitor = true;
                }
                m_monitor.ShowText("This is not a command.", addToMonitor);

            }
            m_computerBrain.UsedCommands.Add(command);
        }
    }
}
