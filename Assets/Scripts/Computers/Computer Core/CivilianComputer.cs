using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hacker.Computer.Core
{
    public class CivilianComputer : AbstractComputer
    {

        //Constructor 
        public CivilianComputer(string owner,  GameObject gameObject, int sceneIndex, int CPUs = B_DEFAULT_CPUs, int memory = B_DEFAULT_MEMORY, SecurityLevel securityLevel = B_DEFAULT_SECURITY_LEVEL)
            : base(owner,  gameObject, sceneIndex, CPUs, memory, securityLevel)
        {
            print("Look who has a computer now!");

        }
    }
}
