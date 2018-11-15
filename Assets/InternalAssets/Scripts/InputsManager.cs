using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : MonoBehaviour
{
    //private static bool INPUTS_MANAGER_DEBUG = false;

    static public Dictionary<int, InputTable> playerInputsDictionary;
    delegate void OnControllerDetached(int controllerID);
    delegate void OnControllerAttached(int controllerID);

    // Use this for initialization
    void Awake()
    {
        ClearplayerInputsDictionary();
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        foreach (KeyValuePair<int, InputTable> entry in InputsManager.playerInputsDictionary)
        {
            entry.Value.UpdateControls();
        }
    }

    //void FixedUpdate()
    //{
    //    foreach (KeyValuePair<int, InputTable> entry in InputsManager.playerInputsDictionary)
    //    {
    //        entry.Value.UpdateControls();
    //    }
    //}

    //void RegisterToAttachedControllerEvent()
    //{

    //}

    //void RegisterToDetachedControllerEvent()
    //{

    //}

    //void OnControllerDetached()
    //{

    //}

    //void OnControllerAttached()
    //{

    //}

    public static bool IsControllerIdUsed(int controllerId_)
    {
        foreach (KeyValuePair<int, InputTable> entry in InputsManager.playerInputsDictionary)
        {
            if (entry.Value.controllerId == controllerId_)
                return true;
        }

        return false;
    }

    public static void ClearplayerInputsDictionary()
    {
        playerInputsDictionary = new Dictionary<int, InputTable>();
        for (int i = 0; i < 3; ++i)
            playerInputsDictionary.Add(i, new InputTable()); // HACK
    }

    public static bool AnyPauseButtonDown()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (playerInputsDictionary[i].controllerId != -1 && playerInputsDictionary[i].PauseDown)
                return true;
        }
        return false;
    }
}


public class InputTable
{
    private static bool INPUTS_TABLE_DEBUG = false;

    public int controllerId;

    public float LeftAnalogForwardAxis = 0.0f; // X Axis && buttons
    public float LeftAnalogStrafeAxis = 0.0f; // Y Axis && buttons
    public float RightAnalogXAxis = 0.0f;
    public float RightAnalogYAxis = 0.0f;
    public bool DashDown = false;
    public bool DashPressed = false;
    public bool PauseDown = false;
    public bool PausePressed = false;
    public bool AttackSphereDown = false;
    public bool AttackSpherePressed = false;
    public bool AttackSphereReleased = false;

    private InControl.InputDevice inControlDevice;

    public InputTable()
    { controllerId = -1; }

    public void SetControllerId(int controllerId_)
    {
        controllerId = controllerId_;
        inControlDevice = InControl.InputManager.Devices[controllerId];
    }

    public void UpdateControls()
    {
        if (inControlDevice == null || controllerId == -1)
        {
            if (INPUTS_TABLE_DEBUG)
                Debug.Log("Unmapped controller");
            return;
        }
        else
        {
            LeftAnalogForwardAxis = inControlDevice.GetControl(InControl.InputControlType.LeftStickY).RawValue;
            LeftAnalogStrafeAxis = inControlDevice.GetControl(InControl.InputControlType.LeftStickX).RawValue;
            RightAnalogXAxis = inControlDevice.GetControl(InControl.InputControlType.RightStickX).RawValue;
            RightAnalogYAxis = inControlDevice.GetControl(InControl.InputControlType.RightStickY).RawValue;

            DashDown = (inControlDevice.GetControl(InControl.InputControlType.RightBumper).IsPressed);
            DashPressed = (inControlDevice.GetControl(InControl.InputControlType.RightBumper).WasPressed);

            PauseDown = inControlDevice.GetControl(InControl.InputControlType.Start).IsPressed;
            PausePressed = inControlDevice.GetControl(InControl.InputControlType.Start).WasPressed;

            AttackSphereDown = (inControlDevice.GetControl(InControl.InputControlType.LeftBumper).IsPressed);
            AttackSpherePressed = (inControlDevice.GetControl(InControl.InputControlType.LeftBumper).WasPressed);
            AttackSphereReleased = (inControlDevice.GetControl(InControl.InputControlType.LeftBumper).WasReleased);

            if (INPUTS_TABLE_DEBUG)
            {
                Debug.Log("------------ INPUT DEBUG ------------");
                Debug.Log("LeftAnalogForwardAxis : " + LeftAnalogForwardAxis);
                Debug.Log("LeftAnalogStrafeAxis : " + LeftAnalogStrafeAxis);
                //Debug.Log("RightAnalogXAxis : " + RightAnalogXAxis);
                //Debug.Log("RightAnalogYAxis : " + RightAnalogYAxis);
                //Debug.Log("Dash : " + Dash);
                //Debug.Log("Pause : " + Pause);
            }
        }
    }
}