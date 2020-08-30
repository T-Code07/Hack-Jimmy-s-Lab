using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Hacker.Computer;


namespace Hacker.Computer.Core
{
     public enum HackerLevel
    {
        NONE,
        NOOB,
        BEGINNER,
        INTERMEDIATE,
        ADVANCED
    }

    public class AbstractHackerComputer : AbstractComputer
    {

        private HackerLevel m_hackerLevel;
        private NetworkIdentity m_networkIdentity;
        //Constructor 
        public AbstractHackerComputer(string owner, HackerLevel hackerLevel, GameObject gameObject, int sceneIndex, int CPUs = B_DEFAULT_CPUs, int memory = B_DEFAULT_MEMORY, SecurityLevel securityLevel = B_DEFAULT_SECURITY_LEVEL) 
            : base(owner, gameObject, sceneIndex, CPUs,memory,securityLevel)
        {
            m_hackerLevel = hackerLevel;
           
            m_commandList.Add(ConsoleCommand.SHOW_ATTACKS);
            m_commandList.Add(ConsoleCommand.SCAN);
            m_commandList.Add(ConsoleCommand.ATTACK);
            
        }


        public override void ImplementCommand(ConsoleCommand command)
        {
            base.ImplementCommand(command);

            if(command == ConsoleCommand.SCAN)
            {
                Scan();
            }

            if(command == ConsoleCommand.ATTACK)
            {
                Scan(true);
            }

        }

        //[Command]
        void Scan(bool moreData = false)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            foreach(GameObject player in gameManager.m_playerList)
            {
                ComputerCreator computerCreator = player.GetComponent<ComputerCreator>();

                if (computerCreator.m_computerOwner == Owner) return;

                if (moreData)
                {
                    base.ShowTextOnMonitor(player.GetComponent<AbstractHackerComputer>().ToString(), true);
                }
                else
                {
                    base.ShowTextOnMonitor(computerCreator.m_computerOwner, true);

                }
            }
        }

      

    }
}
