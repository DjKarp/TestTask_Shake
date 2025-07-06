using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GamePush;

public class SurvivalLevelAccessUI : MonoBehaviour
{
    [Header("UI / Визуальные объекты")]
    [SerializeField] private GameObject[] _lockedIndicator;
    [SerializeField] private GameObject[] _unlockedIndicator;

    // Но я бы использовал Signals от Zenject или другую шину событий
    private SurvivalFinishZone _survivalFinishZone;

    private bool _isFullyUnlocked => DataManager.IsMainGameFinished();
    private bool _hasTempAccess = false;

    private bool _isAsTiggered = false;

    private void Awake()
    {
        _survivalFinishZone = GetComponentInParent<SurvivalFinishZone>();
    }

    private void Start()
    {
        _hasTempAccess = DataManager.data._tempSurvivalModeUnlocked;
        UpdateVisuals();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAsTiggered) 
            return;

        _isAsTiggered = true;

        if (!_isFullyUnlocked && !_hasTempAccess)
            TryShowAd();
    }

    private void TryShowAd()
    {
        /*
        if (GP_Ads.IsRewardedReady("SURVIVAL"))
        {
            GP_Ads.OnRewardedReward += OnAdRewarded;
            GP_Ads.ShowRewarded("SURVIVAL");
        }
        else
        {
            Debug.Log("Реклама не готова");
            _isAsTiggered = false; // можно снова войти
        }*/

        StartCoroutine(TempAd());
    }

    private IEnumerator TempAd()
    {
        int secDelay = 0;

        while (secDelay < 1)
        {
            Debug.LogError("ad see on " + secDelay);
            secDelay++;
            yield return new WaitForSeconds(1);
        }

        Debug.LogError("Ad Success seen!");

        OnAdRewarded("SURVIVAL");
    }

    private void OnAdRewarded(string tag)
    {
        if (tag != "SURVIVAL") 
            return;

        //GP_Ads.OnRewardedReward -= OnAdRewarded;

        DataManager.data._tempSurvivalModeUnlocked = true;
        DataManager.Save();

        _hasTempAccess = true;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        bool accessGranted = _isFullyUnlocked || _hasTempAccess;

        if (accessGranted)
            _survivalFinishZone.UpdateMaterialsOnOpenMode();

        foreach (var go in _lockedIndicator)
            go.SetActive(!accessGranted);

        foreach (var go in _unlockedIndicator)
            go.SetActive(accessGranted);
    }

    public void OnAdUsed()
    {
        DataManager.data._tempSurvivalModeUnlocked = false;
        DataManager.Save();
    }

    /*
    // Когда игрок прошёл все уровни
    public void UnlockPermanently()
    {
        DataManager.data._survivalModeUnlocked = true;
        DataManager.Save();

        _hasTempAccess = true;
        UpdateVisuals();
    }*/
}
