select v.[VoucherYear]
      ,v.[VoucherMonth]
      ,v.[PaymentTerms]
      ,vd.[VdId]
      ,vd.[Abstract]
      ,vd.[AccId]
      ,vd.[AccountCode]
      ,vd.[AccountName]
      ,vd.[Quantity]
      ,vd.[Price]
      ,vd.[Debit]
      ,vd.[Credit]
      ,vd.[InitialQuantity]
      ,vd.[InitialBalance]
      ,vd.[YtdDebitQuantity]
      ,vd.[YtdDebit]
      ,vd.[YtdCreditQuantity]
      ,vd.[YtdCredit]
      ,vd.[YtdBeginBalanceQuantity]
      ,vd.[YtdBeginBalance]
from dbo.T_Voucher v,dbo.T_Voucher_Detail vd
where v.VId=vd.VId and v.AbId='81084FA8-0D66-E711-826E-9C5C8E79F58D'



select * from dbo.T_Account where AccId in (550,582,551,583)