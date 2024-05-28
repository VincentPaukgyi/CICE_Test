#pragma checksum "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\MPT_EloadModel.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BCA30BF331E3CCC5892F9FF34F53CB4D409E2406"

#line 1 "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\MPT_EloadModel.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MPT_EloadModel
/// </summary>

    //public MPT_EloadModel()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    public class MyanPay_ELoadRequest
    {
        public string CredentialUserName { get; set; }
        public string CredentialPassword { get; set; }
        public string OrderNumber { get; set; }
        public string MobileNumber { get; set; }
        public string TopUpAmount { get; set; }
        public string UserEmail { get; set; }
       
    }


    public class MyanPay_ELoadResponse
    {
        public string Status { get; set; }
        public string ResponseMessage { get; set; }
        public string UniqueResellerID { get; set; }
        public string InvoiceNumber { get; set; }
        public string OrderNumber { get; set; }
        public string DateTime { get; set; }
        public bool result { get; set; }
    }

    public class MPT_ELoadInquiryRequest
    {
        public string invoiceNumber { get; set; }
        public string OrderNumber { get; set; }
    }

    public class MPT_EloadInquiryResponse
    {
        public string DeliveryStatus { get; set; }
        public string DeliveryReport { get; set; }
        public string InvoiceNumber { get; set; }
        public string OrderNumber { get; set; }
    }


#line default
#line hidden
