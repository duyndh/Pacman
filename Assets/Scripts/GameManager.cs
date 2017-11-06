using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject pacdotSpawner;
    public int nRowPacdot, nColumnPacdot;
    public float spacing;

	// Use this for initialization
	void Start () {
        for (int x = 0; x < nColumnPacdot; x++)
            for (int y = 0; y < nRowPacdot; y++)
                Instantiate(pacdotSpawner, pacdotSpawner.transform.position + new Vector3(x * spacing, - y * spacing, 0), pacdotSpawner.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
