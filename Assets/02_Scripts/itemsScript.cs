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



public class itemsScript : MonoBehaviour {

    public delegate void ActiveItem();
    ActiveItem activeItem;

    private PlayerScript playerScript;

    private List<GameObject> lasers = new List<GameObject>();

    public int totalItems = 1;



    void start()
    {
        //Get player script from root
        playerScript = transform.root.GetComponent<PlayerScript>();


        //Get lasers GameObjects
        lasers.Add(transform.Find("Laser").gameObject);
        lasers.Add(transform.Find("Laser1").gameObject);
        if (lasers[0] == null|| lasers[1] == null){
            Debug.Log("Laser Game Object not found");
        }
        lasers[0].SetActive(false);
        lasers[1].SetActive(false);
    }

    public void ItemSelector()
    {
        //Set active item to the number in the list
        int randomNum = Random.Range(0, totalItems);

        if(randomNum == 0)
        {
            activeItem = Laser;
        }


    }

    public void Laser()
    {
        if (playerScript.leftTriggerIsTriggered)
        {
            lasers[0].SetActive(true);
            lasers[1].SetActive(true);

        }
        else
        {
            lasers[0].SetActive(false);
            lasers[1].SetActive(false);
        }
    }

    //Set delegate to an empty function
    private void deactivateItem()
    {
        activeItem = ()=>{ };
    }
}
