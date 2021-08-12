using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public static bool aim_mode = true;
    public static bool unli_ammo = false;
    [SerializeField] GameObject Joystick;
    [SerializeField] GameObject OptionsUI;

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
            Joystick.SetActive(true);
        }
        else
        {
            aim_mode = false;
            Joystick.SetActive(false);
        }

        Debug.Log($"Options: {dd.options[dd.value].text}");
    }

    public void OpenHelp(GameObject help)
    {
        help.SetActive(true);
    }
    public void OpenDebug(GameObject debug)
    {
        debug.SetActive(true);
    }
    public void CloseDebug(GameObject debug)
    {
        debug.SetActive(false);
    }


    public void BackMainMenu()
    {
        SceneManager.LoadScene("Title Scene");
    }

    public void OnMaxHealth(Toggle t)
    {
        if (t.isOn)
        {
            GameSystem.health = 99999999;
        }
        else 
        {
            GameSystem.health = 100;
        }
    }

    public void OnMaxCredits(Toggle t)
    {
        if (t.isOn)
        {
            GameSystem.credits = 99999999;
        }
        else
        {
            GameSystem.credits = 500;
        }
    }

    public void OnMaxAmmo(Toggle t)
    {
        if (t.isOn)
        {
            unli_ammo = true;
        }
        else
        {
            unli_ammo = false;
        }
    }

}
