using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjSpawnerBehaviour : MonoBehaviour {
    public GameObject pnj;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((int)Random.Range(0,60) == 30)
        {
            Instantiate(pnj, transform.position, transform.rotation);
        }
	}
}
