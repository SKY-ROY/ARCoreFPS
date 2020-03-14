using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [Header("Weapon Stats")]
    public char weaponClass = 'N'; // A: low rof high damage; B: high rof low damage; C: Broken weapons;
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 0f;
    public float fireRate = 15f;
    public int maxAmmo = 10;
    public float reloadTime = 1f;

    [Header("Elements bound to Weapon")]
    //public Camera fpsCam;
    public GameObject fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;

    [Header("External Gun Effects")]
    public Animator gunAnimator;
    public AnimationClip shootingClip;
    public AudioSource gunShot;
    public AudioClip gunShotClip;

    [Header("Weapon(Gun) UI Elements")]
    private bool isReloading = false;
    private int currentAmmo;
    private bool shootCurrent;
    private float nextTimeToFire = 0f;
    
    void Start()
    {
        fpsCam = GameObject.Find("First Person Camera") ;
        currentAmmo = maxAmmo;
        this.transform.parent.GetComponent<SlotControl>().ammmoTxt.text = maxAmmo.ToString() + "/" + maxAmmo.ToString();
    }
    void OnEnable()
    {
        isReloading = false;
        gunAnimator.SetBool("ReloadingParameter", false);
    }
    // Update is called once per frame
    void Update()
    {
        shootCurrent = this.transform.parent.GetComponent<SlotControl>().Bridge_shootCurrent;

        /// <summary>
        /// if isReloading is true Update function terminates here and called next time when new frame is called 
        /// </summary>
        if (isReloading)
            return;
        
        /// <summary>
        /// currentAmmo check done every frame to keep track and reload once it reaches zero
        /// </summary>
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        /// <summary>
        /// if shootCurrent bool is true weapon gets fired, the bool value is manipulated at run-time by another event handler
        /// </summary>
        if (shootCurrent && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        /// <summary>
        /// Weapon Category wise shooting for debugging with LMB
        /// </summary>
        /*
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
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
        this.transform.parent.GetComponent<SlotControl>().ammmoTxt.text = maxAmmo.ToString() + "/" + maxAmmo.ToString();

        isReloading = false;
    }
    
    /// <summary>
    /// Shoot() method 
    /// </summary>
    void Shoot()
    {
        currentAmmo--;

        this.transform.parent.GetComponent<SlotControl>().ammmoTxt.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();

        muzzleFlash.Play();

        //gunAnimator.SetTrigger("Shoot");//shoot animation
        gunAnimator.Play(shootingClip.name);//shoot animation

        gunShot.PlayOneShot(gunShotClip, 1);

        RaycastHit Shoot;
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Shoot, range))
        {
            //Debug.Log(">>>" + Shoot.transform.name + "<<<");

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
}