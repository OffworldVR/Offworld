using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class playerNetworkManager : MonoBehaviour {

    private GameObject networkManager;

    void Start () {

        networkManager = GameObject.Find("NetworkManager");
        StartCoroutine(Upload());

    }

    void Update () {
		
	}


    IEnumerator Upload()
    {
        while (true)
        {
           
            WWWForm form = new WWWForm();

    
            form.AddField("playerID", networkManager.GetComponent<networkManager>().playerID);
            form.AddField("xPos", transform.position.x.ToString());
            form.AddField("yPos", transform.position.y.ToString());
            form.AddField("zPos", transform.position.z.ToString());
            form.AddField("xRot", transform.eulerAngles.x.ToString());
            form.AddField("yRot", transform.eulerAngles.y.ToString());
            form.AddField("zRot", transform.eulerAngles.z.ToString());


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
