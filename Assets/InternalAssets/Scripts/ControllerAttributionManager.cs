using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class ControllerAttributionManager : MonoBehaviour
{
    private bool CONTROLLERATTRIBUTIONMANAGER_DEBUG = true;

    private GameObject[] playersStatusGO = new GameObject[3];
    private GameObject[] playersPressStartGO = new GameObject[3];
    private int currentPlayerAttribution;

    void Awake()
    {
        playersStatusGO[0] = GameObject.Find("Player1StatusText");
        playersStatusGO[1] = GameObject.Find("Player2StatusText");
        playersStatusGO[2] = GameObject.Find("Player3StatusText");

        playersPressStartGO[0] = GameObject.Find("Player1PressStartText");
        playersPressStartGO[1] = GameObject.Find("Player2PressStartText");
        playersPressStartGO[2] = GameObject.Find("Player3PressStartText");
    }

	// Use this for initialization
	void Start ()
    {
        InputsManager.ClearplayerInputsDictionary();

        currentPlayerAttribution = 0;

        playersStatusGO[0].SetActive(false);
        playersStatusGO[1].SetActive(false);
        playersStatusGO[2].SetActive(false);

        playersPressStartGO[1].SetActive(false);
        playersPressStartGO[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CONTROLLERATTRIBUTIONMANAGER_DEBUG && Input.GetKey(KeyCode.Space))
        {
            Destroy(GameObject.Find("MainMenuMusic"));
            SceneLoader.StaticLoadScene("Level1_Blender");
        }

        for (int i = 0 ; i < InputManager.Devices.Count; ++i)
        {
            if (InputManager.Devices[i].CommandIsPressed)
            {
                if (currentPlayerAttribution <= 2) // still mapping controllers
                {
                    if (!InputsManager.IsControllerIdUsed(i))
                    {
                        Debug.Log("setting controller number : " + i);
                        if (!InputsManager.playerInputsDictionary.ContainsKey(currentPlayerAttribution))
                            InputsManager.playerInputsDictionary[currentPlayerAttribution] = new InputTable();
                        InputsManager.playerInputsDictionary[currentPlayerAttribution].SetControllerId(i);
                        ++currentPlayerAttribution;
                        playersPressStartGO[currentPlayerAttribution - 1].SetActive(false);
                        if (currentPlayerAttribution <= 2)
                            playersPressStartGO[currentPlayerAttribution].SetActive(true);
                        playersStatusGO[currentPlayerAttribution - 1].SetActive(true);
                    }
                }
                else
                {
                    Destroy(GameObject.Find("MainMenuMusic"));
                    SceneLoader.StaticLoadScene("Level1_Blender");
                }
            }
        }
    }
}
