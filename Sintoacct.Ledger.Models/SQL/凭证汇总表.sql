select vd.[AccountCode],vd.AccountName,SUM(vd.[Debit]),SUM(vd.Credit) 
from T_Voucher v,T_Voucher_Detail vd
where v.VId=vd.VId and v.AbId='81084FA8-0D66-E711-826E-9C5C8E79F58D'
and '2017-07-01' < v.VoucherDate and v.VoucherDate < '2017-08-01'
group by vd.AccId,vd.[AccountCode],vd.AccountName