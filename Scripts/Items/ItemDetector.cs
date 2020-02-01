using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemDetector : MonoBehaviour
{
    private float m_StayTime;
    private void Awake()
    {
        if (!GetComponent<BoxCollider>())
        {
            gameObject.AddComponent<BoxCollider>();
            GetComponent<BoxCollider>().size = Vector3.one;
        }
        if (!GetComponent<Rigidbody>())
        {
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
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
        if (other.tag == "Player")
        {
            transform.DOLocalRotate(new Vector3(90, 180, 0), 2)
                .OnComplete(() => transform.DOLocalRotate(Vector3.zero, 0.2f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.DOComplete();
            transform.DOLocalRotate(Vector3.zero, 0.3f);
        }
    }
}
