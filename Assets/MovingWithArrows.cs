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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();
        network();
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
     UnityWebRequest www = UnityWebRequest.Get("http://172.21.111.43/data");
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


        MyClass myObject = new MyClass();
        myObject.level = 1;
        myObject.timeElapsed = transform.position.x;
        myObject.playerName = "Dr Charles Francis";
        string json = JsonUtility.ToJson(myObject);

        WWWForm form = new WWWForm();
        //form.AddField("x", json);
        form.AddField("level", myObject.level);
        Debug.Log(json);
        UnityWebRequest www = UnityWebRequest.Post("http://172.21.111.43/test",form);
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
