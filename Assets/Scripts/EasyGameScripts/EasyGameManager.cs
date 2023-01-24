using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyGameManager : MonoBehaviour
{
    List<int> oneToNine = new List<int>();

    int[] UpLeft = new int[9];
    int[] UpMid = new int[9];
    int[] UpRight = new int[9];

    int[] MiddleLeft = new int[9];
    int[] Middle = new int[9];
    int[] MiddleRight = new int[9];

    int[] BottomLeft = new int[9];
    int[] BottomMid = new int[9];
    int[] BottomRight = new int[9];

    int tempNumberPicker;

    string str = "";

    [SerializeField] GameObject[] ObjUpLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjUpMid = new GameObject[9];
    [SerializeField] GameObject[] ObjUpRight = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddleLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddle = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddleRight = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomMid = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomRight = new GameObject[9];


    void Start()
    {
        ListRefill(oneToNine);
        SetZero();
        PushTable();
        CreateNumbers();
    }


    void Update()
    {

    }

    public void CreateNumbers()
    {
        for(int i = 0; i < 9; i++) // For UpLeft Square
        {
            tempNumberPicker = Random.Range(0, oneToNine.Count);
            /*Debug.Log("i: " + i + " // oneToNine.count : " + oneToNine.Count + " // tempNumberPicker: " + tempNumberPicker);
            for(int j = 0; j < oneToNine.Count; j++)
            {
                Debug.Log(oneToNine[j].ToString() + " -- ");
            }*/
            UpLeft[i] = oneToNine[tempNumberPicker];
            //Debug.Log("UpLeft[" + i +"]: " + UpLeft[i]);
            oneToNine.RemoveAt(tempNumberPicker);
        }

        for(int i = 0; i < 9; i++) // For UpMid Square
        {
            str = "possibleNumbers for upMid : ";
            NumberAccuracyChecker.Instance.FindPossibleNumbers(Middle, BottomMid, UpMid, UpLeft, UpRight, i);
            tempNumberPicker = Random.Range(0, NumberAccuracyChecker.Instance.possibleNumbers.Count);
            Debug.Log("tempNumberPicker for UpMid : " + tempNumberPicker);
            for (int z = 0; z < NumberAccuracyChecker.Instance.possibleNumbers.Count; z++)
            {
                str += NumberAccuracyChecker.Instance.possibleNumbers[z].ToString() + " -- ";
            }
            Debug.Log(str);
            UpMid[i] = NumberAccuracyChecker.Instance.possibleNumbers[tempNumberPicker];
            Debug.Log("UpMid-" + i + " : " + UpMid[i].ToString());
        }

        for (int i = 0; i < 9; i++) // For UpRight Square
        {
            str = " possibleNumbers for UpLeft : ";
            NumberAccuracyChecker.Instance.FindPossibleNumbers(MiddleRight, BottomRight, UpRight, UpLeft, UpMid, i);
            tempNumberPicker = Random.Range(0, NumberAccuracyChecker.Instance.possibleNumbers.Count);
            Debug.Log("tempNumberPicker for UpRight : " + tempNumberPicker);
            for (int z = 0; z < NumberAccuracyChecker.Instance.possibleNumbers.Count; z++)
            {
                str += NumberAccuracyChecker.Instance.possibleNumbers[z].ToString() + " -- ";
            }
            Debug.Log(str);
            UpRight[i] = NumberAccuracyChecker.Instance.possibleNumbers[tempNumberPicker];
            Debug.Log("UpRight-" + i + " : " + UpRight[i].ToString());
        }
    }

    public void SetZero()
    {
        for(int i = 0; i < 9; i++)
        {
            UpLeft[i] = 0;
            UpMid[i] = 0;
            UpRight[i] = 0;
            MiddleLeft[i] = 0;
            Middle[i] = 0;
            MiddleRight[i] = 0;
            BottomLeft[i] = 0;
            BottomMid[i] = 0;
            BottomRight[i] = 0;
        }
    }

    /*public void SetMinusOne()
    {
        for (int i = 0; i < 9; i++)
        {
            UpLeft[i] = -1;
            UpMid[i] = -1;
            UpRight[i] = -1;
            MiddleLeft[i] = -1;
            Middle[i] = -1;
            MiddleRight[i] = -1;
            BottomLeft[i] = -1;
            BottomMid[i] = -1;
            BottomRight[i] = -1;
        }
        PushTable();
    }*/

    public void PushTable()
    {
        for (int i = 0; i < 9; i++)
        {
            if (UpLeft[i] > 0) ObjUpLeft[i].GetComponent<Text>().text = UpLeft[i].ToString();
            else ObjUpLeft[i].GetComponent<Text>().text = " ";
            if (UpMid[i] > 0) ObjUpMid[i].GetComponent<Text>().text = UpMid[i].ToString();
            else ObjUpMid[i].GetComponent<Text>().text = " ";
            if (UpRight[i] > 0) ObjUpRight[i].GetComponent<Text>().text = UpRight[i].ToString();
            else ObjUpRight[i].GetComponent<Text>().text = " ";

            if (MiddleLeft[i] > 0) ObjMiddleLeft[i].GetComponent<Text>().text = MiddleLeft[i].ToString();
            else ObjMiddleLeft[i].GetComponent<Text>().text = " ";
            if (Middle[i] > 0) ObjMiddle[i].GetComponent<Text>().text = Middle[i].ToString();
            else ObjMiddle[i].GetComponent<Text>().text = " ";
            if (MiddleRight[i] > 0) ObjMiddleRight[i].GetComponent<Text>().text = MiddleRight[i].ToString();
            else ObjMiddleRight[i].GetComponent<Text>().text = " ";

            if (BottomLeft[i] > 0) ObjBottomLeft[i].GetComponent<Text>().text = BottomLeft[i].ToString();
            else ObjBottomLeft[i].GetComponent<Text>().text = " ";
            if (BottomMid[i] > 0) ObjBottomMid[i].GetComponent<Text>().text = BottomMid[i].ToString();
            else ObjBottomMid[i].GetComponent<Text>().text = " ";
            if (BottomRight[i] > 0) ObjBottomRight[i].GetComponent<Text>().text = BottomRight[i].ToString();
            else ObjBottomRight[i].GetComponent<Text>().text = " ";
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
