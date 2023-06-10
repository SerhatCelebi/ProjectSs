using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossibleNumbersFinder : MonoBehaviour
{
    public static PossibleNumbersFinder Instance;
    public List<int> possibleNumbers = new List<int>();

    int temp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void FindPossibleNumbers(GameObject[] currSq, GameObject[] ver1, GameObject[] ver2, GameObject[] hor1, GameObject[] hor2, int currCell)
    {
        ListRefill(possibleNumbers);
        //Debug.Log("PossibleNumbersFinder...!");



        for(int i = 0; i < currSq.Length; i++)
        {
            if (currSq[i].GetComponent<NumberCell>().isSolved)
            {
                int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                if (possibleNumbers.Contains(temp))
                {
                    possibleNumbers.Remove(temp);
                }
            }
        }

        if(currCell >= 0 && currCell < 3)
        {
            for(int i = currCell; i <= currCell + 6; i = i + 3)
            {
                if (ver1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
                if (ver2[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
            }
            for(int i = 0; i < 3; i++)
            {
                if (hor1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
                if (hor2[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
            }
        }
        else if(currCell >= 3 && currCell < 6)
        {
            for(int i = currCell - 3; i <= currCell + 3; i = i + 3)
            {
                if (ver1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
                if (ver1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
            }
            for(int i = 3; i < 6; i++)
            {
                if (hor1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
                if (hor2[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
            }
        }
        else if(currCell >= 6 && currCell < 9)
        {
            for(int i = currCell - 6; i <= currCell; i = i + 3)
            {
                if (ver1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
                if (ver1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
            }
            for(int i = 6; i < 9; i++)
            {
                if (hor1[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
                if (hor2[i].GetComponent<NumberCell>().isSolved)
                {
                    int.TryParse(currSq[i].GetComponent<Text>().text, out temp);
                    if (possibleNumbers.Contains(temp))
                    {
                        possibleNumbers.Remove(temp);
                    }
                }
            }
        }

        string str = "Possible Numbers(" + currCell + ") : ";
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
