using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("5Key"))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }

        if (Input.GetButtonDown("6Key"))
        {
            cam2.SetActive(true);
            cam1.SetActive(false);

        }
    }
}
