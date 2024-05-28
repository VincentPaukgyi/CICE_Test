#pragma checksum "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\SMSHelper.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FBDE671E6E2748AB3FB3FA2F14BEFD312603BF7E"

#line 1 "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\SMSHelper.cs"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SMSHelper
/// </summary>
public class SMSHelper
{
	public SMSHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string ChannelCode= ConfigurationManager.AppSettings["ChannelCodeForSMS"].ToString();
    public string ChannelCodeat = ConfigurationManager.AppSettings["ChannelCodeForSMS"].ToString() + " at ";
    public string getMessageTopup(string agentName,string taxID,string biller, string pin,string serialNo,string expiry, string amount,string shopCode)
    {
        StringBuilder sb = new StringBuilder();
       

        if (taxID == "2222222222222" || taxID=="0000000000024")//MPT-CDMA 800
        {
            sb.Append("*166*" + pin + "#");
        }
        else if (taxID == "4444444444444") //MEC-CDMA 800
        {
            sb.Append("*124*" + pin + "#");
        }
        else
        {
            sb.Append("*123*" + pin + "#");
        }
        
        return sb.ToString();
       

    }

    public string getMessageBiller1Stop(string agentName, string taxID, string biller, string ref1Name, string ref2Name, string ref3Name, string ref4Name,
        string ref1Value,string ref2Value,string ref3Value,string ref4Value, string amount, string serviceFee, string totalAmount, string shopCode)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(ChannelCodeat + agentName);
        sb.Append(" *" + biller);
        //if (!String.IsNullOrEmpty(ref1Name))
        //    sb.Append(" *" + ref1Name + " : " + ref1Value);

        sb.Append(" *" + "Partner " + " : " + ref1Value);

        //if (!String.IsNullOrEmpty(ref2Name))
        //    sb.Append(" *" + ref2Name + " : " + ref2Value);
        sb.Append("(" + ref2Value+")");

        if (!String.IsNullOrEmpty(ref3Name))
            sb.Append(" *" + ref3Name + " : " + ref3Value);

        sb.Append(" *Amount : " + amount);
        sb.Append(" *Fee : " + serviceFee);
        sb.Append(" *Total : " + totalAmount);
        
        if (!String.IsNullOrEmpty(ref4Name))
            sb.Append(" *" + ref4Name + " : " + ref4Value);
        //sb.Append(" *Shop : " + shopCode);
        string smsMsg = "";
        
        if (sb.ToString().Length > 159)
        {
            smsMsg = sb.ToString().Substring(0,159);
        }
        else
        {
            smsMsg = sb.ToString();
        }
        return smsMsg;
    }
    public string getMessageBillerMercyCrops(string agentName, string taxID, string biller, string ref1Name, string ref2Name, string ref3Name, string ref4Name,
       string ref1Value, string ref2Value, string ref3Value, string ref4Value, string amount, string serviceFee, string totalAmount, string shopCode)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(ChannelCodeat + agentName);
        sb.Append(" *" + biller);
        //if (!String.IsNullOrEmpty(ref1Name))
        //    sb.Append(" *" + ref1Name + " : " + ref1Value);

        sb.Append(" *" + "Partner Code" + " : " + ref2Value);

        //if (!String.IsNullOrEmpty(ref2Name))
        //    sb.Append(" *" + ref2Name + " : " + ref2Value);
       // sb.Append("(" + ref2Value + ")");

        if (!String.IsNullOrEmpty(ref1Name))
            sb.Append(" *" + ref1Name + " : " + ref1Value);

        sb.Append(" *Amount : " + amount);
        sb.Append(" *Fee : " + serviceFee);
        sb.Append(" *Total : " + totalAmount);

        if (!String.IsNullOrEmpty(ref4Name))
            sb.Append(" *" + ref4Name + " : " + ref4Value);
        //sb.Append(" *Shop : " + shopCode);
        string smsMsg = "";

        if (sb.ToString().Length > 159)
        {
            smsMsg = sb.ToString().Substring(0, 159);
        }
        else
        {
            smsMsg = sb.ToString();
        }
        return smsMsg;
    }
    public string getMessageBiller(string agentName, string taxID, string biller, string ref1Name, string ref2Name, string ref3Name, string ref4Name,
        string ref1Value, string ref2Value, string ref3Value, string ref4Value, string amount, string serviceFee, string totalAmount, string shopCode)

    {
        StringBuilder sb = new StringBuilder();

        sb.Append(ChannelCodeat + agentName);
        sb.Append(" *" + biller);

        if (!String.IsNullOrEmpty(ref1Name)) sb.Append(" * " + ref1Name + " : " + ref1Value);

        if (!String.IsNullOrEmpty(ref2Name)) sb.Append(" *" + ref2Name + " : " + ref2Value);

        if (!String.IsNullOrEmpty(ref3Name)) sb.Append(" *" + ref3Name + " : " + ref3Value);

        if (taxID == "0000000000021")
        {
            if (!string.IsNullOrEmpty(shopCode))
            {
                string[] date = shopCode.Split(' ');
                sb.Append(" *PayForDate :" + date[0] + "To" + date[1]);
            }
        }

        sb.Append(" *Amount : " + amount);

        if (serviceFee !="0.00")
        {
            sb.Append(" *Fee : " + serviceFee);
        }
        
        sb.Append(" *Total : " + totalAmount);

        if (!String.IsNullOrEmpty(ref4Name)) sb.Append(" *" + ref4Name + " : " + ref4Value);
        //sb.Append(" *Shop : " + shopCode);
        string smsMsg = "";

        if (sb.ToString().Length > 259)
        {
            smsMsg = sb.ToString().Substring(0, 259);
        }
        else
        {
            smsMsg = sb.ToString();
        }

        return smsMsg;
    }

    public string getMessageBillerGGI(string agentName, string taxID, string biller, string ref1Name, string ref2Name, string ref3Name, string ref4Name,
       string ref1Value, string ref2Value, string ref3Value, string ref4Value, string amount, string serviceFee, string totalAmount, string shopCode)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(ChannelCodeat + agentName);
        sb.Append(" *" + biller);
        //if (!String.IsNullOrEmpty(ref1Name))
        //    sb.Append(" *" + ref1Name + " : " + ref1Value);

      
        //if (!String.IsNullOrEmpty(ref2Name))
        //    sb.Append(" *" + ref2Name + " : " + ref2Value);
        // sb.Append("(" + ref2Value + ")");

      
        if (taxID == "0000000000020" || taxID == "0000000000021" || taxID == "0000000000022" || taxID == "0000000000029")
        {
            if (!String.IsNullOrEmpty(ref1Name))
                sb.Append("*" + ref1Name + ": " + ref1Value);
            sb.Append("*" + "Name" + ": " + ref2Value);
            sb.Append("*" + "Receipt No" + ": " + ref3Value);
           

            sb.Append("*Amount: " + amount);
            sb.Append("*Fee: " + serviceFee);
            sb.Append("*Total: " + totalAmount);
            sb.Append(" *" + "Ref" + ": " + ref4Value);
        }
        else if (taxID == "0000000000023")
        {
            if (!String.IsNullOrEmpty(ref1Name))
                sb.Append(" *" + "Slip No" + " : " + ref1Value);
            sb.Append(" *" + "Customer ID" + " : " + ref2Value);
            sb.Append(" *" + "Receipt No" + " : " + ref3Value);
            sb.Append(" *" + "Ref" + " : " + ref4Value);

            sb.Append(" *Amount : " + amount);
            sb.Append(" *Fee : " + serviceFee);
            sb.Append(" *Total : " + totalAmount);
        }        
        else
        {
            if (!String.IsNullOrEmpty(ref1Name))
                sb.Append(" *" + ref1Name + " : " + ref1Value);
            sb.Append(" *" + "Ref" + " : " + ref2Value);
            sb.Append(" *Amount : " + amount);
            sb.Append(" *Fee : " + serviceFee);
            sb.Append(" *Total : " + totalAmount);


            sb.Append(" *" + "Expiry Date" + " : " + ref3Value);
        }

       
        //sb.Append(" *Shop : " + shopCode);
        string smsMsg = "";

        //if (sb.ToString().Length > 200)
        //{
        //    smsMsg = sb.ToString().Substring(0, 200);
        //}
        //else
        //{
            smsMsg = sb.ToString();
       // }
        return smsMsg;
    }

    public  string getsuccessregsmsmessagebody(string userid, string password)
    {
        StringBuilder sb = new StringBuilder();
        string smsMsg = string.Empty;
        // string Mpufee=ConfigurationManager.AppSettings["MPUFee"].ToString();
        sb.Append(ChannelCode+" registration is successfully done.");
        sb.Append(" *Login ID : " + userid);
        sb.Append(" *Password : " + password);

        if (sb.ToString().Length > 159)
        {
            smsMsg = sb.ToString().Substring(0, 159);
        }
        else
        {
            smsMsg = sb.ToString();
        }
        return smsMsg;
    }


    public string getMessagelegacyMusic(string agentName, string taxID, string biller, string ref1Name, string ref2Name, string ref3Name, string ref4Name,
    string ref1Value, string ref2Value, string ref3Value, string ref4Value, string amount, string serviceFee, string totalAmount, string shopCode)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(ChannelCodeat + agentName);
        sb.Append(" *" + biller);
        //if (!String.IsNullOrEmpty(ref1Name))
        //    sb.Append(" *" + ref1Name + " : " + ref1Value);

        sb.Append(" *" + ref1Name + " : " + ref1Value);

        //if (!String.IsNullOrEmpty(ref2Name))
        //    sb.Append(" *" + ref2Name + " : " + ref2Value);
        // sb.Append("(" + ref2Value + ")");



        sb.Append(" *Amount : " + amount);
        sb.Append(" *Fee : " + serviceFee);
        sb.Append(" *Total : " + totalAmount);
        if (!String.IsNullOrEmpty(ref4Value))
            sb.Append(" *" + ref4Name + " : " + ref4Value);

        string smsMsg = "";

        if (sb.ToString().Length > 159)
        {
            smsMsg = sb.ToString().Substring(0, 159);
        }
        else
        {
            smsMsg = sb.ToString();
        }
        return smsMsg;
    }

    //public string getMessageGiftCard(string agentName, string taxID, string biller, string ref1Name, string ref2Name, string ref3Name, string ref4Name,
    //string ref1Value, string ref2Value, string ref3Value, string ref4Value, string amount, string serviceFee, string totalAmount, string shopCode)
    //{
    //    StringBuilder sb = new StringBuilder();

    //    sb.Append("1-Stop at " + agentName);
    //    sb.Append(" *" + biller);
    //    if (!String.IsNullOrEmpty(ref1Name))
    //        sb.Append(" *" + ref1Name + " : " + ref1Value);

    //    if (!String.IsNullOrEmpty(ref2Name))
    //        sb.Append(" *" + ref2Name + " : " + ref2Value);

    //    if (!String.IsNullOrEmpty(ref3Name))
    //        sb.Append(" *" + ref3Name + " : " + ref3Value);
    //    sb.Append(" *Amount : " + amount);
    //    sb.Append(" *Fee : " + serviceFee);
    //    sb.Append(" *Total Amount : " + totalAmount);

    //    if (!String.IsNullOrEmpty(ref4Name))
    //        sb.Append(" *" + ref4Name + " : " + ref4Value);
    //    sb.Append(" *Shop : " + shopCode);

    //    return sb.ToString();
    //}

}

#line default
#line hidden
