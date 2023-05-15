using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NumberCell : MonoBehaviour, IPointerClickHandler
{
    public static NumberCell Instance;

    public int squareIndex;
    public int cellIndex;
    public bool isSolved;
    public bool isSelected;
    public bool[] notedNumbers = new bool[9];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {

    }
    private void Update()
    {

    }

    public void TakeNote(int index)
    {
        if (notedNumbers[index])
        {
            notedNumbers[index] = false;
        }
        else
        {
            notedNumbers[index] = true;
        }

        if (!isSolved)
        {
            WriteNotedNumbers();
        }
    }
    public void EraseNotes()
    {
        for(int i = 0; i < 9; i++)
        {
            notedNumbers[i] = false;
        }
    }
    void WriteNotedNumbers()
    {
        string temp = " ";
        for(int i = 0; i < 9; i++)
        {
            if (notedNumbers[i])
            {
                temp += (i+1).ToString();
            }
            else
            {
                temp += " ";
            }

            if(i == 2 || i == 5)
            {
                temp += "\n";
            }
            else if(i != 8)
            {
                temp += " ";
            }
        }
        gameObject.GetComponent<Text>().color = Color.black;
        gameObject.GetComponent<Text>().text = temp;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected && !isSolved)
        {
            isSelected = true;
            EasyGameManager.Instance.CellSelected(squareIndex, cellIndex);
        }
    }

}
