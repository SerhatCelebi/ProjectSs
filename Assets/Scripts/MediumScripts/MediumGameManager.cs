using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MediumGameManager : MonoBehaviour, ISelectable
{
    NumberHighlighter numberHighlighter;
    CellHighlighter cellHighlighter;
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

    bool[] isSquaresFilled = new bool[9];

    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject endGameScreen;

    [SerializeField] Text mistakesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text hintCountText;
    [SerializeField] Text noteModeBoolText;
    [SerializeField] Text statisticsText;

    static int hideCount = 41;
    int[] backupHiddenSquare = new int[hideCount];
    int[] backupHiddenCell = new int[hideCount];

    int mistakes = 0, hintCount = 6;
    float score = 0f;

    bool noteMode = false;
    bool isGameOver = false;
    bool isPaused = false;
    bool isCellSelected = false;
    bool isGameEnded = false;

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
        numberHighlighter = gameObject.GetComponent<NumberHighlighter>();
        cellHighlighter = gameObject.GetComponent<CellHighlighter>();
        SudokuGenerator.Instance.GenerateSudoku(0, 3);
        TakeNumbers();
        PushTable();
        HideNumbers();
        StartUpConfigs();
        Timer.Instance.StartTimer();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Game Control Methods..!
    /// </summary>
    
    bool EndGameCheck()
    {
        /*for (int i = 0; i < 9; i++)
        {
            Debug.Log("Square " + i + " : " + isSquaresFilled[i]);
        }*/
        for (int i = 0; i < 9; i++)
        {
            if (!isSquaresFilled[i])
            {
                return false;
            }
        }
        return true;
    }

    void EndGameCase()
    {
        float duration = Timer.Instance.GetDuration();
        float tempMin = (int)duration / 60;
        float tempSec = (int)duration - (60 * tempMin);
        Timer.Instance.StopTimer();
        endGameScreen.SetActive(true);
        isGameEnded = true;
        statisticsText.text = "Score : " + score + "\n\nTime : " + tempMin + ":" + tempSec + "\n\nMistakes : " + mistakes;
    }
    void GameOverCase()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        UnselectCell();
        Timer.Instance.StopTimer();
    }
    void RestartGame()
    {
        score = 0;
        mistakes = 0;
        ReHideNumbers();
        isGameOver = false;
        isPaused = false;
        isGameEnded = false;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        endGameScreen.SetActive(false);
        UpdateMistakesText();
        UpdateScoreText();
        ActivateNumberButtons();
        UseNumberCounterBackup();
        UpdateNumberCounterText();
        UnselectCell();
        for (int i = 0; i < 9; i++)
        {
            isSquaresFilled[i] = false;
        }
        StartUpConfigs();
        Timer.Instance.SetDuration(0f, 0f);
        Timer.Instance.StartTimer();
    }
    void ReHideNumbers()
    {
        for (int i = 0; i < hideCount; i++)
        {
            allObjSquares[backupHiddenSquare[i]][backupHiddenCell[i]].GetComponent<Text>().text = " ";
            allObjSquares[backupHiddenSquare[i]][backupHiddenCell[i]].GetComponent<NumberCell>().isSolved = false;
            allObjSquares[backupHiddenSquare[i]][backupHiddenCell[i]].GetComponent<NumberCell>().EraseNotes();
        }
    }
    void UseNumberCounterBackup()
    {
        for (int i = 0; i < 9; i++)
        {
            numberCounter[i] = backupNumberCounter[i];
        }
    }
    void ActivateNumberButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            if (!numberCounterText[i].gameObject.transform.parent.gameObject.activeSelf)
            {
                numberCounterText[i].gameObject.transform.parent.gameObject.SetActive(true);
            }
        }
    }
    //************************************************************************************************************************************************



    /// <summary>
    /// Button Methods..!
    /// </summary>
    public void HintButton()
    {
        AudioManager.Instance.Play("Click");
        if (!allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().isSolved)
        {
            if (IsButtonAvailable() && isCellSelected)
            {
                if (hintCount > 0)
                {
                    allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = allSquares[selectedIndexes[0]][selectedIndexes[1]].ToString();
                    StartCoroutine(HintDeclaration(selectedIndexes[0], selectedIndexes[1]));
                    allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved = true;
                    UpdateNumberCounter(allSquares[selectedIndexes[0]][selectedIndexes[1]], true);
                    UpdateNumberCounterText();
                    //hintCount--;
                    hintCountText.text = hintCount.ToString();
                    isSquaresFilled[selectedIndexes[0]] = gameObject.GetComponent<FilledSquareChecker>().CheckSquareIsFilled(allObjSquares[selectedIndexes[0]]);
                    if (EndGameCheck())
                    {
                        EndGameCase();
                    }
                }
            }
        }
    }
    IEnumerator HintDeclaration(int i, int j)
    {
        allObjSquares[i][j].gameObject.GetComponent<Text>().color = new Color(0.625f, 0f, 0.625f);
        yield return new WaitForSeconds(1.5f);
        allObjSquares[i][j].gameObject.GetComponent<Text>().color = Color.cyan;
        yield return null;
    }
    public void PauseButton()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable())
        {
            pauseScreen.gameObject.SetActive(true);
            isPaused = true;
            UnselectCell();
            Timer.Instance.PauseTimer();
        }
        else
        {
            ContinueButton();
        }
    }
    public void RestartButton()
    {
        AudioManager.Instance.Play("Click");
        RestartGame();
    }
    public void ContinueButton()
    {
        AudioManager.Instance.Play("Click");
        pauseScreen.gameObject.SetActive(false);
        isPaused = false;
        Timer.Instance.StartTimer();
    }
    public void EraseButton()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSolved))
            {
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<Text>().text = " ";
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().EraseNotes();
            }
        }
    }
    public void NoteModeButton()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable())
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
    }
    public void BackToMenuButton()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable())
        {
            SceneManager.LoadScene(0);
        }
    }
    public void Number1Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(1);
        }
    }
    public void Number2Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(2);
        }
    }
    public void Number3Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(3);
        }
    }
    public void Number4Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(4);
        }
    }
    public void Number5Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(5);
        }
    }
    public void Number6Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(6);
        }
    }
    public void Number7Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(7);
        }
    }
    public void Number8Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(8);
        }
    }
    public void Number9Button()
    {
        AudioManager.Instance.Play("Click");
        if (IsButtonAvailable() && isCellSelected)
        {
            CheckNumberButton(9);
        }
    }
    //************************************************************************************************************************************************



    /// <summary>
    /// Gameplay Methods
    /// </summary>
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
                isSquaresFilled[selectedIndexes[0]] = gameObject.GetComponent<FilledSquareChecker>().CheckSquareIsFilled(allObjSquares[selectedIndexes[0]]);
                if (EndGameCheck())
                {
                    EndGameCase();
                }
            }
            else if (!(allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().isSolved))
            {
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<Text>().text = number.ToString();
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<Text>().color = Color.red;
                mistakes++;
                UpdateMistakesText();
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].GetComponent<NumberCell>().EraseNotes();
                if (mistakes >= 3) GameOverCase();
            }
        }
    }
    public void CellSelected(int slctdSq, int slctdCl)
    {
        if (IsButtonAvailable())
        {
            if (selectedIndexes[0] == -1 || selectedIndexes[1] == -1)
            {
                selectedIndexes[0] = slctdSq;
                selectedIndexes[1] = slctdCl;
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = true;
                isCellSelected = true;
                //allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 0.2f, 0.31f);
            }
            else
            {
                //CellHighlighter.Instance.TurnOffHighlights();
                cellHighlighter.TurnOffHighlights();
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = false;
                //allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
                selectedIndexes[0] = slctdSq;
                selectedIndexes[1] = slctdCl;
                allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = true;
                //allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 0.2f, 0.31f);
            }
            SendForHighlight();
        }
    }
    void UnselectCell()
    {
        if (isCellSelected)
        {
            //CellHighlighter.Instance.TurnOffHighlights();
            cellHighlighter.TurnOffHighlights();
            allObjSquares[selectedIndexes[0]][selectedIndexes[1]].gameObject.GetComponent<NumberCell>().isSelected = false;
            selectedIndexes[0] = -1;
            selectedIndexes[1] = -1;
            isCellSelected = false;
        }
    }
    //************************************************************************************************************************************************



    /// <summary>
    /// Startup Methods..!
    /// </summary>

    void StartUpConfigs()
    {
        for(int i = 0; i < 9; i++)
        {
            isSquaresFilled[i] = gameObject.GetComponent<FilledSquareChecker>().CheckSquareIsFilled(allObjSquares[i]);
            if(numberCounter[i] == 0)
            {
                numberCounterText[i].gameObject.transform.parent.gameObject.SetActive(false);
            }
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
        int tempHide = hideCount;
        while (hideCount > 0)
        {
            tempSquare = Random.Range(0, 9);
            tempCell = Random.Range(0, 9);
            backupHiddenSquare[tempHide - hideCount] = tempSquare;
            backupHiddenCell[tempHide - hideCount] = tempCell;

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
    //************************************************************************************************************************************************



    /// <summary>
    /// Updating Methods..!
    /// </summary>
    void UpdateMistakesText()
    {
        mistakesText.text = "Mistakes : " + mistakes.ToString() + " / 3";
    }
    void UpdateScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }
    void UpdateNumberCounterText()
    {
        for (int i = 0; i < 9; i++)
        {
            numberCounterText[i].text = numberCounter[i].ToString();
        }
    }
    void UpdateNumberCounter(int number, bool isSubtraction)
    {
        if (isSubtraction)
        {
            numberCounter[number - 1]--;
            if (numberCounter[number - 1] == 0)
            {
                numberCounterText[number - 1].gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            numberCounter[number - 1]++;
            backupNumberCounter[number - 1]++;
        }
    }

    bool IsButtonAvailable()
    {
        if (isPaused) return false;
        else if (isGameOver) return false;
        else if (isGameEnded) return false;
        else return true;
    }
    //************************************************************************************************************************************************



    /// <summary>
    /// Sending Methods..!
    /// </summary>
    void SendForHighlight()
    {
        switch (selectedIndexes[0])
        {
            case 0:
                //CellHighlighter.Instance.HighlightCells(ObjUpLeft, ObjMiddleLeft, ObjBottomLeft, ObjUpMid, ObjUpRight, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjUpLeft, ObjMiddleLeft, ObjBottomLeft, ObjUpMid, ObjUpRight, selectedIndexes[1]);
                break;
            case 1:
                //CellHighlighter.Instance.HighlightCells(ObjUpMid, ObjMiddle, ObjBottomMid, ObjUpLeft, ObjUpRight, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjUpMid, ObjMiddle, ObjBottomMid, ObjUpLeft, ObjUpRight, selectedIndexes[1]);
                break;
            case 2:
                //CellHighlighter.Instance.HighlightCells(ObjUpRight, ObjMiddleRight, ObjBottomRight, ObjUpLeft, ObjUpMid, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjUpRight, ObjMiddleRight, ObjBottomRight, ObjUpLeft, ObjUpMid, selectedIndexes[1]);
                break;
            case 3:
                //CellHighlighter.Instance.HighlightCells(ObjMiddleLeft, ObjUpLeft, ObjBottomLeft, ObjMiddle, ObjMiddleRight, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjMiddleLeft, ObjUpLeft, ObjBottomLeft, ObjMiddle, ObjMiddleRight, selectedIndexes[1]);
                break;
            case 4:
                //CellHighlighter.Instance.HighlightCells(ObjMiddle, ObjUpMid, ObjBottomMid, ObjMiddleLeft, ObjMiddleRight, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjMiddle, ObjUpMid, ObjBottomMid, ObjMiddleLeft, ObjMiddleRight, selectedIndexes[1]);
                break;
            case 5:
                //CellHighlighter.Instance.HighlightCells(ObjMiddleRight, ObjUpRight, ObjBottomRight, ObjMiddleLeft, ObjMiddle, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjMiddleRight, ObjUpRight, ObjBottomRight, ObjMiddleLeft, ObjMiddle, selectedIndexes[1]);
                break;
            case 6:
                //CellHighlighter.Instance.HighlightCells(ObjBottomLeft, ObjUpLeft, ObjMiddleLeft, ObjBottomMid, ObjBottomRight, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjBottomLeft, ObjUpLeft, ObjMiddleLeft, ObjBottomMid, ObjBottomRight, selectedIndexes[1]);
                break;
            case 7:
                //CellHighlighter.Instance.HighlightCells(ObjBottomMid, ObjUpMid, ObjMiddle, ObjBottomLeft, ObjBottomRight, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjBottomMid, ObjUpMid, ObjMiddle, ObjBottomLeft, ObjBottomRight, selectedIndexes[1]);
                break;
            case 8:
                //CellHighlighter.Instance.HighlightCells(ObjBottomRight, ObjUpRight, ObjMiddleRight, ObjBottomLeft, ObjBottomMid, selectedIndexes[1]);
                cellHighlighter.HighlightCells(ObjBottomRight, ObjUpRight, ObjMiddleRight, ObjBottomLeft, ObjBottomMid, selectedIndexes[1]);
                break;
        }
    }
    void SendToNumberHighlighter(int hlSq, int hlCl)
    {
        int num = allSquares[hlSq][hlCl];
        //NumberHighlighter.Instance.HighlightTheNumbers(allObjSquares, num);
        numberHighlighter.HighlightTheNumbers(allObjSquares, num);
    }
    //************************************************************************************************************************************************



    /// <summary>
    /// Interface Methods..!
    /// </summary>
    public void Select(int i, int j)
    {
        CellSelected(i, j);
    }
    public void Highlight(int i, int j)
    {
        SendToNumberHighlighter(i, j);
    }
    //************************************************************************************************************************************************
}
