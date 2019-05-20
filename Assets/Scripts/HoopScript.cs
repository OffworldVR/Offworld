using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour {

	public int hoopNum;
	public bool forPlayer;
	public bool hasItem;
    public float itemRefreshTime = 5f;
    private GameManagerScript gm;

	// Use this for initialization
	void Start () {
		if(!forPlayer){
			transform.GetChild(0).gameObject.SetActive(false);
		}else{
			gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		}
		if(!hasItem){
			transform.GetChild(0).Find("Inner").gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag=="Player"){
			if(forPlayer){
				float d = (c.transform.position-transform.position).magnitude;
				if(d<40){
					if(d<25){
						if(hasItem){
							gm.HitInner(hoopNum);
                            itemPrefabSpawnController shipItemController = c.GetComponent<itemPrefabSpawnController>();
                            if (shipItemController != null && !shipItemController.hasItem()){
                                GiveItem(shipItemController);
                                SleepItem();
                            }
                        }
                        else{
							gm.HitOuter(hoopNum);
						}
						// Debug.Log("hit inner");
					}else{
						gm.HitOuter(hoopNum);
						// Debug.Log("hit outer");
					}
				}
			}
        }
        else if(c.transform.root.tag=="AI")
        {
            float d = (c.transform.position - transform.position).magnitude;
            if (d < 40)
            {
                c.transform.root.GetComponent<AI>().HitHoop(hoopNum);
                if (d < 25)
                {
                    if (hasItem)
                    {
                        itemPrefabSpawnController shipItemController = c.GetComponent<itemPrefabSpawnController>();
                        if (shipItemController != null && !shipItemController.hasItem())
                        {
                            GiveItem(shipItemController);
                            SleepItem();
                        }
                    }
                }
            }
        }

	}


    private void GiveItem (itemPrefabSpawnController shipItemController)
    {
        shipItemController.ItemSelector();
    }

    private void SleepItem()
    {
        hasItem = false;
        Invoke("AwakeItem", itemRefreshTime);
    }

    private void AwakeItem() { 
        hasItem = true; 
    }
}
