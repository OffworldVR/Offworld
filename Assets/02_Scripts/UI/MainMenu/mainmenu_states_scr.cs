using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu_states_scr : MonoBehaviour {

    // Use this for initialization
    enum MenuType { MENU_MAIN, MENU_OPTIONS, MENU_SHIP, MENU_MAP, MENU_INTRO };
    MenuType currentMenu;
    bool[] axis_moving;
	void Start () {
        currentMenu = MenuType.MENU_INTRO;
        axis_moving = new bool[] {false, false, false, false};
	}
	
	// Update is called once per frame
	void Update () {
        UpdateControls();
    }

    void Up()
    {
        Debug.Log("Up");
    }
    void Down()
    {
        Debug.Log("Down");
    }
    void Press()
    {
        Debug.Log("Press");
    }

    void UpdateControls()
    {
        // Up button
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0 && !axis_moving[0])
        {
            axis_moving[0] = true;
            Up();
        }
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y > 0 && !axis_moving[2])
        {
            axis_moving[2] = true;
            Up();
        }

        // Down button
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < 0 && !axis_moving[1])
        {
            axis_moving[1] = true;
            Down();
        }
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y < 0 && !axis_moving[3])
        {
            axis_moving[3] = true;
            Down();
        }
        // Press button
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
        {
            Press();
        }

        // Reset axis_moving
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y <= 0 && axis_moving[0])
        {
            axis_moving[0] = false;
        }
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y <= 0 && axis_moving[2])
        {
            axis_moving[2] = false;
        }
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y >= 0 && axis_moving[1])
        {
            axis_moving[1] = false;
        }
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y >= 0 && axis_moving[3])
        {
            axis_moving[3] = false;
        }
    }
}
