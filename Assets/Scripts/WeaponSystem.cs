using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject magazineHolder;

    //Weapon
    private GunType FAMAS;
    private GunType AWP;
    //private GunType Six;

    //Weapon Holder
    [SerializeField] private GameObject WeaponHolder1;
    [SerializeField] private GameObject WeaponHolder2;

    [SerializeField] private Button WeaponButton2;

    private GunType selectedWeapon;
    public bool changeWeapon = true;

    Color pressed;
    Color normal;

    protected UICallbackScript UICallback;
    public bool shoot = false;
    bool update_values = false;

    // Start is called before the first frame update
    void Awake()
    {
        UICallback = FindObjectOfType<UICallbackScript>();

        FAMAS = new GunType();
        FAMAS.weaponName = "FAMAS";
        FAMAS.damageAmount = 10f;
        FAMAS.magazineSize = 10;
        FAMAS.currentMagazine = 10;
        FAMAS.color = "BLUE";
        FAMAS.available = true;

        AWP = new GunType();
        AWP.weaponName = "AWP";
        AWP.damageAmount = 50f;
        AWP.magazineSize = 5;
        AWP.currentMagazine = 5;
        AWP.color = "GREEN";
        AWP.available = false;

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
        shoot = false;

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
            update_values = false;
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
        if (AWP.available)
        {
            selectedWeapon = AWP;
            changeWeapon = true;
            WeaponHolder1.GetComponent<Image>().color = normal;
            WeaponHolder2.GetComponent<Image>().color = pressed;
            Debug.Log("Changed to AWP");
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
    }

    public GunType GetStats(string weap_name)
    {
        GunType gun = FAMAS;
        switch (weap_name)
        {
            case "FAMAS": gun = FAMAS; break;
            case "AWP": gun = AWP; break;
        }
        return gun;
    }
}
