using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class PlayerData
{
    public string player1;
    public string player2;
    public float player2x;
    public float player2y;
    public float player2z;
}


public class enemyNetworkController : MonoBehaviour {

    public PlayerData enemyData;

    // Use this for initialization
    void Start () {
        enemyData.player1 = "EMPTY";
        enemyData.player2 = "EMPTY";

        StartCoroutine(GetNetworkData());

    }

    // Update is called once per frame
    void Update()
    { 


        movementUpdate();	
	}


    void movementUpdate()
    {
        Vector3 updatedMovement = new Vector3(enemyData.player2x, enemyData.player2y, enemyData.player2z);
        transform.position = updatedMovement;

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


                JsonUtility.FromJsonOverwrite(www.downloadHandler.text, enemyData);
                Debug.Log(enemyData.player2z);
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}
