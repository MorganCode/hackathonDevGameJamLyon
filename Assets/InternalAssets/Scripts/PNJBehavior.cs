using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PNJBehavior : MonoBehaviour
{
    [SerializeField]
    private float targetTime = 1f;
    [SerializeField]
    private GameObject soul;

    //private static bool PNJBehavior_D   EBUG = false;

    private Vector3 positionDestination;
    private NavMeshAgent agent;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private Vector3 player1Position;
    private Vector3 player2Position;
    private Vector3 player3Position;
    private Vector3 positionPNJ;
    private Rigidbody rb;

    // MAP : x -20  20   z : -10  17
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player3 = GameObject.Find("Player3");

        positionDestination = getPointRandom(-20f, 20f, -10f, 17f);
    }

    // Update is called once per frame
    void Update()
    {
        // Getting positions
        positionPNJ = agent.transform.position;
        player1Position = player1.transform.position;
        player2Position = player2.transform.position;
        player3Position = player3.transform.position;

        // timer to prevent pnj from being stuck to each others (ish)
        if (rb.velocity.magnitude < 0.1f)
        {
            if (targetTime <= 0)
            {
                float random = Random.Range(0, 10);
                if (agent.transform.position.x<0) // Gauche
                {
                    if (random < 4)
                    {
                        agent.SetDestination(getPointRandomValide(-20f, 0f, 4f, 17f));
                        targetTime = 1f;
                        return;
                    }
                    else
                    {
                        agent.SetDestination(getPointRandomValide(-20f, -1f, -10f, 3f));
                        targetTime = 1f;
                        return;
                    }
                    
                }
                else // Droite
                {
                    if (random < 4)
                    {
                        agent.SetDestination(getPointRandomValide(3f, 20f, 4f, 17f));
                        targetTime = 1f;
                        return;
                    }
                    else
                    {
                        agent.SetDestination(getPointRandomValide(3f, 20f, -10f, 3f));
                        targetTime = 1f;
                        return;
                    }
                }
            }
            targetTime -= Time.deltaTime;
        }
        else
        {
            targetTime = 1f;
        }

        // Verifying security distance from players
        float distanceJoueur1 = Vector3.Distance(player1Position, positionPNJ);
        float distanceJoueur2 = Vector3.Distance(player2Position, positionPNJ);
        float distanceJoueur3 = Vector3.Distance(player3Position, positionPNJ);
        if (distanceJoueur1 < 2 || distanceJoueur2 < 2 || distanceJoueur3 < 2)
        {
            if (distanceJoueur1 < 2)
            {
                float x = player1Position.x + (positionPNJ.x - player1Position.x) * 2;
                float z = player1Position.z + (positionPNJ.z - player1Position.z) * 2;
                positionDestination = new Vector3(x, 1, z);
            }
            if (distanceJoueur2 < 2)
            {
                float x = player2Position.x + (positionPNJ.x - player2Position.x) * 2;
                float z = player2Position.z + (positionPNJ.z - player2Position.z) * 2;
                positionDestination = new Vector3(x, 1, z);
            }
            if (distanceJoueur3 < 2)
            {
                float x = player3Position.x + (positionPNJ.x - player3Position.x) * 2;
                float z = player3Position.z + (positionPNJ.z - player3Position.z) * 2;
                positionDestination = new Vector3(x, 1, z);
            }

            // Si il est impossible d'éviter l'eventuel conflit du point de destination
            NavMeshHit hit2;
            if (!NavMesh.SamplePosition(positionDestination, out hit2, 1.0f, NavMesh.AllAreas))
            {
                positionDestination = getPointRandomValide(-20f, 20f, -10f, 17f);
            }
            else
            {   // évite l'éventuel conflit (ne fait rien si pas de confilt)
                NavMesh.SamplePosition(positionDestination, out hit2, 1.0f, NavMesh.AllAreas);
                positionDestination = hit2.position;
            }
        }

        //Debug.Log("Distance :" + Vector3.Distance(positionDestination, positionPNJ));
        if (Vector3.Distance(positionDestination, positionPNJ) < 2)
        {
            positionDestination = getPointRandomValide(-20f, 20f, -10f, 17f);
        }
        agent.SetDestination(positionDestination);
    }
    public Vector3 getPointRandom(float xmin, float xmax, float zmin, float zmax)
    {
        float x = Random.Range(xmin, xmax);
        float z = Random.Range(zmin, zmax);
        return new Vector3(x, 0, z);
    }

    public Vector3 getPointRandomValide(float xmin, float xmax, float zmin, float zmax)
    {
        NavMeshHit hit;
        bool result = false;
        int cpt = 0;
        Vector3 pointRandom = getPointRandom(xmin, xmax, zmin, zmax);
        while (result == false && cpt < 100)
        {
            result = NavMesh.SamplePosition(pointRandom, out hit, 1.0f, NavMesh.AllAreas);
            if (result)
            {
                pointRandom = hit.position;
            }
            else
            {
                pointRandom = getPointRandom(xmin, xmax, zmin, zmax);
            }
            cpt++;
        }

        return pointRandom;
    }

    //private void OnDestroy()
    //{
    //    soul.transform.position = transform.position;
    //    Instantiate(soul);
    //}
}