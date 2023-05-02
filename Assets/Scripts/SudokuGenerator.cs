using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGenerator : MonoBehaviour
{
    public static SudokuGenerator Instance;

    public int[] Square0 = new int[9];
    public int[] Square1 = new int[9];
    public int[] Square2 = new int[9];
    public int[] Square3 = new int[9];
    public int[] Square4 = new int[9];
    public int[] Square5 = new int[9];
    public int[] Square6 = new int[9];
    public int[] Square7 = new int[9];
    public int[] Square8 = new int[9];

    public int[][] Squares = new int[9][];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Squares[0] = Square0;
        Squares[1] = Square1;
        Squares[2] = Square2;
        Squares[3] = Square3;
        Squares[4] = Square4;
        Squares[5] = Square5;
        Squares[6] = Square6;
        Squares[7] = Square7;
        Squares[8] = Square8;
        FillFirstRow();
    }
    void Update()
    {
        
    }

    public bool GenerateSudoku(int tmpSq = 0, int tmpCl = 3)
    {
        if(tmpCl == 3)
        {
            if(tmpSq == 0)
            {

            }
            else if(tmpSq != 2 && tmpSq != 5 && tmpSq != 8)
            {
                tmpSq += 1;
                tmpCl = 0;
            }
            else
            {
                tmpSq -= 2;
                tmpCl = 3;
            }
        }
        else if(tmpCl == 6)
        {
            if (tmpSq != 2 && tmpSq != 5 && tmpSq != 8)
            {
                tmpSq += 1;
                tmpCl = 3;
            }
            else
            {
                tmpSq -= 2;
                tmpCl = 6;
            }
        }
        else if(tmpCl >= 9)
        {
            if (tmpSq >= 8)
            {
                //DebugSudoku();
                return true;
            }

            if (tmpSq != 2 && tmpSq != 5 && tmpSq != 8)
            {
                tmpSq += 1;
                tmpCl = 6;
            }
            else
            {
                tmpSq += 1;
                tmpCl = 0;
            }
        }

        for(int num = 1; num <= 9; num++)
        {
            if (IsNumSafe(tmpSq, tmpCl, num))
            {
                Squares[tmpSq][tmpCl] = num;
                if (GenerateSudoku(tmpSq, tmpCl + 1))
                    return true;

                Squares[tmpSq][tmpCl] = 0;
            }
        }
        return false;
    }

    bool IsNumSafe(int Sq, int Cl, int Nm)
    {
        
        switch (Sq)
        {
            case 0:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square0, Square3, Square6, Square1, Square2, Cl, Nm);
            case 1:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square1, Square4, Square7, Square0, Square2, Cl, Nm);
            case 2:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square2, Square5, Square8, Square0, Square1, Cl, Nm);
            case 3:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square3, Square0, Square6, Square4, Square5, Cl, Nm);
            case 4:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square4, Square1, Square7, Square3, Square5, Cl, Nm);
            case 5:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square5, Square2, Square8, Square3, Square4, Cl, Nm);
            case 6:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square6, Square0, Square3, Square7, Square8, Cl, Nm);
            case 7:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square7, Square1, Square4, Square6, Square8, Cl, Nm);
            case 8:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Square8, Square2, Square5, Square6, Square7, Cl, Nm);
            default:
                Debug.Log("Square Not Found..!");
                return false;
        }
    }


    void FillFirstRow()
    {
        int rand;
        List<int> oneToNine = new List<int>() {1, 2, 3 , 4, 5, 6, 7, 8, 9};
        for (int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, oneToNine.Count);
            Square0[i] = oneToNine[rand];
            oneToNine.RemoveAt(rand);

            rand = Random.Range(0, oneToNine.Count);
            Square1[i] = oneToNine[rand];
            oneToNine.RemoveAt(rand);

            rand = Random.Range(0, oneToNine.Count);
            Square2[i] = oneToNine[rand];
            oneToNine.RemoveAt(rand);
        }
    }

    /*void DebugSudoku()
    {
        string str = " ";

        for (int i = 0; i < 3; i++)
        {
            str += Square0[i].ToString();
            str += "  ";
        }
        for (int i = 0; i < 3; i++)
        {
            str += Square1[i].ToString();
            str += "  ";
        }
        for (int i = 0; i < 3; i++)
        {
            str += Square2[i].ToString();
            str += "  ";
        }
        Debug.Log(str);

        str = " ";
        for (int i = 3; i < 6; i++)
        {
            str += Square0[i].ToString();
            str += "  ";
        }
        for (int i = 3; i < 6; i++)
        {
            str += Square1[i].ToString();
            str += "  ";
        }
        for (int i = 3; i < 6; i++)
        {
            str += Square2[i].ToString();
            str += "  ";
        }
        Debug.Log(str);

        str = " ";
        for (int i = 6; i < 9; i++)
        {
            str += Square0[i].ToString();
            str += "  ";
        }
        for (int i = 6; i < 9; i++)
        {
            str += Square1[i].ToString();
            str += "  ";
        }
        for (int i = 6; i < 9; i++)
        {
            str += Square2[i].ToString();
            str += "  ";
        }
        Debug.Log(str);
        Debug.Log("---------------------------------------");

        str = " ";
        for (int i = 0; i < 3; i++)
        {
            str += Square3[i].ToString();
            str += "  ";
        }
        for (int i = 0; i < 3; i++)
        {
            str += Square4[i].ToString();
            str += "  ";
        }
        for (int i = 0; i < 3; i++)
        {
            str += Square5[i].ToString();
            str += "  ";
        }
        Debug.Log(str);

        str = " ";
        for (int i = 3; i < 6; i++)
        {
            str += Square3[i].ToString();
            str += "  ";
        }
        for (int i = 3; i < 6; i++)
        {
            str += Square4[i].ToString();
            str += "  ";
        }
        for (int i = 3; i < 6; i++)
        {
            str += Square5[i].ToString();
            str += "  ";
        }
        Debug.Log(str);

        str = " ";
        for (int i = 6; i < 9; i++)
        {
            str += Square3[i].ToString();
            str += "  ";
        }
        for (int i = 6; i < 9; i++)
        {
            str += Square4[i].ToString();
            str += "  ";
        }
        for (int i = 6; i < 9; i++)
        {
            str += Square5[i].ToString();
            str += "  ";
        }
        Debug.Log(str);
        Debug.Log("---------------------------------------");

        str = " ";
        for (int i = 0; i < 3; i++)
        {
            str += Square6[i].ToString();
            str += "  ";
        }
        for (int i = 0; i < 3; i++)
        {
            str += Square7[i].ToString();
            str += "  ";
        }
        for (int i = 0; i < 3; i++)
        {
            str += Square8[i].ToString();
            str += "  ";
        }
        Debug.Log(str);

        str = " ";
        for (int i = 3; i < 6; i++)
        {
            str += Square6[i].ToString();
            str += "  ";
        }
        for (int i = 3; i < 6; i++)
        {
            str += Square7[i].ToString();
            str += "  ";
        }
        for (int i = 3; i < 6; i++)
        {
            str += Square8[i].ToString();
            str += "  ";
        }
        Debug.Log(str);

        str = " ";
        for (int i = 6; i < 9; i++)
        {
            str += Square6[i].ToString();
            str += "  ";
        }
        for (int i = 6; i < 9; i++)
        {
            str += Square7[i].ToString();
            str += "  ";
        }
        for (int i = 6; i < 9; i++)
        {
            str += Square8[i].ToString();
            str += "  ";
        }
        Debug.Log(str);
    }*/

}
