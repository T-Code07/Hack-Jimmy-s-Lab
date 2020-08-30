using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Hacker.Computer;

public class GameManager : NetworkBehaviour
{
    
    public GameObject[] m_playerList = new GameObject[10];

    public Dictionary<string, GameObject> m_playerIDTags = new Dictionary<string, GameObject>();
     

    [SerializeField] GameObject m_fps_view;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    private void Update()
    {
        
        m_playerList = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in m_playerList)
            {
                ComputerCreator NewComputerCreator = player.GetComponent<ComputerCreator>();

                //return if dic already has this id tag.
                if (m_playerIDTags.ContainsKey(NewComputerCreator.ID)) return;

                //Add to dic
                m_playerIDTags.Add(NewComputerCreator.ID, player);

                if (m_playerList.Length == 1)
                {
                    print("Added player to server: " + NewComputerCreator.m_computerOwner);
                    return;
                }
                foreach (GameObject playerItem in m_playerList)
                {
                    ComputerCreator currentComputerCreator = playerItem.GetComponent<ComputerCreator>();
                    if (NewComputerCreator.m_computerOwner == currentComputerCreator.m_computerOwner && NewComputerCreator.ID != currentComputerCreator.ID)
                    {
                        NewComputerCreator.m_computerOwner += Time.deltaTime.ToString();
                    }
                }

          //  NewComputerCreator.createComputer();
                print("Added player to server: " + NewComputerCreator.m_computerOwner);

            
        }

        print(m_playerIDTags.Count);
      
    }

}
