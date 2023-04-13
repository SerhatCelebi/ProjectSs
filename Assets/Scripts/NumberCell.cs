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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        EasyGameManager.Instance.CellSelected(squareIndex, cellIndex);
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.11f);
    }

}
