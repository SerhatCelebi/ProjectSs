using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyGameManager : MonoBehaviour
{
    int[] UpLeft = new int[9];
    int[] UpMid = new int[9];
    int[] UpRight = new int[9];

    int[] MiddleLeft = new int[9];
    int[] Middle = new int[9];
    int[] MiddleRight = new int[9];

    int[] BottomLeft = new int[9];
    int[] BottomMid = new int[9];
    int[] BottomRight = new int[9];

    public int[][] allSquares = new int[9][];

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
        allSquares[0] = UpLeft;
        allSquares[1] = UpMid;
        allSquares[2] = UpRight;
        allSquares[3] = MiddleLeft;
        allSquares[4] = Middle;
        allSquares[5] = MiddleRight;
        allSquares[6] = BottomLeft;
        allSquares[7] = BottomMid;
        allSquares[8] = BottomRight;
        SudokuGenerator.Instance.GenerateSudoku(0, 3);
        TakeNumbers();
    }
    void Update()
    {
        
    }
    void TakeNumbers()
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                allSquares[i][j] = SudokuGenerator.Instance.Squares[i][j];
            }
        }
    }

    public void SetZero()
    {
        for (int i = 0; i < 9; i++)
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
    public void PushTable()
    {

        for (int i = 0; i < 9; i++)
        {
            ObjUpLeft[i].GetComponent<Text>().text = UpLeft[i].ToString();
            ObjUpMid[i].GetComponent<Text>().text = UpMid[i].ToString();
            ObjUpRight[i].GetComponent<Text>().text = UpRight[i].ToString();
            ObjMiddleLeft[i].GetComponent<Text>().text = MiddleLeft[i].ToString();
            ObjMiddle[i].GetComponent<Text>().text = Middle[i].ToString();
            ObjMiddleRight[i].GetComponent<Text>().text = MiddleRight[i].ToString();
            ObjBottomLeft[i].GetComponent<Text>().text = BottomLeft[i].ToString();
            ObjBottomMid[i].GetComponent<Text>().text = BottomMid[i].ToString();
            ObjBottomRight[i].GetComponent<Text>().text = BottomRight[i].ToString();
        }

        //int visibles = 55;
        //int tempSquare, tempCell;

        //while(visibles > 0)
        //{
        //    tempSquare = Random.Range(0, 9);
        //    tempCell = Random.Range(0, 9);

        //    switch (tempSquare)
        //    {
        //        case 0:
        //            if(ObjUpLeft[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjUpLeft[tempCell].GetComponent<Text>().text = UpLeft[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 1:
        //            if (ObjUpMid[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjUpMid[tempCell].GetComponent<Text>().text = UpMid[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 2:
        //            if (ObjUpRight[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjUpRight[tempCell].GetComponent<Text>().text = UpRight[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 3:
        //            if (ObjMiddleLeft[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjMiddleLeft[tempCell].GetComponent<Text>().text = MiddleLeft[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 4:
        //            if (ObjMiddle[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjMiddle[tempCell].GetComponent<Text>().text = Middle[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 5:
        //            if (ObjMiddleRight[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjMiddleRight[tempCell].GetComponent<Text>().text = MiddleRight[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 6:
        //            if (ObjBottomLeft[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjBottomLeft[tempCell].GetComponent<Text>().text = BottomLeft[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 7:
        //            if (ObjBottomMid[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjBottomMid[tempCell].GetComponent<Text>().text = BottomMid[tempCell].ToString();
        //            visibles--;
        //            break;
        //        case 8:
        //            if (ObjBottomRight[tempCell].GetComponent<Text>().text != " ")
        //            {
        //                continue;
        //            }
        //            ObjBottomRight[tempCell].GetComponent<Text>().text = BottomRight[tempCell].ToString();
        //            visibles--;
        //            break;
        //        default:
        //            break;
        //    }
        //}

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
