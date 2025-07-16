using UnityEngine;
using GamePush;

public class SurvivalReviveAd : MonoBehaviour
{
    private Combat _combat;

    public void OnPlayerDeath(Combat combat)
    {
        /*if (!GP_Ads.IsRewardedAvailable())
        {
            return;
        }*/
        _combat = combat;
        LevelManager.Pause();
        GameManager.Instance.UIManager.ShowRevivePopupUI();
    }

    public void OnReviveConfirmed()
    {
        GameManager.Instance.UIManager.HideRevivePopupUI();
        GP_Ads.OnRewardedReward += OnAdRewarded;
        GP_Ads.ShowRewarded("ShowRewardedOnDeath");
    }

    public void OnReviveDeclined()
    {
        GameManager.Instance.UIManager.HideRevivePopupUI();
        LevelManager.Resume();
        _combat.DeathEvent();
    }

    private void OnAdRewarded(string tag)
    {
        if (tag != "ShowRewardedOnDeath")
            return;

        GP_Ads.OnRewardedReward -= OnAdRewarded;        
        _combat.ResetCombat();
        LevelManager.Resume();
    }
}
