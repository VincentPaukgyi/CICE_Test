﻿using A2AAPIWCF;
using EbaReqResModel;
using Newtonsoft.Json;
using System;
using System.Configuration;

/// <summary>
/// Summary description for SmileCinemaManager
/// </summary>
public class SmileCinemaManager
{
	public SmileCinemaManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ConfrimToEBA(ConfirmResponseModel confirmResponseModel, string amount, int agentId, long txnID,
        double agentAmount, string isAgreement, double agentFeeDbl, string smsStatus, double availablebalance,
        string appType, string topupType, string agentName, string MapTaxID, string serviceFee, string totalAmount
        , string branchCode, string senderName)
    {
        Utils.WriteLog_Biller("This is Smile Cinema from EBA Confirm");
        string ebaUrl = ConfigurationManager.AppSettings["EbaConfirmUrl"].ToString();

        var token = TokenManager.GetOAuthToken();

        var confirmReq = new EbaConfirmReq()
        {
            Token = token.Token,
            PartnerCode = ConfigurationManager.AppSettings["EsbaChannel"].ToString(),
            PartnerRefNo = confirmResponseModel.txnID.ToString(),
            BillerCode = ConfigurationManager.AppSettings["SmileCinemaBillerCode"].ToString(),
            TransactionAmount = amount,
            Detail = "{'ProductCode':'" + confirmResponseModel.ref2 + "'}"
        };

        string jsonReq = JsonConvert.SerializeObject(confirmReq);
        Utils.WriteLog_Biller("EBA Smile Cinema Confirm Jason Request:" + jsonReq);

        SSLPost post = new SSLPost();
        string jsonres = Utils.PostEba(jsonReq, ebaUrl);
        Utils.WriteLog_Biller("EBA Smile Cinema Confirm Jason Response:" + jsonres);

        var responseDescription = string.Empty;
        var responseCode = string.Empty;
        if (string.IsNullOrEmpty(jsonres))
        {
            responseDescription = "No Response From EBA";
            responseCode = "06";

            return (new MobileAPIWCFManager()).GetErrorResponseWithAddBalance(responseCode, responseDescription, txnID, responseDescription, agentId, agentAmount, isAgreement);
        }


        var confirmRes = JsonConvert.DeserializeObject<EbaConfirmRes>(jsonres);
        var errMsg = string.Empty;
        var batchID = default(int);
        if (confirmRes.ErrorCode == "00")
        {
            var detail = JsonConvert.DeserializeObject<SmileCinemaEBAConfirmDetailResponse>(confirmRes.Detail);

            string aesKey = string.Empty;
            aesKey = ConfigurationManager.AppSettings["EsbaAesKey"].ToString();
            detail.ClearPin = Utils.AESDecryptText(detail.ClearPin, aesKey);

            confirmResponseModel.ref5 = confirmResponseModel.ref3;
            confirmResponseModel.ref2 = detail.ProductAmount;
            confirmResponseModel.ref3 = detail.SerialNumber;
            confirmResponseModel.ref4 = detail.ClearPin + " " + detail.ExpiryDate;
 
            if (!(new ServiceClient()).ConfirmUpdate(txnID, confirmResponseModel.ref1, confirmResponseModel.ref2, confirmResponseModel.ref3, confirmResponseModel.ref4,
                    confirmResponseModel.ref5, "", "PA", "Paid Successfully", agentId, confirmResponseModel.email, agentAmount, agentFeeDbl,
                    isAgreement, smsStatus, availablebalance, out errMsg, out batchID))
            {
                Utils.WriteLog_Biller("Error in ConfirmUpdate : " + errMsg);
                responseDescription = "Error in update database";
                responseCode = "06";
                if (!(new ServiceClient()).updateError(txnID, "ER", responseDescription, out errMsg))
                {
                    Utils.WriteLog_Biller("Error in updateError : " + errMsg);
                }
                return Utils.getErrorRes(responseCode, "Transaction fail");
            }
            else
            {
                Utils.WriteLog_Biller("After update = AgentAmount : " + agentAmount + " | Balance : " + availablebalance.ToString() + "| smsStatus:" + smsStatus);
            }

            Utils.WriteLog_Biller("After update = AgentAmount : " + agentAmount + " | Balance : " + availablebalance.ToString());

            #region <-- Send SMS -->
            string smsMsg = string.Empty;
            if (appType == "CS" || appType == "MS")
            {
                Utils.WriteLog_Biller("appType is CS or MS");
                if (string.IsNullOrEmpty(topupType) || topupType == "S") //topup type is null or S
                {
                    Utils.WriteLog_Biller("topupType is null or Not S");
                    SMSHelper smsH = new SMSHelper();
                    MessagingService.MessagingServiceClient smsWcf = new MessagingService.MessagingServiceClient();

                    smsMsg = smsH.getMessageBiller(agentName, MapTaxID, confirmResponseModel.billername, "Code", "Expiry", "", "Ref", detail.ClearPin, detail.ExpiryDate, "", txnID.ToString(), double.Parse(amount).ToString("#,##0.00"), serviceFee, double.Parse(totalAmount).ToString("#,##0.00"), branchCode);

                    try
                    {

                        Utils.WriteLog_Biller("Mobile No :" + confirmResponseModel.ref5 + "| Message :" + smsMsg + "| Sender Name :" + senderName + "|txn ID :" + txnID);
                        smsWcf.SendSms(txnID.ToString(), smsMsg, confirmResponseModel.ref5, senderName);
                        Utils.WriteLog_Biller("sendSMSWithTxnID ends.");
                    }
                    catch (Exception ex)
                    {
                        Utils.WriteLog_Biller(string.Format("Error in SendSms: {0}", ex.ToString()));
                    }
                }
            }

            #endregion

            #region <-- Response Back To Client -->
            confirmResponseModel.rescode = confirmRes.ErrorCode;
            confirmResponseModel.resdesc = confirmRes.ErrorMessage;
            confirmResponseModel.availablebalance = availablebalance.ToString();
            confirmResponseModel.txnID = txnID.ToString();
            
            confirmResponseModel.smsMsg = smsMsg;
            return Utils.getConfirmRes(confirmResponseModel);

            #endregion
        }
        else
        {
            responseDescription = Utils.EsbResponseDescription(confirmRes.ErrorCode);
            return (new MobileAPIWCFManager()).GetErrorResponseWithAddBalance(confirmRes.ErrorCode, confirmRes.ErrorMessage,
                txnID, responseDescription, agentId, agentAmount, isAgreement);
        }
    }

}