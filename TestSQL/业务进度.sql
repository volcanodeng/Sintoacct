  select i.ItemId,s.* from [T_Prog_BizSteps] s,[T_Prog_BizItems] i
  where s.ItemId=i.CateId