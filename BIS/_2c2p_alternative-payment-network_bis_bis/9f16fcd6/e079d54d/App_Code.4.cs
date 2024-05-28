#pragma checksum "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\CreditLimitModel.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "70B6F7E9B2D3BC4118824D76B2C39875B1BA7683"

#line 1 "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\CreditLimitModel.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CreditLimitModel
/// </summary>
public class CreditLimitModel
{
    public string credittermstart { get; set; }
    public string creditterm { get; set; }
    public double avlBal { get; set; }
    public double ledBal { get; set; }
    public string creditlimit { get; set; }
    public string totalAgentAmount { get; set; }
}
public class CreditLimitResultModel
{
    public string rescode { get; set; }
    public string resdesc { get; set; }
    public bool result { get; set; }
}



#line default
#line hidden
