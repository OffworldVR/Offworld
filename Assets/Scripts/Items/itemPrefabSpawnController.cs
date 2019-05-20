using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INSTRUCTIONS TO ADD A NEW ITEM
//    Step 1: Create a new method with no parameters and returns void
//    Step 2: Write what you want the item to do
//    Step 3: Call deactivateItem() after the action of your item has been performed
//    Step 4: Add another if else statement to ItemSelector()   See Other
//    Step 4: Add 1 to the value of totalItems

public class itemPrefabSpawnController : MonoBehaviour {

    private float DRILL_OFFSET = 3f;

    private PlayerScript playerScript; //this used to be static 

    //Item prefabs
    public GameObject[] itemPrefabs;
    //Number of Items the player can get
    private static int totalItems = 5;
    //Current active Item
    public int ActiveItem = 0;
    //Item 0 = None
    //Item 1 = Laser
    //Item 2 = Mine
    //Item 3 = BlackHoleBomb
    //Item 4 = Missle
    //Item 5 = AsteroidDrill

    public void ItemSelector()
    {
        //Set active item to the number in the list
        ActiveItem = Random.Range(1, itemPrefabs.Length+1);
    }

    public void ActivateItem()
    {
        if(hasItem())
        {
            Debug.Log("Fired: " + itemPrefabs[ActiveItem-1].ToString());
            GameObject temp = Instantiate(itemPrefabs[ActiveItem-1], transform.position, transform.rotation);
            temp.GetComponent<baseItem>().setParentShip(gameObject);
            deactivateItem();
        }
    }

    private void deactivateItem(){
        ActiveItem = 0;
    }

    public bool hasItem(){
        return (ActiveItem != 0 && ActiveItem <= itemPrefabs.Length + 1);
    }
}
