USE [master]
GO
/****** Object:  Database [LRWEB]    Script Date: 12/22/2020 1:22:57 PM ******/
CREATE DATABASE [LRWEB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LRWEB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LRWEB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LRWEB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LRWEB_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LRWEB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LRWEB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LRWEB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LRWEB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LRWEB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LRWEB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LRWEB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LRWEB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LRWEB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LRWEB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LRWEB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LRWEB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LRWEB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LRWEB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LRWEB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LRWEB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LRWEB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LRWEB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LRWEB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LRWEB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LRWEB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LRWEB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LRWEB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LRWEB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LRWEB] SET RECOVERY FULL 
GO
ALTER DATABASE [LRWEB] SET  MULTI_USER 
GO
ALTER DATABASE [LRWEB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LRWEB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LRWEB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LRWEB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [LRWEB]
GO
/****** Object:  Table [dbo].[AUDIT_TRAIL]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AUDIT_TRAIL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NOT NULL,
	[action] [varchar](100) NOT NULL,
	[dateCreated] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CSB_BRANCH]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSB_BRANCH](
	[BR_DESC] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_ADMIN_CRED]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRWEB_V1_ADMIN_CRED](
	[IntRecord] [int] IDENTITY(1000000,1) NOT NULL,
	[firstname] [varchar](80) NULL,
	[lastname] [varchar](50) NULL,
	[middlename] [varchar](50) NULL,
	[contactnumber] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[password] [varchar](200) NULL,
	[OTPCode] [varchar](50) NULL,
	[OTPDateTime] [varchar](50) NULL,
	[accesslevel] [varchar](50) NULL,
	[status] [varchar](50) NULL,
	[sent_status] [varchar](2) NULL,
	[attempt] [int] NULL,
	[newPassword] [varchar](3) NULL,
	[dateCreated] [datetime] NULL,
	[dateUpdated] [datetime] NULL,
	[passwordUpdated] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_AUTH]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRWEB_V1_AUTH](
	[IntRecord] [int] IDENTITY(1,1) NOT NULL,
	[GroupCode] [varchar](50) NOT NULL,
	[EmployeeEmail] [varchar](50) NOT NULL,
	[token] [varchar](64) NULL,
	[timestamp] [varchar](50) NULL,
	[timeexpiration] [varchar](50) NULL,
	[logout] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_CLIENT_ACCESS_ID]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRWEB_V1_CLIENT_ACCESS_ID](
	[ID] [int] IDENTITY(1000000,1) NOT NULL,
	[groupCode] [varchar](30) NOT NULL,
	[lastName] [varchar](25) NOT NULL,
	[firstName] [varchar](30) NOT NULL,
	[middleName] [varchar](25) NOT NULL,
	[contactNumber] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[OTPCode] [varchar](50) NULL,
	[OTPDateTime] [varchar](50) NULL,
	[attempt] [int] NOT NULL,
	[sent_status] [varchar](3) NULL,
	[status] [varchar](20) NULL,
	[newPassword] [varchar](3) NULL,
	[dateCreated] [datetime] NULL,
	[dateUpdated] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_CLIENT_EMPLOYEE]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRWEB_V1_CLIENT_EMPLOYEE](
	[IntRecord] [int] IDENTITY(1500000,1) NOT NULL,
	[GroupCode] [varchar](30) NOT NULL,
	[last_name] [varchar](25) NOT NULL,
	[first_name] [varchar](30) NOT NULL,
	[middle_name] [varchar](25) NOT NULL,
	[contactnum] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[Tokens] [varchar](80) NULL,
	[tokenSender] [varchar](50) NULL,
	[Expiration] [varchar](50) NULL,
	[Status] [bit] NULL,
	[Client_Branch] [varchar](50) NULL,
	[SubmitStatus] [varchar](5) NULL,
	[scheme_code] [varchar](50) NULL,
	[submit_cps] [varchar](50) NULL,
	[reason_decline] [varchar](250) NULL,
	[PDF_querystring] [varchar](2000) NULL,
	[branch_delivery] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_CLIENTS]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRWEB_V1_CLIENTS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AED] [datetime] NULL,
	[clientName] [varchar](100) NULL,
	[addressLine] [varchar](300) NULL,
	[barangayId] [varchar](50) NULL,
	[barangay] [varchar](50) NULL,
	[provinceId] [varchar](50) NULL,
	[province] [varchar](50) NULL,
	[cityId] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[mobileNumber] [varchar](100) NULL,
	[officeNumber] [varchar](100) NULL,
	[emailAddress] [varchar](100) NULL,
	[classification] [varchar](50) NULL,
	[structure] [varchar](100) NULL,
	[accountNumber] [varchar](100) NULL,
	[customerID] [varchar](100) NULL,
	[finacle_sol_id] [varchar](50) NULL,
	[sol_id] [varchar](50) NULL,
	[csbBranch] [varchar](100) NULL,
	[schemeCode] [varchar](100) NULL,
	[agencyCode] [varchar](50) NULL,
	[groupCode] [varchar](100) NULL,
	[status] [varchar](50) NULL,
	[remarks] [varchar](300) NULL,
	[admin_id] [varchar](50) NULL,
	[emailFormat] [varchar](50) NULL,
	[creditRatio] [varchar](50) NULL,
	[dateCreated] [datetime] NULL,
	[dateUpdated] [datetime] NULL,
	[dateApproved] [datetime] NULL,
	[approvedBy] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_CUSTOMER_APPLICATION]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRWEB_V1_CUSTOMER_APPLICATION](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[application_number] [varchar](100) NULL,
	[referenceNumber] [varchar](100) NULL,
	[netIncome] [varchar](100) NULL,
	[loanAmount] [varchar](100) NULL,
	[loanTerms] [varchar](100) NULL,
	[loanPurpose] [varchar](100) NULL,
	[loanPurposeID] [varchar](100) NULL,
	[loanPurposeOthers] [varchar](100) NULL,
	[groupCode] [varchar](100) NULL,
	[creditOption] [varchar](100) NULL,
	[creditOptionID] [varchar](100) NULL,
	[nameToDisplay] [varchar](100) NULL,
	[accountNumber] [varchar](50) NULL,
	[bankName] [varchar](50) NULL,
	[attachment1] [varchar](100) NULL,
	[attachment2] [varchar](100) NULL,
	[attachment3] [varchar](100) NULL,
	[attachment4] [varchar](100) NULL,
	[attachment5] [varchar](100) NULL,
	[legalID] [varchar](100) NULL,
	[nameOnID] [varchar](100) NULL,
	[documentName] [varchar](100) NULL,
	[documentNameID] [varchar](100) NULL,
	[issueAuth] [varchar](100) NULL,
	[issueAuthID] [varchar](100) NULL,
	[issueDate] [varchar](100) NULL,
	[expirationDate] [varchar](100) NULL,
	[sol_id] [varchar](100) NULL,
	[submit_cps] [varchar](100) NULL,
	[reason_decline] [varchar](200) NULL,
	[status] [varchar](100) NULL,
	[sent_status] [varchar](100) NULL,
	[dateCreated] [varchar](100) NULL,
	[dateUpdated] [varchar](100) NULL,
	[hrID] [varchar](100) NULL,
	[dateSubmitted] [varchar](100) NULL,
	[token] [varchar](100) NULL,
	[tokenExpire] [varchar](50) NULL,
	[OTP] [varchar](50) NULL,
	[OTPExpiration] [varchar](50) NULL,
	[dateSigned] [varchar](50) NULL,
	[dateApproved] [varchar](100) NULL,
	[dateDeclined] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_CUSTOMER_INFO]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRWEB_V1_CUSTOMER_INFO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[salutation] [varchar](50) NULL,
	[firstName] [varchar](100) NULL,
	[middleName] [varchar](100) NULL,
	[lastName] [varchar](100) NULL,
	[suffix] [varchar](10) NULL,
	[birthPlace] [varchar](100) NULL,
	[birthDate] [varchar](100) NULL,
	[age] [varchar](100) NULL,
	[gender] [varchar](100) NULL,
	[religion] [varchar](100) NULL,
	[citizenship] [varchar](100) NULL,
	[civilStatus] [varchar](100) NULL,
	[civilStatusID] [varchar](100) NULL,
	[TIN] [varchar](100) NULL,
	[SSS] [varchar](100) NULL,
	[GSIS] [varchar](100) NULL,
	[spouseSalutation] [varchar](100) NULL,
	[spouseFirstname] [varchar](100) NULL,
	[spouseMiddlename] [varchar](100) NULL,
	[spouseLastname] [varchar](100) NULL,
	[spouseSuffix] [varchar](10) NULL,
	[spouseGender] [varchar](10) NULL,
	[spouseBirthDate] [varchar](100) NULL,
	[spouseAge] [varchar](100) NULL,
	[dependents] [varchar](100) NULL,
	[spouseEmployer] [varchar](100) NULL,
	[spouseOccupation] [varchar](100) NULL,
	[spouseMonthlyIncome] [varchar](100) NULL,
	[spouseNumber] [varchar](100) NULL,
	[Present_Address] [varchar](100) NULL,
	[Present_Province] [varchar](100) NULL,
	[Present_ProvinceID] [varchar](100) NULL,
	[Present_City] [varchar](100) NULL,
	[Present_CityID] [varchar](100) NULL,
	[Present_Barangay] [varchar](100) NULL,
	[Present_BarangayID] [varchar](100) NULL,
	[Present_Country] [varchar](100) NULL,
	[Present_Zipcode] [varchar](100) NULL,
	[Present_Ownership] [varchar](100) NULL,
	[Present_OwnershipID] [varchar](100) NULL,
	[Present_Years] [varchar](100) NULL,
	[Present_Months] [varchar](100) NULL,
	[Present_LengthOfStay] [varchar](100) NULL,
	[Present_Telephone] [varchar](100) NULL,
	[Permanent_Address] [varchar](100) NULL,
	[Permanent_Province] [varchar](100) NULL,
	[Permanent_ProvinceID] [varchar](100) NULL,
	[Permanent_City] [varchar](100) NULL,
	[Permanent_CityID] [varchar](100) NULL,
	[Permanent_Barangay] [varchar](100) NULL,
	[Permanent_BarangayID] [varchar](100) NULL,
	[Permanent_Country] [varchar](100) NULL,
	[Permanent_Zipcode] [varchar](100) NULL,
	[Permanent_Ownership] [varchar](100) NULL,
	[Permanent_OwnershipID] [varchar](100) NULL,
	[Permanent_Years] [varchar](100) NULL,
	[Permanent_Months] [varchar](100) NULL,
	[Permanent_LengthOfStay] [varchar](100) NULL,
	[Permanent_Telephone] [varchar](100) NULL,
	[dateHired] [varchar](100) NULL,
	[tenure] [varchar](100) NULL,
	[employeeNumber] [varchar](100) NULL,
	[rank] [varchar](100) NULL,
	[department] [varchar](100) NULL,
	[monthlyAllowance] [varchar](100) NULL,
	[occupation] [varchar](100) NULL,
	[natureOfWork] [varchar](100) NULL,
	[natureOfWorkID] [varchar](100) NULL,
	[sourceOfIncomeOthers] [varchar](100) NULL,
	[monthlyIncomeOthers] [varchar](100) NULL,
	[Employer_Address] [varchar](100) NULL,
	[Employer_Province] [varchar](100) NULL,
	[Employer_ProvinceID] [varchar](100) NULL,
	[Employer_City] [varchar](100) NULL,
	[Employer_CityID] [varchar](100) NULL,
	[Employer_Barangay] [varchar](100) NULL,
	[Employer_BarangayID] [varchar](100) NULL,
	[Employer_Country] [varchar](100) NULL,
	[Employer_Zipcode] [varchar](100) NULL,
	[Employer_Telephone] [varchar](100) NULL,
	[employee_reference] [varchar](100) NULL,
	[dateCreated] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PASSWORD_HISTORY]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PASSWORD_HISTORY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NOT NULL,
	[pastPassword] [varchar](50) NULL,
	[dateUpdated] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SCHEMECODE]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCHEMECODE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[schemeCode] [varchar](50) NULL,
	[product] [varchar](50) NULL,
	[interestRate] [float] NULL,
	[bankCharge] [float] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AUDIT_TRAIL] ADD  CONSTRAINT [DF_AUDIT_TRAIL_dateCreated]  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [dbo].[LRWEB_V1_ADMIN_CRED] ADD  CONSTRAINT [DF_EPAY_V2_ADMIN_CRED_status]  DEFAULT ('ACTIVE') FOR [status]
GO
ALTER TABLE [dbo].[LRWEB_V1_ADMIN_CRED] ADD  CONSTRAINT [DF_EPAY_V2_ADMIN_CRED_sent_status]  DEFAULT ((0)) FOR [sent_status]
GO
ALTER TABLE [dbo].[LRWEB_V1_ADMIN_CRED] ADD  CONSTRAINT [DF_EPAY_V2_ADMIN_CRED_attempt]  DEFAULT ((0)) FOR [attempt]
GO
ALTER TABLE [dbo].[LRWEB_V1_ADMIN_CRED] ADD  CONSTRAINT [DF_EPAY_V2_ADMIN_CRED_newPassword]  DEFAULT ('YES') FOR [newPassword]
GO
ALTER TABLE [dbo].[LRWEB_V1_ADMIN_CRED] ADD  CONSTRAINT [DF_EPAY_V2_ADMIN_CRED_dateCreated]  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ADD  CONSTRAINT [DF_LRWEB_V1_CLIENT_ACCESS_ID_attempt]  DEFAULT ((0)) FOR [attempt]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ADD  CONSTRAINT [DF_LRWEB_V1_CLIENT_ACCESS_ID_sent_status]  DEFAULT ((0)) FOR [sent_status]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ADD  CONSTRAINT [DF_LRWEB_V1_CLIENT_ACCESS_ID_status]  DEFAULT ('ACTIVE') FOR [status]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ADD  CONSTRAINT [DF_LRWEB_V1_CLIENT_ACCESS_ID_newPassword]  DEFAULT ('YES') FOR [newPassword]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ADD  CONSTRAINT [DF_LRWEB_V1_CLIENT_ACCESS_ID_dateCreated]  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ADD  CONSTRAINT [DF_EPAY_V2_CLIENT_EMPLOYEE_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ADD  CONSTRAINT [DF_EPAY_V2_CLIENT_EMPLOYEE_SubmitStatus]  DEFAULT ((0)) FOR [SubmitStatus]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ADD  CONSTRAINT [DF_EPAY_V2_CLIENT_EMPLOYEE_submit_cps]  DEFAULT ('NOT') FOR [submit_cps]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENTS] ADD  CONSTRAINT [DF_LRWEB_V1_CLIENTS_creditRatio]  DEFAULT ((30)) FOR [creditRatio]
GO
ALTER TABLE [dbo].[LRWEB_V1_CLIENTS] ADD  CONSTRAINT [DF_LRWEB_V1_CLIENTS_dateCreated]  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ADD  CONSTRAINT [DF_LRWEB_V1_CUSTOMER_APPLICATION_application_number]  DEFAULT ('TDB') FOR [application_number]
GO
ALTER TABLE [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ADD  CONSTRAINT [DF_LRWEB_V1_CUSTOMER_APPLICATION_submit_cps]  DEFAULT ((0)) FOR [submit_cps]
GO
ALTER TABLE [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ADD  CONSTRAINT [DF_LRWEB_V1_CUSTOMER_APPLICATION_status]  DEFAULT ('PENDING') FOR [status]
GO
ALTER TABLE [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ADD  CONSTRAINT [DF_LRWEB_V1_CUSTOMER_APPLICATION_sent_status]  DEFAULT ((0)) FOR [sent_status]
GO
ALTER TABLE [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ADD  CONSTRAINT [DF_LRWEB_V1_CUSTOMER_APPLICATION_dateCreated]  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [dbo].[LRWEB_V1_CUSTOMER_INFO] ADD  CONSTRAINT [DF_LRWEB_V1_CUSTOMER_INFO_dateCreated]  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [dbo].[PASSWORD_HISTORY] ADD  CONSTRAINT [DF_PASSWORD_HISTORY_dateUpdated]  DEFAULT (getdate()) FOR [dateUpdated]
GO
/****** Object:  StoredProcedure [dbo].[ael_stat_change]    Script Date: 12/22/2020 1:22:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chris
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ael_stat_change] 
	-- Add the parameters for the stored procedure here
	@appno varchar(50), 
	@stat varchar(50)
	--@message varchar(50) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @result nvarchar(50)
	DECLARE @token nvarchar(50)
	DECLARE @exdate nvarchar(50)
	DECLARE @message nvarchar(50)
	SET @token = @appno+CONVERT(CHAR(32), CRYPT_GEN_RANDOM(20),2)
	SET @exdate = format(DATEADD(day,7,getdate()),'yyyyMMddHHmmss')
    -- Insert statements for procedure here
	IF @stat = 'APPROVED'
		BEGIN
			UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET 
			status = 'FOR SIGNING', 
			token = @token, 
			tokenExpire = @exdate,
			sent_status = '1'
			WHERE application_number = @appno
		END
	ELSE IF (@stat = 'NEW' OR 
	@stat = 'ONPROCESS' OR 
	@stat = 'PRINTED' OR 
	@stat = 'DISBURSED' OR
	@stat = 'UNPOSTED' OR 
	@stat = 'REPROCESS')
		BEGIN
			UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET 
			status = 'ON PROCESS',
			dateUpdated = GETDATE()
			WHERE application_number = @appno
		END
	ELSE IF @stat = 'POSTED'
		BEGIN
			UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET 
			status = 'APPROVED/DISBURSED', 
			sent_status = '1',
			dateApproved = GETDATE()
			WHERE application_number = @appno
		END
	ELSE IF @stat = 'CANCELLED'
		BEGIN
			UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET 
			status = 'CANCELLED', 
			sent_status = '1',
			dateDeclined = GETDATE()
			WHERE application_number = @appno
		END
	IF @@ROWCOUNT <> 0 
		SET @message = N'SUCCESS';
  ELSE SET @message = N'FAILED';
SELECT @message;
END
GO
USE [master]
GO
ALTER DATABASE [LRWEB] SET  READ_WRITE 
GO
