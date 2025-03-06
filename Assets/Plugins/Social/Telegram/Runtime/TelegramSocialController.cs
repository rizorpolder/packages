using System;
using System.Runtime.InteropServices;
using Plugins.Social.Core.Runtime;
using UnityEngine;

namespace Plugins.Social.Telegram.Rutime
{
	public class TelegramSocialController : SocialManager
	{
		public Action<string> OnLogReceived = s => { };
		private const string link = "https://t.me/robust_testing_bot/robust_game?startapp=ot5nyy";

		public void CheckWindow()
		{
			CheckWindowType();
		}

		public void Share(System.Action callback)
		{
			// Call the share method with the specified message and URL
			//Share();

			// Trigger callback after a delay
			// DG.Tweening.DOVirtual.DelayedCall(1f, () =>
			// {
			// 	callback?.Invoke();
			// 	callback = null;
			// });
		}

// 		public void Share()
// 		{
// 			string telegramUrl = ApiManager.Instance.GetReferralLink();
//
// //#if !UNITY_EDITOR
// 			// Define your message and URL
// 			string telegramMessage = "🎮 Flappy Bird® is back—the classic game loved by all!\n🚀 Play the original with brand new levels!\n🎁 Earn rewards in our upcoming Airdrop!";
//
// //#if !UNITY_EDITOR && UNITY_WEBGL
// 			ShareOnTelegram(telegramMessage, telegramUrl);
// //#else
// 			Debug.Log($"Telegram sharing: Message: {telegramMessage}, URL: {telegramUrl}");
// // #endif
// // #endif
// 		}
//
// 		public void ShareReferral()
// 		{
// 			string telegramUrl = ApiManager.Instance.GetReferralLink();
//
// //#if !UNITY_EDITOR
// 			// Define your message and URL
// 			string telegramMessage = "🎮 Flappy Bird® is back—the classic game loved by all!\n🚀 Play the original with brand new levels!\n🎁 Earn rewards in our upcoming Airdrop!";
//
// //#if !UNITY_EDITOR && UNITY_WEBGL
// 			ShareOnTelegram(telegramMessage, telegramUrl);
// //#else
// 			Debug.Log($"Telegram sharing: Message: {telegramMessage}, URL: {telegramUrl}");
// //#endif
// //#endif
// 		}

// 		public void Share(string message)
// 		{
// 			string telegramUrl = ApiManager.Instance.GetReferralLink();
//
// //#if !UNITY_EDITOR
// 			// Define your message and URL
// 			string telegramMessage = message;
//
// //#if !UNITY_EDITOR && UNITY_WEBGL
// 			ShareOnTelegram(telegramMessage, telegramUrl);
// //#else
// 			Debug.Log($"Telegram sharing: Message: {telegramMessage}, URL: {telegramUrl}");
// //#endif
// //#endif
// 		}



		public override void Share(string message, string url, Action callback = null)
		{

		}

		public override void OpenSocialChannel(string channel)
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			OpenTelegramChannel(channel);
#else
			Debug.Log("This function is intended for WebGL builds.");
#endif
		}

		// This method will be called from JS
		public override void ReceiveLog(string message)
		{
			Debug.Log("TelegramShare: " + message);
			OnLogReceived?.Invoke(message);
		}


		[DllImport("__Internal")]
		private static extern void CheckWindowType();

		[DllImport("__Internal")]
		private static extern void ShareOnTelegram(string message, string url);

		[DllImport("__Internal")]
		private static extern void OpenTelegramChannel(string username);
	}
}