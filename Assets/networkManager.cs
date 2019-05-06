using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class AllPlayerData
{
    public string player1;
    public string player2;


    public float player1xPos;
    public float player1yPos;
    public float player1zPos;
    public float player1xRot;
    public float player1yRot;
    public float player1zRot;

    public float player2xPos;
    public float player2yPos;
    public float player2zPos;
    public float player2xRot;
    public float player2yRot;
    public float player2zRot;


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
