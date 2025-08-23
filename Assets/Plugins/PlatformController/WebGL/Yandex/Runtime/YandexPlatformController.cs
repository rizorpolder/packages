using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Authorizer.Core;
using UnityEngine;

namespace PlatformController.WebGL.Yandex
{
	public class YandexPlatformController : AAuthorizer
	{
		public override UniTask<string> Authorize()
		{
			AuthorizeInternal();
			return _promise.Task;
		}

		public override void ShowLoginWindow()
		{
			ShowLoginWindowInternal();
		}

		#region GameReady

		public static void GameReadySDKReady()
		{
			if (Application.isEditor)
			{
				Debug.Log("YandexSDKReady call");
			}
			else
			{
				YandexSDKReady();
			}
		}

		public static void GameReadySDKStart()
		{
			if (Application.isEditor)
			{
				Debug.Log("YandexSDKStart call");
			}
			else
			{
				YandexSDKStart();
			}
		}

		public static void GameReadySDKStop()
		{
			if (Application.isEditor)
			{
				Debug.Log(" YandexSDKStop call");
			}
			else
			{
				YandexSDKStop();
			}
		}

		#endregion

		#region JSLIB Callbacks

		public override void OnAuthorized(string signature)
		{
			Debug.Log("Auth: YandexAuthorizer OnAuthorized");
			_promise.TrySetResult(signature);
		}

		public override void OnSuccessLogin()
		{
			// var authorizer = Context.Network.Auth;
			// if (!authorizer.IsAuthorized)
			// {
			// 	authorizer.StartLoadUser().Forget();
			// 	Context.Runtime.WindowsController.HideAllWindows();
			// }
		}

		#endregion

		#region JSLIB Methods

		[DllImport("__Internal")]
		private static extern void AuthorizeInternal();

		[DllImport("__Internal")]
		private static extern void ShowLoginWindowInternal();

		[DllImport("__Internal")]
		private static extern void YandexSDKReady();

		[DllImport("__Internal")]
		private static extern void YandexSDKStart();

		[DllImport("__Internal")]
		private static extern void YandexSDKStop();

		#endregion
	}
}