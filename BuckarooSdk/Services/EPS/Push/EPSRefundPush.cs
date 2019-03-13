namespace BuckarooSdk.Services.EPS.Push
{
	/// <summary>
	/// 
	/// </summary>
	public class EPSRefundPush : ActionPush
	{
		public override ServiceEnum ServiceEnum => ServiceEnum.EPS;

		/// <summary>
		/// The name of the issuer (bank) of the consumer.
		/// </summary>
		public string ConsumerIssuer { get; set; }

		internal override void FillFromPush(DataTypes.Response.Service serviceResponse)
		{
			base.FillFromPush(serviceResponse);
		}
	}
}
