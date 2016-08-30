using UnityEngine;
using System.Collections;

public class Autowalk : MonoBehaviour {

    public bool moveUp = false;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.x <= 0)
        {
            transform.localPosition += Vector3.right * Time.deltaTime;
        }
	}
}
