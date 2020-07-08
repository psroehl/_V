using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raycasting : MonoBehaviour
{
    private GameObject raycastedObj;

    [SerializeField] private int rayLength = 10;

    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image uiCrosshair;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100, ~LayerMask.NameToLayer("Enemy")))

        {
            Debug.Log(hit.collider.name);
            CrossHairActive();
        }
        else
        {
            CrossHairNormal();
        }

        //if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        //{
        //    if (hit.collider.CompareTag("Object"))
        //    {
        //        raycastedObj = hit.collider.gameObject;
        //        CrossHairActive();
        //        if (Input.GetKeyDown("e"))
        //        {
        //            Debug.Log("I have interacted with an object");
        //            raycastedObj.SetActive(false);
        //        }
        //    }


        //    else
        //    {
        //        CrossHairNormal();
        //    }
            
           


        }
        void CrossHairActive()
        {
            uiCrosshair.color = Color.red;
        }
        void CrossHairNormal()
        {
            uiCrosshair.color = Color.white;
        }
    }


    