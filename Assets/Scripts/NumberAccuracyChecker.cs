using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberAccuracyChecker : MonoBehaviour
{
    public static NumberAccuracyChecker Instance;
    public List<int> possibleNumbers = new List<int>();

    string str;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void FindPossibleNumbers(int[] ver1, int[] ver2, int[] currSq, int[] hor1, int[] hor2, int currentCell)
    {
        ListRefill(possibleNumbers);
        //Debug.Log("Fonksiyona Girdi. currentCell : " + currentCell);
        if(currentCell >= 0 && currentCell < 3) // If the current cell number is 0, 1 or 2
        {
            //Debug.Log("0&3 If bloðuna girdi.");
            for(int i = currentCell; i <= currentCell + 6; i+=3)
            {
                //Debug.Log("Vertical For Bloðuna girdi.");
                if (possibleNumbers.Contains(ver1[i])) // Checking the first vertical neighbour square
                {
                    possibleNumbers.Remove(ver1[i]);
                }
                if (possibleNumbers.Contains(ver2[i])) // checking the second vertical neighbour square
                {
                    possibleNumbers.Remove(ver2[i]);
                }
            }
            for(int j = 0; j < 3; j++)
            {
                if (possibleNumbers.Contains(hor1[j])) // Checking the first horizontal neighbour square
                {
                    possibleNumbers.Remove(hor1[j]);
                }
                if (possibleNumbers.Contains(hor2[j])) // Checking the second horizontal neighbour square
                {
                    possibleNumbers.Remove(hor2[j]);
                }
            }
        }

        else if (currentCell >= 3 && currentCell <= 5) // If the current cell number is 3, 4 or 5
        {
            for (int k = currentCell - 3; k <= currentCell + 3; k += 3)
            {
                if (possibleNumbers.Contains(ver1[k])) // Checking the first vertical neighbour square
                {
                    possibleNumbers.Remove(ver1[k]);
                }
                if (possibleNumbers.Contains(ver2[k])) // Checking the second vertical neighbour square
                {
                    possibleNumbers.Remove(ver2[k]);
                }
            }
            for (int l = 3; l < 6; l++)
            {
                if (possibleNumbers.Contains(hor1[l])) // Checking the first horizontal neighbour square
                {
                    possibleNumbers.Remove(hor1[l]);
                }
                if (possibleNumbers.Contains(hor2[l])) // Checking the second horizontal neighbour square
                {
                    possibleNumbers.Remove(hor2[l]);
                }
            }
        }

        else if(currentCell >= 6 && currentCell <= 8) // If the current cell number is 6, 7 or 8
        {
            for(int m = currentCell - 6; m <= currentCell; m += 3)
            {
                if(possibleNumbers.Contains(ver1[m])) // Checking the first vertical neighbour square
                {
                    possibleNumbers.Remove(ver1[m]);
                }
                if (possibleNumbers.Contains(ver2[m])) // Checking the second vertical neighbour square
                {
                    possibleNumbers.Remove(ver2[m]);
                }
            }
            for (int n = 6; n < 9; n++)
            {
                if (possibleNumbers.Contains(hor1[n])) // Checking the first horizontal neighbour square
                {
                    possibleNumbers.Remove(hor1[n]);
                }
                if (possibleNumbers.Contains(hor2[n])) // Checking the second horizontal neighbour square
                {
                    possibleNumbers.Remove(hor2[n]);
                }
            }
        }
        else
        {
            Debug.Log("Herhangi bir bloga girmedi... currentCell = " + currentCell);
        }
        for(int i = currentCell; i >= 0; i--)
        {
            if (possibleNumbers.Contains(currSq[i])) // Checking the current square
            {
                possibleNumbers.Remove(currSq[i]);
            }
        }

        if(possibleNumbers.Count == 0)
        {
            Debug.Log(" **********************   EmptyList Hatasý Tespit Edildi..!    ****************************");
            str = "Varsaylan Kare Ýçeriði : ";
            for(int i = 0; i < 9; i++)
            {
                str += currSq[i].ToString() + " -- ";
            }
            Debug.Log(str);
            EmptyListFixer(ver1, ver2, currSq, hor1, hor2, currentCell);
        }
        /*str = currentCell.ToString() + ". Hücre için : ";
        for (int z = 0; z < possibleNumbers.Count; z++)
        {
            str += possibleNumbers[z].ToString() + " -- ";
        }
        Debug.Log(str);*/
        //Debug.Log("i : " + currentCell.ToString());
    }



    void EmptyListFixer(int[] ver1, int[] ver2, int[] currSq, int[] hor1, int[] hor2, int currentCell)
    {
        int count = 0;
        List<int> fixerNumbers = new List<int>();
        List<int> fixerNumbersBackup = new List<int>();
        ListRefill(fixerNumbers);
        for (int i = currentCell; i >= 0; i--)
        {
            if (fixerNumbers.Contains(currSq[i])) // Checking the current square
            {
                fixerNumbers.Remove(currSq[i]);
            }
        }

        str = "fixerNumbers : ";
        for(int i = 0; i < fixerNumbers.Count; i++)
        {
            str += fixerNumbers[i].ToString() + " -- ";
        }
        Debug.Log(str);

        while (true)
        {
            ListRefill(possibleNumbers);
            Debug.Log("While döngüsüne girildi..! *****************************************");
            if (count >= 0 && count < 3) // If the current cell number is 0, 1 or 2
            {
                //Debug.Log("0&3 If bloðuna girdi.");
                for (int i = count; i <= count + 6; i += 3)
                {
                    //Debug.Log("Vertical For Bloðuna girdi.");
                    if (possibleNumbers.Contains(ver1[i])) // Checking the first vertical neighbour square
                    {
                        possibleNumbers.Remove(ver1[i]);
                    }
                    if (possibleNumbers.Contains(ver2[i])) // checking the second vertical neighbour square
                    {
                        possibleNumbers.Remove(ver2[i]);
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    if (possibleNumbers.Contains(hor1[j])) // Checking the first horizontal neighbour square
                    {
                        possibleNumbers.Remove(hor1[j]);
                    }
                    if (possibleNumbers.Contains(hor2[j])) // Checking the second horizontal neighbour square
                    {
                        possibleNumbers.Remove(hor2[j]);
                    }
                }
            }

            else if (count >= 3 && count <= 5) // If the current cell number is 3, 4 or 5
            {
                for (int k = count - 3; k <= count + 3; k += 3)
                {
                    if (possibleNumbers.Contains(ver1[k])) // Checking the first vertical neighbour square
                    {
                        possibleNumbers.Remove(ver1[k]);
                    }
                    if (possibleNumbers.Contains(ver2[k])) // Checking the second vertical neighbour square
                    {
                        possibleNumbers.Remove(ver2[k]);
                    }
                }
                for (int l = 3; l < 6; l++)
                {
                    if (possibleNumbers.Contains(hor1[l])) // Checking the first horizontal neighbour square
                    {
                        possibleNumbers.Remove(hor1[l]);
                    }
                    if (possibleNumbers.Contains(hor2[l])) // Checking the second horizontal neighbour square
                    {
                        possibleNumbers.Remove(hor2[l]);
                    }
                }
            }

            else if (count >= 6 && count <= 8) // If the current cell number is 6, 7 or 8
            {
                for (int m = count - 6; m <= count; m += 3)
                {
                    if (possibleNumbers.Contains(ver1[m])) // Checking the first vertical neighbour square
                    {
                        possibleNumbers.Remove(ver1[m]);
                    }
                    if (possibleNumbers.Contains(ver2[m])) // Checking the second vertical neighbour square
                    {
                        possibleNumbers.Remove(ver2[m]);
                    }
                }
                for (int n = 6; n < 9; n++)
                {
                    if (possibleNumbers.Contains(hor1[n])) // Checking the first horizontal neighbour square
                    {
                        possibleNumbers.Remove(hor1[n]);
                    }
                    if (possibleNumbers.Contains(hor2[n])) // Checking the second horizontal neighbour square
                    {
                        possibleNumbers.Remove(hor2[n]);
                    }
                }
            }
            else
            {
                Debug.Log("Herhangi bir bloga girmedi... count = " + count);
            }

            for(int i = 0; i < fixerNumbers.Count; i++)
            {
                if (possibleNumbers.Contains(fixerNumbers[i]))
                {
                    Debug.Log("Çözücü possibleNumbers kontrol If bloðu..! ***********************************************");
                    fixerNumbersBackup.Add(currSq[count]);
                    currSq[count] = fixerNumbers[i];
                    fixerNumbers.RemoveAt(i);
                    break;
                }
            }
            
            if (fixerNumbers.Count == 0)
            {
                str = "***************************** Çözücü Sonrasý possibleNumbers : ";
                Debug.Log("************************************  Hata çözücü çalýþtý..!");
                possibleNumbers.Clear();
                for(int i = 0; i < fixerNumbersBackup.Count; i++)
                {
                    possibleNumbers.Add(fixerNumbersBackup[i]);
                    str += fixerNumbersBackup[i].ToString() + " -- ";
                }
                Debug.Log(str);
                count = 0;
                break;
            }
            count+=1;
            Debug.Log("count increased to '" + count + "' ..! *********************************************");
        }
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
