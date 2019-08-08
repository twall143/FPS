using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastGunCustom : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Transform muzzleEffectPosition;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;

    public bool isSemi;

    public Text reloadText;
    public Text rtoReload;
    public Text ammoCount;

    private bool canFire;

    public GameObject crossHair;

  //  Health TakeDamage();

    // Start is called before the first frame update
    void Start()
    {
        if (muzzleEffectPosition == null)
            muzzleEffectPosition = gameObject.transform;
        
        currentAmmo = maxAmmo;
        reloadText.gameObject.SetActive(false);
        rtoReload.gameObject.SetActive(false);
        //ammoCount.gameObject.SetActive(true);
   }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Reload"))
        {
            if (currentAmmo <= maxAmmo)
            {
                StartCoroutine(Reload());
            }
            else
            {

            }
        }   
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (isSemi == false)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                muzzleFlash.Play();
                Shoot();
            }
            if (isSemi == true)
            {

            }

        }
        if (Input.GetButtonUp("Fire1") && Time.time >= nextTimeToFire)
            {               
                if (isSemi == true)
                {
                nextTimeToFire = Time.time + 1f / fireRate;
                muzzleFlash.Play();
                Shoot();
                }
                if (isSemi == false)
                {

                }
        }
        //Ammo text references
        ammoCount.text = "AMMO: " + currentAmmo;
        if (isSemi == false)
        {
            if (currentAmmo <= 10)
            {
                rtoReload.gameObject.SetActive(true);
            }
            else
            {
                rtoReload.gameObject.SetActive(false);
            }
            if (isReloading)
            {
                reloadText.gameObject.SetActive(true);
                crossHair.gameObject.SetActive(false);
            }
            else
            {
                reloadText.gameObject.SetActive(false);
            }
        }
        if (isSemi == true)
        {
            if (currentAmmo <= 5)
            {
                rtoReload.gameObject.SetActive(true);
            }
            else
            {
                rtoReload.gameObject.SetActive(false);
            }
            if (isReloading)
            {
                reloadText.gameObject.SetActive(true);
                crossHair.gameObject.SetActive(false);
            }
            else
            {
                reloadText.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        rtoReload.gameObject.SetActive(false);
        reloadText.gameObject.SetActive(true);
        crossHair.gameObject.SetActive(false);
        animator.SetBool("Reloading", true);
        

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
        crossHair.gameObject.SetActive(true);
    }

    public void Ammo()
    { 
         
}
    public void Shoot()
    {
        //Play the muzzle flash on shoot
        //Instantiate(muzzleFlash, muzzleEffectPosition.position, muzzleEffectPosition.rotation);
        //muzzleFlash.Play();

        //Decrease ammo on shoot
        currentAmmo--;
       
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
          

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

           EnemyHealth currentHealth = hit.transform.GetComponent<EnemyHealth>();
            if (currentHealth != null)
            {
                currentHealth.TakeDamage(damage);
              // Health currentHealth -= amount;
              //hit.collider.gameObject.SendMessageUpwards("ChangeHealth", -damage, SendMessageOptions.DontRequireReceiver);       
            }
        }
      
    }

   /* void OnGUI()
    {
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Ammo: " + currentAmmo);
        if (isReloading)
        {
            GUI.Label(new Rect(500, Screen.height - 500, 800, 500), "RELOADING");
        }
        else
        {

        }

    }
    */ 
   /* public void AmmoCount()
    {
       // ammoCount.text = "AMMO: " + currentAmmo;

        if (currentAmmo <= 10)
            {
            rtoReload.gameObject.SetActive(true);
            }
        else
        {
            rtoReload.gameObject.SetActive(false);
        }
        if (isReloading)
        {
            reloadText.gameObject.SetActive(true);
        }
        else
        {
            reloadText.gameObject.SetActive(false);
        }
        
    } */
}
