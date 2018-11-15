using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSphereBehavior : MonoBehaviour
{
    public float stunTime;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovementsBehavior>().StunForSeconds(stunTime);
        }
    }
}
