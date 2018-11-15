using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavior : MonoBehaviour
{
    public GameObject playerBaseOwner;

    private ReaperBehavior playerReaperCpt;

    public void Start()
    {
        playerReaperCpt = playerBaseOwner.GetComponent<ReaperBehavior>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerBaseOwner)
        {
            ScoreManager.ChangePlayerScoreBy(playerBaseOwner.GetComponent<PlayerIdDistributor>().PlayerId, playerReaperCpt.gatheredSouls);
            playerReaperCpt.gatheredSouls = 0;
        }
    }
}
