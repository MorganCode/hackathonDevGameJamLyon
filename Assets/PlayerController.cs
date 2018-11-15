using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Use last device which provided input.
        var inputDevice = InputManager.ActiveDevice;

		float x = inputDevice.RightStickX * Time.deltaTime * 150.0f;
		//float y = 0;
		float z = inputDevice.LeftStickY * Time.deltaTime * 3.0f;


		transform.Rotate(0, x, 0);
		transform.Translate(0,0, z);
    }
}
