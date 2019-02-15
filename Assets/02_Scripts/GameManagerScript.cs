using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public Material toPassOuter;
	public Material passedOuter;
	public Material nextOuter;
	public Material toPassInner;
	public Material passedInner;
	public Material nextInner;

	private GameObject[] allHoops;
	private List<HoopScript> playerHoops = new List<HoopScript>();
	private int nextHoop = -1;

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

	public void HitInner(int n){
		Debug.Log(n);
		Debug.Log(nextHoop);
		if(n==nextHoop){
			Transform hp = GetHoopWithNum(n);
			hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = passedOuter;
			hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = passedInner;
			nextHoop = FindNextHoop(n);
			hp = GetHoopWithNum(nextHoop);
			hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = nextOuter;
			hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = nextInner;
		}
	}

	public void HitOuter(int n){
		Debug.Log(n);
		Debug.Log(nextHoop);
		if(n==nextHoop){
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
