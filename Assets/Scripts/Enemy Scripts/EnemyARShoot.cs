using UnityEngine;
using System.Collections;
public class EnemyARShoot : MonoBehaviour
{
    // Prefab of the bullet
    public Rigidbody m_Bullet;
    //Chiled of weapon, end of barrel where bullets leave
    public Transform m_FireTransform;
    // The force given to the bullet when firing
    public float m_LaunchForce = 100f;

    public float m_ShootDelay = 1f;
    private bool m_CanShoot;
    private float m_ShootTimer;
    private void Awake()
    {
        m_CanShoot = false;
    }
    // Update is called once per frame
    private void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = Time.deltaTime;
                Fire();
            }
        }
    }
    private void Fire()
    {
        // Create an instance of the bullet, (shoot)
        Rigidbody bulletInstance = Instantiate(m_Bullet, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
     
        bulletInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        m_CanShoot = true;
        m_ShootTimer = m_ShootDelay;
    }
    private void OnTriggerExit(Collider other)
    {
        m_CanShoot = false;
    }
}