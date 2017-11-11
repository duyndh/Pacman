using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacdotBehaviour : MonoBehaviour {

    private GameObject pacdotSpawner;
    private int rowPosition, columnPosition;
    private PacdotSpawner pacdotSpawnerScript;
    private int turnKind;
    private bool isAvailable;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            GetComponent<Renderer>().enabled = false;
            isAvailable = false;
        }
    }
    public void SetAvailable(bool ia)
    {
        isAvailable = ia;
    }

    public bool GetAvailable()
    {
        return isAvailable;
    }

    public int GetTurn()
    {
        if (turnKind == 0)
        {
            if (pacdotSpawner == null)
            {
                pacdotSpawner = GameObject.FindGameObjectWithTag("PacdotSpawner");
                pacdotSpawnerScript = pacdotSpawner.GetComponent<PacdotSpawner>();
            }

            pacdotSpawner = GameObject.FindGameObjectWithTag("PacdotSpawner");
            //GameObject[,] pacdots = pacdotSpawner.GetComponent<PacdotSpawner>().GetPacdots();
            
            int sumAdjacentPoint = 0;
            // UP
            if (rowPosition - 1 >= 0
                && pacdotSpawnerScript.GetPacdot(rowPosition - 1, columnPosition).GetComponent<PacdotBehaviour>().GetAvailable() == true)
            {
                sumAdjacentPoint += 2;
            }

            // DOWN
            if (rowPosition + 1 < pacdotSpawnerScript.nRowPacdot
                && pacdotSpawnerScript.GetPacdot(rowPosition + 1, columnPosition).GetComponent<PacdotBehaviour>().GetAvailable() == true)
            {
                sumAdjacentPoint += 8;
            }

            // LEFT
            if (columnPosition - 1 >= 0
                && pacdotSpawnerScript.GetPacdot(rowPosition, columnPosition - 1).GetComponent<PacdotBehaviour>().GetAvailable() == true)
            {
                sumAdjacentPoint += 4;
            }

            // RIGHT
            if (columnPosition + 1 < pacdotSpawnerScript.nColumnPacdot
                && pacdotSpawnerScript.GetPacdot(rowPosition, columnPosition + 1).GetComponent<PacdotBehaviour>().GetAvailable() == true)
            {
                sumAdjacentPoint += 1;
            }

            turnKind = sumAdjacentPoint;
        }

        return turnKind;
    }

    // Use this for initialization
    void Start () {
        pacdotSpawner = GameObject.FindGameObjectWithTag("PacdotSpawner");
        pacdotSpawnerScript = pacdotSpawner.GetComponent<PacdotSpawner>();

        rowPosition = Mathf.RoundToInt((-gameObject.transform.position.y + pacdotSpawner.transform.position.y) / pacdotSpawnerScript.spacing);
        columnPosition = Mathf.RoundToInt((gameObject.transform.position.x - pacdotSpawner.transform.position.x) / pacdotSpawnerScript.spacing);

        turnKind = 0;
    }

    // Update is called once per frame
    void Update () {

    }
}
