using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    // The enemy will stop moving towards the player once it reaches this distance
    public float m_CloseDistance = 8f;
    // The tank's body = will face player
    public Transform m_Body;

    // A reference to the player - this will be set when the enemy is loaded
    private GameObject m_Player;
    // A reference to the nav mesh agent component
    private NavMeshAgent m_NavAgent;
    // A reference to the rigidbody component
    private Rigidbody m_Rigidbody;

    // Will be set to true when the enemy should follow the player
    private bool m_Follow;


    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (m_Follow == false)
            return;

        // get distance from player to enemy
        float distance = (m_Player.transform.position - transform.position).magnitude;
        // if distance is less than stop distance, then stop moving
        if (distance > m_CloseDistance)
        {
            m_NavAgent.SetDestination(m_Player.transform.position);
            m_NavAgent.Resume();
        }

        else
        {
            m_NavAgent.Stop();
        }
        if (m_Body != null)
        {
            m_Body.LookAt(m_Player.transform);
        }
    }

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
      //  m_Player = GameObject.FindGameObjectWithTag("Friendly");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;
    }    private void OnEnable()
    {
        // when the tank is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;
    }
    private void OnDisable()
    {
        // when the tank is turned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
        if (other.tag == "Friendly")
        {
            m_Follow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false;
        }
        if (other.tag == "Friendly")
        {
            m_Follow = false;
        }
    }
}