  select i.ItemId,s.* from [T_Prog_BizSteps] s,[T_Prog_BizItems] i
  where s.ItemId=i.CateId
  
  delete from dbo.T_Prog_BizSteps
  
  
  insert into T_Prog_BizSteps
  select s.StepName,ROW_NUMBER() over (order by i.itemid) as sort_index,i.ItemId  from [T_Prog_BizSteps] s,[T_Prog_BizItems] i
  where s.ItemId=i.CateId
  
 
  
  insert into dbo.T_Prog_BizSteps([StepId],[StepName]
           ,[SortIndex]
           ,[ItemId])
  select [StepId],[StepName]
      ,[SortIndex]
      ,[ItemId] from dbo.temp_step
  
 
  
 
