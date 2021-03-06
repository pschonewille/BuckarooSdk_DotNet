﻿using BuckarooSdk.DataTypes.RequestBases;
using BuckarooSdk.Logging;
using BuckarooSdk.Services.Giftcards.VVVGiftcard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Globalization;
using BuckarooSdk.Tests.Constants;

namespace BuckarooSdk.Tests.Services.Giftcard
{
	[TestClass]
	public class VVVGiftcardTests
	{
		public SdkClient BuckarooClient { get; private set; }

		[TestInitialize]
		public void Setup()
		{
			this.BuckarooClient = new SdkClient(TestSettings.Logger);
		}

		[TestMethod]
		public void PayTest()
		{
			var request =
				this.BuckarooClient.CreateRequest(new StandardLogger()) // Create a request.
				.Authenticate(TestSettings.WebsiteKey, TestSettings.SecretKey, false, new CultureInfo("nl-NL"))
				.TransactionRequest() // One of the request type options.
				.SetBasicFields(new TransactionBase // The transactionbase contains the base information of a transaction.
				{
					Currency = "EUR",
					AmountDebit = 3.50m,
					Invoice = $"SDK_TEST_{DateTime.Now.Ticks}",
					Description = "VVV_PAY_SDK_UNITTEST",
					ReturnUrl = TestSettings.ReturnUrl,
					ReturnUrlCancel = TestSettings.ReturnUrlCancel,
					ReturnUrlError = TestSettings.ReturnUrlError,
					ReturnUrlReject = TestSettings.ReturnUrlReject,
				})
				.VVVGiftcard() // Choose the paymentmethod you want to use
				.Pay(new VVVGiftcardPayRequest // choose the action you want to use and provide the payment method specific info.
				{
                    IntersolveCardnumber = "0000000000000000001",
                    IntersolvePin = "350"
				});

			var response = request.Execute();

			if (response.RequiredAction != null)
			{
				//optie 1
				Process.Start(response.RequiredAction.RedirectURL);
            }

			Console.WriteLine(response.BuckarooSdkLogger.GetFullLog());
		}

		[TestMethod]
		public void RefundTest()
		{
			var request = this.BuckarooClient.CreateRequest()
				.Authenticate(TestSettings.WebsiteKey, TestSettings.SecretKey, false, new CultureInfo("nl-NL"))
				.TransactionRequest()
				.SetBasicFields(new TransactionBase
				{
					AmountCredit = 0.02m,
					Currency = "EUR",
					Invoice = "SDK_TEST_636196508460881929", //set before each refund test
					OriginalTransactionKey = "DDE3795437444D2AB1B803EA829C89E5", //set before each refund test
					Description = "VVVGiftcard_REFUND_SDK_UNITTEST",
				})
				.VVVGiftcard()
				.Refund(new VVVGiftcardRefundRequest());

			var response = request.Execute();

			var logging = response.BuckarooSdkLogger.GetFullLog();
			Console.WriteLine(logging);
		}
	}
}
