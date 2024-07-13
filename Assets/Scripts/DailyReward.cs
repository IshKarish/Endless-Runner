using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private Button claimButton;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Image rewardImage;
    private DateTime _lastClaimedTime;

    [SerializeField] private Sprite[] rewardSprites = new Sprite[7];

    public static RewardType Reward;

    private Vector3 _initialPos;

    public enum RewardType
    {
        DoubleScore,
        ExtraLife, 
        SlowerSpeed,
        BetterCoins,
        RickRoll,
        UnlimitedJumps,
        BibleForSaturday
    }

    private void Start()
    {
        _initialPos = transform.localPosition;
        transform.DOLocalMove(Vector3.zero, 2f);
        
        if (PlayerPrefs.HasKey("LastClaimedTime"))
            _lastClaimedTime = DateTime.Parse(PlayerPrefs.GetString("LastClaimedTime"));
        else
            _lastClaimedTime = DateTime.MinValue;
        
        UpdateRewardUI();
    }

    public void ClosePanel()
    {
        transform.DOLocalMove(_initialPos * -1, 2f);
    }

    private void UpdateRewardUI()
    {
        TimeSpan timeSinceLastClaim = DateTime.Now - _lastClaimedTime;

        if (timeSinceLastClaim.TotalHours >= 24)
        {
            claimButton.interactable = true;
            
            Reward = GetTodayReward();
            rewardText.text = GetRewardDescription(Reward);
            rewardImage.sprite = GetRewardSprite(Reward);
        }
        else
        {
            claimButton.interactable = false;
            rewardText.text = $"Next reward will be available in {(24 - timeSinceLastClaim.TotalHours):F1} hours";
            rewardText.transform.localPosition = Vector3.zero;

            rewardImage.enabled = false;
        }
    }

    public void ClaimReward()
    {
        if (claimButton.interactable)
        {
            _lastClaimedTime = DateTime.Now;
            PlayerPrefs.SetString("LastClaimedTime", _lastClaimedTime.ToString());
            PlayerPrefs.Save();
            
            UpdateRewardUI();
            gameObject.SetActive(false);
        }
    }

    RewardType GetTodayReward()
    {
        switch (DateTime.Now.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                return RewardType.DoubleScore;
            case DayOfWeek.Monday:
                return RewardType.ExtraLife;
            case DayOfWeek.Tuesday:
                return RewardType.SlowerSpeed;
            case DayOfWeek.Wednesday:
                return RewardType.BetterCoins;
            case DayOfWeek.Thursday:
                return RewardType.RickRoll;
            case DayOfWeek.Friday:
                return RewardType.UnlimitedJumps;
            case DayOfWeek.Saturday:
                return RewardType.BibleForSaturday;
            default:
                return RewardType.DoubleScore;
        }
    }

    string GetRewardDescription(RewardType reward)
    {
        switch (reward)
        {
            case RewardType.DoubleScore:
                return "2x score in the next match!";
            case RewardType.ExtraLife:
                return "extra life in the next match!";
            case RewardType.SlowerSpeed:
                return "Slower speed in the next match!";
            case RewardType.BetterCoins:
                return "Higher spawn rate for the cool coins in the next match!";
            case RewardType.RickRoll:
                return "You got nothing lol";
            case RewardType.UnlimitedJumps:
                return "Unlimited jumps for the next match!";
            case RewardType.BibleForSaturday:
                return "Why are you playing on Saturday? go pray or something";
            default:
                return "WTF? What is this reward?";
        }
    }

    Sprite GetRewardSprite(RewardType reward)
    {
        switch (reward)
        {
            case RewardType.DoubleScore:
                return rewardSprites[0];
            case RewardType.ExtraLife:
                return rewardSprites[1];
            case RewardType.SlowerSpeed:
                return rewardSprites[2];
            case RewardType.BetterCoins:
                return rewardSprites[3];
            case RewardType.RickRoll:
                return rewardSprites[4];
            case RewardType.UnlimitedJumps:
                return rewardSprites[5];
            case RewardType.BibleForSaturday:
                return rewardSprites[6];
            default:
                return rewardSprites[0];
        }
    }
}
