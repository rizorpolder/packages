// using System.Runtime.InteropServices;
//
// namespace PlayDeck.InApp
// {
// 	public class PlayDeckStoreService
// 	{
//
//
//
// 		private void RequestPaymentHandler(string paymentRequestJson)
// 		{
// 			var converted = JsonConvert.DeserializeObject<PaymentResponseData>(paymentRequestJson);
// 			_paymentResponseJson = converted;
// 			_paymentRequestCallback?.Invoke(converted);
// 		}
//
// 		private void GetPaymentInfoHandler(string getPaymentInfoJson)
// 		{
// 			var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
// 			var converted = JsonConvert.DeserializeObject<GetPaymentInfoResponseData>(getPaymentInfoJson, settings);
// 			_getPaymentInfoResponseJson = converted;
// 			_getPaymentInfoRequestCallback?.Invoke(converted);
// 		}
//
// 		private void InvoiceClosedHandler(string status)
// 		{
// 			_invoiceClosedCallback?.Invoke(status);
// 		}
//
//
// 		[DllImport("__Internal")]
// 		private static extern void PlayDeckBridge_PostMessage_RequestPayment(string data);
//
// 		[DllImport("__Internal")]
// 		private static extern void PlayDeckBridge_PostMessage_GetPaymentInfo(string data);
//
// 	}
// }