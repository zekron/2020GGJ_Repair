using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDetector : MonoBehaviour
{
    private float m_StayTime = 0;
    private bool m_CanBeDetected = false;
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
            m_CanBeDetected = true;
            Debug.LogFormat("{0} enter here.", other.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item")
        {
            Debug.LogFormat("{0} stay here.", other.name);
            if (!m_CanBeDetected) return;

            m_StayTime -= Time.deltaTime;

            if (m_StayTime <= 0f)
            {
                ItemState curItemState = other.GetComponent<ItemDetector>()._CurItemState;
                if (--curItemState < ItemState.eStateOne)
                {
                    curItemState = ItemState.eStateFour;
                    m_CanBeDetected = false;
                    other.GetComponent<Item>().ChangeSprite(curItemState);
                    m_StayTime = 0;
                    return;
                }
                other.GetComponent<Item>().ChangeSprite(curItemState);
                m_StayTime = 1;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.LogFormat("{0} exit here.", other.name);
        if (other.tag == "Item")
        {
            m_StayTime = 0;
            ItemState curItemState = other.GetComponent<ItemDetector>()._CurItemState;

            if (curItemState != ItemState.eStateFour)
                other.GetComponent<Item>().ChangeSprite(curItemState);
        }
    }
}
