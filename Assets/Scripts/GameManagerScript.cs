using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
	public int numLaps;
	public Material nextIn;
	public Material nextIn1;
	public Material nextIn2;
	public Material nextOut;
	public Material nextOut1;
	public Material nextOut2;
	public Material passed;


	private GameObject[] allHoops;
	private List<HoopScript> playerHoops = new List<HoopScript>();
	private List<int> nextHoops = new List<int>();
	private int nextHoop = -1;
	private int currLap = -1;
	private bool readyToEnd = false;

	private bool go = false;
	private float startTime;

	void Awake () {
		allHoops = GameObject.FindGameObjectsWithTag("Ring");
		foreach(GameObject g in allHoops){
			HoopScript h = g.GetComponent<HoopScript>();
			if(h.forPlayer){
				playerHoops.Add(h);
			}
		}
		setupNextHoops();
	}

	private void setupNextHoops(){
		nextHoop = FindNextHoop(nextHoop);
		nextHoops.Add(nextHoop);
		for(var i = 0; i<2; i++){
			nextHoops.Add(FindNextHoop(nextHoops[i]));
		}
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

    public Transform getNextHoop(int n)
    {
        return GetHoopWithNum((n+1) % allHoops.Length);
        //return allHoops[(n + 1) % allHoops.Length];
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
		if(n==nextHoops[0]){
			if(n==0){
				currLap++;
			}
			if(currLap==numLaps){
				EndGame();
			}else{
				Transform hp = GetHoopWithNum(n);
				hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = passed;
				hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = passed;
				for(var i = 0; i<2; i++){
					nextHoops[i] = nextHoops[i+1];
				}
				nextHoops[2] = FindNextHoop(nextHoops[1]);
				hp = GetHoopWithNum(nextHoops[0]);
				hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = nextOut;
				hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = nextIn;
				hp = GetHoopWithNum(nextHoops[1]);
				hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = nextOut1;
				hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = nextIn1;
				hp = GetHoopWithNum(nextHoops[2]);
				hp.transform.GetChild(0).Find("Outer").GetComponent<MeshRenderer>().material = nextOut2;
				hp.transform.GetChild(0).Find("Inner").GetComponent<MeshRenderer>().material = nextIn2;
				Debug.Log(nextHoops);
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

	public void BeginRace(){
		go = true;
		startTime = Time.time;
		GameObject p = GameObject.Find("Player");
		if(p!=null){
			p.GetComponent<PlayerScript>().CheckSteering();
		}
		// GameObject.Find("Player").GetComponent<PlayerScript>().CheckSteering();
	}

	private void EndGame(){
		readyToEnd = true;
		GameObject.Find("Player").GetComponent<PlayerScript>().ShowEndScoreboard(Time.time-startTime);
	}

	public void ButtonOnePressed(){
		if(readyToEnd){
			SceneManager.LoadScene("MainMenu");
		}
	}

	public bool canGo(){
		return go;
	}
}
