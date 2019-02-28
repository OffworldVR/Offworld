using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MenuScript : MonoBehaviour {

    string[] mapNames;
    int chosenMap;
    int chosenShip;
    bool started;
    public GameObject[] MenuScreens;

	// Use this for initialization
	void Start () {
        UnityEngine.XR.InputTracking.Recenter();
        started = false;
        mapNames = new string[3];
        mapNames[0] = "Proxima";
        mapNames[1] = "Angler";
        mapNames[2] = "Elysium";
        chosenMap = -1;
        chosenShip = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if(!started)
        {
            if(OVRInput.GetDown(OVRInput.Button.One))
            {
                MenuScreens[0].SetActive(false);
                MenuScreens[1].SetActive(true);
                started = true;
            }
        }
	}

    public void StageSelect(int i)
    {
        chosenMap = i;
    }

    public void ShipSelect(int i)
    {
        chosenShip = i;
    }

    public void LoadStage()
    {
        if(chosenMap != -1)
        {
            SceneManager.LoadScene(mapNames[chosenMap]);
        }
    }
    public void SelectShip()
    {
        PlayerPrefs.SetInt("ship", chosenShip);
    }

    public void MusicVol(UnityEngine.UI.Slider val)
    {
        PlayerPrefs.SetFloat("musicVolume", val.value);
    }

    public void SoundVol(UnityEngine.UI.Slider val)
    {
        PlayerPrefs.SetFloat("soundsVolume", val.value);
    }

    public void SetCallibratedPos()
    {
        UnityEngine.XR.InputTracking.Recenter();
    }

}
