#pragma checksum "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\Logger.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17EC4FCB7574301AEECCD071736E727CE6DF854E"

#line 1 "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\Logger.cs"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Logger
/// </summary>
public class Logger
{
	public Logger()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void writeLog(string msg, ref log4net.ILog log)
    {
        //try
        //{
        //    if (ConfigurationManager.AppSettings["isFileLog"].ToString().Equals("Y"))
        //        log.Info(msg);
        //}
        //catch (Exception ex)
        //{
        //    log.Info(ex.Message);
        //}

        if (!log4net.LogManager.GetRepository().Configured)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        if (ConfigurationManager.AppSettings["isFileLog"].ToString() == "Y")
        {
            log.Info(msg);
        }
    }
}

#line default
#line hidden
