using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

        //Constructor 
        public AbstractHackerComputer(string owner, HackerLevel hackerLevel, GameObject gameObject, int sceneIndex, int CPUs = B_DEFAULT_CPUs, int memory = B_DEFAULT_MEMORY, SecurityLevel securityLevel = B_DEFAULT_SECURITY_LEVEL) 
            : base(owner, gameObject, sceneIndex, CPUs,memory,securityLevel)
        {
            m_hackerLevel = hackerLevel;
           
            m_commandList.Add(ConsoleCommand.SHOW_ATTACKS);
            
        }


    }
}
