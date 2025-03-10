using System;
using Plugins.Social.Core.Runtime;

namespace Plugins.PlayDeck.Runtime.Social
{
	public class PlayDeckSocial: ASocialManager
	{
		public override void Share(string message, string url, Action onSuccess = null, Action onFailed = null)
		{
		}

		public override void OpenSocialChannel(string channelName)
		{
		}

		public override void OnActionComplete()
		{
		}

		public override void OnActionFailed()
		{
		}

		public override void ReceiveLog(string message)
		{
		}
	}
}