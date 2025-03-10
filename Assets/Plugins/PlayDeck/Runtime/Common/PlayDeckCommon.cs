using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Plugins.PlayDeck.Runtime.Common
{
	public class PlayDeckCommon
	{
		private const string GET_USER_PROFILE = "getUserProfile";

		private Action<UserData> _getUserCallback;
		private Action<string> _getDataCallback;
		private Action<bool> _getPlaydeckStateCallback;

		private UserData _userData;
		private string _dataJson;

		public void GetData(string key, Action<string> callback)
		{
#if UNITY_EDITOR
			Debug.Log($"[PlayDeckBridge]: Fake GetData[{key}]");
#else
			_getDataCallback = callback;
			Debug.Log($"[PlayDeckBridge]: GetData {key}");
			PlayDeckBridge_PostMessage_GetData(key);
#endif
		}

		public void SetData(string key, string data)
		{
#if UNITY_EDITOR
			Debug.Log($"[PlayDeckBridge]: Fake SetData[{key}]: {data}");
#else
			Debug.Log($"[PlayDeckBridge]: SetData [{key}]: {data}");

			PlayDeckBridge_PostMessage_SetData(key, data);
#endif
		}

		public void GetUserProfile(Action<UserData> callback)
		{
#if UNITY_EDITOR
			Debug.Log($"[PlayDeckBridge]: Fake GetUserProfile");
#else
			_getUserCallback = callback;
			PlayDeckBridge_PostMessage(GET_USER_PROFILE);
#endif
		}

		public void GetPlaydeckState(Action<bool> callback)
		{
#if UNITY_EDITOR
			Debug.Log($"[PlayDeckBridge]: Fake GetPlaydeckState");
			callback?.Invoke(true);
#else
			Debug.Log($"[PlayDeckBridge]: GetPlaydeckState");
			PlayDeckBridge_PostMessage_GetPlaydeckState();

			_getPlaydeckStateCallback = callback;
#endif
		}

		#region Responce

		private void GetDataHandler(string dataJson)
		{
			_dataJson = dataJson;
			_getDataCallback?.Invoke(_dataJson);
		}

		private void GetUserHandler(string userJson)
		{
			var converted = JsonConvert.DeserializeObject<UserData>(userJson);
			_userData = converted;
			_getUserCallback?.Invoke(_userData);
		}

		private void GetPlaydeckStateHandler(int state)
		{
			_getPlaydeckStateCallback?.Invoke(state != 0);
		}

		#endregion

		[Serializable]
		public class UserData
		{
			public string avatar;
			public string username;
			public string firstName;
			public string lastName;
			public long telegramId;
			public string locale;
			public string token;
			public string sessionId;
			public ulong currentGameStarted;

			public Dictionary<string, string> @params;
		}

		[DllImport("__Internal")]
		private static extern void PlayDeckBridge_PostMessage_GetPlaydeckState();

		[DllImport("__Internal")]
		private static extern void PlayDeckBridge_PostMessage(string method);

		[DllImport("__Internal")]
		private static extern void PlayDeckBridge_PostMessage_IntValue(string method, int value);

		[DllImport("__Internal")]
		private static extern void PlayDeckBridge_PostMessage_StringValue(string method, string value);

		[DllImport("__Internal")]
		private static extern void PlayDeckBridge_PostMessage_SetData(string key, string value);

		[DllImport("__Internal")]
		private static extern void PlayDeckBridge_PostMessage_GetData(string key);
	}
}