using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponMode
{
    Raycast,
}


// The Weapon class itself handles the weapon mechanics
public class EnemyShooting : MonoBehaviour
{
    // Weapon Type
    public WeaponType type = WeaponType.Raycast;        // Which weapon system should be used


    // Auto
    public Auto auto = Auto.Full;                       // How does this weapon fire - semi-auto or full-auto

    // General
    public bool playerWeapon = true;                    // Whether or not this is a player's weapon as opposed to an AI's weapon
    public GameObject weaponModel;                      // The actual mesh for this weapon
    public Transform raycastStartSpot;                  // The spot that the raycasting weapon system should use as an origin for rays
    public float delayBeforeFire = 0.0f;                // An optional delay that causes the weapon to fire a specified amount of time after it normally would (0 for no delay)

    // Warmup
    public bool warmup = false;                         // Whether or not the shot will be allowed to "warmup" before firing - allows power to increase when the player holds down fire button longer
                                                        // Only works on semi-automatic raycast and projectile weapons
    public float maxWarmup = 2.0f;                      // The maximum amount of time a warmup can have any effect on power, etc.
    public bool multiplyForce = true;                   // Whether or not the initial force of the projectile should be multiplied based on warmup heat value - only for projectiles
    public bool multiplyPower = false;                  // Whether or not the damage of the projectile should be multiplied based on warmup heat value - only for projectiles
                                                        // Also only has an effect on projectiles using the direct damage system - an example would be an arrow that causes more damage the longer you pull back your bow
    public float powerMultiplier = 1.0f;                // The multiplier by which the warmup can affect weapon power; power = power * (heat * powerMultiplier)
    public float initialForceMultiplier = 1.0f;         // The multiplier by which the warmup can affect the initial force, assuming a projectile system
    public bool allowCancel = false;                    // If true, the player can cancel this warmed up shot by pressing the "Cancel" input button, otherwise a shot will be fired when the player releases the fire key
    private float heat = 0.0f;                          // The amount of time the weapon has been warming up, can be in the range of (0, maxWarmup)



    // Power
    public float power = 80.0f;                         // The amount of power this weapon has (how much damage it can cause) (if the type is raycast or beam)
    public float forceMultiplier = 10.0f;               // Multiplier used to change the amount of force applied to rigid bodies that are shot
    public float beamPower = 1.0f;                      // Used to determine damage caused by beam weapons.  This will be much lower because this amount is applied to the target every frame while firing

    // Range
    public float range = 9999.0f;                       // How far this weapon can shoot (for raycast and beam)

    // Rate of Fire
    public float rateOfFire = 10;                       // The number of rounds this weapon fires per second
    private float actualROF;                            // The frequency between shots based on the rateOfFire
    private float fireTimer;                            // Timer used to fire at a set frequency

    // Ammo
    public bool infiniteAmmo = false;                   // Whether or not this weapon should have unlimited ammo
    public int ammoCapacity = 12;                       // The number of rounds this weapon can fire before it has to reload
    public int shotPerRound = 1;                        // The number of "bullets" that will be fired on each round.  Usually this will be 1, but set to a higher number for things like shotguns with spread
    private int currentAmmo;                            // How much ammo the weapon currently has
    public float reloadTime = 2.0f;                     // How much time it takes to reload the weapon
    public bool showCurrentAmmo = true;                 // Whether or not the current ammo should be displayed in the GUI
    public bool reloadAutomatically = true;             // Whether or not the weapon should reload automatically when out of ammo

    // Accuracy
    public float accuracy = 80.0f;                      // How accurate this weapon is on a scale of 0 to 100
    private float currentAccuracy;                      // Holds the current accuracy.  Used for varying accuracy based on speed, etc.
    public float accuracyDropPerShot = 1.0f;            // How much the accuracy will decrease on each shot
    public float accuracyRecoverRate = 0.1f;            // How quickly the accuracy recovers after each shot (value between 0 and 1)

    // Burst
    public int burstRate = 3;                           // The number of shots fired per each burst
    public float burstPause = 0.0f;                     // The pause time between bursts
    private int burstCounter = 0;                       // Counter to keep track of how many shots have been fired per burst
    private float burstTimer = 0.0f;                    // Timer to keep track of how long the weapon has paused between bursts

    // Recoil
    public bool recoil = true;                          // Whether or not this weapon should have recoil
    public float recoilKickBackMin = 0.1f;              // The minimum distance the weapon will kick backward when fired
    public float recoilKickBackMax = 0.3f;              // The maximum distance the weapon will kick backward when fired
    public float recoilRotationMin = 0.1f;              // The minimum rotation the weapon will kick when fired
    public float recoilRotationMax = 0.25f;             // The maximum rotation the weapon will kick when fired
    public float recoilRecoveryRate = 0.01f;            // The rate at which the weapon recovers from the recoil displacement

    // Effects
    public bool spitShells = false;                     // Whether or not this weapon should spit shells out of the side
    public GameObject shell;                            // A shell prop to spit out the side of the weapon
    public float shellSpitForce = 1.0f;                 // The force with which shells will be spit out of the weapon
    public float shellForceRandom = 0.5f;               // The variant by which the spit force can change + or - for each shot
    public float shellSpitTorqueX = 0.0f;               // The torque with which the shells will rotate on the x axis
    public float shellSpitTorqueY = 0.0f;               // The torque with which the shells will rotate on the y axis
    public float shellTorqueRandom = 1.0f;              // The variant by which the spit torque can change + or - for each shot
    public Transform shellSpitPosition;                 // The spot where the weapon should spit shells from
    public bool makeMuzzleEffects = true;				// Whether or not the weapon should make muzzle effects
    public GameObject[] muzzleEffects =
        new GameObject[] { null };                      // Effects to appear at the muzzle of the gun (muzzle flash, smoke, etc.)
    public Transform muzzleEffectsPosition;             // The spot where the muzzle effects should appear from
    public bool makeHitEffects = true;                  // Whether or not the weapon should make hit effects
    public GameObject[] hitEffects =
        new GameObject[] { null };                      // Effects to be displayed where the "bullet" hit

  

    // Audio
    public AudioClip fireSound;                         // Sound to play when the weapon is fired
    public AudioClip reloadSound;                       // Sound to play when the weapon is reloading
    public AudioClip dryFireSound;						// Sound to play when the user tries to fire but is out of ammo

    // Other
    private bool canFire = true;                        // Whether or not the weapon can currently fire (used for semi-auto weapons)


    // Use this for initialization
    void Start()
    {
      
        // Calculate the actual ROF to be used in the weapon systems.  The rateOfFire variable is
        // designed to make it easier on the user - it represents the number of rounds to be fired
        // per second.  Here, an actual ROF decimal value is calculated that can be used with timers.

        if (rateOfFire != 0)
            actualROF = 1.0f / rateOfFire;
        else
            actualROF = 0.01f;


        // Make sure the fire timer starts at 0
        fireTimer = 0.0f;

        // Start the weapon off with a full magazine
        currentAmmo = ammoCapacity;

        // Give this weapon an audio source component if it doesn't already have one
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent(typeof(AudioSource));
        }

        // Make sure raycastStartSpot isn't null
        if (raycastStartSpot == null)
            raycastStartSpot = gameObject.transform;

        // Make sure muzzleEffectsPosition isn't null
        if (muzzleEffectsPosition == null)
            muzzleEffectsPosition = gameObject.transform;



        // Make sure weaponModel isn't null
        if (weaponModel == null)
            weaponModel = gameObject;



    }

    // Update is called once per frame
    void Update()
    {

        // Calculate the current accuracy for this weapon
        currentAccuracy = Mathf.Lerp(currentAccuracy, accuracy, accuracyRecoverRate * Time.deltaTime);


        // Update the fireTimer
        fireTimer += Time.deltaTime;

       
        // Reload if the weapon is out of ammo
        if (reloadAutomatically && currentAmmo <= 0)
            Reload();




    }     
    

    // A public method that causes the weapon to fire - can be called from other scripts - calls AI Firing for now
    public void RemoteFire()
    {
        AIFiring();
    }

    // Determines when the AI can be firing
    public void AIFiring()
    {

        // Fire if this is a raycast type weapon
        if (type == WeaponType.Raycast)
        {
            if (fireTimer >= actualROF && burstCounter < burstRate && canFire)
            {
                StartCoroutine(DelayFire());    // Fires after the amount of time specified in delayBeforeFire
            }
        }


        // Reset the Burst
        if (burstCounter >= burstRate)
        {
            burstTimer += Time.deltaTime;
            if (burstTimer >= burstPause)
            {
                burstCounter = 0;
                burstTimer = 0.0f;
            }
        }



    }

    IEnumerator DelayFire()
    {
        // Reset the fire timer to 0 (for ROF)
        fireTimer = 0.0f;

        // Increment the burst counter
        burstCounter++;

        // If this is a semi-automatic weapon, set canFire to false (this means the weapon can't fire again until the player lets up on the fire button)
        if (auto == Auto.Semi)
            canFire = false;

        // Send a messsage so that users can do other actions whenever this happens
        SendMessageUpwards("OnEasyWeaponsFire", SendMessageOptions.DontRequireReceiver);

        yield return new WaitForSeconds(delayBeforeFire);
        Fire();
    }


    void OnGUI()
    {



        // Ammo Display
        if (showCurrentAmmo)
        {
            if (type == WeaponType.Raycast)
                GUI.Label(new Rect(10, Screen.height - 30, 100, 20), "Ammo: " + currentAmmo);

        }
    }


    // Raycasting system
    void Fire()
    {
        // Reset the fireTimer to 0 (for ROF)
        fireTimer = 0.0f;

        // Increment the burst counter
        burstCounter++;

        // If this is a semi-automatic weapon, set canFire to false (this means the weapon can't fire again until the player lets up on the fire button)
        if (auto == Auto.Semi)
            canFire = false;

        // First make sure there is ammo
        if (currentAmmo <= 0)
        {
            DryFire();
            return;
        }

        // Subtract 1 from the current ammo
        if (!infiniteAmmo)
            currentAmmo--;


        // Fire once for each shotPerRound value
        for (int i = 0; i < shotPerRound; i++)
        {
            // Calculate accuracy for this shot
            float accuracyVary = (100 - currentAccuracy) / 1000;
            Vector3 direction = raycastStartSpot.forward;
            direction.x += UnityEngine.Random.Range(-accuracyVary, accuracyVary);
            direction.y += UnityEngine.Random.Range(-accuracyVary, accuracyVary);
            direction.z += UnityEngine.Random.Range(-accuracyVary, accuracyVary);
            currentAccuracy -= accuracyDropPerShot;
            if (currentAccuracy <= 0.0f)
                currentAccuracy = 0.0f;

            // The ray that will be used for this shot
            Ray ray = new Ray(raycastStartSpot.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                // Warmup heat
                float damage = power;
                if (warmup)
                {
                    damage *= heat * powerMultiplier;
                    heat = 0.0f;

                }

                // Damage
                hit.collider.gameObject.SendMessageUpwards("ChangeHealth", -damage, SendMessageOptions.DontRequireReceiver);

                // Make sure the hit GameObject is not defined as an exception for bullet holes
                bool exception = false;
               

                // Hit Effects
                if (makeHitEffects)
                {
                    foreach (GameObject hitEffect in hitEffects)
                    {
                        if (hitEffect != null)
                            Instantiate(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    }
                }

                // Add force to the object that was hit
                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForce(ray.direction * power * forceMultiplier);
                }
            }
        }

        // Recoil
        if (recoil)
            Recoil();

        // Muzzle flash effects
        if (makeMuzzleEffects)
        {
            GameObject muzfx = muzzleEffects[Random.Range(0, muzzleEffects.Length)];
            if (muzfx != null)
                Instantiate(muzfx, muzzleEffectsPosition.position, muzzleEffectsPosition.rotation);
        }

        // Instantiate shell props
        if (spitShells)
        {
            GameObject shellGO = Instantiate(shell, shellSpitPosition.position, shellSpitPosition.rotation) as GameObject;
            shellGO.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(shellSpitForce + Random.Range(0, shellForceRandom), 0, 0), ForceMode.Impulse);
            shellGO.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(shellSpitTorqueX + Random.Range(-shellTorqueRandom, shellTorqueRandom), shellSpitTorqueY + Random.Range(-shellTorqueRandom, shellTorqueRandom), 0), ForceMode.Impulse);
        }

        // Play the gunshot sound
        GetComponent<AudioSource>().PlayOneShot(fireSound);




    }



    // Reload the weapon
    void Reload()
    {
        currentAmmo = ammoCapacity;
        fireTimer = -reloadTime;
        GetComponent<AudioSource>().PlayOneShot(reloadSound);

        // Send a messsage so that users can do other actions whenever this happens
        SendMessageUpwards("OnEasyWeaponsReload", SendMessageOptions.DontRequireReceiver);
    }

    // When the weapon tries to fire without any ammo
    void DryFire()
    {
        GetComponent<AudioSource>().PlayOneShot(dryFireSound);
    }


    // Recoil FX.  This is the "kick" that you see when the weapon moves back while firing
    void Recoil()
    {
        // No recoil for AIs
        if (!playerWeapon)
            return;

        // Make sure the user didn't leave the weapon model field blank
        if (weaponModel == null)
        {
            Debug.Log("Weapon Model is null.  Make sure to set the Weapon Model field in the inspector.");
            return;
        }

      
    }

    // Find a mesh renderer in a specified gameobject, it's children, or its parents
    MeshRenderer FindMeshRenderer(GameObject go)
    {
        MeshRenderer hitMesh;

        // Use the MeshRenderer directly from this GameObject if it has one
        if (go.GetComponent<Renderer>() != null)
        {
            hitMesh = go.GetComponent<MeshRenderer>();
        }

        // Try to find a child or parent GameObject that has a MeshRenderer
        else
        {
            // Look for a renderer in the child GameObjects
            hitMesh = go.GetComponentInChildren<MeshRenderer>();

            // If a renderer is still not found, try the parent GameObjects
            if (hitMesh == null)
            {
                GameObject curGO = go;
                while (hitMesh == null && curGO.transform != curGO.transform.root)
                {
                    curGO = curGO.transform.parent.gameObject;
                    hitMesh = curGO.GetComponent<MeshRenderer>();
                }
            }
        }

        return hitMesh;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AIFiring();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}


