using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponry : MonoBehaviour
{
    private Transform cameraTransform;
    private RaycastHit hitTarget;

    public Transform gunBarrelPosition;
    public ParticleSystem bulletEffect;

    [Header("Revolver stats")]
    public float pistolBulletDamage;
    public float pistolBulletRange;
    public float pistolMaxAmmo,pistolCurrentAmmo;
    public float pistolReloadSpeed;

    private bool pistolReloading= false;

    [Header("Shotgun stats")]
    public float knockBackPower;

    //ray from the camera shoots out detects anything hit at the moment


    // Start is called before the first frame update
    void Start()
    {
       pistolCurrentAmmo = pistolMaxAmmo;
       cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pistol();
        shotGun();
      
    }
    
    public void shotGun()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //do a cone attack thing
            print("BBOOOOMM");
            //do a knock back on my baby boy self
            Rigidbody playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            playerRigidBody.AddForce(-cameraTransform.forward * knockBackPower, ForceMode.Impulse);
        }


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
            bulletEffect.Play();

            Enemy target = hitTarget.collider.gameObject.GetComponent<Enemy>();
            //if the target shot is an enemy
            if (pistolCurrentAmmo > 0)
            {
                if (target != null)
                {

                    target.takeDamage(pistolBulletDamage);
                }
            }

           pistolCurrentAmmo--;

        }

        if (pistolCurrentAmmo == 0 && !pistolReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void OnDrawGizmos()
    {
        if(hitTarget.collider != null)
        Gizmos.DrawWireSphere(hitTarget.point, .5f);
    }
    IEnumerator Reload()
    {
        print("pistolReloading");
        pistolReloading = true;
        yield return new WaitForSecondsRealtime(pistolReloadSpeed);
        pistolCurrentAmmo = pistolMaxAmmo;
        pistolReloading = false;
        print("reloaded");
    }


}
