select min(a.AccCode),MIN(a.AccName),
case when min(a.Direction)='質' then min(a.InitialBalance) end as initdebit,
case when min(a.Direction)='湃' then min(a.InitialBalance) end as initcredit
from T_Voucher v,T_Voucher_Detail vd,T_Account a
where v.VId=vd.VId and vd.AccId=a.AccId and v.AbId='81084FA8-0D66-E711-826E-9C5C8E79F58D'
group by a.AccId
order by MIN(a.AccCode)



select min(a.AccCode) as acccode,MIN(a.AccName) as accname,a.Direction,
(select sum(case  a.Direction when '質' then SUM(vd.Debit-vd.Credit) when '湃' then SUM(vd.Credit-vd.Debit) end) from T_Voucher_Detail) as initamount,
case  a.Direction when '質' then SUM(vd.Debit-vd.Credit) when '湃' then SUM(vd.Credit-vd.Debit) end as CurrentOccurrence

from T_Voucher v,T_Voucher_Detail vd,T_Account a
where v.VId=vd.VId and vd.AccId=a.AccId and v.AbId='81084FA8-0D66-E711-826E-9C5C8E79F58D'
and v.[VoucherYear]=2017 and v.VoucherMonth >=6 and v.VoucherMonth<=7
group by a.AccId,a.Direction
order by MIN(a.AccCode)



select vd.AccountCode,vd.AccountName,'init',
case  a.Direction when '質' then SUM(vd.Debit-vd.Credit) when '湃' then SUM(vd.Credit-vd.Debit) end 
from T_Voucher v,T_Voucher_Detail vd,T_Account a 
where v.VId=vd.VId and vd.AccId=a.AccId
  and v.AbId='81084FA8-0D66-E711-826E-9C5C8E79F58D' 
  and v.VoucherYear=2017 and v.VoucherMonth<7
  group by vd.AccId,vd.AccountCode,vd.AccountName,a.Direction
union all
select vd.AccountCode,vd.AccountName,'cur',
case  a.Direction when '質' then SUM(vd.Debit-vd.Credit) when '湃' then SUM(vd.Credit-vd.Debit) end
from T_Voucher v,T_Voucher_Detail vd,T_Account a
where v.VId=vd.VId and vd.AccId=a.AccId 
and v.AbId='81084FA8-0D66-E711-826E-9C5C8E79F58D'
and v.[VoucherYear]=2017 and v.VoucherMonth >=7 and v.VoucherMonth<=8
group by vd.AccId,vd.AccountCode,vd.AccountName,a.Direction
union all
select vd.AccountCode,vd.AccountName,'yearly',
case  a.Direction when '質' then SUM(vd.Debit-vd.Credit) when '湃' then SUM(vd.Credit-vd.Debit) end 
from T_Voucher v,T_Voucher_Detail vd,T_Account a 
where v.VId=vd.VId and vd.AccId=a.AccId
  and v.AbId='81084FA8-0D66-E711-826E-9C5C8E79F58D' 
  and v.VoucherYear=2017 and v.VoucherMonth<=8
  group by vd.AccId,vd.AccountCode,vd.AccountName,a.Direction
