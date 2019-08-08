using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeClass : MonoBehaviour
{
    public void changeClasses()
    {
        GetComponent<GameManager>().classSelector();
    }

}
