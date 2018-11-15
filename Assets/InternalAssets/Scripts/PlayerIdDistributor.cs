using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerIdDistributor : MonoBehaviour
{
    public int PlayerId;// { get { return m_playerId; } private set { m_playerId = value; } }

    //private int m_playerId;
    //private static int playerIdGenerator = 0;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //playerIdGenerator = 0;
    }

    // Use this for initialization
    void Start ()
    {
        //m_playerId = playerIdGenerator;
        //++playerIdGenerator;
        //ScoreManager.RegisterPlayer(m_playerId);
    }
}
