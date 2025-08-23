using Cysharp.Threading.Tasks;

namespace Authorizer.Core
{
	public abstract class AAuthorizer
	{
		protected readonly UniTaskCompletionSource<string> _promise = new UniTaskCompletionSource<string>();

		public abstract UniTask<string> Authorize();
		public abstract void OnAuthorized(string signature);
		public abstract void OnSuccessLogin();

		public abstract void ShowLoginWindow();
	}
}