using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pacdot")
            Destroy(collision.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
