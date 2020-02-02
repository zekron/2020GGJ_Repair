using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDetector : MonoBehaviour
{
    public static DestroyDetector instance = null;
    public List<GameObject> _StayDestroys;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("{0} enter here.", other.name);
        if (other.tag == "Item" || other.tag == "Ancient")
        {
            //Debug.LogFormat("{0} enter here.", other.name);
            other.GetComponent<Detector>().EnterDestroy = true;
        }
        if (other.tag == "Border")
        {
            Debug.LogFormat("{0} enter here.", other.name);
            CharacterAbilities.instance.RebirthCharacter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Ancient")
        {
            other.GetComponent<Detector>().StayDestroy = true;
            if (!_StayDestroys.Contains(other.gameObject))
            {
                _StayDestroys.Add(other.gameObject);
                Debug.LogError("_StayDestroys  " + _StayDestroys.Count);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Ancient")
        {
            //Debug.LogFormat("{0} exit here.", other.name);
            other.GetComponent<Detector>().ExitDestroy = true;

            if (_StayDestroys.Contains(other.gameObject))
            {
                _StayDestroys.Remove(other.gameObject);
                Debug.LogError("Remains _StayDestroys.Count " + _StayDestroys.Count);
            }
        }
    }
}
