using System.Collections.Generic;
using UnityEngine;

public class DestroyDetector : MonoBehaviour
{
    public static DestroyDetector instance = null;
    public List<GameObject> _StayDestroys;

    public SphereCollider _DestroyCollider;
    public ParticleSystem _DestroyParticle;
    // Start is called before the first frame update
    void Awake()
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
        if (other.tag == "Item" || other.tag == "Ancient" || other.tag == "Unavailable")
        {
            //Debug.LogFormat("{0} enter here.", other.name);
            other.GetComponent<Detector>().EnterDestroy = true;
        }
        if (other.tag == "Border")
        {
            CharacterAbilities.instance.RebirthCharacter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Ancient" || other.tag == "Unavailable")
        {
            other.GetComponent<Detector>().StayDestroy = true;
            if (!_StayDestroys.Contains(other.gameObject))
            {
                _StayDestroys.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Ancient" || other.tag == "Unavailable")
        {
            //Debug.LogFormat("{0} exit here.", other.name);
            other.GetComponent<Detector>().ExitDestroy = true;

            if (_StayDestroys.Contains(other.gameObject))
            {
                _StayDestroys.Remove(other.gameObject);
            }
        }
    }

    public void SetDestroyDetectorScale(Vector3 scale)
    {
        _DestroyCollider.radius *= scale.x;

        ParticleSystem.ShapeModule shape = _DestroyParticle.shape;
        shape.radius *= scale.x;
    }
}
