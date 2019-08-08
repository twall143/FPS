using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkFrag : NetworkBehaviour
{
    public PlayerWeapon frag;
    private const string PLAYER_TAG = "Player";

    // [SerializeField]
    //private Camera cam;

    // [SerializeField]
    // private LayerMask mask;
    
    private float delay = 3f;
   
    private float BlastRadius = 10f;
    
    private float ExplosionPower = 700f;
  
    private int MaxDMG = 60;
    
    private Vector3 explosion_position;
    
    float countdown;

    public GameObject explosionEffect;

    public Transform player;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        if (Input.GetButtonDown("4"))
        {
            ThrowFrag();
        }
    }

    [Client]
    void ThrowFrag()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, BlastRadius);
        //Collider[] hitColliders = Physics.OverlapSphere(location, radius);

        foreach (Collider nearbyObject in hitColliders)
        {

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionPower, transform.position, BlastRadius);
                GameObject.FindGameObjectsWithTag("Player");
                if ("Player" != null)
                {
                    RaycastHit hit;
                    var exposed = false;
                    //see if player is exposed
                    if (Physics.Raycast(explosion_position, (player.transform.position - explosion_position), out hit))
                    {
                        exposed = (hit.collider.tag == "Player");
                    }
                    if (exposed)
                    {
                        if (hit.collider.tag == PLAYER_TAG)
                        {
                            CmdPlayerHit(hit.collider.name, frag.damage);
                        }
                        //Hit a player
                        Debug.Log("We hit" + hit.collider.name);
                    }


                }

                //Destruction
                Destruction dest = nearbyObject.GetComponent<Destruction>();
                if (dest != null)
                {
                    dest.Destroy();
                }
                //if (player != null)
                // {
                //     // ExplosionDamage();
                //  }

            }

            Destroy(gameObject);

            Debug.Log("Nade out");

        }
    }

    [Command]
    void CmdPlayerHit(string _playerID, int _damage)
    {
        Debug.Log(_playerID + "has been naded.");
        // Destroy (GameObject.Find(_ID));

        Player _player = MultiplayerGameManager.GetPlayer(_playerID);
        _player.TakeDamage(_damage);
    }
}