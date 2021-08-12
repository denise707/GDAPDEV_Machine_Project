using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static bool aim_mode = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAimChange(Dropdown dd)
    {
        if(dd.options[dd.value].text == "Crosshair") {
            aim_mode = true;
        }
        else
        {
            aim_mode = false;
        }
    }
}
