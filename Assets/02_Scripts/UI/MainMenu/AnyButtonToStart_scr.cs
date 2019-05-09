using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnyButtonToStart_scr : MonoBehaviour {

    float alpha;
    float time;
    TextMeshProUGUI text;

	// Use this for initialization
	void Start () {
        time = 0f;
        text = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        alpha = 0.25f * Mathf.Sin(4 * time) + 0.75f;
        text.color = new Color32(255, 255, 255, (byte)((float)255 * alpha));
	}
}
