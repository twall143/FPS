using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IronSight : MonoBehaviour
{

    public Animator animator;

    public GameObject crossHair;
    public GameObject weaponCamera;

    public Camera mainCamera;
    public float scopedFOV = 20f;
    private float defaultFOV;

    private bool isScoped = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);

            if (isScoped)
                StartCoroutine(OnScoped());
            else
                OnUnScoped();
        }
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);

        crossHair.SetActive(false);
        // weaponCamera.SetActive(false);

        defaultFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }

    void OnUnScoped()
    {

        //  weaponCamera.SetActive(true);
        crossHair.SetActive(true);

        mainCamera.fieldOfView = defaultFOV;
    }
}
