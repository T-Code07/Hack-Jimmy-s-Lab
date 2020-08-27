using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hacker.Computer;
namespace Hacker.Computer.UI
{
    [RequireComponent(typeof(ComputerAndMonitorBridge))]
    /// <summary>
    /// This is the UI part of the computer. It is connected to the data computer by the ComputerAndMonitorBridge.
    /// </summary>
    public class InteractWithMonitor : MonoBehaviour
    {
        [SerializeField] Text m_monitor;
        [SerializeField] InputField m_inputField;
        [SerializeField] Text m_ownerLabel;
        private string m_playerInput = "Player Input";
        private List<string> m_pastCommands = new List<string>();
        private ComputerAndMonitorBridge m_bridge;


        
        // Start is called before the first frame update
        void Start()
        {
            m_inputField.onEndEdit.AddListener(OnInputChanged);
            m_monitor.text = "";
            m_bridge = GetComponent<ComputerAndMonitorBridge>();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        //Called in the bridge class
        public void ShowText(string text, bool addText = false)
        {
            string lastText = m_monitor.text;
            string finishedText = text;

            m_monitor.text = "";
            if (addText)
            {
             
                finishedText = lastText + "\n" + text;

            }
            m_monitor.text = finishedText;
        }

        //Run at start by bridge.
        public void SetOwnerLabel(string ownerName)
        {
            m_ownerLabel.text = ownerName + "'s Computer:";
        }

        //This is called when the inputField text is changed in the . 
        private void OnInputChanged(string input)
        {
            m_bridge.RunCommand(input);
            m_playerInput = input;

            m_pastCommands.Add(m_playerInput);
        }


    }
}