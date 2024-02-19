USE [master]
GO
/****** Object:  Database [University]    Script Date: 19/02/2024 10:21:46 pm ******/
CREATE DATABASE [University]
GO
ALTER DATABASE [University] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [University].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [University] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [University] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [University] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [University] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [University] SET ARITHABORT OFF 
GO
ALTER DATABASE [University] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [University] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [University] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [University] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [University] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [University] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [University] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [University] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [University] SET  DISABLE_BROKER 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [University] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [University] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [University] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [University] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [University] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [University] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [University] SET RECOVERY FULL 
GO
ALTER DATABASE [University] SET  MULTI_USER 
GO
ALTER DATABASE [University] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [University] SET DB_CHAINING OFF 
GO
ALTER DATABASE [University] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [University] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [University] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'University', N'ON'
GO
ALTER DATABASE [University] SET QUERY_STORE = OFF
GO
USE [University]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [University]
GO
/****** Object:  UserDefinedTableType [dbo].[University_Type]    Script Date: 19/02/2024 10:21:46 pm ******/
CREATE TYPE [dbo].[University_Type] AS TABLE(
	[CountryID] [int] NULL,
	[ID] [int] NULL,
	[InfoEmail] [varchar](200) NULL,
	[Name] [varchar](500) NULL,
	[State] [varchar](50) NULL,
	[WebSiteURL] [varchar](500) NULL
)
GO
/****** Object:  Table [dbo].[Country]    Script Date: 19/02/2024 10:21:46 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Code] [varchar](50) NULL,
	[RecordDateTime] [datetime] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Universities]    Script Date: 19/02/2024 10:21:46 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Universities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[WebSiteURL] [varchar](500) NULL,
	[State] [varchar](50) NULL,
	[InfoEmail] [varchar](200) NULL,
	[CountryID] [int] NULL,
	[RecordDateTime] [datetime] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Universities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetUniversityByCountryName]    Script Date: 19/02/2024 10:21:46 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUniversityByCountryName]
	-- Add the parameters for the stored procedure here
	@CountryName varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT 
	 U.[Name]
	,[WebSiteURL]
	,[State]
	,[InfoEmail]
	,[CountryID]
	
	
	FROM 
		[dbo].[Universities] U With (nolock)
		JOIN [dbo].[Country] C with (nolock) on C.ID=U.CountryID
		WHERE C.[Name]=@CountryName

END
GO
/****** Object:  StoredProcedure [dbo].[SaveCountry]    Script Date: 19/02/2024 10:21:46 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveCountry]
	-- Add the parameters for the stored procedure here
	@Name varchar(500),
	@Code varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @vID int=0

	if NOt exists (select 1 from  [dbo].[Country] where [Name]=@Name and [IsDeleted]=0)
    -- Insert statements for procedure here
	begin
		INSERT INTO [dbo].[Country]
           ([Name]
           ,[Code]
           ,[RecordDateTime]
           ,[IsDeleted])
		VALUES
           (@Name
           ,@Code
           ,GETUTCDATE()         
           ,0)

		SELECT @vID=SCOPE_IDENTITY()

	END
	ELSE
	BEGIN
		select @vID=ID from  [dbo].[Country] where [Name]=@Name  and [IsDeleted]=0
	END 

	SELECT 
		ID,
		[Name],
		[Code]
	FROM [dbo].[Country]
	WHERE ID=@vID


END
GO
/****** Object:  StoredProcedure [dbo].[SaveUniversity]    Script Date: 19/02/2024 10:21:46 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveUniversity]
	-- Add the parameters for the stored procedure here
	@CountryID int,
	@University_Type [University_Type] READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  INSERT INTO [dbo].[Universities]
           ([Name]
           ,[WebSiteURL]
           ,[State]
           ,[InfoEmail]
           ,[CountryID]
           ,[RecordDateTime]
           ,[IsDeleted])
	SELECT 
            UT.[Name]
           ,UT.[WebSiteURL]
           ,UT.[State]
           ,UT.[InfoEmail]
           ,@CountryID
           ,GETUTCDATE()
           ,0
		   FROM @University_Type UT
		   LEFT JOIN  [dbo].[Universities] U on U.[Name] = UT.[Name] and U.CountryID=@CountryID and U.IsDeleted=0
		   WHERE U.ID is null


		   UPDATE U

		   SET 
		   u.[WebSiteURL]=UT.WebSiteURL
           ,U.[State]=UT.State
           ,U.[InfoEmail]=UT.InfoEmail
		   ,u.LastUpdatedAt=GETUTCDATE()
		   FROM [dbo].[Universities] U 
		   JOIN @University_Type UT on UT.[Name]=U.[Name] 
		   WHERE U.CountryID=@CountryID and U.IsDeleted=0

		SELECT 1 as IsSavedSuccessfuly
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUniversityByCountryNameAndID]    Script Date: 19/02/2024 10:21:46 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUniversityByCountryNameAndID]
	-- Add the parameters for the stored procedure here
	
	@universityID int,
	@countryName varchar(500),
	@Name varchar(500),
	@State varchar(500),
	@InfoEmail varchar(500),
	@WebSiteURL varchar(500)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[Universities]
   SET [Name] = @Name
      ,[WebSiteURL] = @WebSiteURL
      ,[State] = @State
      ,[InfoEmail] = @InfoEmail
      ,[LastUpdatedBy] = 0
      ,[LastUpdatedAt] = GETUTCDATE()
 WHERE 
	ID =@universityID



		SELECT 1 as IsSavedSuccessfuly
	
END
GO
USE [master]
GO
ALTER DATABASE [University] SET  READ_WRITE 
GO
