using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class DestroyDetector : MonoBehaviour
{
    public static DestroyDetector instance = null;
    public List<GameObject> _StayDestroys;

    [SerializeField] private CircleCollider2D _destroyCollider;
    [SerializeField] private ParticleSystem _destroyParticle;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDestroyable(other))
        {
            //Debug.LogFormat("{0} enter here. {1}", other.name, Time.frameCount);
            other.GetComponent<Detector>().EnterDestroy = true;

            if (!_StayDestroys.Contains(other.gameObject))
            {
                _StayDestroys.Add(other.gameObject);
            }
        }
        if (other.tag == "Border")
        {
            CharacterAbilities.instance.RebirthCharacter();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (IsDestroyable(other))
        {
            other.GetComponent<Detector>().StayDestroy = true;
            //if (!_StayDestroys.Contains(other.gameObject))
            //{
            //    _StayDestroys.Add(other.gameObject);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsDestroyable(other))
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
        _destroyCollider.radius *= scale.x;

        ParticleSystem.ShapeModule shape = _destroyParticle.shape;
        shape.radius *= scale.x;
    }

    private static bool IsDestroyable(Collider2D other)
    {
        return other.tag == "Item"
            || other.tag == "Ancient"
            || other.tag == "Unavailable";
    }
}
