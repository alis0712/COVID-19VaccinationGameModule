using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    float xspeep = 0f;
    float zspeep = 0f;
    float power = 0.001f;
    float friction = 0.99f;
    bool right = false;
    bool left = false;
    bool forward = false;
    bool backward = false;
    public float fuel = 2;

    // Use this for initialization
    void FixedUpdate()
    {
        

        if (forward)
        {
            zspeep += power;
            fuel -= power;
        }
        if (backward)
        {
            zspeep -= power;
            fuel -= power;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("f"))
        {
            forward = true;
        }
        if (Input.GetKeyUp("f"))
        {
            forward = false;
        }
        if (Input.GetKeyDown("b"))
        {
            backward = true;
        }
        if (Input.GetKeyUp("b"))
        {
            backward = false;
        }

        if (Input.GetKeyDown("l"))
        {
            right = true;
            transform.Rotate(0, 0, -120 * Time.deltaTime);
        }
        
        if (Input.GetKeyDown("r"))
        {
            left = true;
            transform.Rotate(0, 0, 120 * Time.deltaTime);

        }



        if (fuel < 0)
        {

            xspeep = 0;

        }

        if (fuel < 0)
        {

            zspeep = 0;

        }

        xspeep *= friction;

        zspeep *= friction;
        transform.Translate(Vector3.up * -zspeep);

  


    }
}
