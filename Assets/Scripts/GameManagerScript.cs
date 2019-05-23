using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
    public int numLaps;
    public int shipValue = 1;
    public Material nextIn;
    public Material nextIn1;
    public Material nextIn2;
    public Material nextOut;
    public Material nextOut1;
    public Material nextOut2;
    public Material passed;
    public float[] shipMaxVelocity =    { 150, 200, 200, 150, 100};
    public float[] shipAcceleration =   {  10,   8,   5,  15,  20};
    public float[] shipRotation =       {  10,   7,  15,  10,   6};
    public float[] shipHealth =         {  80,  90, 150, 100,  50};
    public float[] shipDecceleration =  {   8,   6,   5,  10,   5};
    public GameObject[] playerShipPrefabs;
    public GameObject[] AIShipPrefabs;
    public Transform[] AISpawnLocations;
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
        //fox ship = 0
        //x-wing = 1
        //vertical = 2
        //saucer = 3
        //pod racer = 4
        //for the arrays containing ships speeeds, roations etc
        //if order is diff in shipprefab array let Carter know so he can modify his arrays

        //TODO: GET SELECTED SHIPH VALUE FROM PLAYER PREFS
        shipValue = 1;
		GameObject ship = Instantiate(playerShipPrefabs[shipValue]) as GameObject;
		GameObject player = GameObject.Find("Player");
		ship.transform.position += player.transform.position;
		ship.transform.parent = player.transform;
		int index = 0;
		foreach(Collider c in player.GetComponents<BoxCollider>()){
			if(index!=shipValue){
				c.enabled = false;
			}
			index++;
		}
		List<int> nums = new List<int>();
		for(int i = 0; i<AIShipPrefabs.Length; i++){
			if(i!=shipValue){
				nums.Add(i);
			}
		}
		foreach(Transform t in AISpawnLocations){
			int rnd = Random.Range(0, nums.Count);
			int nAI = nums[rnd];
			nums.RemoveAt(rnd);
			GameObject AI = Instantiate(AIShipPrefabs[nAI]) as GameObject;
			AI.transform.position = t.position;
         
            //assign values to AI ship based on which ship it is
            AI.Max_velocity = shipMaxVelocity[rnd];
            AI.acceleration = shipAcceleration[rnd];
            AI.rotation = shipRotation[rnd];
            AI.health = shipHealth[rnd];
            AI.decceleration = shipDecceleration[rnd];


        }

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

	public int FindNextHoop(int n){
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

	public Transform GetHoopWithNum(int n){
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
		GameObject[] ais = GameObject.FindGameObjectsWithTag("AI");
		foreach(GameObject ai in ais){
			ai.GetComponent<AI>().raceHasStarted = true;
		}
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
    public int getShipValue()
    {
        return shipValue;
    }
}
