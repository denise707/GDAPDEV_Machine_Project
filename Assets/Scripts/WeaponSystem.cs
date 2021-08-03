using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    //Weapon
    private GunType FAMAS;
    private GunType AWP;
    //private GunType Six;

    //Weapon Holder
    [SerializeField] private GameObject WeaponHolder1;
    [SerializeField] private GameObject WeaponHolder2;

    private GunType selectedWeapon;
    public bool changeWeapon = true;

    Color pressed;
    Color normal;

    // Start is called before the first frame update
    void Awake()
    {
        FAMAS = new GunType();
        FAMAS.weaponName = "FAMAS";
        FAMAS.damageAmount = 10f;
        FAMAS.magazineSize = 10;
        FAMAS.color = "BLUE";

        AWP = new GunType();
        AWP.weaponName = "AWP";
        AWP.damageAmount = 50f;
        AWP.magazineSize = 5;
        AWP.color = "GREEN";

        //default
        changeWeapon = true;
        selectedWeapon = FAMAS;

        normal = new Color(255f, 255f, 255f, 0.1f);
        pressed = new Color(0f, 0f, 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeWeapon)
        {
            Debug.Log(selectedWeapon.weaponName);
            changeWeapon = false;
        }
    }

    public GunType GetEquipped()
    {
        return selectedWeapon;
    }

    public void selectFAMAS()
    {
        selectedWeapon = FAMAS;
        changeWeapon = true;
        WeaponHolder1.GetComponent<Image>().color = pressed;
        WeaponHolder2.GetComponent<Image>().color = normal;
        Debug.Log("Changed to FAMAS");
    }

    public void selectAWP()
    {
        selectedWeapon = AWP;
        changeWeapon = true;
        WeaponHolder1.GetComponent<Image>().color = normal;
        WeaponHolder2.GetComponent<Image>().color = pressed;
        Debug.Log("Changed to AWP");
    }
}
