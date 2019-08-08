using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class PlayerLean : MonoBehaviour
{

    public bool leanLeft = false;
    public bool leanRight = false;
    // public var myCharacter;

  public void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            doLeanLeft();
        }

        if (Input.GetKeyDown("e"))
        {
            doLeanRight();
        }

        if (Input.GetKeyUp("q"))
        {
            doLeanBack();
        }

        if (Input.GetKeyUp("e"))
        {
            doLeanBack();
        }

    }

    public void doLeanLeft()
    {

        if (leanLeft == false)
        {
          //  myCharacter.transform.rotation = Quaternion.Euler(0, 0, 30);
            leanLeft = false;
        }

    }

    public void doLeanRight()
    {

        if (leanRight == false)
        {
          //  myCharacter.transform.rotation = Quaternion.Euler(0, 0, -30);
            leanRight = false;
        }

    }

    public void doLeanBack()
    {
      //  myCharacter.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
 