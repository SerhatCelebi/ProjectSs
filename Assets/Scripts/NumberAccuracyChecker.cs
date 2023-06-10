using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberAccuracyChecker : MonoBehaviour
{

    public bool IsNumAccurate(int[] currSq, int[] ver1, int[] ver2, int[] hor1, int[] hor2, int currCell, int number)
    {

        for (int i = 0; i < currCell; i++)
        {
            if (currSq[i] == number) return false;
        }

        if (currCell >= 0 && currCell < 3)
        {
            for (int i = currCell; i <= currCell + 6; i = i + 3)
            {
                if (ver1[i] == number || ver2[i] == number) return false;
            }
            for (int i = 0; i < 3; i++)
            {
                if (hor1[i] == number || hor2[i] == number) return false;
            }
        }
        else if (currCell >= 3 && currCell < 6)
        {
            for (int i = currCell - 3; i <= currCell + 3; i = i + 3)
            {
                if (ver1[i] == number || ver2[i] == number) return false;
            }
            for (int i = 3; i < 6; i++)
            {
                if (hor1[i] == number || hor2[i] == number) return false;
            }
        }
        else if (currCell >= 6 && currCell < 9)
        {
            for (int i = currCell - 6; i <= currCell; i = i + 3)
            {
                if (ver1[i] == number || ver2[i] == number) return false;
            }
            for (int i = 6; i < 9; i++)
            {
                if (hor1[i] == number || hor2[i] == number) return false;
            }
        }

        return true;
    }



    /*void EmptyListFixer(int[] ver1, int[] ver2, int[] currSq, int[] hor1, int[] hor2, int currentCell)
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
    }*/
}
