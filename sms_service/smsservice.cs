using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using sms_service.smsservice;//.smsservice;
using LS_BusinessLayer;
using System.Net;

namespace sms_service
{
    public partial class sms_service : ServiceBase
    {
        private System.Timers.Timer timer;
        public sms_service()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("SmsService"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "SmsService", "SmServiceLog");
            }
            eventLog1.Source = "SmsService";
            eventLog1.Log = "SmServicelog";
            
        }

        protected override void OnStart(string[] args)
        {
            //Debugger.Break(); 
            //eventLog1.WriteEntry("Started");
            eventLog1.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 1);
            this.timer = new System.Timers.Timer(100000);  // 100000 milliseconds = 100 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();
           // eventLog1.WriteEntry("In OnStart");
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            clssms obj_sms = new clssms();
            DataView dv_proxysettings = obj_sms.GetAll(4);
            smsservice.CorporateSMS abc = new smsservice.CorporateSMS();
            //   Icorpo abc = new Service();
            if (dv_proxysettings.Count > 0)
            {
                string _uri = dv_proxysettings[0]["ServerAddress"].ToString().Trim();
                string _port = dv_proxysettings[0]["Port"].ToString().Trim();
                string _proxyuser = dv_proxysettings[0]["UserName"].ToString().Trim();
                string _proxypass = dv_proxysettings[0]["Password"].ToString().Trim();
                string _Domain = dv_proxysettings[0]["Domain"].ToString().Trim();
                WebProxy myproxy = new WebProxy(_uri + ":" + _port, true);
                myproxy.Credentials = new NetworkCredential(_proxyuser, _proxypass, _Domain);

                abc.Proxy = myproxy;
            }
            dv_proxysettings.Dispose();
            abc.Timeout = 120000;
            string username = "";
            string pass = "";
            string From = "";
            string msg = "";
            string RecipientNo = "";
            string msgid = "";
            string result = "";
            string DeliveryDate = "";
            string LabID = "";
            string patientfullname = "";
            string paymentmade = "";
            string patientnumber = "";
            string patientpasswor = "";
            int count_success = 0;

            DataView dv_alerts = obj_sms.GetAll(2);
            eventLog1.WriteEntry("No. of alerts found:" + dv_alerts.Count.ToString());
            for (int i = 0; i < dv_alerts.Count; i++)
            {
                obj_sms.Api = dv_alerts[i]["Api_ID"].ToString().Trim();
                DataView dv_apisettings = obj_sms.GetAll(3);
                username = dv_apisettings[0]["APi_username"].ToString().Trim();
                pass = dv_apisettings[0]["APi_pass"].ToString().Trim();
                From = dv_alerts[i]["Header_From"].ToString().Trim();
                msg = dv_alerts[i]["msg_text"].ToString().Trim();
                obj_sms.Queryfromtable = dv_alerts[i]["confidential"].ToString().Trim();
                DataView dv_recipients = obj_sms.GetAll(1);

                dv_recipients.RowFilter = "sendsms='Y'";
                if (dv_recipients.Count > 0)
                {
                    eventLog1.WriteEntry("No. of Recipients found: " + dv_recipients.Count);
                    for (int j = 0; j < dv_recipients.Count; j++)
                    {
                        msg = dv_alerts[i]["msg_text"].ToString().Trim();
                        msgid = "ls_rdy_" + j.ToString() + "_" + System.DateTime.Now.ToString("dd");
                        //RecipientNo = "923235035141";
                        RecipientNo = dv_recipients[j]["CellPhone"].ToString().Trim();
                        if (RecipientNo.Substring(0, 1) == "0")
                        {
                            RecipientNo = "92" + RecipientNo.Substring(1);
                        }
                        else if (RecipientNo.Substring(0, 1) == "3")
                        {
                            RecipientNo = "92" + RecipientNo;
                        }
                        else if (RecipientNo.Substring(0, 1) == "+")
                        {
                            RecipientNo = RecipientNo.Substring(1);

                        }
                        try
                        {
                            DeliveryDate = dv_recipients[j]["DeliveryDate"].ToString().Trim();
                        }
                        catch { }
                        try
                        {
                            LabID = dv_recipients[j]["Labid"].ToString().Trim();
                        }
                        catch { }
                        try
                        {
                            patientfullname = dv_recipients[j]["PatientFullName"].ToString().Trim();
                        }
                        catch { }
                        try
                        {
                            paymentmade = dv_recipients[j]["paymentmade"].ToString().Trim();
                        }
                        catch { }
                        try
                        {
                            patientnumber = dv_recipients[j]["patientnumber"].ToString().Trim();
                        }
                        catch { }
                        try
                        {
                            patientpasswor = dv_recipients[j]["patientpassword"].ToString().Trim();
                        }
                        catch { }
                        try
                        {
                            msg = msg.Replace("@labid", LabID);
                            msg = msg.Replace("@DeliveryDate", DeliveryDate);
                            msg = msg.Replace("@PatientfullName", patientfullname);
                            msg = msg.Replace("@Paymentmade", paymentmade);
                            msg = msg.Replace("@patientnumber", patientnumber);
                            msg = msg.Replace("@patientpassword", patientpasswor);
                        }
                        catch { }
                        try
                        {
                            result = abc.SendBulkSMS(username, RecipientNo, From, msg, "0", pass);
                            count_success++;
                        }
                        catch (Exception ee)
                        {
                            obj_sms.Alertid = dv_alerts[i]["ID"].ToString().Trim();
                            obj_sms.Api = dv_alerts[i]["Api_ID"].ToString().Trim();
                            obj_sms.MserialNo = dv_recipients[j]["Mserialno"].ToString().Trim();
                            obj_sms.MsgID = msgid;
                            obj_sms.RECIPIENTCATEGORY = "P";
                            obj_sms.PRID = dv_recipients[j]["PRNo"].ToString().Trim();
                            obj_sms.TONumber = RecipientNo;
                            obj_sms.Sms_sent = "N";
                            obj_sms.SendStatus = "N";
                            obj_sms.FAILUREREASON = ee.ToString();
                            if (obj_sms.insertandupdate())
                            {
                                ///
                            }
                            else
                            {
                                eventLog1.WriteEntry("Some Exception occured while sending SMS Exception:" + ee.ToString() + " and status not updated. Reason:" + obj_sms.ErrorMessage);
                            }
                        }
                        //eventLog1.WriteEntry("Send Status:"+result+"Sent to person: "+dv_recipients[j]["Cellphone"].ToString().Trim());
                        #region updating database
                        obj_sms.Alertid = dv_alerts[i]["ID"].ToString().Trim();
                        obj_sms.Api = dv_alerts[i]["Api_ID"].ToString().Trim();
                        obj_sms.MserialNo = dv_recipients[j]["Mserialno"].ToString().Trim();
                        obj_sms.MsgID = msgid;
                        obj_sms.RECIPIENTCATEGORY = "P";
                        obj_sms.PRID = dv_recipients[j]["PRNo"].ToString().Trim();
                        obj_sms.TONumber = RecipientNo;
                        if (result.Contains("Submitted Successfully"))
                        {
                            obj_sms.SentOn = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                            obj_sms.Sms_sent = "Y";
                            obj_sms.SendStatus = "S";
                            obj_sms.MsgID = result.Split(new char[1] { ':' })[1].ToString().Trim();//save msgid returned by the operator
                        }
                        else
                        {
                            obj_sms.Sms_sent = "N";
                            obj_sms.SendStatus = "N";
                            obj_sms.FAILUREREASON = result;

                        }
                        if (obj_sms.insertandupdate())
                        {
                            ///
                        }
                        else
                        {

                            eventLog1.WriteEntry("SMS sent without any exception but status not updated. Reason:" + obj_sms.ErrorMessage);
                        }

                        #endregion
                    }
                }
                else
                {
                    eventLog1.WriteEntry("No Recipient found against this alert id: " + dv_alerts[i]["Name"].ToString().Trim());
                }

            }
            if (count_success > 0)
            {
                eventLog1.WriteEntry("Message Successfully Sent to :" + count_success.ToString() + " recipients.");
            }
            dv_alerts.Dispose();
            //Service abc = new Service();
        
            //string resul4 = abc.SendSMS("RMAdmin77", "ri@medI79", "923235035141", "1", "Lab Event Sms test. From Trees", "RMI");
            //eventLog1.WriteEntry(resul4+ " " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
           // MyServiceApp.ServiceWork.Main(); // my separate static method for do work
        }

        protected override void OnStop()
        {
            eventLog1.Clear();
            eventLog1.WriteEntry("In onStop.");
        }
    }
}
