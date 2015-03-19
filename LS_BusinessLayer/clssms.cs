using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;
using DataLayer;
using LS_BusinessLayer;
/// <summary>
/// Summary description for clssms
/// </summary>
public class clssms
{
    public clssms()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    #region Variables
    QueryBuilder objQB = new QueryBuilder();
    
    clsoperation objTrans = new clsoperation();
    clsdbhims objdbhims = new clsdbhims();
    private static string Default = "~!@";
    private string _MserialNo = Default;
    private string _api = Default;
    private string _sms_sent=Default;
    private string _alertauditid = Default;
    private string _alertid = Default;
    private string _SendStatus = Default;
    private string _TONumber = Default;
    //private string _APIUSED = Default;
    private string _SentOn = Default;
    private string _FAILURECODE = Default;
    private string _FAILUREREASON = Default;
    private string _RECIPIENTCATEGORY = Default;
    private string _PRID = Default;
    private string _PERSONID = Default;
    private string _MSGID = Default;
    private string _ErrorMessage = Default;
    private string _queryfromtable = Default;

    
    #endregion

    #region Properties
    public string Queryfromtable
    {
        get { return _queryfromtable; }
        set { _queryfromtable = value; }
    }
    //public string APIUSED
    //{
    //    get { return _APIUSED; }
    //    set { _APIUSED = value; }
    //}

    public string SentOn
    {
        get { return _SentOn; }
        set { _SentOn = value; }
    }

    public string FAILURECODE
    {
        get { return _FAILURECODE; }
        set { _FAILURECODE = value; }
    }

    public string FAILUREREASON
    {
        get { return _FAILUREREASON; }
        set { _FAILUREREASON = value; }
    }

    public string RECIPIENTCATEGORY
    {
        get { return _RECIPIENTCATEGORY; }
        set { _RECIPIENTCATEGORY = value; }
    }

    public string PRID
    {
        get { return _PRID; }
        set { _PRID = value; }
    }

    public string PERSONID
    {
        get { return _PERSONID; }
        set { _PERSONID = value; }
    }
    public string MsgID
    {
        get { return _MSGID; }
        set { _MSGID = value; }
    }
    
    public string TONumber
    {
        get { return _TONumber; }
        set { _TONumber = value; }
    }
    public string SendStatus
    {
        get { return _SendStatus; }
        set { _SendStatus = value; }
    }
    public string Alertid
    {
        get { return _alertid; }
        set { _alertid = value; }
    }
    public string ErrorMessage
    {
        get { return _ErrorMessage; }
        set { _ErrorMessage = value; }
    }

    public string Alertauditid
    {
        get { return _alertauditid; }
        set { _alertauditid = value; }
    }
    public string Sms_sent
{
  get { return _sms_sent; }
  set { _sms_sent = value; }
}
    public string Api
    {
        get { return _api; }
        set { _api = value; }
    }
    public string MserialNo
    {
        get { return _MserialNo; }
        set { _MserialNo = value; }
    }
    #endregion

    #region Methods
    public DataView GetAll(int flag)
    {
        switch (flag)
        {
            case 1:
                objdbhims.Query = _queryfromtable;
//                objdbhims.Query = @"Select d.mserialno,p.PatientFullName, m.labid, substr(p.prno,1,12) prno, length(p.prno),length(m.prno),p.CELLPHONE,
//                                    count(testid),
//                                    case
//                                        when (select count(testid)
//                                                from ls_tdtransaction d1
//                                            where d1.mserialno = d.mserialno
//                                                and to_number(d1.processid) in (6, 7)) = count(testid) then
//                                        'Y'
//                                        else
//                                        'N'
//                                    end as sendsms
//                                from ls_tdtransaction d
//                                inner join ls_tmtransaction m
//                                on m.mserialno = d.mserialno
//                                inner join whims2.pr_vpatientsearch p
//                                on substr(p.prno,0,12)=substr(m.prno,0,12)
//                                where d.mserialno >= 1287400
//                                and m.iop='O'
//                                and (p.CELLPHONE like('03%') or p.CELLPHONE like('923%') or p.CELLPHONE like('3%') or p.CELLPHONE like('+923%'))
//                                
//and m.mserialno not in (Select mserialno from msg_talertsaudit where alertid=1 and mserialno is not null)
//                                group by d.mserialno, p.prno,m.prno,p.CELLPHONE,m.labid,p.PatientFullName
//                                order by sendsms";//and (m.sms_sent is  null or m.sms_sent<>'Y')
                break;
            case 2:// Active Alerts
                objdbhims.Query = @"Select * from msg_talerttypes t where t.Active='Y' and t.Manual='N' and t.Password=concat(concat(concat(concat(concat(concat(substr(name,4,2),'x',
substr(description,20,4),'y',substr(confidential,10,2),'a',substr(name,1,2)))))))";//substr(t.name,4,2)||'x'||substr(t.description,20,4)||'y'||substr(confidential,10,2)||'a'||substr(t.name,1,2)";
                break;
            case 3:// API settings
                objdbhims.Query = @"Select * from msg_services where id=" + _api;
                break;
            case 4:// Proxy Settings
                objdbhims.Query = @"Select * From MSG_TPROXYSETTINGS where settingid=(Select min(settingid) from MSG_TPROXYSETTINGS where Active='Y')";
                break;
        }
        return objTrans.DataTrigger_Get_All(objdbhims);
    }

    public bool insertandupdate()
    {
        objTrans.Start_Transaction();
        //objdbhims.Query = objQB.QBGetMax("id", "msg_talertsaudit", "10");
        //this._alertauditid = objTrans.DataTrigger_Get_Max(objdbhims);
        //if (this._alertauditid.Equals("True"))
        //{
        //    this._ErrorMessage = objTrans.OperationError;
        //    objTrans.End_Transaction();
        //    return false;
        //}
        objdbhims.Query = objQB.QBInsert(MakeArray(), "msg_talertsaudit");
        _ErrorMessage = objTrans.DataTrigger_Insert(objdbhims);
        if (this._ErrorMessage.Equals("True"))
        {
            this._ErrorMessage = objTrans.OperationError;
            objTrans.End_Transaction();
            return false;
        }
        //objdbhims.Query = @"Update ls_tmtransaction set sms_sent='" + _sms_sent + "' where mserialno=" + _MserialNo;
        //_ErrorMessage = objTrans.DataTrigger_Update(objdbhims);
        //if (this._ErrorMessage.Equals("True"))
        //{
        //    this._ErrorMessage = objTrans.OperationError;
        //    objTrans.End_Transaction();
        //    return false;
        //}
        objTrans.End_Transaction();
        return true;
    }
    private string[,] MakeArray()
    {
        string[,] aryUpdate = new string[13, 3];
        if (!this._alertauditid.Equals(Default))
        {
            aryUpdate[0, 0] = "ID";
            aryUpdate[0, 1] = this._alertauditid;
            aryUpdate[0, 2] = "int";
        }

        if (!this._alertid.Equals(Default))
        {
            aryUpdate[1, 0] = "alertid";
            aryUpdate[1, 1] = this._alertid;
            aryUpdate[1, 2] = "int";
        }

        if (!this._SendStatus.Equals(Default))
        {
            aryUpdate[2, 0] = "SendStatus";
            aryUpdate[2, 1] = this._SendStatus;
            aryUpdate[2, 2] = "string";
        }

        if (!this._TONumber.Equals(Default))
        {
            aryUpdate[3, 0] = "TONumber";
            aryUpdate[3, 1] = this._TONumber;
            aryUpdate[3, 2] = "string";
        }

        if (!this._api.Equals(Default))
        {
            aryUpdate[4, 0] = "APIUSED";
            aryUpdate[4, 1] = this._api;
            aryUpdate[4, 2] = "int";
        }

        if (!_SentOn.Equals(Default))
        {
            aryUpdate[5, 0] = "SentOn";
            aryUpdate[5, 1] = _SentOn;
            aryUpdate[5, 2] = "datetime";
        }

        if (!this._FAILURECODE.Equals(Default))
        {
            aryUpdate[6, 0] = "FAILURECODE";
            aryUpdate[6, 1] = this._FAILURECODE;
            aryUpdate[6, 2] = "string";
        }

        if (!this._FAILUREREASON.Equals(Default))
        {
            aryUpdate[7, 0] = "FAILUREREASON";
            aryUpdate[7, 1] = this._FAILUREREASON;
            aryUpdate[7, 2] = "string";
        }

        if (!this._RECIPIENTCATEGORY.Equals(Default))
        {
            aryUpdate[8, 0] = "RECIPIENTCATEGORY";
            aryUpdate[8, 1] = this._RECIPIENTCATEGORY;
            aryUpdate[8, 2] = "string";
        }
        if (!this._PRID.Equals(Default))
        {
            aryUpdate[9, 0] = "PRID";
            aryUpdate[9, 1] = this._PRID;
            aryUpdate[9, 2] = "int";
        }

        if (!this._PERSONID.Equals(Default))
        {
            aryUpdate[10, 0] = "PERSONID";
            aryUpdate[10, 1] = this._PERSONID;
            aryUpdate[10, 2] = "string";
        }
        if (!this._MSGID.Equals(Default))
        {
            aryUpdate[11, 0] = "MSGID";
            aryUpdate[11, 1] = this._MSGID;
            aryUpdate[11, 2] = "string";
        }
        if (!this._MserialNo.Equals(Default))
        {
            aryUpdate[12, 0] = "MserialNo";
            aryUpdate[12, 1] = this._MserialNo;
            aryUpdate[12, 2] = "int";
        }
        
        return aryUpdate;
    }

    #endregion


}