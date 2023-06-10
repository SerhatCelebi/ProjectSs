using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilledSquareChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckSquareIsFilled(GameObject[] SquareObj)
    {
        for(int i = 0; i < 9; i++)
        {
            if (!SquareObj[i].GetComponent<NumberCell>().isSolved)
            {
                return false;
            }
        }
        return true;
    }
}
