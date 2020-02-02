using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("{0} enter here.", other.name);
        if (other.tag == "Item")
        {
            //Debug.LogFormat("{0} enter here.", other.name);
            other.GetComponent<ItemDetector>().EnterDestroy = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item")
        {
            //Debug.LogFormat("{0} stay here.", other.name);
            other.GetComponent<ItemDetector>().StayDestroy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            //Debug.LogFormat("{0} exit here.", other.name);
            other.GetComponent<ItemDetector>().ExitDestroy = true;
        }
    }
}
