using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICallbackScript : MonoBehaviour
{
    [HideInInspector]
    public bool shoot = false;

    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject shopMenu;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public void onShoot()
    {
        shoot = true;
    }
    public void onShopButton()
    {
        SoundManagerScript.PlaySound("Button");
        gameMenu.SetActive(false);
        shopMenu.SetActive(true);
    }
    public void onReturn()
    {
        SoundManagerScript.PlaySound("Button");
        gameMenu.SetActive(true);
        shopMenu.SetActive(false);
    }
    public void openOptions(GameObject options)
    {
        options.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void closeOptions(GameObject options)
    {
        options.SetActive(false);
        gameMenu.SetActive(true);
    }
}
