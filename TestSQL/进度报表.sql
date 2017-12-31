select o.[ContractTime],o.[BizManager],o.[BizOperations],o.[Recommend],o.[CommercialExpense]
,c.[CustomerName]
,i.ItemName,s.StepName
,p.CompletedTime,p.ResultDesc
,p.CreateTime,p.Creator
from T_Prog_WorkOrder o 
left join T_Prog_Customers c on o.CusId=c.CusId
left join T_Prog_WorkProgress p on o.WoId=p.WoId
left join T_Prog_BizItems i on p.ItemId=i.ItemId
left join T_Prog_BizSteps s on p.StepId=s.StepId
where o.State>0 and p.ResultDesc is not null
order by  p.CreateTime desc