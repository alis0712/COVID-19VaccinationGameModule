using System.Collections;
using UnityEngine;
using System;
//using UnityStandardAssets.Vehicles.Car;


public class EnterExit : MonoBehaviour
{
    private bool inVehicle = false;
    public GameObject vehicleScript;
    public GameObject guiObj;
    public GameObject guiObj2;
    public AudioSource Audio;
    public ProgressBar PB;
    GameObject player;
    public Transform heal;
    public float detectionRange;
    private bool closeEnough;
 

    public float dmgTick = 100; // Damage each tick
    public float timeXTick = 1; // Time in seconds each tick of damage
    public int totalTicks = 3; // how many seconds ( ticks ) 100 damage each tick
    public float healTick = 100; // heal each tick
    

    IEnumerator DmgXSecond()
    {
        int ticks = 0;
        int totalTicksTemp = totalTicks;

        while (ticks < totalTicksTemp)
        {
            ticks++;
            PB.BarValue -= dmgTick;  // Player recive 100 damage
            yield return new WaitForSecondsRealtime(timeXTick);  // wait 1 second
        }

    }

    IEnumerator healXSecond()
    {
        int ticks = 0;
        int totalTicksTemp = totalTicks;

        while (ticks < totalTicksTemp)
        {
            ticks++;
            PB.BarValue += healTick;  // Player recive 100 damage
            yield return new WaitForSecondsRealtime(timeXTick);  // wait 1 second
        }

    }


    void Start()
    {
        vehicleScript = GameObject.Find("FuzzyRed");
        player = GameObject.FindWithTag("Player");
        guiObj.SetActive(false);
        guiObj2.SetActive(false);
        PB.BarValue = 100f;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && inVehicle == false)
        {
            guiObj.SetActive(true);
            if ( Input.GetButtonDown("6Key"))
            {
                guiObj.SetActive(false);
                guiObj2.SetActive(true);
                player.transform.parent = gameObject.transform;
                vehicleScript.SetActive(true);
                player.SetActive(false);
                Audio.Play();
                inVehicle = true;
                StartCoroutine("DmgXSecond");    
            }
        }
    }

  
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiObj.SetActive(false);
        }
    }
    void Update()
    {
        closeEnough = false;
        if (Vector3.Distance(heal.position, transform.position) <= detectionRange)
        {
            closeEnough = true;
        }
        if (closeEnough && inVehicle == true)
        {

            StartCoroutine("healXSecond");

        }

        if (inVehicle == true && Input.GetButtonDown("5Key"))
        {
            vehicleScript.SetActive(false);
            player.SetActive(true);
            guiObj2.SetActive(false);
            Audio.Stop();
            player.transform.parent = null;
            inVehicle = false;
        }

    }
}