using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacdotSpawner : MonoBehaviour {

    public GameObject pacdot;
    public int nRowPacdot, nColumnPacdot;
    public float spacing;

    private GameObject[,] pacdots;
    
    // Use this for initialization
    void Start () {
        pacdots = new GameObject[nRowPacdot, nColumnPacdot];
    
        for (int i = 0; i < nRowPacdot; i++)
        {
            for (int j = 0; j < nColumnPacdot; j++)
            {
                pacdots[i, j] = (GameObject)Instantiate(pacdot, transform.position + spacing * new Vector3(j, -i, 0), transform.rotation);
                pacdots[i, j].GetComponent<PacdotBehaviour>().SetAvailable(true);
            }
        }
    }

    public GameObject[,] GetPacdots()
    {
        return pacdots;
    }

    public GameObject GetPacdot(int row, int column)
    {
        return pacdots[row, column];
    }

    // Update is called once per frame
    void Update () {

	}
}
