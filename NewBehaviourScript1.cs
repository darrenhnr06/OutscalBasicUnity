﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Hello Gamer");
        }

        else if(Input.GetMouseButton(0))
        {
            Debug.Log("Gamer in Action");
        }
    }
}
