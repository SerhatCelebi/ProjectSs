using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberHighlighter : MonoBehaviour
{
    List<int> squareBackup = new List<int>();
    List<int> cellBackup = new List<int>();
    List<Color> colorBackup = new List<Color>();
    int temp;
    bool CR_running = false;
    IEnumerator HlNum;

    public void HighlightTheNumbers(GameObject[][] allSqObj, int num)
    {
        HlNum = HighlightNumerator(allSqObj);
        if (!CR_running)
        {
            CollectNumbers(allSqObj, num);

            StartCoroutine(HlNum);
        }
        else
        {
            //StopCoroutine(HlNum);
            StopAllCoroutines();
            TurnOff(allSqObj);
            squareBackup.Clear();
            cellBackup.Clear();
            colorBackup.Clear();
            CollectNumbers(allSqObj, num);

            StartCoroutine(HlNum);
        }
    }

    IEnumerator HighlightNumerator(GameObject[][] allSq)
    {
        CR_running = true;
        TurnOn(allSq);
        yield return new WaitForSeconds(2.5f);
        TurnOff(allSq);
        squareBackup.Clear();
        cellBackup.Clear();
        colorBackup.Clear();
        CR_running = false;
        yield return null;
    }
    void TurnOn(GameObject[][] Sq)
    {
        for (int i = 0; i < squareBackup.Count; i++)
        {
            Sq[squareBackup[i]][cellBackup[i]].GetComponent<Text>().color = new Color(0f, 0.15f, 0.74f);
        }
    }
    void TurnOff(GameObject[][] Sq)
    {
        for (int i = 0; i < squareBackup.Count; i++)
        {
            Sq[squareBackup[i]][cellBackup[i]].GetComponent<Text>().color = colorBackup[i];
        }
    }

    void CollectNumbers(GameObject[][] allSquareObj, int number)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int.TryParse(allSquareObj[i][j].GetComponent<Text>().text, out temp);
                if (temp == number)
                {
                    squareBackup.Add(i);
                    cellBackup.Add(j);
                    colorBackup.Add(allSquareObj[i][j].GetComponent<Text>().color);
                }
            }
        }
    }
}
