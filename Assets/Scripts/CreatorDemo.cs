using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorDemo : MonoBehaviour
{
    public static CreatorDemo Instance;


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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Try()
    {

    }
}
