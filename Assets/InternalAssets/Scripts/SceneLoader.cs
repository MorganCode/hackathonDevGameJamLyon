using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
 //   void Start ()
 //   {
	//}
	
	// Update is called once per frame
	//void Update ()
 //   {
	//}

    public static void StaticLoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    // Because Unity is still a big dum dum and cannot link button press to static functions
    public void LoadScene(string name)
    {
        StaticLoadScene(name);
    }
}
