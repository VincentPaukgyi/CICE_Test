#pragma checksum "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\IService.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0768D43914F6E5A5F1FD8E12D98D74D9FDE351C3"

#line 1 "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\IService.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

	[OperationContract]
	string GetData(int value);

	[OperationContract]
	CompositeType GetDataUsingDataContract(CompositeType composite);

    [OperationContract]
    string inquiry2Biller(string reqXml);

    [OperationContract]
    string getCanalPlusPackages(string reqXml);

    [OperationContract]
    string ConfirmToBiller(string reqXml);
    [OperationContract]
    string SystemSettingReq(string reqXml);

    [OperationContract]
    string CountryListReq(string reqXml);

    [OperationContract]
    string RegisterReq(string reqXml);

    [OperationContract]
    string RepaymentServiceFeeReq(string reqXml);
    [OperationContract]
    bool doSMS(long txnID, string mobileNo);

    [OperationContract]
    bool SendUnpairEmail(string userID);

    [OperationContract]
    string OTPReq(string reqXml);

    [OperationContract]
    string RegisterUser(string reqXml);

    [OperationContract]
    string UpdateProfileReq(string reqXml);

    [OperationContract]
    bool InsertTransactionForPaymentAPI(string taxID, string agentID, string email, string ref1, string ref2, string amount, string version, string locLatitude, string locLongitude, string productdesc, string appType, string messageid, string paymentMethod, string agentCode, string agentBranchCode, out string agentTxnID);

    [OperationContract]
    bool UpdateTransactionForPaymentAPI(long txnID, string email, string ref1, string ref2, string isAgreement, double availablebalance);

    [OperationContract]
    bool ConfirmRequestToPG(long txnID);

    [OperationContract]
    string UpdateErrorWithAddBalance(string rescode, string resdesc, long txnID, string logerrormessage, int agentID, double amount, string isAgreement);

    [OperationContract]
    bool InsertTransactionInvoice(string reqXml, long txnID, string paymentMethod);

    [OperationContract]
    string TelenorBBInquiry(string reqXML);

    [OperationContract]
    string FtthOrWtthInquiry(string reqXML);

    [OperationContract]
    string MptPackageInquiry(string reqXML);
}

// Use a data contract as illustrated in the sample below to add composite types to service operations.
[DataContract]
public class CompositeType
{
	bool boolValue = true;
	string stringValue = "Hello ";

	[DataMember]
	public bool BoolValue
	{
		get { return boolValue; }
		set { boolValue = value; }
	}

	[DataMember]
	public string StringValue
	{
		get { return stringValue; }
		set { stringValue = value; }
	}
}


#line default
#line hidden
