
CREATE   proc [spGenInsertSQL] (@tablename varchar(256),@number BIGINT,@whereClause NVARCHAR(MAX))
as
begin
declare @sql varchar(8000)
declare @sqlValues varchar(8000)
set @sql =' ('
set @sqlValues = 'values (''+'
select @sqlValues = @sqlValues + cols + ' + '','' + ' ,@sql = @sql + '[' + name + '],'
  from
      (select case
                when xtype in (48,52,56,59,60,62,104,106,108,122,127)       

                     then 'case when '+ name +' is null then ''NULL'' else ' + 'cast('+ name + ' as varchar)'+' end'

                when xtype in (58,61,40,41,42)

                     then 'case when '+ name +' is null then ''NULL'' else '+''''''''' + ' + 'cast('+ name +' as varchar)'+ '+'''''''''+' end'

               when xtype in (167)

                     then 'case when '+ name +' is null then ''NULL'' else '+''''''''' + ' + 'replace('+ name+','''''''','''''''''''')' + '+'''''''''+' end'

                when xtype in (231)

                     then 'case when '+ name +' is null then ''NULL'' else '+'''N'''''' + ' + 'replace('+ name+','''''''','''''''''''')' + '+'''''''''+' end'

                when xtype in (175)

                     then 'case when '+ name +' is null then ''NULL'' else '+''''''''' + ' + 'cast(replace('+ name+','''''''','''''''''''') as Char(' + cast(length as varchar)  + '))+'''''''''+' end'

                when xtype in (239)

                     then 'case when '+ name +' is null then ''NULL'' else '+'''N'''''' + ' + 'cast(replace('+ name+','''''''','''''''''''') as Char(' + cast(length as varchar)  + '))+'''''''''+' end'

                else '''NULL'''

              end as Cols,name

         from syscolumns 

        where id = object_id(@tablename)

      ) T
IF (@number!=0 AND @number IS NOT NULL)
BEGIN
set @sql ='select top '+ CAST(@number AS VARCHAR(6000))+' ''INSERT INTO ['+ @tablename + ']' + left(@sql,len(@sql)-1)+') ' + left(@sqlValues,len(@sqlValues)-4) + ')'' from '+@tablename
print @sql
END
ELSE
BEGIN 
set @sql ='select ''INSERT INTO ['+ @tablename + ']' + left(@sql,len(@sql)-1)+') ' + left(@sqlValues,len(@sqlValues)-4) + ')'' from '+@tablename
print @sql
END


PRINT @whereClause
IF ( @whereClause IS NOT NULL  AND @whereClause <> '')
BEGIN
set @sql =@sql+' where '+@whereClause
print @sql
END

exec (@sql)
end

GO