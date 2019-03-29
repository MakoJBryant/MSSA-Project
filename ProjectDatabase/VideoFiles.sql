CREATE TABLE [dbo].[VideoFiles](  
    [ID] [int] IDENTITY(1,1) NOT NULL,  
    [Name] [nvarchar](50) NULL,  
    [FileSize] [int] NULL,  
    [FilePath] [nvarchar](100) NULL,  
 CONSTRAINT [PK_VideoFiles] PRIMARY KEY CLUSTERED   
(  
    [ID] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY]  
  
GO  
  
CREATE procedure [dbo].[spAddNewVideoFile]  
(  
@Name nvarchar(50),  
@FileSize int,  
@FilePath nvarchar(100)  
)  
as  
begin  
insert into VideoFiles (Name,FileSize,FilePath)   
values (@Name,@FileSize,@FilePath)   
end  
  
CREATE procedure [dbo].[spGetAllVideoFile]  
as  
begin  
select ID,Name,FileSize,FilePath from VideoFiles  
end 