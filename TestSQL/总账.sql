select * from
(
select 
vd.AccId,
min(vd.AccountCode) as AccountCode,
min(vd.AccountName) as AccountName,
min(v.PaymentTerms) as Period,
'期初余额' as Abstract,
SUM(vd.Debit) as Debit,
SUM(vd.Credit) as Credit,
min(a.Direction) as Direction,
0 as Balance,
1 as Sort 
from T_Voucher v, T_Voucher_Detail vd, T_Account a 
where v.VId = vd.VId and vd.AccId = a.AccId 
and v.AbId = '81084FA8-0D66-E711-826E-9C5C8E79F58D'
and v.VoucherYear = 2017 and v.VoucherMonth < 7
group by vd.AccId, v.VoucherYear, v.VoucherMonth 

union all

select 
vd.AccId,
min(vd.AccountCode) as AccountCode,
min(vd.AccountName) as AccountName,
min(v.PaymentTerms) as Period,
'本期合计' as Abstract,
SUM(vd.Debit) as Debit,
SUM(vd.Credit) as Credit,
min(a.Direction) as Direction,
0 as Balance,
2 as Sort 
from T_Voucher v, T_Voucher_Detail vd, T_Account a 
where v.VId = vd.VId and vd.AccId = a.AccId 
and v.AbId = '81084FA8-0D66-E711-826E-9C5C8E79F58D'
and v.VoucherYear = 2017 and 6 <= v.VoucherMonth and v.VoucherMonth <= 7
group by vd.AccId, v.VoucherYear, v.VoucherMonth

union all

select 
vd.AccId,
min(vd.AccountCode) as AccountCode,
min(vd.AccountName) as AccountName,
min(v.PaymentTerms) as Period,
'本年累计' as Abstract,
SUM(vd.Debit) as Debit,
SUM(vd.Credit) as Credit,
min(a.Direction) as Direction,
0 as Balance,
3 as Sort 
from T_Voucher v, T_Voucher_Detail vd, T_Account a 
where v.VId = vd.VId and vd.AccId = a.AccId 
and v.AbId = '81084FA8-0D66-E711-826E-9C5C8E79F58D'
and v.VoucherYear = 2017 and v.VoucherMonth <= 7
group by vd.AccId, v.VoucherYear 
) t
order by  AccountCode,Period,Sort


select 

(select 
vd.AccId,
min(vd.AccountCode) as AccountCode,
min(vd.AccountName) as AccountName,
min(v.PaymentTerms) as Period,
'本年累计' as Abstract,
SUM(vd.Debit) as Debit,
SUM(vd.Credit) as Credit,
min(a.Direction) as Direction,
0 as Balance,
3 as Sort 
from T_Voucher v, T_Voucher_Detail vd, T_Account a 
where v.VId = vd.VId and vd.AccId = a.AccId 
and v.AbId = v1.AbId
and v.VoucherYear = v1.VoucherYear and v.VoucherMonth <= v1.VoucherMonth
group by vd.AccId, v.VoucherYear )

from T_Voucher v1
where AbId = '81084FA8-0D66-E711-826E-9C5C8E79F58D' and PaymentTerms <= '201707'
group by AbId,VoucherYear,VoucherMonth


select PaymentTerms from T_Voucher where AbId = '81084FA8-0D66-E711-826E-9C5C8E79F58D' and PaymentTerms <= '201707' group by PaymentTerms