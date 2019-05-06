using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class MyClass
{
    public int level;
    public float timeElapsed;
    public string playerName;
}
public class MovingWithArrows : MonoBehaviour
{
    private GameObject networkManager;

    // Use this for initialization
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager");

        addPlayer();
        StartCoroutine(Upload());

    }

    // Update is called once per frame
    void Update()
    {
        move();
        network();

    }

    void addPlayer()
    {

    }
    void network()
    {


        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(GetText());
            StartCoroutine(Upload());
        }
    }


    
    void move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            position.x--;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.x++;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.z++;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.z--;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 position = this.transform.position;
            //position.y++;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 position = this.transform.position;
            //position.y--;
            this.transform.position = position;
        }
    }

    IEnumerator GetText()
    {

        UnityWebRequest www = UnityWebRequest.Get("http://172.21.79.81/data");
     yield return www.SendWebRequest();

     if (www.isNetworkError || www.isHttpError)
     {
        Debug.Log(www.error);
    }
    else
    {
        // Show results as text
        Debug.Log(www.downloadHandler.text);

        // Or retrieve results as binary data
        byte[] results = www.downloadHandler.data;
    }
    }


    IEnumerator Upload()
    {
        while (true)
        {
            /*
            MyClass myObject = new MyClass();
            myObject.level = 1;
            myObject.timeElapsed = transform.position.x;
            myObject.playerName = "Dr Charles Francis";
            string json = JsonUtility.ToJson(myObject);

            */
            WWWForm form = new WWWForm();



            form.AddField("playerID", networkManager.GetComponent<networkManager>().playerID);

            form.AddField("xPos", transform.position.x.ToString());
            form.AddField("yPos", transform.position.y.ToString());
            form.AddField("zPos", transform.position.z.ToString());



            Debug.Log(form);
            UnityWebRequest www = UnityWebRequest.Post("http://172.21.79.81/gameManager/update", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
