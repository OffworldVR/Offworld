﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INSTRUCTIONS TO ADD A NEW ITEM
//    Step 1: Create a new method with no parameters and returns void
//    Step 2: Write what you want the item to do
//    Step 3: Call deactivateItem() after the action of your item has been performed
//    Step 4: Add another if else statement to ItemSelector()   See Other
//    Step 4: Add 1 to the value of totalItems

//    Other: Use playerScript.leftTriggerisTriggered as the button to activate the item   See: Laser()
//           ActiveItem is a delegate variable which holds a pointer to a function, the current item
//           When the player ship enters an item block ItemSelector will be called and ActiveItem randomly assigned



public class itemPrefabSpawnController : MonoBehaviour {

    private float DRILL_OFFSET = 3f;
    //Item prefabs
    public GameObject minePrefab;
    public GameObject blackHoleBombPrefab;
    public GameObject misslePrefab;
    public GameObject asteroidDrillPrefab;

    public GameObject laser1;
    public GameObject laser2;
    private PlayerScript playerScript; //this used to be static if something breaks then dont blame sammy, but instead add back
    

    //Time the player can use the laser after the item is activated
    public const int laserActiveTime = 4;

    //Number of Items the player can get
    private static int totalItems = 4;//changed from 5 to 4 to exclude laser

    //Current active Item
    public int ActiveItem = 0;
    //Item 0 = None
    //Item 1 = Laser
    //Item 2 = Mine
    //Item 3 = BlackHoleBomb
    //Item 4 = Missle
    //Item 5 = AsteroidDrill
    private void Awake()
    {
        //instance = this;
    }

    void Start()
    {



    }



public void ItemSelector()
    {
        //Set active item to the number in the list
        ActiveItem = Random.Range(2, (totalItems));//changed range from 1 to 2 and decreased end by 1 to exlucde laser


        //Functions you want to only run once when the player activates the Item

        //Deactive the Laser in 4 seconds
        if(ActiveItem == 1)
        {
            Invoke("deactivateItem", laserActiveTime);
        }

  
    }

    public void ActivateItem()
    {
        if(ActiveItem == 1)
        {
            laserController();
        }
        if(ActiveItem == 2)
        {
            mineController();
        }
        if(ActiveItem == 3)
        {
            blackHoleBombController();      
        }
        if(ActiveItem == 4)
        {
            missleController();
        }
        if(ActiveItem == 5)
        {
            asteroidDrillController();
        }
        return;
    }
    





    public void laserController()
    {
        if (triggeringItem())
        {
            Debug.Log("Laser Fired");
            laser1.SetActive(true);
            laser2.SetActive(true);
        }
        else
        {
            laser1.SetActive(false);
            laser2.SetActive(false);
        }
        deactivateItem();
    }

    public void mineController()
    {

        if (triggeringItem())
        {
            Debug.Log("Mine Fired");
            GameObject temp = Instantiate(minePrefab, transform.position, Quaternion.identity);
            //Add parentShip property to the instantiated Item
            //temp.GetComponent<baseItem>().parentShip = gameObject;

            deactivateItem();
        }
      
    }

    public void blackHoleBombController()
    {
        if (triggeringItem())
        {
            Debug.Log("Black Hole Bomb Fired");
            GameObject temp = Instantiate(blackHoleBombPrefab, transform.position, transform.rotation);
            //Add parentShip property to the instantiated Item
            //temp.GetComponent<baseItem>().parentShip = gameObject;

            
            deactivateItem();
        }
    }
    public void missleController()
    {
        if (triggeringItem())
        {
            Debug.Log("Missle Fired");
            GameObject temp = Instantiate(misslePrefab, transform.position, transform.rotation);
            temp.AddComponent<Rigidbody>();
            temp.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity*1.5f; 
            //Add parentShip property to the instantiated Item
            //temp.GetComponent<baseItem>().parentShip = gameObject;

            //replace this with AI script for pathfinding and when get velocity of ship double or even triple it 
            //after that then have it lock onto a ship when in close proximity
            deactivateItem();
        }
    }
    public void asteroidDrillController(){
        if (triggeringItem()){
            Debug.Log("Asteroid Drill ACtivated");
            GameObject temp = Instantiate(asteroidDrillPrefab, (transform.position + (transform.forward * DRILL_OFFSET)), transform.rotation,transform);
            //Add parentShip property to the instantiated Item
            //temp.GetComponent<baseItem>().parentShip = gameObject;


            deactivateItem();
        }
    }
    private void deactivateItem(){
        laser1.SetActive(false);
        laser2.SetActive(false);
        minePrefab.SetActive(false);
        blackHoleBombPrefab.SetActive(false);
        misslePrefab.SetActive(false);
        asteroidDrillPrefab.SetActive(false);
        ActiveItem = 0;
    }

    public bool hasItem(){
        return (ActiveItem != 0);
    }

    public bool triggeringItem(){
        return (PlayerScript.leftTriggerIsTriggered || gameObject.tag == "AI");
    }
}
