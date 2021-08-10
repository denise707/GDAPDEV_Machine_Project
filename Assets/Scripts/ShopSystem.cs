using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    //Credits
    [SerializeField] private Text CreditHolder;
    [SerializeField] private Text BillHolder;

    //From other scripts
    protected WeaponSystem weaponsys;
    protected GameSystem gamesys;

    //Buy and Upgrade Pop 
    [SerializeField] private GameObject Buy_Pop_Up;
    [SerializeField] private GameObject Upgrade_Pop_Up;

    //Buy Button
    [SerializeField] private Button AWPButton_Buy;
    [SerializeField] private Button SixButton_Buy;

    //Upgrade Button
    [SerializeField] private Button FAMASButton_Upgrade;
    [SerializeField] private Button AWPButton_Upgrade;
    [SerializeField] private Button SixButton_Upgrade;

    [SerializeField] private Button ConfirmButton;
    [SerializeField] private Button ReturnButton;
    [SerializeField] private Button CancelButton;

    //Price List
    int AWP_Price = 50;
    int Six_Price = 100;

    //Upgrade
    [SerializeField] private Image Weapon_Image;
    [SerializeField] private Text Weapon_Name;

    [SerializeField] private Sprite FAMAS_Sprite;
    [SerializeField] private Sprite AWP_Sprite;
    [SerializeField] private Sprite Six_Sprite;

    [SerializeField] private Image Damage_Image;
    [SerializeField] private Text Damage_Label;

    [SerializeField] private Image Magazine_Image;
    [SerializeField] private Text Magazine_Label;

    //Cart
    string item_name;
    int damage_up = 10;
    int magazine_up = 10;

    //Temporary Values
    int damage_cost = 0;
    int magazine_cost = 0;

    // Start is called before the first frame update
    void Start()
    {
        weaponsys = FindObjectOfType<WeaponSystem>();
        gamesys = FindObjectOfType<GameSystem>();

        CreditHolder.text = $"Credits: {gamesys.credits}";
        BillHolder.text = $"Total: {0}";
    }

    // Update is called once per frame
    void Update()
    {
        CreditHolder.text = $"Credits: {gamesys.credits}";
        BillHolder.text = $"Total: {damage_cost}";
    }

    //----------------Start of Buy Weapons----------------//
    public void Buy_AWP(Text message)
    {
        if (gamesys.credits >= AWP_Price)
        {
            Debug.Log(gamesys.credits);
            int current_credits = gamesys.credits - AWP_Price;
            message.text = "Are you sure you wan to buy AWP for " + AWP_Price + " Credits?";
            item_name = "AWP";                      
        }

        else
        {
            message.text = "Insufficient Credits";
            ConfirmButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            ReturnButton.gameObject.SetActive(true);
        }

        Buy_Pop_Up.SetActive(true);
    }

    public void Buy_Six(Text message)
    {
        if (gamesys.credits >= Six_Price)
        {
            Debug.Log(gamesys.credits);
            int current_credits = gamesys.credits - Six_Price;
            message.text = "Are you sure you wan to buy Six for " + Six_Price + " Credits?";
            item_name = "Six";
        }

        else
        {
            message.text = "Insufficient Credits";
            ConfirmButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            ReturnButton.gameObject.SetActive(true);
        }

        Buy_Pop_Up.SetActive(true);
    }

    public void Buy_Confirm()
    {
        weaponsys.Buy(item_name);
        switch (item_name)
        {
            case "AWP":
                AWPButton_Buy.interactable = false;
                AWPButton_Upgrade.interactable = true;
                gamesys.credits -= AWP_Price;
                break;
            case "Six":
                SixButton_Buy.interactable = false;
                SixButton_Upgrade.interactable = true;
                gamesys.credits -= Six_Price;
                break;
        }
        Buy_Pop_Up.SetActive(false);
    }

    public void Buy_Cancel(GameObject buy_pop_up)
    {
        buy_pop_up.SetActive(false);
    }
    //----------------End of Buy Weapons----------------//

    //----------------Start of Upgrade Weapons----------------//
    public void Upgrade_FAMAS()
    {
        item_name = "FAMAS";
        Weapon_Name.text = "FAMAS";
        Weapon_Image.sprite = FAMAS_Sprite;

        damage_up = (int) weaponsys.GetStats(item_name).damageAmount;
        damage_cost = Damage_Cost();

        Upgrade_Pop_Up.SetActive(true);

        Damage_Image.fillAmount = (float) weaponsys.GetStats("FAMAS").damageAmount / 100;
        Damage_Label.text = (weaponsys.GetStats("FAMAS").damageAmount).ToString() + " / 100";
        Magazine_Image.fillAmount = weaponsys.GetStats("FAMAS").magazineSize / 100;
        Magazine_Label.text = (weaponsys.GetStats("FAMAS").magazineSize).ToString() + " / 100";
    }

    public void Upgrade_AWP()
    {
        item_name = "AWP";
        Weapon_Name.text = "AWP";
        Weapon_Image.sprite = AWP_Sprite;

        damage_up = (int)weaponsys.GetStats(item_name).damageAmount;
        damage_cost = Damage_Cost();

        Upgrade_Pop_Up.SetActive(true);

        Damage_Image.fillAmount = (float)weaponsys.GetStats("AWP").damageAmount / 100;
        Damage_Label.text = (weaponsys.GetStats("AWP").damageAmount).ToString() + " / 100";
        Magazine_Image.fillAmount = weaponsys.GetStats("AWP").magazineSize / 100;
        Magazine_Label.text = (weaponsys.GetStats("AWP").magazineSize).ToString() + " / 100";
    }

    public void More_Damage()
    {
        if(damage_up < 100)
        {
            damage_up += 10;
            damage_cost = Damage_Cost();

            if (damage_cost > gamesys.credits)
            {
                damage_up -= 10;
                damage_cost = Damage_Cost();               
            }            
        }

        Damage_Image.fillAmount = (float)damage_up / 100;
        Damage_Label.text = (damage_up).ToString() + " / 100";
        Magazine_Image.fillAmount = magazine_up / 100;
        Magazine_Label.text = (magazine_up).ToString() + " / 100";
    }

    public void Less_Damage()
    {
        damage_up -= 10;
        damage_cost = Damage_Cost();

        if (damage_up < weaponsys.GetStats(item_name).damageAmount)
        {
            damage_up += 10;
            damage_cost = Damage_Cost();

        }

        Damage_Image.fillAmount = (float)damage_up / 100;
        Damage_Label.text = (damage_up).ToString() + " / 100";
        Magazine_Image.fillAmount = magazine_up / 100;
        Magazine_Label.text = (magazine_up).ToString() + " / 100";
    }

    public void Upgrade_Confirm(GameObject upgrade_pop_up)
    {
        weaponsys.Upgrade(item_name, damage_up, damage_cost, magazine_up, magazine_cost);
        gamesys.credits -= damage_cost;
        upgrade_pop_up.SetActive(false);
    }

    public void Upgrade_Cancel(GameObject upgrade_pop_up)
    {
        damage_up = 0;
        damage_cost = 0;
        upgrade_pop_up.SetActive(false);
    }

    int Damage_Cost()
    {
        int damage_cost = 0;

        switch (damage_up)
        {
            case 10: damage_cost = 0; break;
            case 20: damage_cost = 200; break;
            case 30: damage_cost = 300; break;
            case 40: damage_cost = 500; break;
            case 50: damage_cost = 1200; break;
            default: damage_cost = 1500; break;
        }

        if (damage_up == weaponsys.GetStats(item_name).damageAmount){
            damage_cost = 0;           
        }
        
        return damage_cost;
    }
    //----------------End of Upgrade Weapons----------------//
}
