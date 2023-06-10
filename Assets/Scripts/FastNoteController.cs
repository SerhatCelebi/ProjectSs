using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastNoteController : MonoBehaviour
{
    public static FastNoteController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeFastNote(int[][] allSquares, GameObject[][] objSquares)
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if (!objSquares[i][j].GetComponent<NumberCell>().isSolved)
                {
                    Send(i, j, objSquares);
                }
                for(int k = 0; k < 9; k++)
                {
                    if (!objSquares[i][j].GetComponent<NumberCell>().isSolved)
                    {
                        if (PossibleNumbersFinder.Instance.possibleNumbers.Contains(k))
                        {
                            objSquares[i][j].GetComponent<NumberCell>().TakeNote(k);
                        }
                    }
                }
            }
        }
    }

    void Send(int currSq, int currCell, GameObject[][] allObj)
    {
        switch (currSq)
        {
            case 0:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[0], allObj[1], allObj[2], allObj[3], allObj[6], currCell);
                break;
            case 1:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[1], allObj[0], allObj[2], allObj[4], allObj[7], currCell);
                break;
            case 2:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[2], allObj[0], allObj[1], allObj[5], allObj[8], currCell);
                break;
            case 3:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[3], allObj[4], allObj[5], allObj[0], allObj[6], currCell);
                break;
            case 4:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[4], allObj[3], allObj[5], allObj[1], allObj[7], currCell);
                break;
            case 5:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[5], allObj[3], allObj[4], allObj[2], allObj[8], currCell);
                break;
            case 6:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[6], allObj[7], allObj[8], allObj[0], allObj[3], currCell);
                break;
            case 7:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[7], allObj[6], allObj[8], allObj[1], allObj[4], currCell);
                break;
            case 8:
                PossibleNumbersFinder.Instance.FindPossibleNumbers(allObj[8], allObj[6], allObj[7], allObj[2], allObj[5], currCell);
                break;
        }
    }
}
