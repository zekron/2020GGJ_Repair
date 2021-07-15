using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameMgr : MonoBehaviour
{
    [SerializeField] private Image _imageSetting;

    private Tween m_ReturnTween;
    private bool _canEnterSetting = false;

    private void OnEnable()
    {
        _canEnterSetting = false;
    }
    public void AddGameplayListener()
    {
        TouchMgr.instance.AddListener(TouchMgr.instance._EnterSettingBtn, EnterSetting);
    }
    void RemoveGameplayListener()
    {
        TouchMgr.instance.RemoveListener(TouchMgr.instance._EnterSettingBtn, EnterSetting);
    }
    void EnterSetting()
    {
        if (!_canEnterSetting)
        {
            _canEnterSetting = true;

            _imageSetting.transform.localScale = Vector3.zero;
            _imageSetting.DOColor(Color.white, 1);
            _imageSetting.transform.DOScale(1, 1);
            m_ReturnTween = DOTween.Sequence().AppendInterval(3).AppendCallback(() =>
                {
                    _imageSetting.transform.DOScale(0, 1).OnComplete(() => _imageSetting.transform.localScale = Vector3.one);
                    _imageSetting.DOColor(Color.clear, 1);
                    _canEnterSetting = false;
                });
            return;
        }

        RemoveGameplayListener();
        _canEnterSetting = false;
        m_ReturnTween.Complete(false);
        _imageSetting.DOColor(Color.clear, 1)/*.OnComplete(() => GameMgr.instance.SetGameState(eGameState.eInSetting))*/;
        //_imageSetting.transform.DOScale(0, 1).OnComplete(() => _imageSetting.enabled = false);

        GameMgr.Instance.GetGameStateMgr().SetGameState(GameState.InSetting);
    }
}
