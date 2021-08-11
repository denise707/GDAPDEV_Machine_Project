using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    protected Joystick JoyStick;
    protected UICallbackScript UICallback;
    protected GameSystem GameSys;
    protected WeaponSystem WeaponSys;

    //public ParticleSystem bulletFlash;

    private GunType selectedWeapon;
    float bonus_damage = 0.0f;
    const float fire_rate = 1.0f;
    float ticks = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        JoyStick = FindObjectOfType<Joystick>();
        UICallback = FindObjectOfType<UICallbackScript>();
        GameSys = FindObjectOfType<GameSystem>();
        WeaponSys = FindObjectOfType<WeaponSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponSys.change_weapon)
        {
            ChangeGun();
            
        }
        

        MoveCrosshair();

        ticks += Time.deltaTime;

        if (UICallback.shoot)
        {
            if(WeaponSys.GetStats(selectedWeapon.weapon_name).current_magazine > 0 && ticks >= fire_rate)
            {
                Shoot();
                ticks = 0.0f;
            }
            else
            {
                UICallback.shoot = false;
            }
        }
    }

    void MoveCrosshair()
    {
        var crosshair = GetComponent<Rigidbody2D>();

        crosshair.velocity = new Vector3(JoyStick.Horizontal * 300f, JoyStick.Vertical * 300f, 0);

        if (this.transform.localPosition.x <= -882 && JoyStick.Horizontal <= 0)
        {
            crosshair.velocity = new Vector3(-JoyStick.Horizontal * 300f, 0, 0);
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(this.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                //Application of rock-paper-scissors mechanic
                bonus_damage = this.selectedWeapon.damage_amount * 0.3f;
                switch (selectedWeapon.color)
                {
                    case "BLUE":
                        if (enemy.type == "WASP") { enemy.TakeDamage(this.selectedWeapon.damage_amount + bonus_damage); }
                        else { enemy.TakeDamage(this.selectedWeapon.damage_amount); }
                        break;
                    case "GREEN":
                        if (enemy.type == "METAL ARM") { enemy.TakeDamage(this.selectedWeapon.damage_amount + bonus_damage); }
                        else { enemy.TakeDamage(this.selectedWeapon.damage_amount); }
                        break;
                    case "RED":
                        if (enemy.type == "INSECT") { enemy.TakeDamage(this.selectedWeapon.damage_amount + bonus_damage); }
                        else { enemy.TakeDamage(this.selectedWeapon.damage_amount); }
                        break;
                }
            }
        }
        WeaponSys.on_shoot = true;
        UICallback.shoot = false;
    }
    void ChangeGun()
    {
        this.selectedWeapon = WeaponSys.GetEquipped();      
    }
}
