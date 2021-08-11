using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    //Weapon
    private GunType FAMAS;
    private GunType AWP;
    private GunType Six;

    //Weapon Holder (Used to show if equipped ot not)
    [SerializeField] private GameObject Weapon_Holder_1;
    [SerializeField] private GameObject Weapon_Holder_2;
    [SerializeField] private GameObject Weapon_Holder_3;

    //Current magazine of gun equipped 
    [SerializeField] private GameObject Magazine_Holder;    

    //Button States
    Color pressed;
    Color normal;

    private GunType selected_weapon;
    public bool change_weapon = true;
    public bool on_shoot = false;
    bool update_values = false;

    // Start is called before the first frame update
    void Awake()
    {
        //FAMAS Stats
        FAMAS = new GunType();
        FAMAS.weapon_name = "FAMAS";
        FAMAS.damage_amount = 10f;
        FAMAS.magazine_size = 10;
        FAMAS.current_magazine = 10;
        FAMAS.color = "BLUE";
        FAMAS.available = true;

        //AWP Stats
        AWP = new GunType();
        AWP.weapon_name = "AWP";
        AWP.damage_amount = 20f;
        AWP.magazine_size = 10;
        AWP.current_magazine = 10;
        AWP.color = "GREEN";
        AWP.available = false;

        //Six Stats
        Six = new GunType();
        Six.weapon_name = "Six";
        Six.damage_amount = 30f;
        Six.magazine_size = 20;
        Six.current_magazine = 20;
        Six.color = "RED";
        Six.available = false;

        //Set button colors
        normal = new Color(255f, 255f, 255f, 90f / 255f);
        pressed = new Color(0f, 0f, 0f, 0.5f);

        //default
        selected_weapon = FAMAS;
        Weapon_Holder_1.GetComponent<Image>().color = pressed;
        Magazine_Holder.GetComponent<Text>().text = FAMAS.current_magazine + " / " + FAMAS.magazine_size;
    }

    // Update is called once per frame
    void Update()
    {
        //Update current  magazine
        if(selected_weapon.weapon_name == "FAMAS")
        {
            if (on_shoot)
            {
                FAMAS.current_magazine -= 1;
            }              
            Magazine_Holder.GetComponent<Text>().text = FAMAS.current_magazine + " / " + FAMAS.magazine_size;
        }

        if (selected_weapon.weapon_name == "AWP")
        {
            if (on_shoot)
            {
                AWP.current_magazine -= 1;
            }
            Magazine_Holder.GetComponent<Text>().text = AWP.current_magazine + " / " + AWP.magazine_size;
        }

        if (selected_weapon.weapon_name == "Six")
        {
            if (on_shoot)
            {
                Six.current_magazine -= 1;
            }
            Magazine_Holder.GetComponent<Text>().text = Six.current_magazine + " / " + Six.magazine_size;
        }

        on_shoot = false;

        //Update stats from upgrade
        if (update_values)
        {
            if(selected_weapon.weapon_name == "FAMAS")
            {
                selected_weapon = FAMAS;
            }

            else if (selected_weapon.weapon_name == "AWP")
            {
                selected_weapon = AWP;
            }

            else if (selected_weapon.weapon_name == "Six")
            {
                selected_weapon = Six;
            }

            update_values = false;
        }
    }

    //Get currently equipped weapon
    public GunType GetEquipped()
    {
        return selected_weapon;
    }

    public void SelectFAMAS()
    {
        selected_weapon = FAMAS;
        change_weapon = true;
        Weapon_Holder_1.GetComponent<Image>().color = pressed;
        Weapon_Holder_2.GetComponent<Image>().color = normal;
        Weapon_Holder_3.GetComponent<Image>().color = normal;
        Debug.Log("Changed to FAMAS");
    }

    public void SelectAWP()
    {
        if (AWP.available)
        {
            selected_weapon = AWP;
            change_weapon = true;
            Weapon_Holder_1.GetComponent<Image>().color = normal;
            Weapon_Holder_2.GetComponent<Image>().color = pressed;
            Weapon_Holder_3.GetComponent<Image>().color = normal;
            Debug.Log("Changed to AWP");
        }       
    }

    public void SelectSix()
    {
        if (Six.available)
        {
            selected_weapon = Six;
            change_weapon = true;
            Weapon_Holder_1.GetComponent<Image>().color = normal;
            Weapon_Holder_2.GetComponent<Image>().color = normal;
            Weapon_Holder_3.GetComponent<Image>().color = pressed;
            Debug.Log("Changed to Six");
        }
    }

    public void Upgrade(string weap_name, int damage_up,int damage_cost, int magazine_up, int magazine_cost)
    {
        if(weap_name == "FAMAS")
        {
            FAMAS.damage_amount = damage_up;
            Debug.Log(FAMAS.damage_amount);
        }

        else if (weap_name == "AWP")
        {
            AWP.damage_amount = damage_up;
            Debug.Log(AWP.damage_amount);
        }

        else if (weap_name == "Six")
        {
            Six.damage_amount = damage_up;
            Debug.Log(Six.damage_amount);
        }

        update_values = true;
    }

    public void Buy(string gun)
    {
        if(gun == "AWP")
        {
            AWP.available = true;
            Weapon_Holder_2.GetComponent<Image>().color = normal;
            Weapon_Holder_2.GetComponent<Button>().interactable = true;
            Debug.Log("You bought" + selected_weapon.weapon_name);
        }

        if (gun == "Six")
        {
            Six.available = true;
            Weapon_Holder_3.GetComponent<Image>().color = normal;
            Weapon_Holder_3.GetComponent<Button>().interactable = true;
            Debug.Log("You bought" + selected_weapon.weapon_name);
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
