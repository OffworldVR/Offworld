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

        Vector3 updatedMovement,updatedRotation; 


        //Determine if player 1 or 2 is the enemy and update enemy to that position
        if (networkManager.GetComponent<networkManager>().playerID == "1")
        {
            updatedMovement = new Vector3(enemyData.player2xPos, enemyData.player2yPos, enemyData.player2zPos);
            updatedRotation = new Vector3(enemyData.player2xRot, enemyData.player2yRot, enemyData.player2zRot);

        }
        else
        {
            updatedMovement = new Vector3(enemyData.player1xPos, enemyData.player1yPos, enemyData.player1zPos);
            updatedRotation = new Vector3(enemyData.player1xRot, enemyData.player1yRot, enemyData.player1zRot);

        }

        transform.position = updatedMovement;
        transform.eulerAngles = updatedRotation;
    }
   
}
