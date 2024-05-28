using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Xml;

namespace WebApp
{
    public partial class PutetAPITest : System.Web.UI.Page
    {
        string rescode = string.Empty;
        string resdecs = string.Empty;
        string version = string.Empty;
        string messageid = string.Empty;
        string ref1 = string.Empty;
        string ref2 = string.Empty;
        string ref3 = string.Empty;
        string hashvalue=string.Empty;
        string amount = string.Empty;
        string serialno = string.Empty;
        string ts = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAmount.Text = "000000148500";
                txtPINType.Text = "3M";
            }
            
            txtURL.Text = ConfigurationManager.AppSettings["URl"].ToString();
            
        }

        protected void butPINRequest_Click(object sender, EventArgs e)
        {

            StringBuilder hb = new StringBuilder();
            //Guid id = new Guid();
            string messageid = Guid.NewGuid().ToString();
            string ts=  System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string hashkeystr =  GethashKey("1.0", ts, messageid, txtPINType.Text, txtAmount.Text);


            hb.Append("<PINReq>");
            hb.Append("<Version>1.0</Version>");
            hb.Append("<TimeStamp>" + ts + "</TimeStamp>");
            hb.Append("<MessageID>" + messageid + "</MessageID>");
            hb.Append("<PINType>"+txtPINType.Text+"</PINType>");
            hb.Append("<Amount>" + txtAmount.Text + "</Amount>");
            hb.Append("<HashValue>"+ hashkeystr +"</HashValue>");
            hb.Append("</PINReq>");


            txtRequest.Text = System.Xml.Linq.XDocument.Parse(hb.ToString()).ToString();
        }
       
        protected void butSubmit_Click(object sender, EventArgs e)
        {
            string responsestr =  postXMLData(txtURL.Text, txtRequest.Text.ToString().Trim());

           // txtResponse.Text 
          //   string response   =  System.Xml.Linq.XDocument.Parse(responsestr).ToString();
             Hashtable htt = getHTableFromXML(responsestr);

             if (!IsValidateConfirmReqPutet(htt, out rescode, out resdecs, out version,out messageid, out ref1, out ref2, out ref3,out hashvalue ,out amount,out ts))
             {                                
                 StringBuilder sb = new StringBuilder();
                 sb.Append("<Error>");
                 sb.Append("<Version>1.0</Version>");
                 sb.Append("<TimeStamp>" + System.DateTime.Now.ToString("yyyyMMddhhmmssffff") + "</TimeStamp>");
                 sb.Append("<ResCode>" + rescode + "</ResCode>");
                 sb.Append("<ResDesc>" + resdecs + "</ResDesc>");
                 sb.Append("</Error>");

                 txtResponse.Text = sb.ToString();// System.Xml.Linq.XDocument.Parse(sb.ToString()).ToString(); 
             }
             else
             {
                 StringBuilder sbd = new StringBuilder();
                 sbd.Append("<PINReq>");
                 sbd.Append("<Version>1.0</Version>");
                 sbd.Append("<TimeStamp>" + System.DateTime.Now.ToString("yyyyMMddhhmmssffff") + "</TimeStamp>");
                 sbd.Append("<MessageID>" + messageid + "</MessageID>");
                
                 sbd.Append("<ResDesc>" + resdecs + "</ResDesc>");
                 sbd.Append("<PINType>" + ref2 + "</PINType>");
                 sbd.Append("<Amount>"+amount+"</txtAmount>");
                 
                 sbd.Append("<PIN>" + ref3 + "</PIN>");
                 sbd.Append("<Expiry>" + "" + "</Expiry>");
                 sbd.Append("<SerialNo>" + ref1 + "</SerialNo>");
                 sbd.Append("<ResCode>" + rescode + "</ResCode>");
                 sbd.Append("<Hashvalue>" + hashvalue+ "</Hashvalue>");
                 sbd.Append("</PINReq>");


                 txtResponse.Text = sbd.ToString();
                 
             }
        }



        #region tz
        public static Hashtable getHTableFromXML(string requestXML)
        {
            Hashtable ht = new Hashtable();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(requestXML);
            XmlNodeList xnl = xdoc.ChildNodes;
            if (xnl.Count > 0)
            {

                XmlNode rootNode;
                if (xnl.Count > 1)
                    rootNode = xnl.Item(1);
                else

                    rootNode = xnl.Item(0);

                XmlNodeList subNodes = rootNode.ChildNodes;
                if (subNodes.Count >= 1)
                {
                    foreach (XmlNode xn in subNodes)
                    {
                        ht.Add(xn.Name, xn.InnerText);
                    }
                }
                else
                {
                    ht = null;
                }
            }
            else
            {
                ht = null;
            }

            return ht;
        }
        public bool IsValidateConfirmReqPutet(Hashtable ht, out string rescode, out string resdesc, out string version, out string msgid, out string ref1, out string ref2, out string ref3, out string reshashvalue, out string amt, out string ts)
        {                                               
            //
            rescode = string.Empty;
            version = string.Empty;
             ts = string.Empty;
            resdesc = string.Empty;
            ref1 = string.Empty;
            ref2 = string.Empty;
            ref3 = string.Empty;
            msgid = string.Empty;
            amt = string.Empty;
            // string serialno=string.Empty;

           
            string expiry = string.Empty;
            //string replacetime=string.Empty;
             reshashvalue = string.Empty;
            string tmphashvalue = string.Empty;
            if (ht.ContainsKey("Version"))
            {
                version = ht["Version"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
               
                return false;
            }
            if (ht.ContainsKey("TimeStamp"))
            {
                ts = ht["TimeStamp"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
               
                return false;
            }


            if (ht.ContainsKey("MessageID"))
            {
                msgid = ht["MessageID"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
              
                return false;
            }

            if (ht.ContainsKey("ResCode"))
            {
                rescode = ht["ResCode"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
                
                return false;
            }

            if (ht.ContainsKey("ResDesc"))
            {
                resdesc = ht["ResDesc"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
              
                return false;
            }
            if (ht.ContainsKey("PINType"))
            {
                ref2 = ht["PINType"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
               
                return false;
            }

            if (ht.ContainsKey("PIN"))
            {
                ref3 = ht["PIN"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
               
                return false;
            }
            if (ht.ContainsKey("Amount"))
            {
                amt = ht["Amount"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
               
                return false;
            }
            if (ht.ContainsKey("Expiry"))
            {
                expiry = ht["Expiry"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
            }
            if (ht.ContainsKey("SerialNo"))
            {
                ref1 = ht["SerialNo"].ToString();
            }
            else
            {
                rescode = "06";
                resdesc = "Invalid Request";
            }
            if (ht.ContainsKey("HashValue"))
            {
                reshashvalue = ht["HashValue"].ToString();
            }
            // writeLog("Confirm Response OneCash HashValue :" + reshashvalue);
            //  Version + TimeStamp + MessageID + ResCode + ResDesc + PINType + Amount + PIN + Expiry + SerialNo

            tmphashvalue = GethashKeyres(version, ts, msgid, rescode, resdesc, ref2, amt, ref3, expiry, ref1);

            // writeLog("Confirm Response A2A HashValue :" + tmphashvalue);,
            //  replacetime = Regex.Replace(ts, "[^0-9a-zA-Z]+", "");
            //if (reshashvalue != tmphashvalue)
            //{
            //    rescode = "05";
            //    resdesc = "Hash Value Missmatch";
               
            //    return false;
            //}


            //Check Null or Empty String
            if (String.IsNullOrEmpty(version) || String.IsNullOrEmpty(ts) || String.IsNullOrEmpty(rescode))
            {
                rescode = "07";
                resdesc = "Invalid Message";
                txtResponse.Text="Error in Validation : Data is Null or Empty";
                return false;
            }

            return true;


        }
                                            
        public static string GethashKeyres(string version, string ts, string messageid,string rescode, string resdec, string ref2, string amt, string ref3, string expiry, string ref1)
        { 
            ArrayList ar = new ArrayList();
            ar.Add(version);
            ar.Add(ts);
            ar.Add(messageid);
            ar.Add(rescode);
            ar.Add(resdec);
            ar.Add(ref2);
            ar.Add(amt);
            ar.Add(ref3);
            ar.Add(expiry);
            ar.Add(ref1);


            StringBuilder sb = new StringBuilder();
            foreach (string item in ar)
            {
                sb.Append(item);
            }


            string key = ConfigurationManager.AppSettings["secret_key"].ToString();

            string hashstr = generateHashValue(sb.ToString(), key);
            return hashstr;
        }
        #endregion
        public string postXMLData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;

            //string encodingString = HttpUtility.UrlEncode(requestXml);
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "application/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        #region hash
         private string GethashKey(string version,string ts, string messageid,string PINType,string amount)
         {
             ArrayList ar = new ArrayList();
             ar.Add(version);
             ar.Add(ts);
             ar.Add(messageid);
             ar.Add(PINType);
             ar.Add(amount);


             StringBuilder sb = new StringBuilder();
             foreach (string item in ar)
             {
                 sb.Append(item);
             }


             string key = ConfigurationManager.AppSettings["secret_key"].ToString();

             string hashstr = generateHashValue(sb.ToString(), key);
             return hashstr;
         }
         public static string generateHashValue(string signatureString, string secretKey)
         {
             return getHMAC(signatureString, secretKey);
         }

         private static string getHMAC(string signatureString, string secretKey)
         {

             System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

             byte[] keyByte = encoding.GetBytes(secretKey);

             HMACSHA1 hmac = new HMACSHA1(keyByte);


             byte[] messageBytes = encoding.GetBytes(signatureString);

             byte[] hashmessage = hmac.ComputeHash(messageBytes);

             return ByteArrayToHexString(hashmessage);

         }

         private static string ByteArrayToHexString(byte[] Bytes)
         {
             StringBuilder Result = new StringBuilder();
             string HexAlphabet = "0123456789abcdef";
             //string HexAlphabet = "0123456789ABCDEF";

             foreach (byte B in Bytes)
             {
                 Result.Append(HexAlphabet[(int)(B >> 4)]);
                 Result.Append(HexAlphabet[(int)(B & 0xF)]);
             }

             return Result.ToString();
         }
        #endregion
    }
}