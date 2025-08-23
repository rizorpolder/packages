
using Cysharp.Threading.Tasks;

namespace Plugins.Authorizer.Core
{
	public interface IAuthorizer
	{
		public UniTask<string> Authorize();

		public void OnAuthorized(string signature);
		public void OnSuccessLogin();

	}
}