using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu_states_scr : MonoBehaviour {

    // Use this for initialization
    enum MenuType { MENU_MAIN, MENU_OPTIONS, MENU_SHIP, MENU_MAP, MENU_INTRO };
    MenuType currentMenu;
    bool[] axis_moving;
    RectTransform[] text_positions;
	void Start () {
        currentMenu = MenuType.MENU_INTRO;
        axis_moving = new bool[] {false, false, false, false};
        text_positions = new RectTransform[13];
        for( int i = 0; i < 13; i++ )
        {
            text_positions[i] = transform.GetChild(0).GetChild(i).GetComponent<RectTransform>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdateControls();
    }

    void Up()
    {
        Debug.Log("Up");
        switch(currentMenu)
        {

        }
    }
    void Down()
    {
        Debug.Log("Down");
        switch (currentMenu)
        {

        }
    }
    void Press()
    {
        Debug.Log("Press");
        switch (currentMenu)
        {

        }
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
