using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyGameManager : MonoBehaviour
{
    public static EasyGameManager Instance;

    int[] UpLeft = new int[9];
    int[] UpMid = new int[9];
    int[] UpRight = new int[9];

    int[] MiddleLeft = new int[9];
    int[] Middle = new int[9];
    int[] MiddleRight = new int[9];

    int[] BottomLeft = new int[9];
    int[] BottomMid = new int[9];
    int[] BottomRight = new int[9];

    int[][] allSquares = new int[9][];

    int[] selectedIndexes = new int[2];

    [SerializeField] GameObject[] ObjUpLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjUpMid = new GameObject[9];
    [SerializeField] GameObject[] ObjUpRight = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddleLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddle = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddleRight = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomMid = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomRight = new GameObject[9];

    GameObject[][] allObjSquares = new GameObject[9][];

    [SerializeField] GameObject mistakesText;
    [SerializeField] GameObject scoreText;
    [SerializeField] Text hintCountText;

    int mistakes = 0, hintCount = 10;
    float score = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
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

        allObjSquares[0] = ObjUpLeft;
        allObjSquares[1] = ObjUpMid;
        allObjSquares[2] = ObjUpRight;
        allObjSquares[3] = ObjMiddleLeft;
        allObjSquares[4] = ObjMiddle;
        allObjSquares[5] = ObjMiddleRight;
        allObjSquares[6] = ObjBottomLeft;
        allObjSquares[7] = ObjBottomMid;
        allObjSquares[8] = ObjBottomRight;
        selectedIndexes[0] = -1;
        selectedIndexes[1] = -1;
        SudokuGenerator.Instance.GenerateSudoku(0, 3);
        TakeNumbers();
        PushTable();
        Timer.Instance.StartTimer();
    }
    void Update()
    {
        
    }

    public void CellSelected(int slctdSq, int slctdCl)
    {
        if(selectedIndexes[0] == -1 || selectedIndexes[1] == -1)
        {
            selectedIndexes[0] = slctdSq;
            selectedIndexes[1] = slctdCl;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = true;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 0.2f, 0.31f);
        }
        else
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = false;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            selectedIndexes[0] = slctdSq;
            selectedIndexes[1] = slctdCl;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = true;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 0.2f, 0.31f);
        }

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
    void PushTable()
    {

        for (int i = 0; i < 9; i++)
        {
            ObjUpLeft[i].GetComponent<Text>().text = UpLeft[i].ToString();
            ObjUpLeft[i].GetComponent<NumberCell>().isSolved = true;

            ObjUpMid[i].GetComponent<Text>().text = UpMid[i].ToString();
            ObjUpMid[i].GetComponent<NumberCell>().isSolved = true;

            ObjUpRight[i].GetComponent<Text>().text = UpRight[i].ToString();
            ObjUpRight[i].GetComponent<NumberCell>().isSolved = true;

            ObjMiddleLeft[i].GetComponent<Text>().text = MiddleLeft[i].ToString();
            ObjMiddleLeft[i].GetComponent<NumberCell>().isSolved = true;

            ObjMiddle[i].GetComponent<Text>().text = Middle[i].ToString();
            ObjMiddle[i].GetComponent<NumberCell>().isSolved = true;

            ObjMiddleRight[i].GetComponent<Text>().text = MiddleRight[i].ToString();
            ObjMiddleRight[i].GetComponent<NumberCell>().isSolved = true;

            ObjBottomLeft[i].GetComponent<Text>().text = BottomLeft[i].ToString();
            ObjBottomLeft[i].GetComponent<NumberCell>().isSolved = true;

            ObjBottomMid[i].GetComponent<Text>().text = BottomMid[i].ToString();
            ObjBottomMid[i].GetComponent<NumberCell>().isSolved = true;

            ObjBottomRight[i].GetComponent<Text>().text = BottomRight[i].ToString();
            ObjBottomRight[i].GetComponent<NumberCell>().isSolved = true;
        }

        int hideCount = 31;
        int tempSquare, tempCell;

        while(hideCount > 0)
        {
            tempSquare = Random.Range(0, 9);
            tempCell = Random.Range(0, 9);

            switch (tempSquare)
            {
                case 0:
                    if (ObjUpLeft[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjUpLeft[tempCell].GetComponent<Text>().text = " ";
                        ObjUpLeft[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 1:
                    if (ObjUpMid[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjUpMid[tempCell].GetComponent<Text>().text = " ";
                        ObjUpMid[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 2:
                    if (ObjUpRight[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjUpRight[tempCell].GetComponent<Text>().text = " ";
                        ObjUpRight[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 3:
                    if (ObjMiddleLeft[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjMiddleLeft[tempCell].GetComponent<Text>().text = " ";
                        ObjMiddleLeft[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 4:
                    if (ObjMiddle[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjMiddle[tempCell].GetComponent<Text>().text = " ";
                        ObjMiddle[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 5:
                    if (ObjMiddleRight[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjMiddleRight[tempCell].GetComponent<Text>().text = " ";
                        ObjMiddleRight[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 6:
                    if (ObjBottomLeft[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjBottomLeft[tempCell].GetComponent<Text>().text = " ";
                        ObjBottomLeft[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 7:
                    if (ObjBottomMid[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjBottomMid[tempCell].GetComponent<Text>().text = " ";
                        ObjBottomMid[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                case 8:
                    if (ObjBottomRight[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjBottomRight[tempCell].GetComponent<Text>().text = " ";
                        ObjBottomRight[tempCell].GetComponent<NumberCell>().isSolved = false;
                        hideCount--;
                    }
                    break;
                default:
                    break;
            }
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

    public void Number1Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 1 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "1";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if(!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "1";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number2Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 2 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "2";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "2";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number3Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 3 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "3";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "3";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number4Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 4 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "4";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "4";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number5Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 5 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "5";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "5";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number6Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 6 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "6";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "6";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number7Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 7 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "7";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "7";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number8Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 8 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "8";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "8";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }
    public void Number9Button()
    {
        if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == 9 && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "9";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.cyan;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            score += 100;
            UpdateScoreText();
        }
        else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = "9";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().color = Color.red;
            mistakes++;
            UpdateMistakesText();
        }
    }

    public void EraseButton()
    {
        if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = " ";
        }
    }
    public void HintButton()
    {
        if(hintCount > 0)
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = allSquares[selectedIndexes[0]][selectedIndexes[1]].ToString();
            StartCoroutine(HintDeclaration(selectedIndexes[0], selectedIndexes[1]));
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
            hintCount--;
            hintCountText.text = hintCount.ToString();
        }
    }
    IEnumerator HintDeclaration(int i, int j)
    {
        allObjSquares[i][j].gameObject.GetComponent<Text>().color = new Color(0.625f, 0f, 0.625f);
        yield return new WaitForSeconds(1.5f);
        allObjSquares[i][j].gameObject.GetComponent<Text>().color = Color.cyan;
        yield return null;
    }

    void UpdateMistakesText()
    {
        mistakesText.GetComponent<Text>().text = "Mistakes : " + mistakes.ToString() + " / 5";
    }
    void UpdateScoreText()
    {
        scoreText.GetComponent<Text>().text = "Score : " + score.ToString();
    }

    /*bool SendChecker(int Nm)
    {
        switch (selectedIndexes[0])
        {
            case 0:
                return NumberAccuracyChecker.Instance.IsNumAccurate(UpLeft, MiddleLeft, BottomLeft, UpMid, UpRight, selectedIndexes[1], Nm);
            case 1:
                return NumberAccuracyChecker.Instance.IsNumAccurate(UpMid, Middle, BottomMid, UpLeft, UpRight, selectedIndexes[1], Nm);
            case 2:
                return NumberAccuracyChecker.Instance.IsNumAccurate(UpRight, MiddleRight, BottomRight, UpLeft, UpMid, selectedIndexes[1], Nm);
            case 3:
                return NumberAccuracyChecker.Instance.IsNumAccurate(MiddleLeft, UpLeft, BottomLeft, Middle, MiddleRight, selectedIndexes[1], Nm);
            case 4:
                return NumberAccuracyChecker.Instance.IsNumAccurate(Middle, UpMid, BottomMid, MiddleLeft, MiddleRight, selectedIndexes[1], Nm);
            case 5:
                return NumberAccuracyChecker.Instance.IsNumAccurate(MiddleRight, UpRight, BottomRight, MiddleLeft, Middle, selectedIndexes[1], Nm);
            case 6:
                return NumberAccuracyChecker.Instance.IsNumAccurate(BottomLeft, UpLeft, MiddleLeft, BottomMid, BottomRight, selectedIndexes[1], Nm);
            case 7:
                return NumberAccuracyChecker.Instance.IsNumAccurate(BottomMid, UpMid, Middle, BottomLeft, BottomRight, selectedIndexes[1], Nm);
            case 8:
                return NumberAccuracyChecker.Instance.IsNumAccurate(BottomRight, UpRight, MiddleRight, BottomLeft, BottomMid, selectedIndexes[1], Nm);
            default:
                return false;
        }
    }*/

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
