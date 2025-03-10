using System;
using System.Runtime.InteropServices;
using Ads.Core.Runtime;
using Ads.Core.Runtime.AdStatus;
using UnityEngine;

namespace Plugins.PlayDeck.Runtime.Ads
{
	public class PlayDeckBridgeAds : AdsController<GamePlacements>
	{
		private bool _adInProgress;
		public override bool AdInProgress => _adInProgress;

		private GamePlacements _currentPlacement;

		protected override void Initialize(string sdkKey, string playerId, bool ageRestrictedFlag)
		{
		}

		public override bool IsInitialized()
		{
			return true;
		}

		public override AdStatus InitializeAdForPlacement(GamePlacements placement)
		{
			return SetStatusForPlacement(placement, AdStatusValue.Ready);
		}

		public override bool IsRewardedAdReady(GamePlacements placement)
		{
			return true;
		}

		public override void PreloadRewardedAd(GamePlacements placement)
		{
		}

		public override void ShowRewardedAd(GamePlacements placement)
		{
			AudioListener.volume = 0;
			AudioListener.pause = true;
			_adInProgress = true;
			_currentPlacement = placement;
//#if UNITY_WEBGL
			if (Application.isEditor)
			{
				OnRewardedVideoClosed();
				OnRewardedVideoWatched();
			}
			else
			{
				PlayDeckBridge_PostMessage_ShowAd();
			}

//endif
		}

		private void AdHandled()
		{
			_adInProgress = false;
			ResetLastAdTimer();
			AudioListener.volume = 1f;
			AudioListener.pause = false;
		}

		#region Called from JS

		public void OnRewardedVideoClosed()
		{
			AdHandled();

			if (TryGetStatusForPlacement(_currentPlacement, out var rewardStatus))
			{
				rewardStatus.Set(AdStatusValue.Hidden);
				rewardStatus.Set(AdStatusValue.Ready);
			}
		}

		public void OnRewardedVideoWatched()
		{
			if (TryGetStatusForPlacement(_currentPlacement, out var rewardStatus))
			{
				rewardStatus.Set(AdStatusValue.SuccessWatched);
				rewardStatus.Set(AdStatusValue.Ready);
			}
		}

		public void OnRewardedVideoFailed()
		{
			AdHandled();
			if (TryGetStatusForPlacement(_currentPlacement, out var rewardStatus))
			{
				rewardStatus.Set(AdStatusValue.Failed);
				rewardStatus.Set(AdStatusValue.Ready);
			}
		}

		#endregion

		#region Banners

		public override void CreateBanner(IBannerData data)
		{
			throw new NotImplementedException();
		}

		public override void ShowBanner(GamePlacements placement)
		{
			throw new NotImplementedException();
		}

		public override void HideBanner(GamePlacements placement)
		{
			throw new NotImplementedException();
		}

		public override void DestroyBanner(GamePlacements placement)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Interstitials

		public override void PreloadInterstitial(GamePlacements placement)
		{
			throw new NotImplementedException();
		}

		public override void ShowInterstitialAd(GamePlacements placement)
		{
			throw new NotImplementedException();
		}

		#endregion

		//called from js
		public void RewardedAdHandler(string data)
		{
			OnRewardedVideoWatched();
		}

		//called from js

		public void ErrAdHandler(string data)
		{
			OnRewardedVideoFailed();
		}

		//called from js

		public void SkipAdHandler(string data)
		{
			OnRewardedVideoClosed();
		}

		//called from js
		private void NotFoundAdHandler(string data)
		{
			OnRewardedVideoFailed();
		}

		//called from js
		private void StartAdHandler(string data)
		{
		}

		[DllImport("__Internal")]
		private static extern void PlayDeckBridge_PostMessage_ShowAd();
	}
}