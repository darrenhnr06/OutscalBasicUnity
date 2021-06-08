using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Key a pressed");
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("Key a released");
        }
        else if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Left mouse button unclicked");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right mouse button clicked");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Right mouse button unclicked");
        }
    }
}
