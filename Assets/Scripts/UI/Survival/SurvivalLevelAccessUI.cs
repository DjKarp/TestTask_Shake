using UnityEngine;
using GamePush;

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

    private async void Start()
    {
        _hasTempAccess = DataManager.data._tempSurvivalModeUnlocked;
        UpdateVisuals();

        await GP_Init.Ready;
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
        
        if (CheckReady() && !DataManager.data._tempSurvivalModeUnlocked)
        {
            GP_Ads.OnRewardedReward += OnAdRewarded;
            GP_Ads.ShowRewarded("SURVIVAL");
        }
        else
        {
            Debug.Log("Реклама не готова");
            _isAsTiggered = false; // можно снова войти
        }
    }

    private void OnAdRewarded(string tag)
    {
        if (tag != "SURVIVAL") 
            return;

        GP_Ads.OnRewardedReward -= OnAdRewarded;

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

    private void OnPluginReady()
    {
        Debug.LogError("Ad Plugin ready");
    }

    private bool CheckReady()
    {
        if (GP_Init.isReady)
        {
            return true;
        }
        else
        {
            Debug.LogError("Ad Plugin NOT ready");
            return false;
        }
    }
}
