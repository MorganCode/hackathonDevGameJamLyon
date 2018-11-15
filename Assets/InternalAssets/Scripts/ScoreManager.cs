using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int victoryScoreEditorVariable;

    public static int VictoryScore;
    public static Dictionary<int, int> playerScores;
    private static float sliderValuePerSoul; 

    private void Awake()
    {
        playerScores = new Dictionary<int, int>();
        for (int i = 0; i < 3; ++i)
            playerScores.Add(i, 0); // HACK
    }

    // Use this for initialization
    void Start ()
    {
        VictoryScore = victoryScoreEditorVariable;
        sliderValuePerSoul = 1.0f / VictoryScore;
    }

    public static int GetPlayerScore(int m_playerId)
    {
        return playerScores[m_playerId];
    }

    public static void ChangePlayerScoreBy(int m_playerId, int numberOfSoul)
    {
        playerScores[m_playerId] += numberOfSoul;
        HudManager.IncrementScoreSliderValue(m_playerId, sliderValuePerSoul * numberOfSoul);
        if (playerScores[m_playerId] >= VictoryScore)
        {
            SceneLoader.StaticLoadScene("Victory");
            //Time.timeScale = 0.0f;
            //Debug.Log("WIN SCREEN, win for Player "+m_playerId+1);
        }
    }

}
