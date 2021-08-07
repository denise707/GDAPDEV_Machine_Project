using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    protected WeaponSystem weaponsys;

    // Start is called before the first frame update
    void Start()
    {
        weaponsys = FindObjectOfType<WeaponSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Buy Weapons
    public void BuyAWP(Button button)
    {
        weaponsys.Buy("AWP");
        button.interactable = false;
    }

    //Upgrade Weapons
    
}
