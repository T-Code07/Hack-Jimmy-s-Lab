using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Hacker.Computer;

public class GameManager : NetworkBehaviour
{
    public List<GameObject> m_players = new List<GameObject>();
    [SerializeField] GameObject m_fps_view;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnPlayerConnected(NetworkIdentity player)
    {
        print("New player has arrived!!");
        foreach(ComputerCreator computerCreator in FindObjectsOfType<ComputerCreator>())
        {
            m_players.Add(computerCreator.gameObject);
        }
        foreach(GameObject playerItem in m_players)
        {
            print(playerItem.ToString());
        }
    }
    private void Update()
    {
       Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 1)
        {
            NetworkServer.Spawn(m_fps_view);
        }
    }

}
