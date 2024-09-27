using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace DailyRewardSystem
{
	public enum RewardType
	{
		keys,
		Coins
	}

	[Serializable]
	public struct Reward
	{
		public RewardType Type;
		public int Amount;
	}

	public class DailyRewards : MonoBehaviour
	{

		[Header("Main Menu UI")]
		[SerializeField] Text coinsText;
		[SerializeField] Text keysText;

		[Space]
		[Header("Reward UI")]
		[SerializeField] GameObject rewardsCanvas;
		[SerializeField] Button openButton;
		[SerializeField] Button closeButton;
		[SerializeField] Image rewardImage;
		[SerializeField] Text rewardAmountText;
		[SerializeField] Button claimButton;
		[SerializeField] GameObject rewardsNotification;
		[SerializeField] GameObject RewardsPanel;
		[SerializeField] GameObject noMoreRewardsPanel;


		[Space]
		[Header("Rewards Images")]
		[SerializeField] Sprite iconCoinsSprite;
		[SerializeField] Sprite iconKeysSprite;

		[Space]
		[Header("Rewards Database")]
		[SerializeField] RewardsDatabase rewardsDB;

		[Space]
		[Header("FX")]
		[SerializeField] ParticleSystem fxCoins;
		[SerializeField] ParticleSystem fxMetals;

		private int nextRewardIndex;

		void Start()
		{
			string lastTime = PlayerPrefs.GetString("LastClaimTime", "");

			DateTime lastClaimTime;

            if (!string.IsNullOrEmpty(lastTime))
            {
				lastClaimTime = DateTime.Parse(lastTime);
			}
            else
            {
				lastClaimTime = DateTime.MinValue;
            }

			if(DateTime.Today > lastClaimTime)
            {
				ActivateReward();
			}
            else
            {
				DeactivateReward();
			}

			Initialize();
		}

		void Initialize()
		{
			nextRewardIndex = PlayerPrefs.GetInt("Next_Reward_Index", 0);

			//Update Mainmenu UI (metals,coins,gems)
			UpdateCoinsTextUI();
			UpdateKeysTextUI();

			//Add Click Events
			openButton.onClick.AddListener(OnOpenButtonClick);

			closeButton.onClick.AddListener(OnCloseButtonClick);

			claimButton.onClick.AddListener(OnClaimButtonClick);

			//Check if the game is opened for the first time then set Reward_Claim_Datetime to the current datetime
			
		}

		void ActivateReward()
		{

			noMoreRewardsPanel.SetActive(false);
			rewardsNotification.SetActive(true);

			//Update Reward UI
			Reward reward = rewardsDB.GetReward(nextRewardIndex);

			//Icon
			if (reward.Type == RewardType.Coins)
				rewardImage.sprite = iconCoinsSprite;
			else
				rewardImage.sprite = iconKeysSprite;

			//Amount
			rewardAmountText.text = string.Format("+{0}", reward.Amount);

		}

		void DeactivateReward()
		{

			noMoreRewardsPanel.SetActive(true);
			RewardsPanel.SetActive(false);
			rewardsNotification.SetActive(false);
		}

		void OnClaimButtonClick()
		{
			Reward reward = rewardsDB.GetReward(nextRewardIndex);

			//check reward type
			if (reward.Type == RewardType.Coins)
			{
				GameData.Coins += reward.Amount;
				fxCoins.Play();
				UpdateCoinsTextUI();

			}
			else
			{
				GameData.Keys += reward.Amount;
				fxMetals.Play();
				UpdateKeysTextUI();

			}


			//Save next reward index
			nextRewardIndex++;
			if (nextRewardIndex >= rewardsDB.rewardsCount)
				nextRewardIndex = 0;

			PlayerPrefs.SetInt("Next_Reward_Index", nextRewardIndex);

			//Save DateTime of the last Claim Click
			PlayerPrefs.SetString("LastClaimTime", DateTime.Now.ToString());

			DeactivateReward();
		}

		//Update Mainmenu UI (metals,coins,gems)--------------------------------
		void UpdateCoinsTextUI()
		{
			coinsText.text = GameData.Coins.ToString();
		}

		void UpdateKeysTextUI()
		{
			keysText.text = GameData.Keys.ToString();
		}

		//Open | Close UI -------------------------------------------------------
		void OnOpenButtonClick()
		{
			rewardsCanvas.SetActive(true);
		}

		void OnCloseButtonClick()
		{
			rewardsCanvas.SetActive(false);
		}
	}

}

