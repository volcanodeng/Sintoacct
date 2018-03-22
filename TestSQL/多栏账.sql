--固定列数据
select v.VoucherYear,v.VoucherMonth,(select CertWord from T_Certificate_Word where CwId=v.CertificateWord_CwId) certword,
v.CertWordSN,vd.Abstract,vd.Debit,vd.Credit,a.Direction,0 as balance,
vd.vdid
from T_Voucher v ,T_Voucher_Detail vd,T_Account a
where v.VId=vd.VId and vd.AccId=a.AccId
and a.AccCode like '1101%'


select a.AccId,a.AccName,v.VoucherYear,v.VoucherMonth,sum(vd.Debit),sum(vd.Credit)
from T_Voucher v ,T_Voucher_Detail vd,T_Account a
where v.VId=vd.VId and vd.AccId=a.AccId
--and a.ParentAccCode = '110101'
group by v.VoucherYear,v.VoucherMonth,a.AccId,a.AccName


select * from T_Voucher_Detail where Debit>0 or Credit>0

select accid,AccountName from T_Voucher_Detail where Debit<>0 or Credit<>0 group by accid,AccountName


--借方（贷方）明细
select VdId,
case when accid=185 then Debit+Credit else NULL END as [银行存款],
 case when accid=188 then Debit+Credit else NULL END as [应收票据],
case when accid=194 then Debit+Credit else NULL END as [材料采购],
case when accid=218 then Debit+Credit else NULL END as [应付账款],
case when accid=219 then Debit+Credit else NULL END as [预收账款],
case when accid=235 then Debit+Credit else NULL END as [研发支出],
case when accid=236 then Debit+Credit else NULL END as [工程施工],
case when accid=251 then Debit+Credit else NULL END as [股票],
case when accid=253 then Debit+Credit else NULL END as [基金]
from T_Voucher_Detail where Debit<>0 or Credit<>0



--本期合计
select v.VoucherYear,v.VoucherMonth,
sum(case when accid=185 then Debit+Credit else 0 END) as [银行存款],
sum(case when accid=188 then Debit+Credit else 0 END) as [应收票据],
sum(case when accid=194 then Debit+Credit else 0 END) as [材料采购],
sum(case when accid=218 then Debit+Credit else 0 END) as [应付账款],
sum(case when accid=219 then Debit+Credit else 0 END) as [预收账款],
sum(case when accid=235 then Debit+Credit else 0 END) as [研发支出],
sum(case when accid=236 then Debit+Credit else 0 END) as [工程施工],
sum(case when accid=251 then Debit+Credit else 0 END) as [股票],
sum(case when accid=253 then Debit+Credit else 0 END) as [基金]
from T_Voucher v ,T_Voucher_Detail vd 
where v.VId=vd.VId and vd.Debit<>0 or vd.Credit<>0
group by v.VoucherYear,v.VoucherMonth


--期初余额
select 
sum(case when accid=185 then Debit+Credit else 0 END) as [银行存款],
sum(case when accid=188 then Debit+Credit else 0 END) as [应收票据],
sum(case when accid=194 then Debit+Credit else 0 END) as [材料采购],
sum(case when accid=218 then Debit+Credit else 0 END) as [应付账款],
sum(case when accid=219 then Debit+Credit else 0 END) as [预收账款],
sum(case when accid=235 then Debit+Credit else 0 END) as [研发支出],
sum(case when accid=236 then Debit+Credit else 0 END) as [工程施工],
sum(case when accid=251 then Debit+Credit else 0 END) as [股票],
sum(case when accid=253 then Debit+Credit else 0 END) as [基金]
from T_Voucher v ,T_Voucher_Detail vd 
where v.VId=vd.VId 
and v.PaymentTerms < '201702'


--本年累计
select 
sum(case when accid=185 then Debit+Credit else 0 END) as [银行存款],
sum(case when accid=188 then Debit+Credit else 0 END) as [应收票据],
sum(case when accid=194 then Debit+Credit else 0 END) as [材料采购],
sum(case when accid=218 then Debit+Credit else 0 END) as [应付账款],
sum(case when accid=219 then Debit+Credit else 0 END) as [预收账款],
sum(case when accid=235 then Debit+Credit else 0 END) as [研发支出],
sum(case when accid=236 then Debit+Credit else 0 END) as [工程施工],
sum(case when accid=251 then Debit+Credit else 0 END) as [股票],
sum(case when accid=253 then Debit+Credit else 0 END) as [基金]
from T_Voucher v ,T_Voucher_Detail vd 
where v.VId=vd.VId 
and v.VoucherYear = 2017 and v.VoucherMonth <= 2



--期末余额
select 
sum(case when accid=185 then Debit+Credit else 0 END) as [银行存款],
sum(case when accid=188 then Debit+Credit else 0 END) as [应收票据],
sum(case when accid=194 then Debit+Credit else 0 END) as [材料采购],
sum(case when accid=218 then Debit+Credit else 0 END) as [应付账款],
sum(case when accid=219 then Debit+Credit else 0 END) as [预收账款],
sum(case when accid=235 then Debit+Credit else 0 END) as [研发支出],
sum(case when accid=236 then Debit+Credit else 0 END) as [工程施工],
sum(case when accid=251 then Debit+Credit else 0 END) as [股票],
sum(case when accid=253 then Debit+Credit else 0 END) as [基金]
from T_Voucher v ,T_Voucher_Detail vd 
where v.VId=vd.VId 
and v.PaymentTerms <= '201702'



