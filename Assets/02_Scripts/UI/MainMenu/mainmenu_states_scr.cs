using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu_states_scr : MonoBehaviour {


    // Enums
    enum MenuType { MENU_MAIN, MENU_OPTIONS, MENU_SHIP, MENU_MAP, MENU_INTRO };
    enum MainChoice { MAIN_PLAY, MAIN_OPTIONS, MAIN_QUIT };
    enum OptionsChoice { OPTIONS_SOUND, OPTIONS_MUSIC, OPTIONS_CALLIBRATE, OPTIONS_BACK};
    enum ShipChoice { SHIP_OFFWORLD, SHIP_XWING, SHIP_VERTICAL, SHIP_UFO, SHIP_PODRACER, SHIP_BACK};
    enum StageChoice { STAGE_SPACE, STAGE_CITY, STAGE_UNDERWATER, STAGE_BACK};
    

    // Selections
    MenuType currentMenu;
    MainChoice currentMain;
    OptionsChoice currentOption;
    ShipChoice currentShip;
    StageChoice currentStage;

    // Max lengths
    const int MAX_MAIN = 2, MAX_OPTIONS = 3, MAX_SHIP = 5, MAX_STAGE = 3;

    // Menu Objects
    GameObject[] menus;

    bool[] axis_moving;
    RectTransform[] text_positions;

    // Lerp data
    float lerpTime;
    struct LerpData
    {
        public Transform item;
        public Transform start;
        public Transform end;
    }
    List<LerpData> lerpsNeeded;
    public float lerpSpeed;


    void Start () {
        menus = new GameObject[5];
        menus[0] = transform.GetChild(1).gameObject;
        menus[1] = transform.GetChild(2).gameObject;
        menus[2] = transform.GetChild(3).gameObject;
        menus[3] = transform.GetChild(4).gameObject;
        menus[4] = transform.GetChild(5).gameObject;

        currentMenu = MenuType.MENU_INTRO;
        axis_moving = new bool[] {false, false, false, false};
        text_positions = new RectTransform[13];
        for( int i = 0; i < 13; i++ )
        {
            text_positions[i] = transform.GetChild(0).GetChild(i).GetComponent<RectTransform>();
        }

        currentMain = 0;
        currentOption = 0;
        currentShip = 0;
        currentStage = 0;

        lerpsNeeded = new List<LerpData>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateControls();
        UpdateLerp();
    }

    void Up()
    {
        Debug.Log("Up");
        int startIndex;
        if (currentMenu != MenuType.MENU_INTRO)
        {
            startIndex = FindPosition(menus[(int)currentMenu].transform.GetChild(0).position);
            Debug.Log(startIndex);

            switch (currentMenu)
            {
                case MenuType.MENU_MAIN:
                    if (currentMain - 1 >= 0)
                    {
                        currentMain--;
                        for(int i = 0; i <= MAX_MAIN; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i + 1];
                            lerpsNeeded.Add(newPos);
                        }
                        lerpTime = 0f;
                    }
                    break;
                case MenuType.MENU_OPTIONS:
                    if (currentOption - 1 >= 0)
                    {
                        currentOption--;
                        for (int i = 0; i <= MAX_OPTIONS; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i + 1];
                            lerpsNeeded.Add(newPos);
                        }
                        lerpTime = 0f;
                    }
                    break;
                case MenuType.MENU_SHIP:
                    if (currentShip - 1 >= 0)
                    {
                        currentShip--;
                        for (int i = 0; i <= MAX_SHIP; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i + 1];
                            lerpsNeeded.Add(newPos);
                        }
                        lerpTime = 0f;
                    }
                    break;
                case MenuType.MENU_MAP:
                    if (currentStage - 1 >= 0)
                    {
                        currentStage--;
                        for (int i = 0; i <= MAX_STAGE; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i + 1];
                            lerpsNeeded.Add(newPos);
                        }
                        lerpTime = 0f;
                    }
                    break;

            }
        }
        else
        {
            currentMenu = MenuType.MENU_MAIN;
            menus[(int)MenuType.MENU_INTRO].SetActive(false);
            menus[(int)MenuType.MENU_MAIN].SetActive(true);
        }
    }
    void Down()
    {
        Debug.Log("Down");
        int startIndex;
        if (currentMenu != MenuType.MENU_INTRO)
        {
            startIndex = FindPosition(menus[(int)currentMenu].transform.GetChild(0).position);
            switch (currentMenu)
            {
                case MenuType.MENU_MAIN:
                    if ((int)(currentMain + 1) <= MAX_MAIN)
                    {
                        currentMain++;
                        for (int i = 0; i <= MAX_MAIN; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i - 1];
                            lerpsNeeded.Add(newPos);
                            lerpTime = 0f;
                        }
                    }
                    break;
                case MenuType.MENU_OPTIONS:
                    if ((int)(currentOption + 1) <= MAX_OPTIONS)
                    {
                        currentOption++;
                        for (int i = 0; i <= MAX_OPTIONS; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i - 1];
                            lerpsNeeded.Add(newPos);
                            lerpTime = 0f;
                        }
                    }
                    break;
                case MenuType.MENU_SHIP:
                    if ((int)(currentShip + 1) <= MAX_SHIP)
                    {
                        currentShip++;
                        for (int i = 0; i <= MAX_SHIP; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i - 1];
                            lerpsNeeded.Add(newPos);
                            lerpTime = 0f;
                        }
                    }
                    break;
                case MenuType.MENU_MAP:
                    if ((int)(currentStage + 1) <= MAX_STAGE)
                    {
                        currentStage++;
                        for (int i = 0; i <= MAX_STAGE; i++)
                        {
                            LerpData newPos;
                            newPos.item = menus[(int)currentMenu].transform.GetChild(i);
                            newPos.start = text_positions[startIndex + i];
                            newPos.end = text_positions[startIndex + i - 1];
                            lerpsNeeded.Add(newPos);
                            lerpTime = 0f;
                        }
                    }
                    break;
            }
        }
    }
    void Press()
    {
        Debug.Log("Press");
        switch (currentMenu)
        {
            case MenuType.MENU_INTRO:
                currentMenu = MenuType.MENU_MAIN;
                menus[(int)MenuType.MENU_INTRO].SetActive(false);
                menus[(int)MenuType.MENU_MAIN].SetActive(true);
                break;
            case MenuType.MENU_MAIN:
                switch(currentMain)
                {
                    case MainChoice.MAIN_PLAY:
                        currentMenu = MenuType.MENU_MAP;
                        menus[(int)MenuType.MENU_MAIN].SetActive(false);
                        menus[(int)MenuType.MENU_MAP].SetActive(true);
                        break;
                    case MainChoice.MAIN_OPTIONS:
                        currentMenu = MenuType.MENU_OPTIONS;
                        menus[(int)MenuType.MENU_MAIN].SetActive(false);
                        menus[(int)MenuType.MENU_OPTIONS].SetActive(true); 
                        break;
                    case MainChoice.MAIN_QUIT:
                        Application.Quit();
                        break;
                }
                break;
            case MenuType.MENU_MAP:
                switch(currentStage)
                {
                    case StageChoice.STAGE_BACK:
                        menus[(int)MenuType.MENU_MAP].SetActive(false);
                        menus[(int)MenuType.MENU_MAIN].SetActive(true);
                        currentMenu = MenuType.MENU_MAIN;
                        break;
                    case StageChoice.STAGE_SPACE:
                        PlayerPrefs.SetInt("map", 0);
                        menus[(int)MenuType.MENU_MAP].SetActive(false);
                        menus[(int)MenuType.MENU_SHIP].SetActive(true);
                        currentMenu = MenuType.MENU_SHIP;
                        break;
                    case StageChoice.STAGE_CITY:
                        PlayerPrefs.SetInt("map", 1);
                        menus[(int)MenuType.MENU_MAP].SetActive(false);
                        menus[(int)MenuType.MENU_SHIP].SetActive(true);
                        currentMenu = MenuType.MENU_SHIP;
                        break;
                    case StageChoice.STAGE_UNDERWATER:
                        PlayerPrefs.SetInt("map", 2);
                        menus[(int)MenuType.MENU_MAP].SetActive(false);
                        menus[(int)MenuType.MENU_SHIP].SetActive(true);
                        currentMenu = MenuType.MENU_SHIP;
                        break;
                }
                break;
            case MenuType.MENU_SHIP:
                switch(currentShip)
                {
                    case ShipChoice.SHIP_BACK:
                        menus[(int)MenuType.MENU_SHIP].SetActive(false);
                        menus[(int)MenuType.MENU_MAP].SetActive(true);
                        currentMenu = MenuType.MENU_MAP;
                        break;
                    case ShipChoice.SHIP_OFFWORLD:
                        PlayerPrefs.SetInt("ship", 0);
                        ChangeScene();
                        break;
                    case ShipChoice.SHIP_XWING:
                        PlayerPrefs.SetInt("ship", 1);
                        ChangeScene();
                        break;
                    case ShipChoice.SHIP_VERTICAL:
                        PlayerPrefs.SetInt("ship", 2);
                        ChangeScene();
                        break;
                    case ShipChoice.SHIP_UFO:
                        PlayerPrefs.SetInt("ship", 3);
                        ChangeScene();
                        break;
                    case ShipChoice.SHIP_PODRACER:
                        PlayerPrefs.SetInt("ship", 4);
                        ChangeScene();
                        break;
                }
                break;
            case MenuType.MENU_OPTIONS:
                switch(currentOption)
                {
                    case OptionsChoice.OPTIONS_BACK:
                        menus[(int)MenuType.MENU_OPTIONS].SetActive(false);
                        menus[(int)MenuType.MENU_MAIN].SetActive(true);
                        currentMenu = MenuType.MENU_MAIN;
                        break;
                    case OptionsChoice.OPTIONS_SOUND:
                        break;
                    case OptionsChoice.OPTIONS_MUSIC:
                        break;
                    case OptionsChoice.OPTIONS_CALLIBRATE:
                        break;
                }
                break;
        }
    }

    int FindPosition(Vector3 pos)
    {
        int i = 0;
        while (pos.y < text_positions[i].position.y)
        {
            i++;
        }
        return i;
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
    void UpdateLerp()
    {
        if (lerpTime < 1.0f) {
            lerpTime += Time.deltaTime * lerpSpeed ;
            for(int i = 0; i < lerpsNeeded.Count; i++)
            {
                lerpsNeeded[i].item.localPosition = Vector3.Lerp(lerpsNeeded[i].start.localPosition, lerpsNeeded[i].end.localPosition, lerpTime);
                lerpsNeeded[i].item.localScale = Vector3.Lerp(lerpsNeeded[i].start.localScale, lerpsNeeded[i].end.localScale, lerpTime);
            }
        }
        else if(lerpsNeeded.Count > 0)
        {
            lerpsNeeded.Clear();
        }
    }

    void ChangeScene()
    {
        Debug.Log("Change scene");
    }
}
