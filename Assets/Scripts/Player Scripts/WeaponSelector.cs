using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSelector : MonoBehaviour
{
    // This defines a custom class that we're using to associate a reference
    // to a weapon along with whether or not it has been collected.
    [System.Serializable]
    public class SelectableWeapon
    {
        public GameObject weaponGameObject;
        public GameObject grenadeGameObject;
        public bool collected = false;
    }
    // This is a list of that class. This is where the data is actually
    // stored.
    public List<SelectableWeapon> weapons = new List<SelectableWeapon>();
    public List<SelectableWeapon> grenades = new List<SelectableWeapon>();
    void Update()
    {
        // Here we have one button per weapon, and select the weapon if the
        // associated button is pressed. Alternatively, we could have next/
        // previous weapon buttons.
        if (Input.GetButtonDown("SelectWeapon1"))
        {
            SelectWeapon(0);
        }
        else if (Input.GetButtonDown("SelectWeapon2"))
        {
            SelectWeapon(1);
        }
        else if (Input.GetButtonDown("SelectWeapon3"))
        {
            SelectWeapon(2);
        }
        else if (Input.GetButtonDown("SelectWeapon4"))
        {
            SelectWeapon(3);
        }
        if (Input.GetButtonDown("SelectGrenade1"))
        {
            SelectGrenade(0);
        }
        else if (Input.GetButtonDown("SelectGrenade2"))
        {
            SelectGrenade(1);
        }
        else if (Input.GetButtonDown("SelectGrenade3"))
        {
            SelectGrenade(2);
        }
    }
    public void CollectWeapon(string collectedWeaponName)
    {
        // Look at each weapon in our list of selectable weapons...
        for (int i = 0; i < weapons.Count; i++)
        {
            // ... and if we find a match, set it's "collected" variable to true...
            if (weapons[i] != null & weapons[i].weaponGameObject.name == collectedWeaponName)
            {
                weapons[i].collected = true;
                // ... and also select it.
                SelectWeapon(i);
            }
        }
    }
    // Select the weapon at a specified index of the collection.
    public void SelectWeapon(int selectedIndex)
    {
        // Make sure that the selection is valid - it must be in range and
        // must refer to a weapon that has been collected.
        if (selectedIndex < 0 ||
        selectedIndex >= weapons.Count ||
        weapons[selectedIndex] == null ||
        weapons[selectedIndex].collected == false)
        {
            return;
        }
        // Activate the selected weapon, deactivate all others.
        for (int index = 0; index < weapons.Count; index++)
        {
            if (weapons[index] != null)
            {
                weapons[index].weaponGameObject.SetActive(index == selectedIndex);
            }
        }
    }
    public void CollectGrenade(string collectedGrenadeName)
    {
        // Look at each weapon in our list of selectable weapons...
        for (int i = 0; i < grenades.Count; i++)
        {
            // ... and if we find a match, set it's "collected" variable to true...
            if (grenades[i] != null & grenades[i].grenadeGameObject.name == collectedGrenadeName)
            {
                grenades[i].collected = true;
                // ... and also select it.
                SelectGrenade(i);
            }
        }
    }
    // Select the weapon at a specified index of the collection.
    public void SelectGrenade(int selectedIndex)
    {
        // Make sure that the selection is valid - it must be in range and
        // must refer to a weapon that has been collected.
        if (selectedIndex < 0 ||
        selectedIndex >= grenades.Count ||
        grenades[selectedIndex] == null ||
        grenades[selectedIndex].collected == false)
        {
            return;
        }
        // Activate the selected weapon, deactivate all others.
        for (int index = 0; index < grenades.Count; index++)
        {
            if (grenades[index] != null)
            {
                grenades[index].grenadeGameObject.SetActive(index == selectedIndex);
            }
        }
    }
}