using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEnabler : MonoBehaviour
{
    float time = 30f; //Seconds to read the text

    void Start()
    {
        Invoke("Hide", time);
    }

    void Hide()
    {
        Destroy(gameObject);
    }
}
