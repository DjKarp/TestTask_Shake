using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePush;

public class SurvivalReviveAd : MonoBehaviour
{
    private Combat _combat;

    public void Init (Combat combat)
    {
        _combat = combat;
    }

    public void OnPlayerDeath()
    {
        if (!GP_Ads.IsRewardedAvailable())
        {
            return;
        }

        Time.timeScale = 0f;
        GameManager.Instance.UIManager.ShowRevivePopupUI();
    }

    public void OnReviveConfirmed()
    {
        GameManager.Instance.UIManager.HideRevivePopupUI();

        GP_Ads.ShowRewarded("ShowRewardedOnDeath");
        GP_Ads.OnRewardedReward += OnAdRewarded;
    }

    public void OnReviveDeclined()
    {
        GameManager.Instance.UIManager.HideRevivePopupUI();
        Time.timeScale = 1f;
        _combat.DeathEvent();
    }

    private void OnAdRewarded(string tag)
    {
        if (tag != "ShowRewardedOnDeath")
            return;

        GP_Ads.OnRewardedReward -= OnAdRewarded;
        Time.timeScale = 1f;
        _combat.ResetCombat();
    }
}
