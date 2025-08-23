using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Authorizer.Core;

namespace PlatformController.WebGL.OK
{
	public class OKPlatformController : AAuthorizer
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

		#region JSLIB Callbacks

		public override void OnAuthorized(string signature)
		{
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

		#endregion
	}
}