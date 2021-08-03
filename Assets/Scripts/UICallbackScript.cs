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

    //---Game Menu---//
    public void onShoot()
    {
        shoot = true;
    }
    public void onShopButton()
    {
        gameMenu.SetActive(false);
        shopMenu.SetActive(true);
    }
    public void onReturn()
    {
        gameMenu.SetActive(true);
        shopMenu.SetActive(false);
    }
    //---Game Menu---//


    //---Shop Menu---//
    public void onBuy()
    {

    }

    //---Shop Menu---//
}
