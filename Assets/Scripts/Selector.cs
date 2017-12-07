using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    Vector3 scale;
	// Use this for initialization
	void Start () {
        scale = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward, 10);
        scale.x = Mathf.Sin(Time.time) /5;
        scale.y = Mathf.Sin(Time.time) /5;
        this.transform.localScale = scale;
    }
}
