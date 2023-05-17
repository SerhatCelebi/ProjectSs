using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHighlighter : MonoBehaviour
{
    public static CellHighlighter Instance;

    GameObject[] cellBackup = new GameObject[20];
    GameObject currCellBackup;
    int index = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddBackup(GameObject tempObj)
    {
        cellBackup[index] = tempObj;
        index++;
    }

    public void HighlightCells(GameObject[] currSq, GameObject[] ver1, GameObject[] ver2, GameObject[] hor1, GameObject[] hor2, int currCell)
    {
        index = 0;
        for (int i = 0; i < 9; i++)
        {
            if(i != currCell)
            {
                currSq[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(currSq[i]);
            }
            else if(i == currCell)
            {
                currSq[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 0.2f, 0.31f);
                currCellBackup = currSq[i];
            }
        }

        if (currCell >= 0 && currCell < 3)
        {
            for (int i = currCell; i <= currCell + 6; i += 3)
            {
                ver1[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(ver1[i]);
                ver2[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(ver2[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                hor1[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(hor1[i]);
                hor2[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(hor2[i]);
            }
        }
        else if (currCell >= 3 && currCell < 6)
        {
            for (int i = currCell - 3; i <= currCell + 3; i += 3)
            {
                ver1[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(ver1[i]);
                ver2[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(ver2[i]);
            }
            for (int i = 3; i < 6; i++)
            {
                hor1[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(hor1[i]);
                hor2[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(hor2[i]);
            }
        }
        else if (currCell >= 6 && currCell < 9)
        {
            for (int i = currCell - 6; i <= currCell; i += 3)
            {
                ver1[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(ver1[i]);
                ver2[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(ver2[i]);
            }
            for (int i = 6; i < 9; i++)
            {
                hor1[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(hor1[i]);
                hor2[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.11f);
                AddBackup(hor2[i]);
            }
        }
    }

    public void TurnOffHighlights()
    {
        for(int i = 0; i < 20; i++)
        {
            cellBackup[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }
        currCellBackup.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
    }

}
