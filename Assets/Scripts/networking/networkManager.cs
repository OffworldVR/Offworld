using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;



public class networkManager : MonoBehaviour {

    public int playerID = 0;
    public string allPlayerDataString;

    void Start () {
        
        StartCoroutine(joinGame());

        StartCoroutine(GetNetworkData());

    }

    void Update () {
		
	}

    IEnumerator joinGame()
    {
            UnityWebRequest www = UnityWebRequest.Get("http://172.21.79.81/serverManager/addPlayer");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Assign the response to playerID
                int playerID = Int32.Parse(www.downloadHandler.text);

            Debug.Log("Player " + Int32.Parse(www.downloadHandler.text) + "has been added successfully");
            }
        
    }

    IEnumerator GetNetworkData()
    {

        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get("http://172.21.79.81/serverManager/refresh");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                allPlayerDataString = www.downloadHandler.text;

                var parsedJSON = JSON.Parse(www.downloadHandler.text);

                Debug.Log("Player " + parsedJSON["playerID"] +  " Data: " + parsedJSON);
           

            }
        }
    }
}
