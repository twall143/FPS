using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class CaptureScreen : MonoBehaviour
{
    public bool cap;
    public bool startFading;

    RawImage image;
    FadeOut fadeOut;

    public AudioClip audioClip;

    // Start is called before the first frame update
    
    void Start()
    {
       image = GameObject.FindGameObjectWithTag("OveralTexture").GetComponent<RawImage>();
       fadeOut = image.transform.GetComponent<FadeOut>();
    }

    // Update is called once per frame
 
    void Update()
    {
        if(startFading)
        {
            fadeOut.fade = true;
            startFading = false;
        }
    }
  
    void OnPostRender()
    {
        if(cap)
        {
            image.texture = Capture(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            cap = false;
        }
       
    }
   
    public static Texture Capture(Rect captureZone, int destX, int destY)
    {
        Texture2D result;
        result = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        result.ReadPixels(captureZone, destX, destY, false);

        result.Apply();

        return result;
    }
}
