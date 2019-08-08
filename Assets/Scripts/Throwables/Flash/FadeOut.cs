using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class FadeOut : MonoBehaviour
{
    public bool fade;

    RawImage img;
    RawImage colorMultiplier;
    

    float timer;

    Color color;
    Color color2;

    bool count;

    // Start is called before the first frame update

    void Start()
    {
        img = GetComponent<RawImage>();
        RawImage[] imgs = transform.parent.GetComponentsInChildren<RawImage>(); 

        foreach(RawImage im in imgs)
        {
            if(im != img)
            {
                colorMultiplier = im;
            }
        }
        color = img.color;
        color.a = 0;
        img.color = color;

        color2 = colorMultiplier.color;
        color2.a = 0;
        colorMultiplier.color = color2;

    }

    // Update is called once per frame
  
    void Update()
    {
        if(fade)
        {
            color.a = 1f;
            img.color = color;

            color2.a = 0.7f;
            colorMultiplier.color = color2;

            img.enabled = true;
            colorMultiplier.enabled = true;

            count = true;
            fade = false;

        }
        if(count)
        {
            timer += Time.deltaTime;
            if(timer >2)
            {
                color.a -= Time.deltaTime * 0.1f;
                img.color = color;

                color2.a -= Time.deltaTime * 0.05f;
                colorMultiplier.color = color2;

                if(img.color.a <= 0)
                {
                    timer = 0;
                    count = false;
                }

            }
        }
        else
        {
            img.enabled = false;
            colorMultiplier.enabled = false;
        }
    }
}
