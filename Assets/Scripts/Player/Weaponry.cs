using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Weaponry : MonoBehaviour
{
    private Transform cameraTransform;
    private RaycastHit hitTarget;

    public Transform gunBarrelPosition;
    public ParticleSystem bulletEffect;
    public ParticleSystem ShotGunEffect;
    public SFXManagerGun sfxMG; //to play Gun SFX

    public Transform CamTransform;
    public Transform WeaponryTransform;

    [Header("Revolver stats")]
    public float pistolBulletDamage;
    public float pistolBulletRange;
    public float pistolMaxAmmo, pistolCurrentAmmo;
    public float pistolReloadSpeed;

    private bool pistolReloading = false;

    [Header("Shotgun stats")]
    public float knockBackPower;
    public float shotGunMaxAmmo, shotGunCurrentAmmo;
    public float shotGunReloadSpeed;
   
    private bool shottyReloading = false;


    // Start is called before the first frame update
    void Start()
    {
      
        pistolCurrentAmmo = pistolMaxAmmo;
        shotGunCurrentAmmo = shotGunMaxAmmo;
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateGun();

        pistol();
        shotGun();

        GameManager.Instance.updateAmmoUI((int)pistolCurrentAmmo, (int)shotGunCurrentAmmo);
    }

    public void shotGun()
    {

        //on Right Click
        if (Input.GetKeyDown(KeyCode.Mouse1)  && shotGunCurrentAmmo >0)
        {
            shotGunCurrentAmmo--;
            sfxMG.ShotGunFired();
            //do a cone attack thing
            ShotGunEffect.Play();
            //do a knock back on my baby boy self
            Rigidbody playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            playerRigidBody.AddForce(-cameraTransform.forward * knockBackPower, ForceMode.Impulse);

            if (shotGunCurrentAmmo == 0 && !shottyReloading)
            {
                StartCoroutine(ShotGunReload());
            }
        }


    }

    //rotates the gun so that shotgun particles launch in the right direction
    public void rotateGun()
    {
        //match gun rotation with camera rotation
        // transform
        WeaponryTransform.rotation = cameraTransform.rotation;
        print("why");
    }

    public void pistol()
    {
        //ignores the player which is layer of wall type?
        int layerMask = ~LayerMask.GetMask("Wall");
        // Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 8000, Color.black, 1);

        Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hitTarget, 6000, layerMask);

        //On left click
        if (Input.GetKeyDown(KeyCode.Mouse0) && !pistolReloading)
        {
            sfxMG.PistolFired();
            bulletEffect.Play();

            pistolCurrentAmmo--;
            Enemy target = null;
            // sicko mode null check
            if (hitTarget.collider.gameObject.GetComponent<Enemy>()!= null) { target = hitTarget.collider.gameObject.GetComponent<Enemy>(); }
            
            //if the target shot is an enemy
            if (pistolCurrentAmmo > 0)
            {
                if (target != null)
                {

                    target.takeDamage(pistolBulletDamage);
                }
            }


        }

        if (pistolCurrentAmmo == 0 && !pistolReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void OnDrawGizmos()
    {
        if (hitTarget.collider != null)
            Gizmos.DrawWireSphere(hitTarget.point, .5f);
    }
    IEnumerator Reload()
    {
      
        pistolReloading = true;
        yield return new WaitForSecondsRealtime(pistolReloadSpeed);
        pistolCurrentAmmo = pistolMaxAmmo;
        pistolReloading = false;
       
    }
    IEnumerator ShotGunReload()
    {
       
        shottyReloading = true;
        yield return new WaitForSecondsRealtime(shotGunReloadSpeed);
        shotGunCurrentAmmo = shotGunMaxAmmo;
        shottyReloading = false;
      
    }

}
