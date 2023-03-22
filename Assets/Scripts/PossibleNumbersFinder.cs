using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleNumbersFinder : MonoBehaviour
{
    public static PossibleNumbersFinder Instance;
    public List<int> possibleNumbers = new List<int>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void FindPossibleNumbers(int[] currSq, int[] ver1, int[] ver2, int[] hor1, int[] hor2, int currCell)
    {
        ListRefill(possibleNumbers);
        Debug.Log("PossibleNumbersFinder...!");

        for(int i = 0; i < currSq.Length; i++)
        {
            if (possibleNumbers.Contains(currSq[i]))
            {
                possibleNumbers.Remove(currSq[i]);
            }
        }

        if(currCell >= 0 && currCell < 3)
        {
            for(int i = currCell; i <= currCell + 6; i = i + 3)
            {
                if (possibleNumbers.Contains(ver1[i]))
                {
                    possibleNumbers.Remove(ver1[i]);
                }
                if (possibleNumbers.Contains(ver2[i]))
                {
                    possibleNumbers.Remove(ver2[i]);
                }
            }
            for(int i = 0; i < 3; i++)
            {
                if (possibleNumbers.Contains(hor1[i]))
                {
                    possibleNumbers.Remove(hor1[i]);
                }
                if (possibleNumbers.Contains(hor2[i]))
                {
                    possibleNumbers.Remove(hor2[i]);
                }
            }
        }
        else if(currCell >= 3 && currCell < 6)
        {
            for(int i = currCell - 3; i <= currCell + 3; i = i + 3)
            {
                if (possibleNumbers.Contains(ver1[i]))
                {
                    possibleNumbers.Remove(ver1[i]);
                }
                if (possibleNumbers.Contains(ver2[i]))
                {
                    possibleNumbers.Remove(ver2[i]);
                }
            }
            for(int i = 3; i < 6; i++)
            {
                if (possibleNumbers.Contains(hor1[i]))
                {
                    possibleNumbers.Remove(hor1[i]);
                }
                if (possibleNumbers.Contains(hor2[i]))
                {
                    possibleNumbers.Remove(hor2[i]);
                }
            }
        }
        else if(currCell >= 6 && currCell < 9)
        {
            for(int i = currCell - 6; i <= currCell; i = i + 3)
            {
                if (possibleNumbers.Contains(ver1[i]))
                {
                    possibleNumbers.Remove(ver1[i]);
                }
                if (possibleNumbers.Contains(ver2[i]))
                {
                    possibleNumbers.Remove(ver2[i]);
                }
            }
            for(int i = 6; i < 9; i++)
            {
                if (possibleNumbers.Contains(hor1[i]))
                {
                    possibleNumbers.Remove(hor1[i]);
                }
                if (possibleNumbers.Contains(hor2[i]))
                {
                    possibleNumbers.Remove(hor2[i]);
                }
            }
        }

        string str = "Possible Numbers(PossibleNumbersFinder) : ";
        for (int i = 0; i < possibleNumbers.Count; i++)
        {
            str += possibleNumbers[i].ToString() + " -- ";
        }
        Debug.Log(str);
    }


    void ListRefill(List<int> lst)
    {
        lst.Clear();
        lst.Add(1);
        lst.Add(2);
        lst.Add(3);
        lst.Add(4);
        lst.Add(5);
        lst.Add(6);
        lst.Add(7);
        lst.Add(8);
        lst.Add(9);
    }
}
