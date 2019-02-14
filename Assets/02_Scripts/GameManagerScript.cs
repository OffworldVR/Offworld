﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public Material toPass;
	public Material passed;
	public Material next;

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
			hp.transform.GetChild(0).GetChild(3).GetComponent<MeshRenderer>().material = passed;
			hp.transform.GetChild(0).GetChild(4).GetComponent<MeshRenderer>().material = passed;
			nextHoop = FindNextHoop(n);
			hp = GetHoopWithNum(nextHoop);
			hp.transform.GetChild(0).GetChild(3).GetComponent<MeshRenderer>().material = next;
			hp.transform.GetChild(0).GetChild(4).GetComponent<MeshRenderer>().material = next;
		}
	}

	public void HitOuter(int n){
		Debug.Log(n);
		Debug.Log(nextHoop);
		if(n==nextHoop){
			Transform hp = GetHoopWithNum(n);
			hp.transform.GetChild(0).GetChild(3).GetComponent<MeshRenderer>().material = passed;
			hp.transform.GetChild(0).GetChild(4).GetComponent<MeshRenderer>().material = passed;
			nextHoop = FindNextHoop(n);
			hp = GetHoopWithNum(nextHoop);
			hp.transform.GetChild(0).GetChild(3).GetComponent<MeshRenderer>().material = next;
			hp.transform.GetChild(0).GetChild(4).GetComponent<MeshRenderer>().material = next;
		}
	}
}
