SELECT x.ShortName,x.FieldType,x.FormId, COUNT(x.FormId) AS Duplicates
FROM(
       SELECT formID, 
	          CAST (c.query('*:ShortName/text()') as varchar(MAX)) ShortName, 
			  CAST (c.query('*:Type/text()') as varchar(MAX)) FieldType
       FROM FormsManagement.FormData  CROSS APPLY  FormLevelXml.nodes('
									declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
									declare namespace a = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
									declare namespace b = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
									(/ArrayOfanyType/anyType/*:SectionElements/*:anyType)') T(c)		
    ) AS x
GROUP BY x.ShortName, x.FieldType, x.FormId
HAVING COUNT(x.FormId) > 1 AND x.ShortName != ''
----------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT *
FROM (
		SELECT EntityId,SequenceNumber,Body,PollOrder, [Body].query('
									declare default element namespace "http://schemas.datacontract.org/2004/07/SCS.Domain.DocumentManagement.Contract.EventModel";
									declare namespace a = "http://schemas.datacontract.org/2004/07/SCS.Domain.DocumentManagement.Contract.Base";
									declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Common.Contract";
									(/DocumentUploaded
									 /UploadDocument
									 /a:Type
									 /n:Id
									 /text()
									)[1]')  AS Type
		FROM [Events]
		WHERE Type LIKE '%documentuploaded%'
) FullData
WHERE CAST(FullData.Type AS varchar(MAX)) = '00000000-0000-0000-0000-000000000000'

-----------------------------------------------  XML Qeury to Select Values -------------------------------------------------------------------------
SELECT FormLevelXml.query('
	                        declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
	                        declare namespace i = "http://www.w3.org/2001/XMLSchema-instance";
	                        declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
							declare namespace a = "SCS.Domain.FormsAndRecordsManagement.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";(/ArrayOfanyType
							/anyType[@i:type="a:SCS.Domain.FormsAndRecordsManagement.Contract.Base.SingleSelectField"][n:ShortName = "want to enter measurement"]
							/n:IsCompliant
							/n:FieldConfiguration
							/n:Compliance
							/n:ComplianceValue)[1]')
FROM RecordsManagement.RecordData



-----------------------------------------------  XML Qeury to Select Values using cross apply -------------------------------------------------------------------------
SELECT formID, Content = T.c.value('.', 'VARCHAR(MAX)')
FROM FormsManagement.FormData  CROSS APPLY  FormLevelXml.nodes('//*:ShortName') T(c)


-----------------------------------------------  XML Qeury to Insert Values ---------------------------------------------------------------------------
UPDATE RecordsManagement.RecordData
SET FormLevelXml.modify('declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
	                        declare namespace i = "http://www.w3.org/2001/XMLSchema-instance";
	                        declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
							declare namespace a = "SCS.Domain.FormsAndRecordsManagement.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
	                        insert  sql:variable("@NewValue")  as last into  
	                        (/ArrayOfanyType
							/anyType[@i:type="a:SCS.Domain.FormsAndRecordsManagement.Contract.Base.SingleSelectField"][n:ShortName = "want to enter measurement"]
							/n:Configuration
							/n:FieldConfiguration
							/n:Compliance)[1]')
WHERE Id = 'D04E0A91-3FB2-4A74-BCC8-05248DEF18F2'


-----------------------------------------------  XML Qeury to Update/Replace Values ---------------------------------------------------------------------------
UPDATE RecordsManagement.RecordData
SET FormLevelXml.modify('declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
	                        declare namespace i = "http://www.w3.org/2001/XMLSchema-instance";
	                        declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
							declare namespace a = "SCS.Domain.FormsAndRecordsManagement.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
							replace value of (/ArrayOfanyType
							/anyType[@i:type="a:SCS.Domain.FormsAndRecordsManagement.Contract.Base.SingleSelectField"][n:ShortName = "want to enter measurement"]
							/n:IsCompliant/text())[1] with "true"')
WHERE Id = 'D04E0A91-3FB2-4A74-BCC8-05248DEF18F2'


-----------------------------------------------  XML Qeury to Delete Values ------------------------------------------------------------------------------------
UPDATE RecordsManagement.RecordData
SET FormLevelXml.modify('declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
	                        declare namespace i = "http://www.w3.org/2001/XMLSchema-instance";
	                        declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
							declare namespace a = "SCS.Domain.FormsAndRecordsManagement.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
							delete (/ArrayOfanyType
							/anyType[@i:type="a:SCS.Domain.FormsAndRecordsManagement.Contract.Base.SingleSelectField"][n:ShortName = "want to enter measurement"]
							/n:Configuration
							/n:FieldConfiguration
							/n:Compliance
							/n:ComplianceValue)')
WHERE Id = 'D04E0A91-3FB2-4A74-BCC8-05248DEF18F2'
------------------------------------------------------------------------------------------------------------------------------------------------------------------
  UPDATE [SCS_Tenant_Reporting_gardenfresh].[RecordsManagement].[RecordData] 
  SET [FormLevelXml] = REPLACE( CAST( [FormLevelXml] as varchar(max) ), '<IsCompliant>false</IsCompliant>', '<IsCompliant>true</IsCompliant>') 
  where Id = '6f91ac5b-1729-4a9b-aa5f-5581e85f830b'
------------------------------------------------------------------------------------------------------------------------------------------------------------------

SELECT [EntityId]
      ,[SequenceNumber]
      ,[Type]
      ,[Body]
      ,[Timestamp]
      ,[PollOrder]
  FROM [dbo].[Events]
  where Type like '%FormSubmitted%'
  and CHARINDEX('.e-file',CONVERT(VARCHAR(MAX),[Body]),1) <= 0

------------------------------------------------------------------------------------------------------------------------------------------------------------------

  UPDATE [SCS_Tenant_Reporting_oceanmist].[FormsManagement].[FormData]
SET FormLevelXml.modify('declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
                         declare namespace i = "http://www.w3.org/2001/XMLSchema-instance";
						 declare namespace a = "SCS.Domain.FormsAndRecordsManagement.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
						 declare namespace b = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
						 declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
						 delete 
						 (/ArrayOfanyType
						  /anyType[n:Id= "054f8351-6b48-4688-9bdf-0f3997f474bd"]
						  /n:SectionElements
						  /b:anyType[@i:type="a:SCS.Domain.FormsAndRecordsManagement.Contract.Base.NumericField"][n:Name="Second Test Re-Swab: Results"]
						  /n:ShortName
						  )
						')
WHERE FormId = 'C60805E3-FFC8-E611-80D4-408D5C51D2B0'

------------------------------------------------------------------------------------------------------------------------------------------------------------------
UPDATE [SCS_Tenant_Reporting_oceanmist].[FormsManagement].[FormData]
SET FormLevelXml.modify('declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
                         declare namespace i = "http://www.w3.org/2001/XMLSchema-instance";
						 declare namespace a = "SCS.Domain.FormsAndRecordsManagement.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
						 declare namespace b = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
						 declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
						 insert  "<ShortName>Second Test Re-Swab: Res</ShortName>"  as last into  
						 (/ArrayOfanyType
						  /anyType[n:Id= "054f8351-6b48-4688-9bdf-0f3997f474bd"]
						  /n:SectionElements
						  /b:anyType[@i:type="a:SCS.Domain.FormsAndRecordsManagement.Contract.Base.NumericField"][n:Name="Second Test Re-Swab: Results"]
						  )[1]
						')
WHERE FormId = 'C60805E3-FFC8-E611-80D4-408D5C51D2B0'

------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT FormLevelXml.query('declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
                         declare namespace i = "http://www.w3.org/2001/XMLSchema-instance";
						 declare namespace a = "SCS.Domain.FormsAndRecordsManagement.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
						 declare namespace b = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
						 declare namespace n = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
						 (/ArrayOfanyType
						  /anyType[n:Id= "054f8351-6b48-4688-9bdf-0f3997f474bd"]
						  /n:SectionElements
						  /b:anyType[@i:type="a:SCS.Domain.FormsAndRecordsManagement.Contract.Base.NumericField"][n:Name="Second Test Re-Swab: Results"]
						  )
						')
FROM  [SCS_Tenant_Reporting_oceanmist].[FormsManagement].[FormData]
WHERE FormId = 'C60805E3-FFC8-E611-80D4-408D5C51D2B0'


------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Add new XML nodes
declare @newNodeData xml = cast('<b:NamedIdentifierOfguid xmlns:b="http://schemas.datacontract.org/2004/07/SCS.Common.Contract">
									  <b:Id>34fac547-6c1e-439a-a2f7-b4b33c155c65</b:Id>
									  <b:Name>Fish</b:Name>
									</b:NamedIdentifierOfguid>' as xml)
UPDATE [SCS_Tenant_Reporting_srcmilling].[RecordsManagement].RecordData
SET FormLevelXml.modify('declare default element namespace "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
                         declare namespace a = "http://schemas.datacontract.org/2004/07/SCS.Domain.FormsAndRecordsManagement.Contract.Base";
						 insert sql:variable("@newNodeData") as last into  
						 (/ArrayOfanyType
						  /anyType[a:Id= "82f28e1f-ef3a-41d9-bd2b-a87440188e27"]
						  /a:Configuration
						  /a:FieldConfiguration
						  /a:Compliance
						  /a:ComplianceValue
						  )[1]
						')
WHERE Id = '4CD33265-63B8-490F-919A-5FCF47A4C111'