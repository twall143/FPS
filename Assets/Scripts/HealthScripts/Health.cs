using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public bool canDie = true;					// Whether or not this health can die
	
	public float startingHealth = 101.0f;		// The amount of health to start with
	public float maxHealth = 101.0f;			// The maximum amount of health
	public float currentHealth;		            // The current ammount of health

    //Slider for health bar
    public Slider healthSlider;
    //Wanring image health below 30%
    public Image damageImage;
    //Speed at which damage image flashes
    public float flashSpeed = 30f;
    //Flash Colour
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    //Death clip
    public AudioClip dealthClip;


	public bool replaceWhenDead = false;		// Whether or not a dead replacement should be instantiated.  (Useful for breaking/shattering/exploding effects)
	public GameObject deadReplacement;			// The prefab to instantiate when this GameObject dies
	public bool makeExplosion = false;			// Whether or not an explosion prefab should be instantiated
	public GameObject explosion;				// The explosion prefab to be instantiated

	public bool isPlayer = false;				// Whether or not this health is the player
	public GameObject deathCam;                 // The camera to activate when the player dies

    AudioSource playerAudio;

    public bool damaged;

	public bool dead = false;					// Used to make sure the Die() function isn't called twice

    public Text healthText;

    public bool respawning;

    public GameObject deathCanvas;

    public Text deadText;
    public Text timetoSpawn;
   //public Transform playerSpawn;
    
    /*
    //Prefabs for respawning
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject friendlyPrefab;
    */

    //Respawn bools
    public bool isEnemy;
    public bool isFriendly;

    //Respawn Floats
    public float respawnTime = 3f;
    //  public float respawnPlayer = 3f;
    //   public float timer = 3.0f;

    public Image deathImage;

    void Start()
    {
        deathCanvas.gameObject.SetActive(false);
        currentHealth = startingHealth;
        deadText.gameObject.SetActive(false);
        timetoSpawn.gameObject.SetActive(false);
        deathImage.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Update()
    {
        healthText.text = " " + currentHealth.ToString("f0");


        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        if (dead == true)
        {
            timetoSpawn.gameObject.SetActive(true);
            respawnTime -= Time.deltaTime;
         
           timetoSpawn.text = "RESPAWNING IN: " + Mathf.Round(respawnTime);
           
        }

        // Reset the damaged flag.
        damaged = false;
    }

public void TakeDamage(float amount)
{
    // Set the damaged flag so the screen will flash.
    damaged = true;

    // Reduce the current health by the damage amount.
    currentHealth -= amount;

    // Set the health bar's value to the current health.
    healthSlider.value = currentHealth;

    // Play the hurt sound effect.
    //playerAudio.Play();
  //if (currentHealth <= 25)
   //     {
     //       damageImage.color = flashColour;

            //damageImage.gameObject.SetActive(true);
       // }
       
    // If the player has lost all it's health
    if (currentHealth <= 0)
    {
            StartCoroutine(spawnPlayer());
            return;
            /*     dead = true;
                 GameManager.redKills += 1;
                 spawnPlayer();
                 deathCanvas.gameObject.SetActive(true); */
        }
    //Blood affect for damaged player
      
    }
    void Death()
    {
        // This GameObject is officially dead.  This is used to make sure the Die() function isn't called again
        //dead = true;
        GameManager.redKills += 1;
        StartCoroutine(spawnPlayer());
        return;
        //spawnPlayer();
        // Make death effects
        /* if (replaceWhenDead)
            Instantiate(deadReplacement, transform.position, transform.rotation);
        if (makeExplosion)
            Instantiate(explosion, transform.position, transform.rotation);

        if (isPlayer && deathCam != null)
            deathCam.SetActive(true);
        if (respawning && !isPlayer)
        {
            GameManager.redKills += 1;
           spawnPlayer();
            
        }
        if (respawning && !isFriendly)
        {
            GameManager.redKills += 1;
            GetComponent<GameManager>().spawnFriendly();
            Destroy(gameObject);
        }       */
    }
   IEnumerator spawnPlayer()
    {
            deathCanvas.gameObject.SetActive(true);
            deathImage.gameObject.SetActive(true);
            dead = true;
            GameManager.redKills += 1;
           
            
            //Instantiate(deadReplacement, transform.position, transform.rotation);

            yield return new WaitForSeconds(respawnTime - .1f);

            yield return new WaitForSeconds(.1f);
            dead = false;
            respawnTime = 3;
            deathImage.gameObject.SetActive(false);
            timetoSpawn.gameObject.SetActive(false);
            deathCanvas.gameObject.SetActive(false);
            transform.position = new Vector3(148.0f, 17.32f, 157);
            transform.rotation = Quaternion.identity;
            currentHealth = startingHealth;

    }
    
    /*public void respawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
    public void respawnFriendly()
    {
        Instantiate(friendlyPrefab, transform.position, transform.rotation);
    }
    public void spawnPlayer()
    {
        
        timer += Time.deltaTime;
        timetoSpawn.gameObject.SetActive(true);
        timetoSpawn.text = " " + timer.ToString("f1"); 

        if (timer > respawnPlayer)
        {
            Destroy(gameObject);            
            timer = timer - respawnPlayer;
        }
        */
}




    /* IEnumerator spawnPlayer()
     {

         Debug.Log("Respawning...");        

         yield return new WaitForSeconds(respawnTime - .5f);
         Instantiate(playerPrefab, transform.position, transform.rotation);
         yield return new WaitForSeconds(.5f);
         deadText.gameObject.SetActive(false);

     }
     */



