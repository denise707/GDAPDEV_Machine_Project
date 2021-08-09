using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Joystick joystick;
    protected UICallbackScript UICallback;
    protected GameSystem gamesys;
    protected WeaponSystem weaponsys;

    //public ParticleSystem bulletFlash;

    private GunType selectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        UICallback = FindObjectOfType<UICallbackScript>();
        gamesys = FindObjectOfType<GameSystem>();
        weaponsys = FindObjectOfType<WeaponSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponsys.changeWeapon)
        {
            ChangeGun();
            
        }
        

        MoveCrosshair();

        if (UICallback.shoot)
        {
            Shoot();
        }
    }

    void MoveCrosshair()
    {
        var crosshair = GetComponent<Rigidbody2D>();

        crosshair.velocity = new Vector3(joystick.Horizontal * 300f, joystick.Vertical * 300f, 0);

        if (this.transform.localPosition.x <= -882 && joystick.Horizontal <= 0)
        {
            crosshair.velocity = new Vector3(-joystick.Horizontal * 300f, 0, 0);
        }
    }

    void Shoot()
    {
        //weaponsys.UpdateMagazine();
        //bulletFlash.Play();
        Ray ray = Camera.main.ScreenPointToRay(this.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                //if(selectedWeapon.color == "BLUE")
                //{
                enemy.TakeDamage(this.selectedWeapon.damageAmount);
                Debug.Log("Damage: " + selectedWeapon.damageAmount);
                //}
                //else
                //{
                //    enemy.TakeDamage(100);
                //}
            }
        }
        weaponsys.shoot = true;
        UICallback.shoot = false;
    }
    void ChangeGun()
    {
        this.selectedWeapon = weaponsys.GetEquipped();      
    }
}
