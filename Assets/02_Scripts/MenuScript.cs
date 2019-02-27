using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    string[] mapNames;
    int chosenMap;
    int chosenShip;

	// Use this for initialization
	void Start () {
        mapNames = new string[3];
        mapNames[0] = "Proxima";
        mapNames[1] = "Angler";
        mapNames[2] = "Elysium";
        chosenMap = -1;
        chosenShip = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
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
        PlayerPrefs.SetInt("Ship", chosenShip);
    }

    public void SetCallibratedPos()
    {
        UnityEngine.XR.InputTracking.Recenter();
    }

}
