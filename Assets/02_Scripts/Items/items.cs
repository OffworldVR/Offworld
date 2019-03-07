using System.Collections;
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



public class items : MonoBehaviour {


    public GameObject laser1;
    public GameObject laser2;
    private static PlayerScript playerScript;
    public static items instance;

    //Time the player can use the laser after the item is activated
    public const int laserActiveTime = 4;

    //Number of Items the player can get
    private static int totalItems = 1;

    //Current active Item
    public static int ActiveItem = 0;
    //Item 0 = None
    //Item 1 = Laser
    //Item 2 = 
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
        //activeItem = Random.Range(1, totalItems);

        ActiveItem = 1;

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
    
    }
    

    public void laserController()
    {
        Debug.Log("Laser Activated");
        if (PlayerScript.leftTriggerIsTriggered)
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
    }

    private void deactivateItem()
    {
        ActiveItem = 0;
    }
}
