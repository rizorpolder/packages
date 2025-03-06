// using UnityEngine;
// using System.Runtime.InteropServices;
//
// public class TelegramShare
// {
//
//
//     public void Share(System.Action callback)
//     {
//         // Call the share method with the specified message and URL
//         Share();
//
//         // Trigger callback after a delay
//         DG.Tweening.DOVirtual.DelayedCall(1f, () =>
//         {
//             callback?.Invoke();
//             callback = null;
//         });
//     }
//
//     public void Share()
//     {
//         string telegramUrl = ApiManager.Instance.GetReferralLink();
//
// //#if !UNITY_EDITOR
//         // Define your message and URL
//         string telegramMessage = "🎮 Flappy Bird® is back—the classic game loved by all!\n🚀 Play the original with brand new levels!\n🎁 Earn rewards in our upcoming Airdrop!";
//
// //#if !UNITY_EDITOR && UNITY_WEBGL
//         ShareOnTelegram(telegramMessage, telegramUrl);
// //#else
//         Debug.Log($"Telegram sharing: Message: {telegramMessage}, URL: {telegramUrl}");
// // #endif
// // #endif
//     }
//
//     public void ShareReferral()
//     {
//         string telegramUrl = ApiManager.Instance.GetReferralLink();
//
// //#if !UNITY_EDITOR
//         // Define your message and URL
//         string telegramMessage = "🎮 Flappy Bird® is back—the classic game loved by all!\n🚀 Play the original with brand new levels!\n🎁 Earn rewards in our upcoming Airdrop!";
//
// //#if !UNITY_EDITOR && UNITY_WEBGL
//         ShareOnTelegram(telegramMessage, telegramUrl);
// //#else
//         Debug.Log($"Telegram sharing: Message: {telegramMessage}, URL: {telegramUrl}");
// //#endif
// //#endif
//     }
//
//     public void Share(string message)
//     {
//         string telegramUrl = ApiManager.Instance.GetReferralLink();
//
// //#if !UNITY_EDITOR
//         // Define your message and URL
//         string telegramMessage = message;
//
// //#if !UNITY_EDITOR && UNITY_WEBGL
//         ShareOnTelegram(telegramMessage, telegramUrl);
// //#else
//         Debug.Log($"Telegram sharing: Message: {telegramMessage}, URL: {telegramUrl}");
// //#endif
// //#endif
//     }
//
//     // This method will be called from JavaScript to log messages
//     public void ReceiveLog(string message)
//     {
//         Debug.Log("TelegramShare: " + message);
//     }
// }
