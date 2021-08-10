using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    //Weapon
    private GunType FAMAS;
    private GunType AWP;
    private GunType Six;

    //Weapon Holder (Used to show if equipped ot not)
    [SerializeField] private GameObject WeaponHolder1;
    [SerializeField] private GameObject WeaponHolder2;
    [SerializeField] private GameObject WeaponHolder3;

    //Weapon Button (used to either make it interactable or not)
    [SerializeField] private Button WeaponButton2;
    [SerializeField] private Button WeaponButton3;

    //Current magazine of gun equipped 
    [SerializeField] private GameObject magazineHolder;    

    //Button States
    Color pressed;
    Color normal;

    private GunType selectedWeapon;
    public bool changeWeapon = true;
    public bool shoot = false;
    bool update_values = false;

    // Start is called before the first frame update
    void Awake()
    {
        //FAMAS Stats
        FAMAS = new GunType();
        FAMAS.weaponName = "FAMAS";
        FAMAS.damageAmount = 10f;
        FAMAS.magazineSize = 10;
        FAMAS.currentMagazine = 10;
        FAMAS.color = "BLUE";
        FAMAS.available = true;

        //AWP Stats
        AWP = new GunType();
        AWP.weaponName = "AWP";
        AWP.damageAmount = 20f;
        AWP.magazineSize = 10;
        AWP.currentMagazine = 10;
        AWP.color = "GREEN";
        AWP.available = false;

        //Six Stats
        Six = new GunType();
        Six.weaponName = "Six";
        Six.damageAmount = 30f;
        Six.magazineSize = 20;
        Six.currentMagazine = 20;
        Six.color = "RED";
        Six.available = false;

        //Set button colors
        normal = new Color(255f, 255f, 255f, 90f / 255f);
        pressed = new Color(0f, 0f, 0f, 0.5f);

        //default
        selectedWeapon = FAMAS;
        WeaponHolder1.GetComponent<Image>().color = pressed;
        magazineHolder.GetComponent<Text>().text = FAMAS.currentMagazine + " / " + FAMAS.magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        //Update current  magazine
        if(selectedWeapon.weaponName == "FAMAS")
        {
            if (shoot)
            {
                FAMAS.currentMagazine -= 1;
            }              
            magazineHolder.GetComponent<Text>().text = FAMAS.currentMagazine + " / " + FAMAS.magazineSize;
        }

        if (selectedWeapon.weaponName == "AWP")
        {
            if (shoot)
            {
                AWP.currentMagazine -= 1;
            }
            magazineHolder.GetComponent<Text>().text = AWP.currentMagazine + " / " + AWP.magazineSize;
        }

        if (selectedWeapon.weaponName == "Six")
        {
            if (shoot)
            {
                Six.currentMagazine -= 1;
            }
            magazineHolder.GetComponent<Text>().text = Six.currentMagazine + " / " + Six.magazineSize;
        }

        shoot = false;

        //Update stats from upgrade
        if (update_values)
        {
            if(selectedWeapon.weaponName == "FAMAS")
            {
                selectedWeapon = FAMAS;
            }

            else if (selectedWeapon.weaponName == "AWP")
            {
                selectedWeapon = AWP;
            }

            else if (selectedWeapon.weaponName == "Six")
            {
                selectedWeapon = Six;
            }

            update_values = false;
        }
    }

    //Get currently equipped weapon
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
        WeaponHolder3.GetComponent<Image>().color = normal;
        Debug.Log("Changed to FAMAS");
    }

    public void selectAWP()
    {
        if (AWP.available)
        {
            selectedWeapon = AWP;
            changeWeapon = true;
            WeaponHolder1.GetComponent<Image>().color = normal;
            WeaponHolder2.GetComponent<Image>().color = pressed;
            WeaponHolder3.GetComponent<Image>().color = normal;
            Debug.Log("Changed to AWP");
        }       
    }

    public void selectSix()
    {
        if (Six.available)
        {
            selectedWeapon = Six;
            changeWeapon = true;
            WeaponHolder1.GetComponent<Image>().color = normal;
            WeaponHolder2.GetComponent<Image>().color = normal;
            WeaponHolder3.GetComponent<Image>().color = pressed;
            Debug.Log("Changed to Six");
        }
    }

    public void Upgrade(string weap_name, int damage_up,int damage_cost, int magazine_up, int magazine_cost)
    {
        if(weap_name == "FAMAS")
        {
            FAMAS.damageAmount = damage_up;
            Debug.Log(FAMAS.damageAmount);
        }

        else if (weap_name == "AWP")
        {
            AWP.damageAmount = damage_up;
            Debug.Log(AWP.damageAmount);
        }

        else if (weap_name == "Six")
        {
            Six.damageAmount = damage_up;
            Debug.Log(Six.damageAmount);
        }

        update_values = true;
    }

    public void Buy(string gun)
    {
        if(gun == "AWP")
        {
            AWP.available = true;
            WeaponHolder2.GetComponent<Image>().color = normal;
            WeaponButton2.interactable = true;
            Debug.Log("You bought" + selectedWeapon.weaponName);
        }

        if (gun == "Six")
        {
            Six.available = true;
            WeaponHolder3.GetComponent<Image>().color = normal;
            WeaponButton3.interactable = true;
            Debug.Log("You bought" + selectedWeapon.weaponName);
        }
    }

    public GunType GetStats(string weap_name)
    {
        GunType gun = FAMAS;
        switch (weap_name)
        {
            case "FAMAS": gun = FAMAS; break;
            case "AWP": gun = AWP; break;
            case "Six": gun = Six; break;
        }
        return gun;
    }
}
