using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSelector : MonoBehaviour
{
   
    [System.Serializable]
    public class SelectableGrenade
    {
        public GameObject grenadeGameObject;
        public bool collected = false;
    }
    // This is a list of that class. This is where the data is actually
    // stored.
    public List<SelectableGrenade> grenades = new List<SelectableGrenade>();
    void Update()
    {
        // Here we have one button per weapon, and select the weapon if the
        // associated button is pressed. Alternatively, we could have next/
        // previous weapon buttons.
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