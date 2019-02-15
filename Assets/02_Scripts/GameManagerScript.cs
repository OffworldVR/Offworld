using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
	public int numLaps;
	public Material toPassOuter;
	public Material passedOuter;
	public Material nextOuter;
	public Material toPassInner;
	public Material passedInner;
	public Material nextInner;

	private GameObject[] allHoops;
	private List<HoopScript> playerHoops = new List<HoopScript>();
	private int nextHoop = -1;
	private int currLap = -1;
	private bool readyToEnd = false;

	void Start () {
		allHoops = GameObject.FindGameObjectsWithTag("Ring");
		foreach(GameObject g in allHoops){
			HoopScript h = g.GetComponent<HoopScript>();
			if(h.forPlayer){
				playerHoops.Add(h);
			}
		}
		nextHoop = FindNextHoop(nextHoop);
		Debug.Log(nextHoop);
	}

	private int FindNextHoop(int n){
		if(n==allHoops.Length-1){
			return 0;
		}
		int min = 1000;
		int prevHoop = n;
		foreach(HoopScript h in playerHoops){
			int num = h.hoopNum;
			if(num>prevHoop && num<min){
				min = num;
			}
		}
		return min;
	}

	private int GetPlayerHoopIndex(int n){
		for(var i = 0; i<playerHoops.Count; i++){
			if(n==playerHoops[i].hoopNum){
				return i;
			}
		}
		return -1;
	}

	private Transform GetHoopWithNum(int n){
		foreach(GameObject g in allHoops){
			if(g.GetComponent<HoopScript>().hoopNum==n){
				return g.transform;
			}
		}
		return null;
	}

	private void HitHoop(int n){
		if(n==nextHoop){
			if(n==0){
				currLap++;
			}
			if(currLap==numLaps){
				EndGame();
			}else{
				Transform hp = GetHoopWithNum(n);
				hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = passedOuter;
				hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = passedInner;
				nextHoop = FindNextHoop(n);
				hp = GetHoopWithNum(nextHoop);
				hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = nextOuter;
				hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = nextInner;
			}
		}
	}

	public void HitInner(int n){
		//TODO GET ITEM
		HitHoop(n);
	}

	public void HitOuter(int n){
		HitHoop(n);
	}

	private void EndGame(){
		readyToEnd = true;
	}

	public void ButtonOnePressed(){
		if(readyToEnd){
			SceneManager.LoadScene("MainMenu");
		}
	}
}
