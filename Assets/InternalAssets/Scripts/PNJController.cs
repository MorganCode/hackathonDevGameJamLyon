using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJController : MonoBehaviour {
   
    
    public GameObject soul;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        float amplitude = Random.Range(-20f, 20f);
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(0, amplitude, 0));
        transform.Translate(3f * Time.deltaTime,0,0);
    }

    private void OnDestroy()
    {
        
        soul.transform.position = transform.position;
        Instantiate(soul);
        
    }
}
