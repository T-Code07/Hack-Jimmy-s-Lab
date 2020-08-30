using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

namespace Hacker.Computer.Core
{ 
    /*
     There will be two types of computers. One for the hacker.

        Hacker Computer Abilities:

        - Hacking Abilites (obviously)
            1. Password Cracker --Beginner
            2. Evil Code Download -- Intermediate 
            3. Wifi Attack --Begineer
            4. Social Engineering -- all
            5. Website Takeover  -- Advanced
            6. Man in the Middle --exploits chat
           
        - Security Stages:
            1. Password Player Sets
            2. Evil Code Destroyers
            3. VPN
            4. Webiste scanner (For evil Code)

    
        General Computer Stuff:
        
        - CPUs -- will speed up computer and attacks. Make Security more reactive
        - Memory -- Will let the player download for security
       

         */

    
    public enum SecurityLevel
    {
        NOOB,
        BEGINNER,
        INTERMEDIATE,
        ADVANCED
    }

    /// <summary>
    /// Security types avaible.
    /// </summary>
    public enum Security
    {
        PASSWORD_GEN,//generates a hard password to crack. Makes the probability that it'll crack go down. 
        VPN,//Prevents MITM attacks
        EVIL_CODE_FINDER,//Finds evil code
    }

    /// <summary>
    /// Types of hacks that will be used against computer.
    /// </summary>
    public enum Hack
    {
        PASSWORDCRACKED,
        WIFI_KICKER,
        MITM,
        EVIL_CODE,
        DEFENSE_SCANNER,
        VICTIM_SCANNER
    }


    /// <summary>
    /// Console Commands Player can use.
    /// </summary>
    public enum ConsoleCommand
    {
        HELP,
        HISTORY,
        SHOW_STATS,
        EXIT,
        SHOW_ATTACKS,
        CLEAR,
        STOP,
        SCAN,
        ATTACK
    }

    public enum ComputerStates
    {
        RUNNING_COMMAND,
        IDLE
    }

    /// <summary>
    /// This is the base class for all computers in this game. It included Console Commands and possible hacks enemies can do. 
    /// Written by Taylor Bledsoe - Main Game Dev. 8/23/20 -
    /// </summary>
    public class AbstractComputer : NetworkBehaviour
    {
        private string m_owner; //This will tell the hacker who owns the computer
        private int m_CPUs; //How fast the computer will run. 
        private int m_Memory; //How many security systems computer can download. Meassured in GBs. 
        private string m_password = "I<3ThisGame!"; //Default Security Password
        private bool m_connectToWIFI = true;
        private SecurityLevel m_securityLevel = SecurityLevel.NOOB;
        private List<Security> m_securityList = new List<Security>();
        private int m_exitSceneIndex;
        private GameObject m_GameObject;
        protected ComputerStates m_computerState = ComputerStates.IDLE;
        protected string m_stats;
        protected List<ConsoleCommand> m_commandList = new List<ConsoleCommand>();
        private List<string> m_usedCommands = new List<string>();
        

        //Default Values:
        public const int B_DEFAULT_MEMORY = 2048;
        public const int B_DEFAULT_CPUs = 4;
        public const SecurityLevel B_DEFAULT_SECURITY_LEVEL = SecurityLevel.NOOB;


        //Get and Set--------
        public List<string> UsedCommands
        {
            get { return m_usedCommands; }
           
        }

       
        public string Owner
        {
            get { return m_owner; }
            set { value = m_owner; }
        }

        protected SecurityLevel SecurityLevel
        {
            get { return m_securityLevel; }
        }

        protected int CPUs
        {
            get { return m_CPUs; }
            set { m_CPUs = value; }
        }

        protected int Memory
        {
            get { return m_Memory; }
            set { m_Memory = value;}
        }

        protected string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        public bool ConnectedToWifi
        {
            get { return m_connectToWIFI; }
            set { m_connectToWIFI = value; }
        }

        public List<Security> UsedSecurity
        {
            get { return m_securityList; }
        }

        public List<ConsoleCommand> ConsoleCommandList
        {
            get { return m_commandList; }
        }
        
        public ComputerStates ComputerState
        {
            get { return m_computerState; }
            set { m_computerState = value; }
        }

        public int ExitScene
        {
            get { return m_exitSceneIndex; }
            set { value = m_exitSceneIndex; }
        }

        //--------------------

        public AbstractComputer(string owner, GameObject gameObjectCreated, int sceneIndex, int starting_CPUs =  B_DEFAULT_CPUs, int starting_Memory = B_DEFAULT_MEMORY, SecurityLevel securityLevel = B_DEFAULT_SECURITY_LEVEL)
        {
            m_owner = owner;
            m_CPUs = starting_CPUs;
            m_exitSceneIndex = sceneIndex;
            m_Memory = starting_Memory;
            m_stats = "This computer is a computer owned by " + m_owner + " with " + m_CPUs.ToString() + "CPUs and " + m_Memory + "GBs of memory.";
            m_GameObject = gameObjectCreated;

            m_commandList.Add(ConsoleCommand.HELP);
            m_commandList.Add(ConsoleCommand.HISTORY);
            m_commandList.Add(ConsoleCommand.SHOW_STATS);
            m_commandList.Add(ConsoleCommand.CLEAR);
            m_commandList.Add(ConsoleCommand.STOP);
            m_commandList.Add(ConsoleCommand.EXIT);


        }

        public void Attacked(Hack attack, string hackerName)
        {
            if(attack == Hack.WIFI_KICKER && m_connectToWIFI)
            {
               ShowTextOnMonitor("Kicked from WIFI by " + hackerName + ".");
               return;
            }
            if(attack == Hack.DEFENSE_SCANNER)
            {
                foreach(Security securityItem in m_securityList)
                {
                    ShowTextOnMonitor(securityItem.ToString());
                }
            }

           ShowTextOnMonitor("Attack Unsuccessful by " + hackerName + ".");
        }

        public virtual void ImplementCommand(ConsoleCommand command)
        {
            m_computerState = ComputerStates.RUNNING_COMMAND;
            ShowTextOnMonitor("------------" + command + "------------");
            //Help - Lists all possible commands.
            if(command == ConsoleCommand.HELP)
            {
                foreach(ConsoleCommand consoleCommand in m_commandList)
                {
                    ShowTextOnMonitor(consoleCommand.ToString().ToLower(), true);
                }
            }

            //History - Shows all possible commands
            else if (command == ConsoleCommand.HISTORY)
            {

                foreach (string usedConsoleCommand in m_usedCommands)
                {
                    ShowTextOnMonitor(usedConsoleCommand, true);
                }
            }

            //Show Stats - Shows player stats
            else if(command == ConsoleCommand.SHOW_STATS)
            {
                ShowTextOnMonitor(this.ToString());
            }

            //Clear - Clears the console
            else if(command == ConsoleCommand.CLEAR)
            {
                ShowTextOnMonitor("");
            }

            //Stops the currently running command
            else if(command == ConsoleCommand.STOP)
            {
                ShowTextOnMonitor("Command stopped.");
                m_computerState = ComputerStates.IDLE;
            }

            else if (command == ConsoleCommand.EXIT)
            {
                ShowTextOnMonitor("Comming Soon!");

             //   SceneManager.LoadScene(m_exitSceneIndex);
            }

         
            //no need to add anything to the used commands list. The bridge adds it for us.
        }

        //todo: fix this method
        protected virtual void ShowTextOnMonitor(string text, bool addText = false)
        {
            object[] textData = new object[2];
            textData[0] = (string)text;
            textData[1] = (bool)addText;
          
            m_GameObject.SendMessage("ShowTextBridge", textData);
        }

        public override string ToString()
        {
            
            return m_stats;
        }

    }
}
