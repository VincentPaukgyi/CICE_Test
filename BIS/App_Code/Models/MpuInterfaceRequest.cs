﻿using System.Collections.Generic;


public class MpuInterfaceRequest
{
	public string NearMePaymentRefNo { get; set; }
	public decimal Amount { get; set; }
	public Dictionary<string, string> PaymentInfo { get; set; }
	public Dictionary<string, string> PaymentProfileInfo { get; set; }
	
}

