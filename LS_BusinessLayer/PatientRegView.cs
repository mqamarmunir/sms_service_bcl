using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using System.Data;

namespace LS_BusinessLayer
{
    public class PatientRegView
    {
        #region "DataLayer Objects"
        QueryBuilder objQB = new QueryBuilder();
        clsoperation objTrans = new clsoperation();
        clsdbhims objdbhims = new clsdbhims();
        
        #endregion
        #region "Class Variables"

        private const string Default = "~!@";
        private const string TableName = "LS_TTest";
        private string StrErrorMessage = "";
        private string _FromDate = Default;
        private string _ToDate = Default;
        private string _DepartmentId = Default;
        private string _SubdepartmentId = Default;
        private string _PRNO = Default;
        private string _VisitNo = Default;
        private string _SectionId = Default;
        private string _TestName = Default;
        private string _TestId = Default;
        private string _TestGroupId = Default;
        private string _TestBatchNo = Default;
        private string _SpeedKey = Default;
        private string _Charges = Default;
        private string _ReferredBy = Default;
        private string _OriginBy = Default;
        private string _ReportCollection = Default;
        private string _EnteredBy=Default;
        private string _EnteredOn=Default;
        private string _TotalAmount=Default;
        private string _PRID=Default;
        private string[,] _Test;
        private string _OriginPlaceBy = Default;
        private string _MSerialNo = Default;
        private string _Priority = Default;
        private string _DSerialNo = Default;
        private string _MethodId = Default;
        private string _MaxTime = Default;
        private string _TAT = Default;
        private string _Delivery_time = Default;
        private string _TransNo = Default;
        private string _LabId = Default;
        private string StrDeliveryDate = Default;
        private string StrPriority_td = Default;
        private string _sex = Default;

        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        #endregion
        #region Class Properties
        public string TransNo { get { return this._TransNo; } set { this._TransNo = value; } }
        public string Delivery_time { get { return this._Delivery_time; } set { this._Delivery_time = value; } }
        public string DeliveryDate { get { return this.StrDeliveryDate; } set { this.StrDeliveryDate = value; } }
        public string MaxTime
        { get { return this._MaxTime; } set { this._MaxTime = value; } }
        public string TAT { get { return this._TAT; } set { this._TAT = value; } }
        public string MethodId
        { get { return this._MethodId; } set { this._MethodId = value; } }
        public string DSerialNo
        { get { return this._DSerialNo; } set { this._DSerialNo = value; } }
        public string Priority
        { get { return this._Priority; } set { this._Priority = value; } }
        public string MSerialNo
        { get { return this._MSerialNo; } set { this._MSerialNo = value; } }
        public string OriginPlaceBy
        { get { return this._OriginPlaceBy; } set { this._OriginPlaceBy = value; } }
        public string[,] Test
        {
            set { this._Test = value; }
        }
        public string PRID
        {get{return this._PRID;}set{this._PRID=value;}}
        public string TotalAmount
        {get{return this._TotalAmount;}set{this._TotalAmount=value;}}
        public string EnteredBy
        {get{return this._EnteredBy;}set{this._EnteredBy=value;}}
        public string EnteredOn
        {get{return this._EnteredOn;}set{ this._EnteredOn=value;}}
        public string ReferredBy
        { get { return this._ReferredBy; } set { this._ReferredBy = value; } }
        public string OriginBy
        { get { return this._OriginBy; } set { this._OriginBy = value; } }
        public string ReportCollection
        { get { return this._ReportCollection; } set { this._ReportCollection = value; } }
        public string ErrorMessage
        { get { return this.StrErrorMessage; } set { this.StrErrorMessage = value; } }
        public string FromDate
        { get { return this._FromDate; } set { this._FromDate = value; } }
        public string ToDate
        { get { return this._ToDate; } set { this._ToDate = value; } }
        public string DepartmentId
        { get { return this._DepartmentId; } set { this._DepartmentId = value; } }
        public string PRNO
        { get { return this._PRNO; } set { this._PRNO = value; } }
        public string VisitNo
        { get { return this._VisitNo; } set { this._VisitNo = value; } }
        public string SectionId
        { get { return this._SectionId; } set { this._SectionId = value; } }
        public string TestName
        { get { return this._TestName; } set { this._TestName = value; } }
        public string TestId
        { get { return this._TestId; } set { this._TestId = value; } }
        public string TestGroupId
        { get { return this._TestGroupId; } set { this._TestGroupId = value; } }
        public string TestBatchNo
        { get { return this._TestBatchNo; } set { this._TestBatchNo = value; } }
        public string SpeedKey
        { get { return this._SpeedKey; } set { this._SpeedKey = value; } }
        public string Charges
        { get { return this._Charges; } set { this._Charges = value; } }
        public string SubDepartmentId
        { get { return this._SubdepartmentId; } set { this._SubdepartmentId = value; } }
        public string LabId { set { this._LabId = value; } get { return this._LabId; } }

        public string Priority_td
        { get { return this.StrPriority_td; } set { this.StrPriority_td = value; } }

        #endregion
        public DataView GetAll(int Flag)
        {
            switch (Flag)
            { 
                case 1:
                    objdbhims.Query = @"select distinct vd.prno,
                                        vd.visitno,
                                        pr.salutation || ' ' || pr.fname || ' ' || pr.mname || ' ' ||
                                        pr.lname as name,
                                        whims2.fgetage(pr.dob) as age,
                                        case
                                          when pr.sex = 'M' then
                                           'Male'
                                          else
                                           'Female'
                                        end Gender,
                                        case
                                          when pr.maritalstatus = 'M' then
                                           'Married'
                                          else
                                           'Single'
                                        end marital_status,
                                        vd.enteredat as date_time,
                                        vd.charges as Total_Charges,
                                        pr.prid
                          from whims2.pr_tpatientvisitd vd,
                               whims2.pr_tpatientreg    pr,
                               whims2.pr_tpatientvisitm vm
                         where vd.prno = pr.prno
                           and vm.visitno = vd.visitno
                           and trim(vm.prno) = trim(pr.prno)
                           and vd.departmentid = '011'
                           and vm.mstatus != 'P'
                           and vd.paidno is null
                           and trim(vd.visitno) not in
                               (select trim(t.visitno)
                                  from ls_tmtransaction t
                                 where to_date(to_char(t.entrydatetime, 'dd/MM/yyyy'), 'dd/MM/yyyy') between
                                       (to_date('" + this._FromDate+@"', 'dd/MM/yyyy')) and
                                       (to_date('"+this._ToDate+@"', 'dd/MM/yyyy')))
                           and to_date(to_char(vd.enteredat, 'dd/MM/yyyy'), 'dd/MM/yyyy') between
                               (to_date('" + this._FromDate + @"', 'dd/MM/yyyy')) and
                               (to_date('"+this._ToDate+"', 'dd/MM/yyyy'))";
                    break;
                case 2:
                    objdbhims.Query = @"select distinct vd.prno, vd.visitno, pr.salutation || ' ' || pr.fname || ' ' || pr.mname || ' ' || pr.lname as name, whims2.fgetage(pr.dob) as age,                  
                            case when pr.sex = 'M' then 'Male' else 'Female' end Gender,case when pr.maritalstatus = 'M' then
                            'Married' else'Single'
                            end marital_status,
                             vd.enteredat as date_time,                          
                             vd.charges as Total_Charges,
                             pr.prid
                             from whims2.pr_tpatientvisitd vd, whims2.pr_tpatientreg pr, whims2.pr_tpatientvisitm vm
                             where vd.prno = pr.prno
                             and vm.visitno = vd.visitno
                             and vm.prno = pr.prno";
                             objdbhims.Query+=" and vd.prno='"+_PRNO +"'";
                             objdbhims.Query+=" and vd.visitno='"+_VisitNo+"'";
                             objdbhims.Query+=@" and vd.departmentid = '011'
                             and vm.mstatus != 'P'
                             and vd.paidno is null";
                    
//                    objdbhims.Query = @"select distinct vd.prno,
//                                        vd.visitno,
//                                        pr.salutation || ' ' || pr.fname || ' ' || pr.mname || ' ' ||
//                                        pr.lname as name,
//                                        whims2.fgetage(pr.dob) as age,
//                                        case
//                                          when pr.sex = 'M' then
//                                           'Male'
//                                          else
//                                           'Female'
//                                        end Gender,
//                                        case
//                                          when pr.maritalstatus = 'M' then
//                                           'Married'
//                                          else
//                                           'Single'
//                                        end marital_status,
//                                        vd.enteredat as date_time,
//                                        vd.charges as Total_Charges,
//                                        pr.prid
//                          from whims2.pr_tpatientvisitd vd, whims2.pr_tpatientreg pr, whims2.pr_tpatientvisitm vm
//                         where vd.prno = pr.prno
//                           and vm.visitno = vd.visitno
//                           and vm.prno = pr.prno
//                           and vd.departmentid = '011'
//                           and vm.mstatus != 'P'
//                           and vd.paidno is null";
                    // and vd.prno='"+_PRNO+@"'
                        //   and vm.visitno='"+_VisitNo+@"'
                    break;
                case 3:
                    objdbhims.Query = @"select t.subdepartmentid, t.name
                              from whims2.hr_tsubdepartment t, whims2.hr_tdepartment d
                             where d.departmentid = t.departmentid
                               and d.departmentid = '011'";
                    break;
                case 4:
                    objdbhims.Query = @"select t.sectionid, t.sectionname as name
                                  from ls_tsection t
                                 where t.active = 'Y'
                                 order by dorder asc";
                    break;
                case 5:
                    objdbhims.Query = @"select t.sectionid, t.testgroupid as groupid, t.testgroup as groupname
                                  from ls_ttestgroup t
                                 where t.active = 'Y'
                                   and t.sectionid = '007  ' order by dorder";
                    break;
                    
                case 6:
                    objdbhims.Query = @"select t.test,";
                    if (this._Priority.Contains("U"))
                    {
                        objdbhims.Query+=" t.chargesurgent as amount";
                    }
                    else
                    {
                    objdbhims.Query+=" t.charges as amount";
                    }
                    objdbhims.Query+=@" from ls_ttest t
                                 where  t.sectionid = '"+_SectionId+@"'
                                 or t.testgroupid='"+_TestGroupId+@"'
                                 and t.active = 'Y'
                                    order by t.dorder";
                    break;
                case 7:
                    objdbhims.Query = @"select t.testid,
                                       t.sectionid,
                                       t.testgroupid,
                                       t.test,";
                    if (this._Priority.Contains("U"))
                    {
                        objdbhims.Query += " t.chargesurgent as amount ";
                    }
                    else
                    {
                        objdbhims.Query += " t.charges as amount ";
                    }
                                       //t.charges,
                                       //t.chargesurgent
                          objdbhims.Query += @"from ls_ttest t
                                 where t.testbatchno = '"+_TestBatchNo+@"'
                                   and t.active = 'Y'";
                    break;
                case 8:
                    objdbhims.Query = @"select o.originid, o.origin
                                  from ls_torigin o
                                 where o.active = 'Y'
                                 order by o.origin asc";

                    break;
                case 9:
                    objdbhims.Query = @"select op.originplaceid, op.originplace
                                  from ls_toriginplace op
                                 where active = 'Y'";
                    break;
                case 10:
                    objdbhims.Query = @"Select tt.testid,
                                       tt.test as testname,
                                       tt.testgroupid,
                                       tt.sectionid,tt.D_METHODID,tt.DELIVERYDATEONSPECIMEN,tt.RoundDelivery,tt.batchtime,";
                    if (this._Priority.Contains("U"))
                    {
                        objdbhims.Query += " tt.chargesurgent as amount, ";
                    }
                    else
                    {
                        objdbhims.Query += " tt.charges as amount, ";
                    }
                                 
                     objdbhims.Query+=@"tt.testbatchno
                                  from ls_ttest tt
                                where lower(tt.test) like ('%" + _TestName.ToLower() + "%') and tt.active='Y' and tt.testid in (Select ar.testid from ls_tattributerange ar,ls_ttestattribute ta where ar.testid=tt.testid and ta.attributeid=ar.attributeid  and ta.Active='Y' and (ar.sex='" + _sex + "'  or ar.sex='" + _sex.Trim().Substring(0, 1) + "' or ar.Sex='All')) order by tt.TestBatchNo";
                    break;
                case 11:
                    objdbhims.Query=@"Select tt.testid,
                                       tt.test,
                                       tt.testgroupid,
                                       tt.sectionid,tt.D_METHODID,tt.batchtime,";
                    if (this._Priority.Contains("U"))
                    {
                        objdbhims.Query += " tt.chargesurgent as amount ";
                    }
                    else
                    {
                        objdbhims.Query += " tt.charges as amount ";
                    }
                    objdbhims.Query += @" from ls_ttest tt
                                where tt.speedtest is not null and Active='Y' and tt.testid in (Select testid from ls_tattributerange ar,ls_ttestattribute ta where ar.testid=tt.testid and ta.attributeid=ar.attributeid  and ta.Active='Y' and (ar.sex='" + _sex + "'  or ar.sex='" + _sex.Trim().Substring(0, 1) + "' or ar.Sex='All'))";
                    break;
                case 12:
                    objdbhims.Query = @"select t.testgroupid, t.testgroup as groupname
                                  from ls_ttestgroup t, ls_tsection ts
                                 where t.sectionid = ts.sectionid
                                   and t.sectionid = '" + _SubdepartmentId + "' and t.active='Y' order by t.dorder";
                    
                    break;
                case 13:
                    objdbhims.Query = @"select t.testid,t.testgroupid,t.sectionid,t.test as testname,t.testbatchno as testbatchno,t.D_METHODID,t.DELIVERYDATEONSPECIMEN,t.RoundDelivery,t.batchtime, ";
                    if (this._Priority.Contains("U"))
                    {
                        objdbhims.Query += " t.chargesurgent as amount ";
                    }
                    else
                    {
                        objdbhims.Query += " t.charges as amount ";
                    }
                    objdbhims.Query += @" from ls_ttest t
                                 where  t.sectionid = '" + _SubdepartmentId + @" '
                               
                                 and t.active = 'Y' and t.testid in (Select ar.testid from ls_tattributerange ar,ls_ttestattribute ta where ar.testid=t.testid and ta.attributeid=ar.attributeid  and ta.Active='Y' and  (ar.sex='" + _sex + "'  or ar.sex='" + _sex.Trim().Substring(0, 1) + "' or ar.Sex='All'))";
                    if(!_TestGroupId.Equals(Default) && !_TestGroupId.Equals(""))
                    {
                        objdbhims.Query+=" and t.TestGroupid='"+_TestGroupId+"'";
                    }
                    objdbhims.Query+=@" order by t.testbatchno, t.dorder";
                    break;
                   // or t.testgroupid='" + _TestGroupId + @"'
                    //group by t.testid,
                    //              t.testgroupid,
                    //              t.sectionid,
                    //              t.test,
                    //              t.testbatchno,
                    //               t.charges,
                    //               t.dorder
                case 14:
                    objdbhims.Query = @"select m.maxtime, m.tat
                                      from ls_tmethod m
                                     where m.methodid in (select ar.methodid
                                                           from ls_tattributerange ar
                                                          where ar.testid = '"+this._TestId+@"'
                                                            and ar.sectionid = '"+this._SectionId+"')";
                    break;
                case 15:
                    objdbhims.Query = @"select t.prno,
                                       t.salutation,
                                       t.fname,
                                       t.mname,
                                       t.lname,
                                       t.sex,
                                       t.dob,
                                       whims2.fgetage(t.dob) as age,
                                       t.maritalstatus,
                                       t.prid,
                                       t.salutation || '' || t.fname || '' || t.mname || '' || t.lname as name
                                  from whims2.pr_tpatientreg t
                                 where t.prno = '"+this._PRNO+"'";
                    break;
                case 16:
                    objdbhims.Query = @"select max(dt.testno)+1 as testno
                                  from ls_tdtransaction dt
                                 where dt.testid = '" + this._TestId + @"'
                                   and dt.sectionid = '" + this._SectionId + "'";
                    break;
                case 17:
                    objdbhims.Query = @"select distinct t.sectionid,
                                        t.testgroupid,
                                        tg.testgroup as groupname,
                                        t.testid,
                                        t.test as testname,
                                        t.testbatchno as testbatchno,t.d_methodid,t.DELIVERYDATEONSPECIMEN,t.RoundDelivery,";
                    if (this._Priority.Contains("U"))
                    {
                        objdbhims.Query += " t.chargesurgent as amount ";
                    }
                    else
                    {
                        objdbhims.Query += " t.charges as amount ";
                    }
                    objdbhims.Query += @"from ls_ttest t, ls_ttestgroup tg
                         where t.sectionid = '"+this._SubdepartmentId+ @"'
                           and t.testgroupid = tg.testgroupid
                           and t.active = 'Y'
                           group by 
                                       t.sectionid,
                                        t.testgroupid,
                                        tg.testgroup,
                                        t.testid,
                                        t.test,
                                        t.testbatchno,
                                        t.charges,
                                        t.chargesurgent,t.d_methodid,t.DELIVERYDATEONSPECIMEN,t.RoundDelivery 
                           order by t.testbatchNo,t.test,t.testgroupid ";
                    break;
                case 18:
                    objdbhims.Query = @" select distinct vd.prno,
                                        vd.visitno,
                                        pr.salutation || ' ' || pr.fname || ' ' || pr.mname || ' ' ||
                                        pr.lname as name,
                                        whims2.fgetage(pr.dob) as age,
                                        case
                                          when pr.sex = 'M' then
                                           'Male'
                                          else
                                           'Female'
                                        end Gender,
                                        case
                                          when pr.maritalstatus = 'M' then
                                           'Married'
                                          else
                                           'Single'
                                        end marital_status,
                                        vd.enteredat as date_time,
                                        vd.charges as Total_Charges,
                                        pr.prid
                          from whims2.pr_tpatientvisitd vd, whims2.pr_tpatientreg pr, whims2.pr_tpatientvisitm vm
                         where vd.prno = pr.prno
                           and vm.visitno = vd.visitno
                           and vm.prno = pr.prno
                           and vd.departmentid = '011'
                           and vm.mstatus != 'P'
                           and vd.EnteredAt between to_date('" +_FromDate+@"','dd/MM/yyyy hh:mi:ss am') and to_date('"+_ToDate+@"','dd/mm/yyyy hh:mi:ss am')
                           and vd.paidno is null";
//                    objdbhims.Query = @"select distinct vd.prno,
//                                        vd.visitno,
//                                        pr.salutation || ' ' || pr.fname || ' ' || pr.mname || ' ' ||
//                                        pr.lname as name,
//                                        whims2.fgetage(pr.dob) as age,
//                                        case
//                                          when pr.sex = 'M' then
//                                           'Male'
//                                          else
//                                           'Female'
//                                        end Gender,
//                                        case
//                                          when pr.maritalstatus = 'M' then
//                                           'Married'
//                                          else
//                                           'Single'
//                                        end marital_status,
//                                        vd.enteredat as date_time,
//                                        vd.charges as Total_Charges,
//                                        pr.prid
//                          from whims2.pr_tpatientvisitd vd, whims2.pr_tpatientreg pr, whims2.pr_tpatientvisitm vm
//                         where vd.prno = pr.prno
//                           and vm.visitno = vd.visitno
//                           and vm.prno = pr.prno
//                           and vd.departmentid = '011'
//                           and vm.mstatus != 'P'
//
//                           and vd.paidno is null";
                    break;

                case 19:
                    objdbhims.Query = @"Select ttgd.TestID,ttgd.Charges as Amount,tt.Test TestName,tt.D_METHODID,ttg.TestGroupid,ts.sectionid,tt.DELIVERYDATEONSPECIMEN,tt.RoundDelivery,tt.TestbatchNo
                                        From LS_TTestGroupD ttgd Inner Join Ls_TTestGroup ttg
                                        On ttg.testgroupid=ttgd.Testgroupid
                                        Inner Join Ls_TSection ts 
                                        On ts.SectionID= ttg.sectionid
                                        Inner Join Ls_ttest tt 
                                        ON tt.testid= ttgd.testid
                                        where ttgd.testgroupid='" +_TestGroupId+@"'
                                        and ts.sectionid='" + _SubdepartmentId + "' and ttgd.Active='Y' order by tt.TestBatchNo";
                    break;
                case 20:
                    objdbhims.Query = @"Select SectionID,TestGroupID From LS_TTest where TestID='" + _TestId + "'";
                    break;
                case 21:
                    objdbhims.Query = @"Select PersonID,FNAme || ' ' ||  MName || ' '|| salutation as PersonName
                                      From whims2.Hr_TPersonnel
                                     where Active = 'Y' and DepartmentID='011' and designationid in (SELECT designationid
                                                              FROM whims2.hr_tdesignation ht2
                                                             WHERE lower(ht2.NAME) LIKE ('%consultant%')
                                                               AND ht2.active = 'Y')";
                    break;
                case 22:// Complete Consultants list
                    objdbhims.Query = @"SELECT NVL(ht.fname, '') || ' ' || NVL(MName, '') || ' ' || NVL(Lname, '') || '.' ||
                                       NVL(ht.salutation, '') AS CompleteName,
                                       ht.personid
                                  FROM whims2.hr_tpersonnel ht
                                 WHERE ht.designationid IN (SELECT designationid
                                                              FROM whims2.hr_tdesignation ht2
                                                             WHERE lower(ht2.NAME) LIKE ('%consultant%')
                                                               AND ht2.active = 'Y')
                                   AND ht.active = 'Y'";
                    break;

            }
            return objTrans.DataTrigger_Get_All(objdbhims);
        }
        public bool insert()
        {
            objTrans.Start_Transaction();
            objdbhims.Query = objQB.QBGetMax("mserialno", "ls_tmtransaction", "10");
            this._MSerialNo = objTrans.DataTrigger_Get_Max(objdbhims);
            if (this._MSerialNo.Equals("True"))
            {
                this.StrErrorMessage = objTrans.OperationError;
                objTrans.End_Transaction();
                return false;
            }

            objdbhims.Query = "select fgetnewlabid as MaxID from dual";
            //objdbhims.Query = "select To_Number(Max(substr(LabID,8,7)))+1 as MaxID from ls_tmtransaction";
            //string labid = objTrans.DataTrigger_Get_Max(objdbhims);
            this._LabId = objTrans.DataTrigger_Get_Max(objdbhims);
            if (this._LabId.Equals("True"))
            {
                this.StrErrorMessage = objTrans.OperationError;
                objTrans.End_Transaction();
                return false;
            }

            objdbhims.Query = @"insert into ls_tmtransaction
                                  (mserialno,
                                   type,
                                   priority,
                                   iop,
                                   entrydatetime,
                                   entryperson,
                                   deliverytype,
                                   paymentmode,
                                   totalamount,
                                   mstatus,
                                   referredby,
                                   originid,
                                   prno,
                                   labid,
                                   originplaceid)
                                values
                                ('" + this._MSerialNo + "','C','" + this._Priority + "','O',to_date('" + this._EnteredOn + "','dd/MM/yyyy hh:mi:ss AM'),'" + this.EnteredBy + "','" + this._ReportCollection + "','C','" + this._TotalAmount + "','A','" + this._ReferredBy + "','" + this._OriginBy + "','" + this._PRNO + "','" + this._LabId + "','" + this._OriginPlaceBy + "')";
            this.StrErrorMessage = objTrans.DataTrigger_Insert(objdbhims);

            if (this.StrErrorMessage.Equals("True"))
            {
                this.StrErrorMessage = objTrans.OperationError;
                objTrans.End_Transaction();
                return false;
            }

            objdbhims.Query = @"select t.prno,
                                       t.salutation,
                                       t.fname,
                                       t.mname,
                                       t.lname,
                                       t.sex,
                                       to_char(t.dob,'dd/mm/yyyy hh:mi:ss am') as dob,
                                       whims2.fgetage(t.dob) as age,
                                       t.maritalstatus,
                                       t.prid,
                                       t.salutation || '' || t.fname || '' || t.mname || '' || t.lname as name
                                  from whims2.pr_tpatientreg t
                                 where t.prno = '" + this._PRNO + "'";

            DataView dvpr = objTrans.Transaction_Get_Single(objdbhims);
            if (dvpr.Count == 0)
            {
                this.StrErrorMessage = "Sorry no Record Found Against this PRNumber";
                objTrans.End_Transaction();
                return false;

            }
            double age = Convert.ToDouble(dvpr.Table.Rows[0]["age"].ToString().Substring(0, 2));
            string ageu = "";
            if (dvpr.Table.Rows[0]["age"].ToString().Contains("Years"))
            {
                ageu = "Y";

            }
            else if (dvpr.Table.Rows[0]["age"].ToString().Contains("Months"))
            {
                ageu = "M";
            }
            else if (dvpr.Table.Rows[0]["age"].ToString().Contains("Weeks"))
            {
                ageu = "W";
            }
            else if (dvpr.Table.Rows[0]["age"].ToString().Contains("Days"))
            {
                ageu = "D";
            }
            //objTrans.Start_Transaction();
            //invalid identifier prid therefore excluded/////
            objdbhims.Query = @"insert into ls_tpatient
                                  (mserialno,
                                   pfname,
                                   pmname,
                                   plname,
                                   psex,
                                   paged,
                                   pageu,
                                   pdob,
                                   kinship,
                                   kfname,
                                   kmname,
                                   klname,
                                   refdoctor,
                                   ptitle)
                                values
                                  ('" + this._MSerialNo + "', '" + dvpr[0]["fname"].ToString().Trim() + "', '" + dvpr[0]["mname"].ToString().Trim() + "', '" + dvpr[0]["lname"].ToString().Trim() + "', '" + dvpr[0]["sex"].ToString().Trim() + "', '" + age + "','" + ageu + "', to_date('" + dvpr[0]["dob"].ToString().Trim() + "','dd/MM/yyyy hh:mi:ss AM'), 'Self', '" + dvpr[0]["fname"].ToString().Trim() + "', '" + dvpr[0]["mname"].ToString().Trim() + "', '" + dvpr[0]["lname"].ToString().Trim() + "', '" + this._ReferredBy + "', '" + dvpr[0]["Salutation"].ToString().Trim() + "')";
            this.StrErrorMessage = objTrans.DataTrigger_Insert(objdbhims);

            if (StrErrorMessage.Equals("True"))
            {
                StrErrorMessage = objTrans.OperationError;
                objTrans.End_Transaction();
                return false;
            }


            //objTrans.End_Transaction();
            for (int i = 0; i <= this._Test.GetUpperBound(0); i++)
            {
                this._TestId = _Test[i, 0].ToString().Trim();
                this._Charges = _Test[i, 2].ToString().Trim();
                this._EnteredOn = _Test[i, 3].ToString().Trim();
                this.StrDeliveryDate = _Test[i, 4].ToString().Trim();
                this.StrPriority_td = _Test[i, 5].ToString().Trim();
                this._SectionId = _Test[i, 6].ToString().Trim();
                this._TestGroupId = _Test[i, 7].ToString().Trim();
                this._MethodId = _Test[i, 8].ToString().Trim();
                // clsoperation objTrans_tdGetmax = new clsoperation();
                objdbhims.Query = objQB.QBGetMax("dserialno", "ls_tdtransaction", "10");
                // objTrans.Start_Transaction();
                this._DSerialNo = objTrans.DataTrigger_Get_Max(objdbhims);
                //objTrans.End_Transaction();

                if (this._DSerialNo.Equals("True"))
                {
                    this.StrErrorMessage = objTrans.OperationError;
                    //objTrans_tdGetmax = null;
                    objTrans.End_Transaction();
                    return false;
                }


                //objTrans_tdGetmax = null;
                #region depricated
                ///////////////////////////////////////////////////Getting Method max time for setting Deliverydate(depricated)//////////////////////////////////////////////////////
                //                                objdbhims.Query = @"select m.maxtime, m.tat
                //                                      from ls_tmethod m
                //                                     where m.methodid in (select ar.methodid
                //                                                           from ls_tattributerange ar
                //                                                          where ar.testid = '" + this._TestId + @"'
                //                                                            and ar.sectionid = '" + this._SectionId + "')";

                //                                DataView dv = objTrans.Transaction_Get_All(objdbhims);
                //                                if (dv.Count > 0)
                //                                {
                //                                    double dtMtd = Convert.ToDouble(dv[0]["maxtime"].ToString());
                //                                    DateTime dtNow = Convert.ToDateTime(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
                //                                    DateTime ts;
                //                                    if (dv[0]["tat"].ToString().Equals("W"))
                //                                    {
                //                                        int days = Convert.ToInt32(dv[0]["maxtime"].ToString().Trim());
                //                                        days = days * 7;
                //                                        dtMtd = Convert.ToDouble(days);
                //                                        ts = dtNow.AddDays(dtMtd);
                //                                    }
                //                                    else if (dv[0]["tat"].ToString().Equals("D"))
                //                                    {
                //                                        ts = dtNow.AddDays(dtMtd);
                //                                    }
                //                                    else if (dv[0]["tat"].ToString().Equals("H"))
                //                                    {
                //                                        ts = dtNow.AddHours(dtMtd);
                //                                    }
                //                                    else
                //                                    {
                //                                        ts = dtNow.AddMinutes(dtMtd);
                //                                    }

                //                                    _Delivery_time = ts.ToString("dd/MM/yyyy hh:mm:ss tt");
                //                                }
                ///////////////////////////////////////////////----------------------/////////////////////////////////////////////////////////////////
                #endregion
                // clsoperation objTrans_maxtestno = new clsoperation();
                objdbhims.Query = @"select NVL(max(dt.testno),0)+1 as testno
                                  from ls_tdtransaction dt
                                 where dt.testid = '" + this._TestId + @"'
                                   and dt.sectionid = '" + this._SectionId + "'";
                // objTrans_maxtestno.Start_Transaction();
                DataView dv = objTrans.DataTrigger_Get_All(objdbhims);
                //objTrans.End_Transaction();
                if (dv.Count == 0)
                {
                    this.StrErrorMessage = objTrans.OperationError;
                    objTrans.End_Transaction();
                    // objTrans_maxtestno = null;
                    return false;

                }

                //if (!_TestGroupId.Equals(Default) && !_TestGroupId.Equals(""))
                //{
                //objTrans.Start_Transaction();
                //clsoperation objTrans_inserttd = new clsoperation();
                if (StrDeliveryDate != "" && StrDeliveryDate.Trim() != "On Specimen Collection.")
                {
                    objdbhims.Query = @"insert into ls_tdtransaction
                                      (mserialno,
                                       dserialno,
                                       testid,
                                       testno,
                                       sectionid,
                                       charges,
                                       deliverydate,
                                       procedureid,
                                       processid,
                                       enteredby,
                                       enteredate,
                                       rserialno,
                                       print,
                                       specimencollected,
                                       testgroupid,
                                       PRIORITY,methodid)
                                    values
                                    ('" + this._MSerialNo + "','" + this._DSerialNo + "','" + this._TestId + "','" + dv[0]["testno"].ToString().Trim() + "','" + this._SectionId + "','" + this._Charges + "',to_date('" + this.StrDeliveryDate + "','dd/MM/yyyy hh:mi:ss AM'),'001','0008','" + this._EnteredBy + "',to_date('" + this._EnteredOn + "','dd/MM/yyyy hh:mi:ss AM'),'0','N','N','" + this._TestGroupId + "','" + this.StrPriority_td + "','" + this._MethodId + "')";
                    // objTrans.Start_Transaction();
                    this.StrErrorMessage = objTrans.DataTrigger_Insert(objdbhims);
                }
                else
                {

                    objdbhims.Query = @"insert into ls_tdtransaction
                                      (mserialno,
                                       dserialno,
                                       testid,
                                       testno,
                                       sectionid,
                                       charges,
                                       
                                       procedureid,
                                       processid,
                                       enteredby,
                                       enteredate,
                                       rserialno,
                                       print,
                                       specimencollected,
                                       testgroupid,
                                       PRIORITY,methodid)
                                    values
                                    ('" + this._MSerialNo + "','" + this._DSerialNo + "','" + this._TestId + "','" + dv[0]["testno"].ToString().Trim() + "','" + this._SectionId + "','" + this._Charges + "','001','0008','" + this._EnteredBy + "',to_date('" + this._EnteredOn + "','dd/MM/yyyy hh:mi:ss AM'),'0','N','N','" + this._TestGroupId + "','" + this.StrPriority_td + "','" + this._MethodId + "')";
                    // objTrans.Start_Transaction();
                    this.StrErrorMessage = objTrans.DataTrigger_Insert(objdbhims);
                }
                //  objTrans_inserttd.End_Transaction();
                //  }
                #region Depricated
                // else
                //                                    {
                //                                        if (_SectionId.Equals(Default) || _SectionId.Equals(""))
                //                                        {
                //                                            clsoperation objTrans_sectionandgroup = new clsoperation();
                //                                            objTrans_sectionandgroup.Start_Transaction();
                //                                            string groupid = "";
                //                                            string sectionid = "";
                //                                            objdbhims.Query = "Select SectionID,TestGroupID From LS_TTest where TestID='" + _TestId + "'";
                //                                            DataView dv_sectionandgroup = objTrans_sectionandgroup.DataTrigger_Get_All(objdbhims);
                //                                            objTrans_sectionandgroup.End_Transaction();
                //                                            if (dv_sectionandgroup.Count > 0)
                //                                            {
                //                                                sectionid = dv_sectionandgroup[0]["SectionID"].ToString();
                //                                                groupid = dv_sectionandgroup[0]["TESTGROUPID"].ToString();
                //                                                dv_sectionandgroup.Dispose();
                //                                            }
                //                                            else
                //                                            {
                //                                                this.StrErrorMessage = "No SectionID or TestGroupID found against this Test ";
                //                                                return false;
                //                                            }
                //                                            clsoperation objTrans_last = new clsoperation();
                //                                            objdbhims.Query = @"insert into ls_tdtransaction
                //                                                              (mserialno,
                //                                                               dserialno,
                //                                                               testid,
                //                                                               testno,
                //                                                               sectionid,
                //                                                               charges,
                //                                                               deliverydate,
                //                                                               procedureid,
                //                                                               processid,
                //                                                               enteredby,
                //                                                               enteredate,
                //                                                               rserialno,
                //                                                               print,
                //                                                               specimencollected,
                //                                                               testgroupid,
                //                                                               PRIORITY)
                //                                                            values
                //                                                            ('" + this._MSerialNo + "','" + this._DSerialNo + "','" + this._TestId + "','" + dv[0]["testno"].ToString().Trim() + "','" + sectionid + "','" + this._Charges + "',to_date('" + this.StrDeliveryDate + "','dd/MM/yyyy hh:mi:ss AM'),'001','0008','" + this._EnteredBy + "',to_date('" + this._EnteredOn + "','dd/MM/yyyy hh:mi:ss AM'),'0','N','N','" + groupid + "','" + this.StrPriority_td + "')";
                //                                            objTrans_last.Start_Transaction();
                //                                            this.StrErrorMessage = objTrans_last.DataTrigger_Insert(objdbhims);
                //                                            objTrans_last.End_Transaction();


                //                                        }
                //                                        else
                //                                        {
                //                                            clsoperation objTrans_new = new clsoperation();
                //                                            objTrans_new.Start_Transaction();
                //                                            string groupid = "";
                //                                            objdbhims.Query = "Select TESTGROUPID From LS_TTEst where TestID='" + _TestId + "'";
                //                                            DataView dv_groupid = objTrans_new.DataTrigger_Get_All(objdbhims);
                //                                            objTrans_new.End_Transaction();
                //                                            if (dv_groupid.Count > 0)
                //                                            {
                //                                                groupid = dv_groupid[0]["TESTGROUPID"].ToString();
                //                                                dv_groupid.Dispose();
                //                                            }
                //                                            else
                //                                            {
                //                                                this.StrErrorMessage = "No TestGroupID found against this Test ";
                //                                                return false;
                //                                            }
                //                                            objdbhims.Query = @"insert into ls_tdtransaction
                //                                                                  (mserialno,
                //                                                                   dserialno,
                //                                                                   testid,
                //                                                                   testno,
                //                                                                   sectionid,
                //                                                                   charges,
                //                                                                   deliverydate,
                //                                                                   procedureid,
                //                                                                   processid,
                //                                                                   enteredby,
                //                                                                   enteredate,
                //                                                                   rserialno,
                //                                                                   print,
                //                                                                   specimencollected,
                //                                                                   testgroupid,
                //                                                                   PRIORITY)
                //                                                                values
                //                                                                ('" + this._MSerialNo + "','" + this._DSerialNo + "','" + this._TestId + "','" + dv[0]["testno"].ToString().Trim() + "','" + this._SectionId + "','" + this._Charges + "',to_date('" + this.StrDeliveryDate + "','dd/MM/yyyy hh:mi:ss AM'),'001','0008','" + this._EnteredBy + "',to_date('" + this._EnteredOn + "','dd/MM/yyyy hh:mi:ss AM'),'0','N','N','" + groupid + "','" + this.StrPriority_td + "')";
                //                                            //objTrans.Start_Transaction();
                //                                            clsoperation objTrans_insertt = new clsoperation();
                //                                            objTrans_insertt.Start_Transaction();
                //                                            this.StrErrorMessage = objTrans_insertt.DataTrigger_Insert(objdbhims);
                //                                            objTrans_insertt.End_Transaction();
                //                                            //objTrans.End_Transaction();
                //                                        }
                //                                    }
                #endregion

                if (this.StrErrorMessage.Equals("True"))
                {
                    StrErrorMessage = objTrans.OperationError;
                    objTrans.End_Transaction();
                    return false;
                }
                //clsoperation objtrans_transhistory = new clsoperation();

                objdbhims.Query = objQB.QBGetMax("transno", "ls_ttranshistory", "8");
                //objtrans_transhistory.Start_Transaction();
                this._TransNo = objTrans.DataTrigger_Get_Max(objdbhims);
                // objtrans_transhistory.End_Transaction();

                if (this._TransNo.Equals("True"))
                {
                    StrErrorMessage = objTrans.OperationError;
                    objTrans.End_Transaction();
                    // objtrans = null;
                    return false;
                }

                // clsoperation objTrans_historyinsert=new clsoperation();
                objdbhims.Query = @"insert into ls_ttranshistory
                                              (transno, personid, processid, entdatetime, mserialno, dserialno)
                                            values
                                              ('" + this._TransNo + "', '" + this._EnteredBy + "', '0002', to_date('" + this._EnteredOn + "','dd/MM/yyyy hh:mi:ss AM'), '" + this._MSerialNo + "', '" + this._DSerialNo + "')";
                // objTrans_historyinsert.Start_Transaction();
                this.StrErrorMessage = objTrans.DataTrigger_Insert(objdbhims);
                // objTrans_historyinsert.End_Transaction();
                if (this.StrErrorMessage.Equals("True"))
                {
                    this.StrErrorMessage = objTrans.OperationError;
                    objTrans.End_Transaction();
                    // o//bjTrans_historyinsert = null;
                    return false;
                }






            }
            objTrans.End_Transaction();
            return true;




        }

        public bool UpdateDeliverydateforinpatients()
        {
            objdbhims.Query = "Update ls_tdtransaction set DeliveryDate=to_date('" + StrDeliveryDate + "','dd/mm/yyyy hh:mi:ss am'),methodID='"+_MethodId+"' where DSerialNo=" + _DSerialNo;
            objTrans.Start_Transaction();
            this.StrErrorMessage= objTrans.DataTrigger_Update(objdbhims);
            if (StrErrorMessage.Equals("True"))
            {
                this.StrErrorMessage = objTrans.OperationError;
                objTrans.End_Transaction();
                return false;

            }
            else
            {
               
                    objTrans.End_Transaction();
                    return true;
                
            }
            
        }

        public bool Updatemethodid_outp()
        {
            objdbhims.Query = "Update ls_tdtransaction set methodID='" + _MethodId + "' where DSerialNo=" + _DSerialNo;
            objTrans.Start_Transaction();
            this.StrErrorMessage = objTrans.DataTrigger_Update(objdbhims);
            if (StrErrorMessage.Equals("True"))
            {
                this.StrErrorMessage = objTrans.OperationError;
                objTrans.End_Transaction();
                return false;

            }
            else
            {
                //objdbhims.Query = @"Update ls_ttestresultm set methodID='" + _MethodId + "' where DSerialNo=" + _DSerialNo;
                //this.StrErrorMessage = objTrans.DataTrigger_Update(objdbhims);
                //if (StrErrorMessage.Equals("True"))
                //{
                //    this.StrErrorMessage = objTrans.OperationError;
                //    objTrans.End_Transaction();
                //    return false;

                //}
                //else
                //{
                    objTrans.End_Transaction();
                    return true;
                //}
            }

        }
    }
}
