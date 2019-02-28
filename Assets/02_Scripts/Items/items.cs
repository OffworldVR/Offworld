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

    public static int ActiveItem = 0;

    private static PlayerScript playerScript;

    //private List<GameObject> lasers = new List<GameObject>();

    private static int totalItems = 1;

    public static items instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        //Get player script from root


        //Get lasers GameObjects
        //transform.Find("Laser1").gameObject.SetActive(false);
        //transform.Find("Laser1").gameObject.SetActive(false);
    }








    public static void ItemSelector()
    {
        //Set active item to the number in the list
        //activeItem = Random.Range(1, totalItems);

        ActiveItem = 1;

        if(ActiveItem == 1)
        {
            instance.Invoke("deactivateItem", 4);
        }
  
    }

    public void Laser()
    {
        if (PlayerScript.leftTriggerIsTriggered)
        {
          transform.Find("Laser1").gameObject.SetActive(true);
          transform.Find("Laser1").gameObject.SetActive(true);

        }
        else
        {
          transform.Find("Laser1").gameObject.SetActive(false);
          transform.Find("Laser1").gameObject.SetActive(false);
        }
    }

    //Set delegate to an empty function
    private void deactivateItem()
    {
        //activeItem = ()=>{ };
        ActiveItem = 0;
    }
}
