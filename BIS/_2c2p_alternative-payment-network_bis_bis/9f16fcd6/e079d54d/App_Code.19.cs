#pragma checksum "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\663Model.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "948BB7FDBB24D36274427EF80154F7197052B955"

#line 1 "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\663Model.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _663Result
/// </summary>

public class Response663
{
    public bool Result { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseDescription { get; set; }
    public string AgentCode { get; set; }
    public string AgentName { get; set; }
    public string Amount { get; set; }
    public string RefID { get; set; }
    public string responseCts { get; set; }
    public string RequestCts { get; set; }
    public string ClientType { get; set; }
    public string TransType { get; set; }
    public string ExterResponse { get; set; }
}

public class Request663
{
    public string transactionType { get; set; }
    public string agentMobileNo { get; set; }
    public string receiverMobileNo { get; set; }
    public string txnAmount { get; set; }
    public string oTp { get; set; }
    public string txnID { get; set; }
}

public class InquiryRequest
{
    public string Token { get; set; }
    public string RegisteredMobileNo { get; set; }
    public string Channel { get; set; }

    public InquiryRequest(string token, string registeredMobileNo, string channel)
    {
        Token = token;
        RegisteredMobileNo = registeredMobileNo;
        Channel = channel;
    }
}

public class InquiryEbaResponse
{
    public string CustomerID { get; set; }
    public string CustomerName { get; set; }
    public string PlanType { get; set; }
    public List<Plans> Plans { get; set; }
    public int ResponseCode { get; set; }
    public string ResponseDescription { get; set; }
}

public class InquiryMptDataPackageResquest
{
    public string Token { get; set; }
    public string PartnerCode { get; set; }
    public string BillerCode { get; set; }
    public string Detail { get; set; }

    public InquiryMptDataPackageResquest(string token, string partnerCode, string billerCode, string detail)
    {
        Token = token;
        PartnerCode = partnerCode;
        BillerCode = billerCode;
        Detail = detail;
    }
}

public class InquiryMptDataPackageResponse
{
    public string TransactionStatus { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public double PartnerAmount { get; set; }
    public double TransactionAmount { get; set; }
    public string Detail { get; set; }
}

public class Data
{
    public string DataPack { get; set; }
}

public class Packagelists
{
    public List<packageList> PackageLists { get; set; }
}

public class packageList
{
    public string packageCode { get; set; }
    public string packageName { get; set; }
    public double price { get; set; }
}

public class Plans
{
    public string Devices { get; set; }
    public string Expired_on { get; set; }
    public List<Packs> Packs { get; set; }
}

public class Packs
{
    public string Price { get; set; }
    public string Description { get; set; }
}

public class ConfirmRequest
{
    public string Token { get; set; }
    public string Channel { get; set; }
    public string ChannelRefId { get; set; }
    public string CustomerId { get; set; }
    public string Amount { get; set; }
    public string Devices { get; set; }

    public ConfirmRequest(string token, string channel, string channelRefID, string customerID, string amount, string devices)
    {
        Token = token;
        Channel = channel;
        ChannelRefId = channelRefID;
        CustomerId = customerID;
        Amount = amount;
        Devices = devices;
    }
}

public class ConfirmResponse
{
    public string CustomerID { get; set; }
    public string Devices { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal serviceFee { get; set; }
    public int ResponseCode { get; set; }
    public string ResponseDescription { get; set; }
}

    

#line default
#line hidden
