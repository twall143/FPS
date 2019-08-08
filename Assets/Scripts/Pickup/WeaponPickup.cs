using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // The name of the weapon this should enable upon collection.
    // It defaults to an empty string.
    public string weaponName = string.Empty;
    void OnTriggerEnter(Collider other)
    {
        // Does the GameObject that collided with us have a WeaponSelector?
        WeaponSelector weaponSelector =
        other.gameObject.GetComponentInChildren<WeaponSelector>();
        // If there is a WeaponSelector, tell it to mark the matching weapon
        // as collected.
        if (weaponSelector != null)
        {
            weaponSelector.CollectWeapon(weaponName);
            gameObject.SetActive(false);
        }
    }
}