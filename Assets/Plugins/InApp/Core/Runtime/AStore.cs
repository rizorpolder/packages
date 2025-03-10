using UnityEngine;

namespace InApp.Core.Runtime
{
	public abstract class AStore : MonoBehaviour, IStoreService
	{
		public bool IsPurchaseInProcess { get; }
		public abstract bool IsInitialized();

		public abstract void Purchase(string iapID);

		public abstract void Consume(string productId);

		public abstract decimal GetCost(string productId);

		public abstract void ConfirmPurchaseReceiving(string productId);

		public abstract void Cancel();
	}
}