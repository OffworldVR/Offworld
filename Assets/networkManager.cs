using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class AllPlayerData
{
    public string player1;
    public string player2;


    public float player1x;
    public float player1y;
    public float player1z;

    public float player2x;
    public float player2y;
    public float player2z;



}

public class networkManager : MonoBehaviour {

    public string playerID = "NULL";
    public AllPlayerData allPlayerData;

    void Start () {
        
        StartCoroutine(joinGame());

        StartCoroutine(GetNetworkData());

    }

    void Update () {
		
	}

    IEnumerator joinGame()
    {
            UnityWebRequest www = UnityWebRequest.Get("http://172.21.79.81/gameManager/addPlayer");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Assign the response to playerID
                playerID = www.downloadHandler.text;

            Debug.Log("Player " + www.downloadHandler.text + "has been added successfully");
            }
        
    }

    IEnumerator GetNetworkData()
    {

        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get("http://172.21.79.81/gameManager/refresh");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);


                JsonUtility.FromJsonOverwrite(www.downloadHandler.text, allPlayerData);
                Debug.Log("Player 2: " + allPlayerData.player2);
           

            }
        }
    }
}
