namespace BuckarooSdk.Services.Afterpay.Push
{
	/// <summary>
	/// 
	/// </summary>
	public class AfterpayRefundPush : ActionPush
	{
		public override Constants.Services.ServiceNames ServiceNames => Constants.Services.ServiceNames.Afterpay;

		internal override void FillFromPush(DataTypes.Response.Service serviceResponse)
		{
			base.FillFromPush(serviceResponse);
		}
	}
}
