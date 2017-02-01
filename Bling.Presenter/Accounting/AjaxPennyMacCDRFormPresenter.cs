using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using System.IO;

namespace Bling.Presenter.Accounting
{
    public class AjaxPennyMacCDRFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private IPennyMacCDRDao m_Dao;
        private string m_Path;

        public AjaxPennyMacCDRFormPresenter(IAjaxView view)
            : this (view, new PennyMacCDRDao(DMDDataSession()))
        {
        }

        public AjaxPennyMacCDRFormPresenter(IAjaxView view, IPennyMacCDRDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Generate(string path, List<string> loanNumber, string csvType, string targetFile)
        {
            m_Path = path;

            StringBuilder loans = new StringBuilder();

            loanNumber.ForEach(x => loans.AppendFormat("'{0}',", x.Trim())); 
            loans.Remove(loans.Length - 1, 1);

            string sql = GetSQL(csvType, loans.ToString());

            var data = m_Dao.GetData(sql);

            using (TextWriter writer = File.CreateText(m_Path + "\\" + targetFile))
            {
                foreach (var row in data)
                {
                    int colCount = row.Count;
                    int counter = 1;
                    foreach (var col in row)
                    {
                        writer.Write("\"{0}\"{1}", col, counter++ < colCount ? "," : "");
                    }
                    writer.WriteLine("");
                }
            }

            m_View.ResponseText = "Click this <a href='Report/" + targetFile + "'>link</a> to get the CSV file.";

        }

        private string GetSQL(string csvType, string loans)
        {
            if (csvType.ToLower() == "cdr")
            {
                return CreateCDRSQL(loans);
            }
            if (csvType.ToLower() == "epp")
            {
                return CreateEPPSQL(loans);
            }

            return "";
        }

        private string CreateEPPSQL(string loans)
        {
            string sql = @"
            select
               '0' as 'Fixed'
               , '' as 'Blank'
               , '' as 'Blank'
               , '' as 'Blank'
               , convert(varchar(10), getdate(), 101) as 'Run Date'
               , '' as 'Blank'
               , '' as 'Blank'
               , g.loan_num as 'Loan Number'
               , g.loan_amt as 'Loan Amount'
               , g.loan_amt as 'Loan Amount'
            from 
               dbo.gen g (nolock)
            ";
            sql = sql + " where g.loan_num in (" + loans + ")";
            return sql;
        }



        //public void GenerateCDR(string path, List<string> loanNumber)
        //{
        //    m_Path = path;

        //    StringBuilder loans = new StringBuilder();

        //    loanNumber.ForEach(x => loans.AppendFormat("'{0}',", x.Trim())); 
        //    loans.Remove(loans.Length - 1, 1);

        //    var data = m_Dao.GetData(CreateSQL(loans.ToString()));

        //    string targetFile = "PennyMacCDR.csv";

        //    using (TextWriter writer = File.CreateText(m_Path + "\\" + targetFile ))
        //    {
        //        foreach (var row in data)
        //        {
        //            int colCount = row.Count;
        //            int counter = 1;
        //            foreach (var col in row)
        //            {
        //                writer.Write("\"{0}\"{1}", col, counter++ < colCount ? "," : "");
        //            }
        //            writer.WriteLine("");
        //        }
        //    }

        //    m_View.ResponseText = "Click this <a href='Report/" + targetFile + "'>link</a> to get the CSV file.";

        //}

        private string CreateCDRSQL(string loans)
        {
            StringBuilder sb = new StringBuilder();

            string sql = @"
                select
                   '700009' as 'ClientNum'
                   , g.loan_num as 'CorrLoanNum'
                   , '' as 'DraftNum'
                   , case g.purpose
                        when 'p' then 'PR'
                        when 'r' then 'RF'
                        when 'c' then 'CP'
                        when 'd' then 'DC'
                        when 'h' then 'HI'
                     end as 'LoanPurposeType'
                   , g.borrow_ln as 'FirstBorrLastName'
                   , g.borrow_mn as 'FirstBorrMidName'
                   , g.borrow_fn as 'FirstBorrFirstName'
                   , dbo.xGEM_GetCoborrowerOrSpouseLastName (g.file_id) as 'SecBorrLastName'
                   , dbo.xGEM_GetCoborrowerOrSpouseFirstName (g.file_id) as 'SecBorrFirstName'
                   , case g.prop_type
                          when '2' then 'MFR'
                          when '3' then 'MFR'
                          when '4' then 'MFR'
                          when 'a' then 'CON'
                          when 'b' then 'MIX'
                          when 'c' then 'CON'
                          when 'd' then 'COO'
                          when 'e' then 'CON'
                          when 'f' then 'LOT'
                          when 'g' then 'MFH'
                          when 'h' then 'MFH'
                          when 'i' then 'MFH'
                          when 'j' then 'MFH'
                          when 'k' then 'MFH'
                          when 'l' then 'LOT'
                          when 'm' then 'MFH'
                          when 'n' then 'CON'
                          when 'o' then 'INV'
                          when 'p' then 'PUD'
                          when 'q' then 'CON'
                          when 's' then 'SFR'
                          when 't' then 'TWN'
                          when 'u' then 'MFR'
                          when 'x' then 'MIX'                   
                      end as 'PropType'          
                   , g.prop_no + ' ' + g.prop_street as 'PropAddress'
                   , g.prop_city as 'PropCity'
                   , g.prop_state as 'PropState'
                   , replace(g.prop_zip, '-', '') as 'PropZip'
                   , u.num_units  as 'MFRUnits'
                   , case g.owner_occu
                        when 'yes' then 'OO'
                        when 'no' then 'NO'
                        when '2nd' then 'SH'
                     end as 'OccType'
                   , replace(g.borrow_ssn, '-', '') as 'FirstBorrSSN'
                   , replace(dbo.xGEM_GetCoborrowerOrSpouseSSN(g.file_id), '-', '') as 'SecBorrSSN'
                   , case cb.cborrow_citizenship_type when '1' then 'N' else 'Y' end as 'IsNonCitizen'
                   , case when g.owner_occu = 'yes' then '' else g.mail_street end as 'MailingAddress'
                   , case when g.owner_occu = 'yes' then '' else g.mail_city end as 'MailingCity'
                   , case when g.owner_occu = 'yes' then '' else g.mail_state end as 'MailingState'
                   , case when g.owner_occu = 'yes' then '' else replace(g.mail_zip, '-', '') end as 'MailingZip'
                   , case p.prog_name
                          when 'CO2/A' then 'CONF228'
                          when 'CO5/1' then 'CONF525'
                          when 'CO7/1' then 'CONF723'
                          when 'CONF10F' then 'CONF10F'
                          when 'CO15' then 'CONF10F'
                          when 'CO20' then 'CONF20F'
                          when 'CONF25F' then 'CONF25F'
                          when 'CO30' then 'CONF30F'
                          when 'FHA1/1' then 'FHA-A'
                          when 'FHA30' then 'FHA-F'
                          when 'FHA30STRM' then 'FHA-FS'
                          when 'VA1/1' then 'VA-A'
                          when 'VA30' then 'VA-F'
                          when 'VA30IRRRL' then 'VA-FS'
                          when 'NC2/A' then 'JUMB228'
                          when 'NC3/1' then 'JUMB327'
                          when 'NC7/1' then 'JUMB723'
                          when 'NC10' then 'JUMB10F'
                          when 'NC15' then 'JUMB15F'
                          when 'NC20' then 'JUMB20F'
                          when 'NC30' then 'JUMB30F'
                      end as 'ProductType'
                   , g.loan_amt as 'OrigPrinBal'
                   , 'UnpaidPrinBal' = (g.loan_amt - isnull((select sum(prin_amt) from payments p where p.file_id = g.file_id), 0))
                   , g.p_and_i 'PrinInt'
                   , g.int_rate 'OrigNoteRate'
                   , convert(varchar(10), f.funded, 101) as 'OrigDate'
                   , convert(varchar(10), getdate(), 101) 'AdvanceDate'
                   , convert(varchar(10), g.first_pmt, 101) as 'FirstPayDate'
                   , convert(varchar(10), d.last_pmt, 101) 'MaturityDate'
                   , case when p.prog_type = 'arm' then 'A' when p.prog_type = 'fixed' then 'F' end as 'LoanPaymentType'
                   , '' as 'AdvanceAmt'
                   , g.loan_amt 'FundingAmt'  
                   , 'W' 'FundingMethodType' 
                   , '' 'FundingID'
                   , case 
                          when left(b.short_name, 4) = 'bots' then 'Loan Operation Center - Warehouse'
                          when left(b.short_name, 2) = 'cb' then 'Customers Bank Warehouse Lending'
                          when left(b.short_name, 3) = 'tcb' then 'Golden Empire Mortgage'
                          when left(b.short_name, 3) = 'vpb' then 'Golden Empire Mortgage'
                          else ''
                      end 'FirstPayeeName'
                   , b.addr 'FirstPayeeAddress'
                   , b.city 'FirstPayeeCity'
                   , b.state 'FirstPayeeState'
                   , replace(b.zip, '-', '') 'FirstPayeeZip'
                   , '' 'TitleInsurer'
                   , '' 'EscrowTitleClosingNum'
                   , g.loan_num + '-' + g.borrow_ln 'AgentInstruction'
                   , b.long_name 'BankName'
                   , b.city + ' ' + b.state 'BankCityState'
                   , b.aba_no 'BankABANum'
                   , b.acct_no 'BankAccount'
   
   
                   , '' 'SecPayeeName'
                   , '' 'SecPayeeAddress'
                   , '' 'SecPayeeCity'
                   , '' 'SecPayeeState'
                   , '' 'SecPayeeZip'
                   , '' 'FurtherCreditAccountNum'
                   , '' 'FurtherCreditABANum'
                   , '' 'FurtherAgentInstruction'
                   , '' 'FurtherCreditBankName'
                   , 'R' 'LineType'   
                   , '' 'UWConfirmApprovalType'
                   , '' 'UWConfirmationNum'
                   , '' 'UWExpirationDate'
                   , '' 'LockConfirmApprovalType'
                   , '' 'RegisterFlag'
                   , '' 'Ext'
                   , '' 'SubCustomer'
                   , '' 'BulkBatchNum'
                   , '' 'TakeoutInvestor'
                   , '' 'CommitNum'
                   , '' 'CommitPrice'
                   , '' 'HUD-1Date'
                   , '' 'NonPerformingFlag'
                   , '' 'CommitDate'
                   , '' 'CommitExpDate'
                   , '' 'InvestorLoanNum'
                   , case g.lien
                          when 'f' then '1'
                          when 's' then '2'
                     end 'LienType'
                   , u.borr_fico as 'BorrFICO'
                   , u.bratio_dti as 'DTI'
                   , g.ltv as 'LTV'
                   , g.cltv as 'CLTV'
                   , '0' 'JnrLienAmount'
                   , case  
                       when mic.short_name = 'radian' then 'RAD'
                       when mic.short_name = 'united guaranty' then 'UG'
                       when left (mic.short_name, 8) = 'genworth' then 'GENW'
                       when mic.short_name = 'ge' then 'GE'
                       when mic.short_name = 'MGIC' then 'MGIC'
                       when mic.short_name = 'mercury insurance' then 'MRCY'
                       when mic.short_name = 'primary mortgage insurers' then 'PMI'
                       when mic.short_name = 'republic' then 'RMIC'
                       when mic.short_name = 'triad' then 'TRI'
                       else ''
                     end  'PMIInsurer'
                   , u.mi_perc 'PMIPct'
   
                   , case when isnull(f.paid_by_lender, 0) = 0 then 'N' else 'Y' end 'isLPMI'
                   , 'N' 'LPMIPremium'
                   , '000' 'LPMIPct'
                   , '' 'SubPrimeCreditGrade'
                   , g.loan_term 'OrigTerm'
                   , g.amort_term 'AmortTerm'
                   , g.appras_val 'AppraisedValue'
                   , a1.cont_first + ' ' + a1.cont_last 'AppraiserName'



                   , case g.doc_type
                          when 'alternate' then 'AD'
                          when 'full' then 'FD'
                          when 'nina' then 'NN'
                          when 'ninane' then ''
                          when 'nineva' then ''
                          when 'niv' then ''
                          when 'NO DOC' then 'RD'
                          when 'NO RATIO' then 'NR'
                          when 'STATED' then 'SS'
                          when 'STATED/VERIFIED' then ''
                          when 'STREAMLINE' then 'SL'
                     end 'DocType'
                   , 'BC' 'AmortType'
                   , case when isnull(g.req_setup, 0) = 0 then 'N' else 'Y' end 'Escrow'
                   , '' 'ARMMargin'
                   , '' 'ARMIndex'
                   , '' 'ARMTeaser'
                   , '' 'StartRate'
                   , '' 'InitialCap'
                   , '' 'RateChangeFreq'
                   , '' 'PeriodicCap'
                   , '' 'LifetimeCap'
                   , '' 'PeriodicFloor'
                   , '' 'LifetimeFloor'


                   , g.purch_pric 'PurchasePrice'
                   , case when u.prepay_buy = 0 then 'N' else 'Y' end 'PrePayPenaltyFlag'
                   , g.prepay_perc 'PrePayPenaltyPct'
                   , g.prepay_mos 'PrePayPeriod'
                   , '' 'PrePayType'
                   , 'soft' 'PrePayEnforceability'
                   , 'N' 'PiggyBack'
                   , 'N' 'IntOnlyFlag'
                   , '000' 'IntOnlyTerm'
   
   
                   , convert(varchar(10), g.oint_to, 101) 'PaidToDate'
                   , 'Y' 'IsAssetVerified'
                   , '' 'TimesDlq30'
                   , '' 'TimesDlq60'
                   , '' 'TimesDlq90'
                   , '' 'TimesDlq120'


                   , case when cb.cborrow_job_self_emp = '1' then 'Y' else 'N' end 'FirstBorrSelfEmployed'
                   , case dbo.xGEM_GetCoborrowerOrSpouseSelfEmployed(g.file_id) when '1' then 'Y' when 'N' then 'N' else '' end 'SecBorrSelfEmployed'
                   , case u.borr_marital
                          when 'm' then 'Married'
                          when 'p' then 'Divorced'
                          else 'Single'
                     end 'FirstBorrMaritalStatus'
                   , case u.cborr_marital
                          when 'm' then 'Married'
                          when 'p' then 'Divorced'
                          when 'n' then 'Single'
                          when 'u' then 'Single'
                          when 'z' then 'Single'
                          else ''
                     end 'SecBorrMaritalStatus'
                   , u.borr_age 'FirstBorrAge'
                   , case when u.cborr_age = '0' then '' else cast(u.cborr_age as varchar) end  'SecBorrAge'
                   , case u.borr_sex when '1' then 'M' when '2' then 'F' else 'INNP' end 'FirstBorrGender'
                   , case u.cborr_sex when '1' then 'M' when '2' then 'F' else '' end 'SecBorrGender'
                   , case first_time_buyer when '1' then 'Y' else 'N' end 'IsFirstTimeBuyer'
                   , case when g.neg_amort = '0' then 'N' else 'Y' end 'NegAm'
                   , 'N' 'ARMOption'
                   , '' 'FirstDlqDate'
                   , '' 'BankruptcyChapter'
                   , '' 'BankruptcyDischargeDate'
                   , '' 'ForeclosureDate'
                   /* , u.aus_recommendation */
                   , isnull(ausr.descriptn, '') 'DUResponse'
                   , u.aus_no 'DUCaseNum'
                   , '' 'MaxNegAm'
                   , '' 'FirstRateAdjPeriod'
                   , '' 'Tradelines'
                   , u.months_of_reserves 'ReservedTerm'
                   , u.months_of_reserves * g.tot_pmt  'ReservedAmt'
   
                   , g.mers_no 'MERSMin'
                   , '' 'ServicingFee'
                   , '' 'Servicer'
                   , g.int_rate 'CurNoteRate'
                   , '' 'InitRecastPeriod'
                   , '' 'SubRecastPeriod'
                   , '' 'TeaserPeriod'
                   , 'N' 'isBalloon'
                   , '' 'MarketValueCap'
                   , g.fha_va_case 'FHACaseNum'
                   , u.max_rate 'MaxRate'
                   , '' 'LoanType2'
                   , '' 'IntOnlyEndDate'
                   , '' 'FirstPayChangeDate'
                   , '' 'LookBackPeriod'
                   , '' 'IntRoundingFeature'
                   , '' 'FirstAdjMaxInitialRate'
                   , '' 'FirstAdjMinInitialRate'
                   , case g.section32 when '1' then 'Y' else 'N' end 'Section32'
                from
                   dbo.gen g (nolock)
                   left join dbo.fun f (nolock) on g.file_id = f.file_id
                   left join dbo.act a (nolock) on g.file_id = a.file_id
                   left join dbo.und u (nolock) on g.file_id = u.file_id
                   left join dbo.ins i (nolock) on g.file_id = i.file_id
                   left join dbo.doc d (nolock) on g.file_id = d.file_id
                   left join dbo.banks b (nolock) on f.banks_id = b.banks_id
                   left join dbo.programs p (nolock) on g.programs_id = p.programs_id
                   left join dbo.coborrow cb (nolock) on g.file_id = cb.file_id and cb.primary_bor = 1
                   left join dbo.insuranc mic (nolock) on u.mi_comp = mic.insuranc_id
                   left join dbo.appraisr a1 (nolock) on u.appraisr_id = a1.appraisr_id
                   left join dbo.lu_popup ausr (nolock) on u.aus_recommendation = ausr.alias and ausr.type = 'ausrec'

                
                ";
            sql = sql + " where g.loan_num in (" + loans + ")";
            return sql;
        }
    }
    
}
