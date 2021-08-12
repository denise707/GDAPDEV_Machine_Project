using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    protected OnScreenJoystick JoyStick;
    protected UICallbackScript UICallback;
    protected GameSystem GameSys;
    protected WeaponSystem WeaponSys;

    //public ParticleSystem bulletFlash;

    private GunType selectedWeapon;
    float bonus_damage = 0.0f;
    const float fire_rate = 1.0f;
    float ticks = 0.0f;
    float speed = 300f;

    //Gestures
    private Touch trackedFinger1;
    float gestureTime = 0.0f;
    Vector2 startPoint;
    Vector2 endPoint;

    float bufferTime = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        JoyStick = FindObjectOfType<OnScreenJoystick>();
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

        if (Dragged() && !Options.aim_mode)
        {
            transform.position = trackedFinger1.position;
        }
        
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
        if (Options.aim_mode)
        {
            Debug.Log("Here");
            float x = JoyStick.JoystickAxis.x;
            float y = JoyStick.JoystickAxis.y;
            transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
        }
        
    }

    void Shoot()
    {
        SoundManagerScript.PlaySound("Shoot");
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

    bool Dragged()
    {
        bool dragged = false;
        if (Input.touchCount > 0)
        {
            Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);

            //Assign the tracked finger
            trackedFinger1 = Input.GetTouch(0);

            if (trackedFinger1.phase == TouchPhase.Began)
            {
                startPoint = trackedFinger1.position;
                gestureTime = 0;
            }
            else if (trackedFinger1.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger1.position;
            }

            else
            {
                gestureTime += Time.deltaTime;
                if (gestureTime >= bufferTime)
                {
                    dragged = true;
                }
            }
        }

        return dragged;
    }
}
