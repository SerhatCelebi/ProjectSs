using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    int[] numberCounter = new int[9];
    int[] backupNumberCounter = new int[9];

    [SerializeField] GameObject[] ObjUpLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjUpMid = new GameObject[9];
    [SerializeField] GameObject[] ObjUpRight = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddleLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddle = new GameObject[9];
    [SerializeField] GameObject[] ObjMiddleRight = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomLeft = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomMid = new GameObject[9];
    [SerializeField] GameObject[] ObjBottomRight = new GameObject[9];

    [SerializeField] Text[] numberCounterText = new Text[9];

    GameObject[][] allObjSquares = new GameObject[9][];

    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] Text mistakesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text hintCountText;
    [SerializeField] Text noteModeBoolText;

    int hideCount = 31;
    int[] backupHiddenSquare = new int[31];
    int[] backupHiddenCell = new int[31];

    int mistakes = 0, hintCount = 10;
    float score = 0f;

    bool noteMode = false;
    bool isGameOver = false;
    bool isPaused = false;

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
        HideNumbers();
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
            CellHighlighter.Instance.TurnOffHighlights();
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = false;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            selectedIndexes[0] = slctdSq;
            selectedIndexes[1] = slctdCl;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = true;
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 0.2f, 0.31f);
        }
        SendForHighlight();
    }

    /// <summary>
    /// BUTTON METHODS ...........................................
    /// </summary>

    public void PauseButton()
    {
        pauseScreen.gameObject.SetActive(true);
        isPaused = true;
        Timer.Instance.PauseTimer();
    }
    public void RestartButton()
    {
        RestartGame();
    }
    public void ContinueButton()
    {
        pauseScreen.gameObject.SetActive(false);
        isPaused = false;
        Timer.Instance.StartTimer();
    }

    public void EraseButton()
    {
        if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = " ";
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().EraseNotes();
        }
    }
    public void NoteModeButton()
    {
        if (noteMode)
        {
            noteMode = false;
            noteModeBoolText.text = "Off";
        }
        else
        {
            noteMode = true;
            noteModeBoolText.text = "On";
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
        mistakesText.text = "Mistakes : " + mistakes.ToString() + " / 5";
    }
    void UpdateScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }
    void UpdateNumberCounterText()
    {
        for(int i = 0; i < 9; i++)
        {
            numberCounterText[i].text = numberCounter[i].ToString();
        }
    }

    public void BackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Number1Button()
    {
        CheckNumberButton(1);
    }
    public void Number2Button()
    {
        CheckNumberButton(2);
    }
    public void Number3Button()
    {
        CheckNumberButton(3);
    }
    public void Number4Button()
    {
        CheckNumberButton(4);
    }
    public void Number5Button()
    {
        CheckNumberButton(5);
    }
    public void Number6Button()
    {
        CheckNumberButton(6);
    }
    public void Number7Button()
    {
        CheckNumberButton(7);
    }
    public void Number8Button()
    {
        CheckNumberButton(8);
    }
    public void Number9Button()
    {
        CheckNumberButton(9);
    }
    void CheckNumberButton(int number)
    {
        if (noteMode)
        {
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().TakeNote(number);
        }
        else
        {

            if (allSquares[selectedIndexes[0]][selectedIndexes[1]] == number && !(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().isSolved))
            {
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<Text>().text = number.ToString();
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<Text>().color = Color.cyan;
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().isSolved = true;
                score += 100;
                UpdateScoreText();
                UpdateNumberCounter(number, true);
                UpdateNumberCounterText();
            }
            else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().isSolved))
            {
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<Text>().text = number.ToString();
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<Text>().color = Color.red;
                mistakes++;
                if (mistakes >= 5) GameOverCase();
                UpdateMistakesText();
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().EraseNotes();
            }
        }
    }

    void GameOverCase()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Timer.Instance.StopTimer();
    }
    void RestartGame()
    {
        score = 0;
        mistakes = 0;
        ReHideNumbers();
        isGameOver = false;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        UpdateMistakesText();
        UpdateScoreText();
        ActivateNumberButtons();
        UseNumberCounterBackup();
        UpdateNumberCounterText();
        Timer.Instance.StartTimer();
    }
    void ReHideNumbers()
    {
        for (int i = 0; i < 31; i++)
        {
            allObjSquares[backupHiddenSquare[i]][backupHiddenCell[i]].GetComponent<Text>().text = " ";
            allObjSquares[backupHiddenSquare[i]][backupHiddenCell[i]].GetComponent<NumberCell>().isSolved = false;
            allObjSquares[backupHiddenSquare[i]][backupHiddenCell[i]].GetComponent<NumberCell>().EraseNotes();
        }
    }
    void UseNumberCounterBackup()
    {
        for(int i = 0; i < 9; i++)
        {
            numberCounter[i] = backupNumberCounter[i];
        }
    }
    void ActivateNumberButtons()
    {
        for(int i = 0; i < 9; i++)
        {
            if (!numberCounterText[i].gameObject.transform.parent.gameObject.activeSelf)
            {
                numberCounterText[i].gameObject.transform.parent.gameObject.SetActive(true);
            }
        }
    }
    void SendForHighlight()
    {
        switch (selectedIndexes[0])
        {
            case 0:
                CellHighlighter.Instance.HighlightCells(ObjUpLeft, ObjMiddleLeft, ObjBottomLeft, ObjUpMid, ObjUpRight, selectedIndexes[1]);
                break;
            case 1:
                CellHighlighter.Instance.HighlightCells(ObjUpMid, ObjMiddle, ObjBottomMid, ObjUpLeft, ObjUpRight, selectedIndexes[1]);
                break;
            case 2:
                CellHighlighter.Instance.HighlightCells(ObjUpRight, ObjMiddleRight, ObjBottomRight, ObjUpLeft, ObjUpMid, selectedIndexes[1]);
                break;
            case 3:
                CellHighlighter.Instance.HighlightCells(ObjMiddleLeft, ObjUpLeft, ObjBottomLeft, ObjMiddle, ObjMiddleRight, selectedIndexes[1]);
                break;
            case 4:
                CellHighlighter.Instance.HighlightCells(ObjMiddle, ObjUpMid, ObjBottomMid, ObjMiddleLeft, ObjMiddleRight, selectedIndexes[1]);
                break;
            case 5:
                CellHighlighter.Instance.HighlightCells(ObjMiddleRight, ObjUpRight, ObjBottomRight, ObjMiddleLeft, ObjMiddle, selectedIndexes[1]);
                break;
            case 6:
                CellHighlighter.Instance.HighlightCells(ObjBottomLeft, ObjUpLeft, ObjMiddleLeft, ObjBottomMid, ObjBottomRight, selectedIndexes[1]);
                break;
            case 7:
                CellHighlighter.Instance.HighlightCells(ObjBottomMid, ObjUpMid, ObjMiddle, ObjBottomLeft, ObjBottomRight, selectedIndexes[1]);
                break;
            case 8:
                CellHighlighter.Instance.HighlightCells(ObjBottomRight, ObjUpRight, ObjMiddleRight, ObjBottomLeft, ObjBottomMid, selectedIndexes[1]);
                break;
            default:
                break;
        }
    }
    void TakeNumbers()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
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
    void UpdateNumberCounter(int number, bool isSubtraction)
    {
        if (isSubtraction)
        {
            numberCounter[number - 1]--;
            if(numberCounter[number - 1] == 0)
            {
                numberCounterText[number -1].gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            numberCounter[number - 1]++;
            backupNumberCounter[number - 1]++;
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
    }
    void HideNumbers()
    {
        int tempSquare, tempCell;

        while (hideCount > 0)
        {
            tempSquare = Random.Range(0, 9);
            tempCell = Random.Range(0, 9);
            backupHiddenSquare[31 - hideCount] = tempSquare;
            backupHiddenCell[31 - hideCount] = tempCell;

            switch (tempSquare)
            {
                case 0:
                    if (ObjUpLeft[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjUpLeft[tempCell].GetComponent<Text>().text = " ";
                        ObjUpLeft[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(UpLeft[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 1:
                    if (ObjUpMid[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjUpMid[tempCell].GetComponent<Text>().text = " ";
                        ObjUpMid[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(UpMid[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 2:
                    if (ObjUpRight[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjUpRight[tempCell].GetComponent<Text>().text = " ";
                        ObjUpRight[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(UpRight[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 3:
                    if (ObjMiddleLeft[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjMiddleLeft[tempCell].GetComponent<Text>().text = " ";
                        ObjMiddleLeft[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(MiddleLeft[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 4:
                    if (ObjMiddle[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjMiddle[tempCell].GetComponent<Text>().text = " ";
                        ObjMiddle[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(Middle[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 5:
                    if (ObjMiddleRight[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjMiddleRight[tempCell].GetComponent<Text>().text = " ";
                        ObjMiddleRight[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(MiddleRight[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 6:
                    if (ObjBottomLeft[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjBottomLeft[tempCell].GetComponent<Text>().text = " ";
                        ObjBottomLeft[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(BottomLeft[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 7:
                    if (ObjBottomMid[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjBottomMid[tempCell].GetComponent<Text>().text = " ";
                        ObjBottomMid[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(BottomMid[tempCell], false);
                        hideCount--;
                    }
                    break;
                case 8:
                    if (ObjBottomRight[tempCell].GetComponent<NumberCell>().isSolved)
                    {
                        ObjBottomRight[tempCell].GetComponent<Text>().text = " ";
                        ObjBottomRight[tempCell].GetComponent<NumberCell>().isSolved = false;
                        UpdateNumberCounter(BottomRight[tempCell], false);
                        hideCount--;
                    }
                    break;
                default:
                    break;
            }
        }
        UpdateNumberCounterText();
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
