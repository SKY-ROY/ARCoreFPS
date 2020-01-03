using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [Header("Weapon Stats")]
    public char weaponClass = 'N'; // A: low rof high damage; B: high rof low damage; C: Broken weapons;
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public int maxAmmo = 10;
    public float reloadTime = 1f;
    
    private bool isReloading = false;
    private int currentAmmo;
    
    [Header("Elements bound to Weapon")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;

    [Header("External Gun Effects")]
    public Animator gunAnimator;
    public AnimationClip shootingClip;
    public AudioSource gunShot;
    public AudioClip gunShotClip;

    [Header("Weapon(Gun) UI Elements")]
    public Text ammoTxt;
    public Text ammoCount3D;

    private float nextTimeToFire = 0f;
    void Start()
    {
        currentAmmo = maxAmmo;
        ammoTxt.text = maxAmmo.ToString() + "/" + maxAmmo.ToString();
        ammoCount3D.text = ammoTxt.text;
    }
    void OnEnable()
    {
        isReloading = false;
        gunAnimator.SetBool("RealoadingParameter", false);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        /// <summary>
        /// Weapon Category wise shooting style 
        /// </summary>
        /*
        if (weaponClass == 'A')
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else if (weaponClass == 'B')
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else if (weaponClass == 'C')
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        */
    }

    /// <summary>
    /// Coroutine for Reload
    /// </summary>
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        gunAnimator.SetBool("ReloadingParameter", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        
        gunAnimator.SetBool("ReloadingParameter", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        ammoTxt.text = maxAmmo.ToString() + "/" + maxAmmo.ToString();
        ammoCount3D.text = ammoTxt.text;

        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo--;

        ammoTxt.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        ammoCount3D.text = ammoTxt.text;

        muzzleFlash.Play();

        gunAnimator.Play(shootingClip.name);

        gunShot.PlayOneShot(gunShotClip, 1);

        RaycastHit Shoot;
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Shoot, range))
        {
            Debug.Log(">>>" + Shoot.transform.name + "<<<");

            Target target = Shoot.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (Shoot.rigidbody != null)
            {
                Shoot.rigidbody.AddForce(-Shoot.normal * impactForce);
            }
            
            GameObject impactGO = Instantiate(ImpactEffect , Shoot.point, Quaternion.LookRotation(Shoot.normal));
            impactGO.GetComponent<ParticleSystem>().Play();
            Destroy(impactGO, 2f);
        }
    }
    public void ClickToShoot(int weaponClass)
    {
        Debug.Log("clicked type:" + weaponClass);

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
}