using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ItemDetector : Detector, IPointerClickHandler
{
    [SerializeField] private Item _myItem;

    private float _stayTime = 0;

    private void OnEnable()
    {
        _myItem = GetComponent<Item>();
    }
    private void Start()
    {
        CharacterAbilities.Add_OnTimeLock(ResetItemState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestroyDetector"))
        {
            if (/*EnterDestroy &&*/ !_CanBeDetected)
            {
                _CanBeDetected = true;
            }
        }
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"{name} {other.name} {_CanBeDetected}");
            if (other.TryGetComponent(out Damageable damageableComp))
            {
                IAggressive attacker = _myItem as IAggressive;
                attacker?.Attack(damageableComp);
                if (_myItem is HeartItem)
                {
                    HeartItem item = _myItem as HeartItem;
                    item.ChangeSprite();
                    item.Heal(damageableComp);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.LogFormat("{0} stay here.", other.name);

        if (other.CompareTag("DestroyDetector"))
        {
            if (!_CanBeDetected || !StayDestroy) return;

            _stayTime -= Time.deltaTime;

            if (_stayTime <= 0f)
            {
                ItemStatus itemStatus = _myItem.GetDetectedItemStatus();
                if (itemStatus.ItemState - 1 < GameItemState.StateOne)
                {
                    _CanBeDetected = false;
                    _stayTime = 0;
                    return;
                }
                _myItem.SetItemState(itemStatus.ItemState - 1);
                _myItem.ChangeSprite();
                _stayTime = StaticData.DestroyDuration;
            }
        }
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Damageable damageableComp) && !damageableComp.GetHit)
            {
                IAggressive attacker = _myItem as IAggressive;
                attacker?.Attack(damageableComp);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.LogFormat("{0} exit here.", other.name);
        if (other.CompareTag("DestroyDetector"))
        {
            if (ExitDestroy)
            {
                _stayTime = 0;
                if (_myItem.GetDetectedItemStatus().ItemState != GameItemState.StateFour)
                {
                    //GetComponent<Item>().ChangeSprite(_CurItemState = ItemState.eStateFour);
                    //_OnDestroyDetectorTriggered.Invoke(_CurItemState = ItemState.eStateFour);
                }
            }
        }
    }

    public void ResetItemState()
    {
        if (!_IsInTimeWalkBack) return;

        _myItem.SetItemState(GameItemState.StateOne);
        _myItem.ChangeSprite();
        _IsInTimeWalkBack = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (StayDestroy && CompareTag("Item"))
        {
            IFetched fetched = _myItem as IFetched;
            fetched?.Fetch();
        }
    }
}
