using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class enemyNetworkController : MonoBehaviour {

    private GameObject networkManager;

    // Use this for initialization
    void Start ()
    {

        networkManager = GameObject.Find("NetworkManager");
    
    }

    // Update is called once per frame
    void Update()
    { 
        movementUpdate();	
	}


    void movementUpdate()
    {
        AllPlayerData enemyData = networkManager.GetComponent<networkManager>().allPlayerData;

        Vector3 updatedMovement; 

        //Determine if player 1 or 2 is the enemy and update enemy to that position
        if (networkManager.GetComponent<networkManager>().playerID == "")
        {
            updatedMovement = new Vector3(enemyData.player2x, enemyData.player2y, enemyData.player2z);
        }
        else
        {
            updatedMovement = new Vector3(enemyData.player1x, enemyData.player1y, enemyData.player1z);
        }

        transform.position = updatedMovement;

    }
   
}
