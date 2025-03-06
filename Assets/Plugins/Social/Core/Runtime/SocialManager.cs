using UnityEngine;

namespace Plugins.Social.Core.Runtime
{
	public abstract class SocialManager : MonoBehaviour
	{
		protected System.Action onShareCallback;

		public bool IsAvailable { get; }
		public abstract void Share(string message, string url,System.Action callback = null);

		public abstract void OpenSocialChannel(string channelName);

		public abstract void ReceiveLog(string message);
	}
}