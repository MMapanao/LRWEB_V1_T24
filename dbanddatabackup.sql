USE [master]
GO
/****** Object:  Database [LRWEB]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[AUDIT_TRAIL]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[CSB_BRANCH]    Script Date: 12/22/2020 1:25:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSB_BRANCH](
	[BR_DESC] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRWEB_V1_ADMIN_CRED]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[LRWEB_V1_AUTH]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[LRWEB_V1_CLIENT_ACCESS_ID]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[LRWEB_V1_CLIENT_EMPLOYEE]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[LRWEB_V1_CLIENTS]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[LRWEB_V1_CUSTOMER_APPLICATION]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[LRWEB_V1_CUSTOMER_INFO]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[PASSWORD_HISTORY]    Script Date: 12/22/2020 1:25:37 PM ******/
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
/****** Object:  Table [dbo].[SCHEMECODE]    Script Date: 12/22/2020 1:25:37 PM ******/
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
SET IDENTITY_INSERT [dbo].[AUDIT_TRAIL] ON 

INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-10T14:39:20.530' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2, N'another1@citysavings.com.ph', N'Added Group Code TUT01', CAST(N'2020-11-10T14:44:10.783' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (3, N'fortunochristian@yahoo.com', N'Successfully Login as On-Boarding User', CAST(N'2020-11-10T14:45:48.017' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (4, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-10T14:46:14.470' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (5, N'fortunochristian@yahoo.com', N'Successfully Login as On-Boarding User', CAST(N'2020-11-10T14:46:31.377' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (6, N'fortunochristian@yahoo.com', N'Verified Group Code TUT01', CAST(N'2020-11-10T14:46:43.377' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (7, N'fortunochristian@yahoo.com', N'Added HR fortunochristian@yahoo.com on Group Code TUT01', CAST(N'2020-11-10T14:47:56.283' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (8, N'fortunochristian@yahoo.com', N'Password Change', CAST(N'2020-11-10T14:50:37.813' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (9, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T14:50:39.267' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (10, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T14:56:04.110' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (12, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T15:31:48.377' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (13, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T15:38:49.437' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (14, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T16:14:34.190' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (15, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T16:35:28.440' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (16, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T16:40:52.970' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (17, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T16:56:45.330' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (18, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:00:56.160' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (19, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:04:11.237' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (20, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:13:00.347' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (21, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:15:03.503' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (22, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:17:02.080' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (23, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:18:10.143' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (24, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:19:09.283' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (25, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:24:13.733' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (26, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:25:37.640' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (27, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T17:42:04.030' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1002, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-17T12:31:08.807' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1003, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-17T17:08:34.930' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1004, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-18T17:12:38.697' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2002, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T13:12:28.483' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2003, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T13:13:28.610' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2004, N'fortunochristian@yahoo.com', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T13:17:54.207' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2005, N'fortunochristian@yahoo.com', N'Verified Group Code YTP01', CAST(N'2020-11-23T13:18:04.677' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2006, N'fortunochristian@yahoo.com', N'Added HR c.wew22@gmail.com on Group Code YTP01', CAST(N'2020-11-23T13:18:57.333' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2007, N'c.wew22@gmail.com', N'Password Change', CAST(N'2020-11-23T13:19:52.570' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2008, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T13:19:53.927' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2009, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T14:16:48.863' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2010, N'another1@citysavings.com.ph', N'Updated Group Code YTP01', CAST(N'2020-11-23T14:18:03.037' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2011, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T14:25:46.230' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2012, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T14:40:54.487' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2013, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-23T14:41:24.847' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2014, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T14:41:46.707' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2015, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-23T14:49:40.163' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2016, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T14:49:49.460' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2017, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-23T14:55:39.510' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2018, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T14:55:48.680' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2019, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T15:06:43.060' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2020, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T15:10:14.467' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2021, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T15:19:40.970' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2022, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-23T15:26:40.753' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2023, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T15:26:49.740' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2024, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T16:16:28.643' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2025, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T16:24:19.583' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2026, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T16:44:55.990' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2027, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T17:00:25.867' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2028, N'another1@citysavings.com.ph', N'Added Group Code HH195', CAST(N'2020-11-23T17:01:32.040' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2029, N'fortunochristian@yahoo.com', N'Successfully Login as On-Boarding User', CAST(N'2020-11-23T17:02:24.913' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2030, N'fortunochristian@yahoo.com', N'Verified Group Code HH195', CAST(N'2020-11-23T17:02:40.083' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2031, N'fortunochristian@yahoo.com', N'Added HR fortunochristian22@gmail.com on Group Code HH195', CAST(N'2020-11-23T17:03:39.803' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2032, N'fortunochristian22@gmail.com', N'Password Change', CAST(N'2020-11-23T17:04:21.773' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2033, N'fortunochristian22@gmail.com', N'Successfully Login as HR on BRILLIANT METAL CRAFT AND METAL DESIGN.', CAST(N'2020-11-23T17:04:23.833' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2034, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-23T17:21:43.290' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2035, N'fortunochristian22@gmail.com', N'Successfully Login as HR on BRILLIANT METAL CRAFT AND METAL DESIGN.', CAST(N'2020-11-23T17:21:52.213' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2036, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T17:23:17.167' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2037, N'fortunochristian22@gmail.com', N'Successfully Login as HR on BRILLIANT METAL CRAFT AND METAL DESIGN.', CAST(N'2020-11-23T17:24:11.353' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2038, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T17:29:33.810' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2039, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-23T18:44:30.497' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2040, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T18:44:52.340' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2041, N'c.wew22@gmail.com', N'Successfully Login as HR on Yokogawa Techno Philippines.', CAST(N'2020-11-23T18:52:55.013' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (2042, N'fortunochristian22@gmail.com', N'Successfully Login as HR on BRILLIANT METAL CRAFT AND METAL DESIGN.', CAST(N'2020-11-23T18:53:12.263' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (11, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-10T15:10:19.080' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1005, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-19T12:28:32.010' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1006, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-19T12:28:40.570' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1007, N'fortunochristian@yahoo.com', N'Successfully Login as HR on TUTELA MARINE.', CAST(N'2020-11-19T12:28:46.697' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1008, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-19T12:40:36.760' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1009, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-19T13:11:07.387' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1010, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-19T13:16:58.747' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1011, N'another1@citysavings.com.ph', N'Added Group Code YTP01', CAST(N'2020-11-19T13:18:22.573' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1012, N'another1@citysavings.com.ph', N'Successfully Login as On-Boarding User', CAST(N'2020-11-19T13:28:35.947' AS DateTime))
INSERT [dbo].[AUDIT_TRAIL] ([ID], [userName], [action], [dateCreated]) VALUES (1013, N'another1@citysavings.com.ph', N'Updated Group Code TUT01', CAST(N'2020-11-19T13:28:59.230' AS DateTime))
SET IDENTITY_INSERT [dbo].[AUDIT_TRAIL] OFF
GO
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Alaminos Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Angeles Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Anonas Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Antique Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Bacolod Branch Head')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Bacolod Branch Loans & Coll')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Bacoor Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Baguio Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Balamban BBH')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Balamban BOH')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Bogo Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Bogo Branch Ops')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Borongan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Borongan Branch Loans & Coll')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Butuan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Cabanatuan Marketing')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Cagayan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Calamba Branch ')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Calamba Branch Manager')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Calapan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Calbayog Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Caloocan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Carcar Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Catarman Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Catarman Sales')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Daet Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Dagupan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Dasmarinas Branch/ROH R4')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Dasmarinas, Cavite Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Davao Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Digos Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Dipolog Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Dumaguete Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Espana  Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Gensan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Gumaca Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Head Office Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Iloilo Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Infanta Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Iriga Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Kabankalan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Kalibo Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Kidapawan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Koronadal Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'La Union Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Laoag Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Las Pinas Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Leganes Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Legazpi Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Legazpi Branch-ROH 5')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Lemery Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Lipa Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Lucena Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Mandaue Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Marikina Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Marikina Branch/ROH-NCR')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Marilao Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Masbate Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Mati Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Naga - Operations')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Naga - Sales & Marketing')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Navotas Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'North Caloocan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Olongapo Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Ormoc Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Ormoc Branch Head')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Ortigas Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Ortigas Branch Cashier')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Pagadian Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Puerto Princesa, Palawan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Pulilan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Roxas Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'San Carlos Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'San Fernando, Pampanga Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'San Jose Occ. Mindoro')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Santiago, Isabela Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'SJ Nueva Ecija Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'SJDM  Bulacan')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Sogod Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Sogod Branch Coll')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Solano Nueva Vizcaya')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Sorsogon Branch Head')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Sorsogon Regional Head')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Sta. Cruz Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Surigao Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tacloban Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tacloban Sales')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tagbilaran Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tagbilaran Branch Manager')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Taguig Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tagum Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tanjay Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tarlac Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Taytay Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tondo Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tuguegarao Branch Head')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tuguegarao Branch OIC')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Ubay Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Urdaneta Branch')
GO
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Urdaneta Branch AA')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Valenzuela Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Vigan Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Zamboanga Branch')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Aparri')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Balanga, Bataan')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Baler, Aurora')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Bangued, Abra')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Batangas')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Bontoc, Mt. Province')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Cabarroguis, Quirino')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Cadiz')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Catbalogan')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Danao')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Estancia, Iloilo')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Glan, Sarangani')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Goa')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Iligan')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Isulan, Sultan Kuldarat')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Jordan, Guimaras')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Lagawe, Ifugao')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Luna, Apayao')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Malita, Davao Occidental')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Mangagoy, Bislig, Surigao del Sur')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Meycauayan, Bulacan')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Nabunturan, Compostela Valley')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Naval, Biliran')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'San Franciso, Agusan Del Sur')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Sibugay - Ipil, Zambo. Sibugay')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Sta.Rosa, Laguna')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tabuk, Kalinga')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Tandag')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Valencia, Bukidnon')
INSERT [dbo].[CSB_BRANCH] ([BR_DESC]) VALUES (N'Virac, Catanduanes')
GO
SET IDENTITY_INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ON 

INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000000, N'NOT SUPER', N'ADMIN', N'ADMIN', N'09178262313', N'admin@citysavings.com.ph', N'123456Abcde!123', NULL, NULL, N'ADMIN', N'ACTIVE', N'1', 0, N'NO', CAST(N'1992-11-22T00:00:00.000' AS DateTime), NULL, CAST(N'2020-01-27T15:39:55.440' AS DateTime))
INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000008, N'CHRISTIAN', N'FORTUNO', N'ODENA', N'9161844861', N'fortunochristian@yahoo.com', N'123456Abcde!', N'8NV493FH', N'202009030510', N'ON-BOARDING USER', N'ACTIVE', N'1', 0, N'NO', CAST(N'2020-05-18T20:55:42.207' AS DateTime), CAST(N'2020-05-29T14:09:57.933' AS DateTime), CAST(N'2020-05-29T14:09:57.933' AS DateTime))
INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000011, N'ANOTHER', N'ONE', N'PO', N'9123123123', N'another1@citysavings.com.ph', N'123456Abcde@!', N'', N'', N'ON-BOARDING USER', N'ACTIVE', N'1', 0, N'NO', CAST(N'2020-07-14T13:58:58.467' AS DateTime), CAST(N'2020-09-09T13:54:31.073' AS DateTime), CAST(N'2020-09-09T13:54:31.073' AS DateTime))
INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000014, N'MA. LOUISA', N'MADRID', N'SANTIAGO', N'9178364698', N'malouisa.madrid@citysavings.com.ph', N'Louisa@12345', N'', N'', N'ON-BOARDING USER', N'ACTIVE', N'1', 0, N'NO', CAST(N'2020-09-09T10:25:33.380' AS DateTime), CAST(N'2020-10-06T15:06:02.157' AS DateTime), CAST(N'2020-10-06T15:06:02.157' AS DateTime))
INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000013, N'QWESAD', N'QWESAD', N'QWEDSA', N'9123123123', N'sadlayp@citysavings.com.ph', N'123456Abcde!', NULL, NULL, N'ON-BOARDING USER', N'ACTIVE', N'1', 0, N'NO', CAST(N'2020-09-03T03:29:19.607' AS DateTime), CAST(N'2020-09-03T03:30:29.560' AS DateTime), CAST(N'2020-09-03T03:30:29.560' AS DateTime))
INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000015, N'ANGELICA', N'AGUILAR', NULL, N'9178600472', N'angelica.aguilar@citysavings.com.ph', N'MOGMC1NLG5', NULL, NULL, N'ON-BOARDING USER', N'ACTIVE', N'1', 0, N'YES', CAST(N'2020-10-13T21:55:08.967' AS DateTime), NULL, NULL)
INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000016, N'MA CRISTINA', N'DARIO', NULL, N'9772774613', N'macristina.dario@citysavings.com.ph', N'XJTG28EHE5', NULL, NULL, N'ON-BOARDING USER', N'ACTIVE', N'1', 0, N'YES', CAST(N'2020-10-13T21:56:18.073' AS DateTime), NULL, NULL)
INSERT [dbo].[LRWEB_V1_ADMIN_CRED] ([IntRecord], [firstname], [lastname], [middlename], [contactnumber], [email], [password], [OTPCode], [OTPDateTime], [accesslevel], [status], [sent_status], [attempt], [newPassword], [dateCreated], [dateUpdated], [passwordUpdated]) VALUES (1000017, N'JOCELYN', N'RABANG', NULL, N'9176512164', N'jocelyn.rabang@citysavings.com.ph', N'VCNGA6B92Y', NULL, NULL, N'ON-BOARDING USER', N'ACTIVE', N'1', 0, N'YES', CAST(N'2020-10-13T21:57:20.277' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[LRWEB_V1_ADMIN_CRED] OFF
GO
SET IDENTITY_INSERT [dbo].[LRWEB_V1_AUTH] ON 

INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1, N'ON-BOARDING USER', N'1000011', N'fVcpF2OyIuny1OeZqAiaJmpJskkoP4i3PgrR0qrW2VVVkUQ54fGibpj2pS6fRjjO', N'20201110143920', N'20201111053920', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2, N'ON-BOARDING USER', N'1000008', N'RDwrtMcKMPxLAbd6q5gwhpE4zHH2udJA4xUwDtdGgNfWNFHqvFcQN10l812mFjZt', N'20201110144547', N'20201111054547', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (3, N'ON-BOARDING USER', N'1000011', N'2DdgG9caShAsOXdDDEYlnLKfYMYQ9Dy2NgvZQMGBuzFWqXXB0dUX2Tcr28H3kLbO', N'20201110144614', N'20201111054614', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (4, N'ON-BOARDING USER', N'1000008', N'8nyQCeZaddJ8HNycUXaOA2PNpnjMNX2254OHGQOnMNDDUyosc73yz5JFYlkY5NNF', N'20201110144631', N'20201111054631', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (5, N'TUT01', N'fortunochristian@yahoo.com', N'w6fQ0EKhNMxA0MJRrCRFukmTKemRix4zTtDIyH9DFZ2eUZM8t9TPlU4LUMFVb38a', N'20201110145039', N'20201110165039', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (6, N'TUT01', N'fortunochristian@yahoo.com', N'LpVrkvWwyJCqhqAoTCDqzwoLrH5bYpiShFwYKqdGA6WHAGuGGEoMUrFZAK9dz5YI', N'20201110145604', N'20201110165603', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (7, N'TUT01', N'fortunochristian@yahoo.com', N'TMNAsMxYQ2Lkhwdrl7T1GUTjVlm91vweMyRTGOz1OqMmO0VoKQYYw2S92npGjyge', N'20201110151018', N'20201110171018', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (8, N'TUT01', N'fortunochristian@yahoo.com', N'skVTq67IW5XuvlGladrVqRbi1WOXvDrHJcVplj5gd5FZC5vbsaFTIjZaIxbTrjXT', N'20201110153148', N'20201110173148', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (9, N'TUT01', N'fortunochristian@yahoo.com', N'1l0Kbez6WdRIXWgmx4d0Um4bW4FW3ysM6tXhAWKU5VV56uDxeyRv7tyPrYxgYdDi', N'20201110153849', N'20201110173849', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (10, N'TUT01', N'fortunochristian@yahoo.com', N'y9Chh7Y3hnnhK6f6c1x20SA6HS8ChAaj3cymTuJo8vQRoDKiCvZpkgtg9FaP9WVh', N'20201110161434', N'20201110181434', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (11, N'TUT01', N'fortunochristian@yahoo.com', N'cEQDOYrUIi7cPuUWHEAYonqV7gci9UQdJUfXbyBhI3Ov80Goqbmun8SzY5Gs5JUy', N'20201110163528', N'20201110183528', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (12, N'TUT01', N'fortunochristian@yahoo.com', N'8cz7FPsoBnLe7pcDytZu3KF5p2Ic30UBQawV0hgFILGdobpa8NUUbVFxX4SNGQuL', N'20201110164052', N'20201110184052', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (13, N'TUT01', N'fortunochristian@yahoo.com', N'5bxEmyJcZbcu0CeAUg8CVx7VGyVczTdwbBHTA7VbXUlJ4EjzEXlHihDIeb8xcrnF', N'20201110165645', N'20201110185645', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (14, N'TUT01', N'fortunochristian@yahoo.com', N'GvVMQ4NCh9Dv9rmv1nyE80f62o1usk9W9wYGtaCX9X1jbIilkveMarNPPZUmDdT7', N'20201110170056', N'20201110190056', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (15, N'TUT01', N'fortunochristian@yahoo.com', N'L5w1UqNMACwxrAt4k12a2TZSIsJtuLJYAPKiWdsUoOEFvcMuEEdQIZi2fdtmgdcz', N'20201110170411', N'20201110190411', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (16, N'TUT01', N'fortunochristian@yahoo.com', N'GMru3WGHAiJGVadxD15lGiefSSD8xMFkVuAPeX3k5VLxSMoeY0S5eCfirMckSITx', N'20201110171300', N'20201110191300', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (17, N'TUT01', N'fortunochristian@yahoo.com', N'vOf6PQUgg0Zt5zgGnd1khRzxBwaurUgH8Q1pwY19OJhY9NlZ8jdSrkjdYr5dzjyL', N'20201110171503', N'20201110191503', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (18, N'TUT01', N'fortunochristian@yahoo.com', N'HvqlouFRzAZ4TcGD1hNvs2lXxYCf17IWCCnaAocYW2wWaRvn0yJy52IOJTxQrjZ2', N'20201110171701', N'20201110191701', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (19, N'TUT01', N'fortunochristian@yahoo.com', N'rqKRIp38YL7C80fvZ53TvAbkRQoFHaojvm8HKU7mXkpB1Tk3KBFevH1rOk91Ax18', N'20201110171810', N'20201110191810', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (20, N'TUT01', N'fortunochristian@yahoo.com', N'XIyY145HZtb476sM886MzlgCCymBtVR0Ur8BoG6Z4Mx8QXnvSdW16anwmvVCS3XD', N'20201110171909', N'20201110191909', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (21, N'TUT01', N'fortunochristian@yahoo.com', N'tYhDX6f183eTN1rA2UiqLuziNtVizxF02BRlQ1O3MgPbkvUd5JF5XKi9j90nl7BL', N'20201110172413', N'20201110192413', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (22, N'TUT01', N'fortunochristian@yahoo.com', N'7y4qqqLsrbpOxHSy3ZEFXNqpRXksKba32cuqpDTheNGmGTW8iKKMReqtHPYSFdhP', N'20201110172537', N'20201110192537', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (23, N'TUT01', N'fortunochristian@yahoo.com', N'ieQcOB9AN77I2eQZetNt8sI3yK8dP2UG9oVBVlcYRcGjAoSdS6j98WH0VXoDCfvV', N'20201110174203', N'20201110194203', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1002, N'TUT01', N'fortunochristian@yahoo.com', N'2qdd82onE4AoxOk2YuMyHF8m4po2goyccEc21QGeBHFD3UKfFQaOHFxxr0iTOwGa', N'20201117123108', N'20201117143108', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1003, N'TUT01', N'fortunochristian@yahoo.com', N'w7YIrMDaln2WLKTFbesj0fYBlouxXrmTLvKTkE6lvii07RYmqZiOVstQjWgCbPC3', N'20201117170834', N'20201117190834', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1004, N'ON-BOARDING USER', N'1000011', N'h95h0svXaABa4XzEnI17qfy5ZEctpuzVijCTLtCt7YlvaCDvUXe8SKuBL9HvQBrq', N'20201118171238', N'20201119081238', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2002, N'ON-BOARDING USER', N'1000011', N'f4Beur0qxzb8RPcGpeL7VvFd20N8RsNExuLA6NVo1bjGnRGG35lqopCuyhl4d1YE', N'20201123131228', N'20201124041228', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2003, N'ON-BOARDING USER', N'1000011', N'Dnl78Sik1VrmRyMAhYUDvaK8TzvcR3VHe8TJb9HneNv4ENah40IFPcZndnoJhFS8', N'20201123131328', N'20201124041328', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2004, N'ON-BOARDING USER', N'1000008', N'LsJJcUIqrcEyNx2CiLv7ZOdgYe6uPG2UfeHysEQY8CG4rXGyQkTJnE06Onu8XXbl', N'20201123131754', N'20201124041754', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2005, N'YTP01', N'c.wew22@gmail.com', N'lQ7v34JykR9OHoiouAe9zJnGyYOq3f7DFt7sEuihqLS3hSallD4mjIUcm07eqc4E', N'20201123131953', N'20201123151953', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2007, N'ON-BOARDING USER', N'1000011', N'IeOI4eKkLhpTbrN09RYMkkbGyNLDcIc6AKKIax5wcmViu8EpqBnt9BVhCIUrTHIC', N'20201123142546', N'20201124052546', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2008, N'ON-BOARDING USER', N'1000011', N'jKo9jks1jH2JLBFEdDYpOIHcxhnHT2EVYHw3HOYjw2wC30XPctrI2X5aDMLB6khq', N'20201123144054', N'20201124054054', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2009, N'TUT01', N'fortunochristian@yahoo.com', N'nxnNyrQ4psUnk0XNS5YzGkY7ozyo31w8KsZTzEYNuf31QI3vbXBsGo7DZqi92mAy', N'20201123144124', N'20201123164124', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2010, N'YTP01', N'c.wew22@gmail.com', N'6Hllv13s0gB166Z9AJeUft9GCbr5PNuHEFmOvRNLKHi87BhArFJFMxUNHfjUJ7cn', N'20201123144146', N'20201123164146', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2011, N'TUT01', N'fortunochristian@yahoo.com', N'Nnh6x4oMyVMt9WghsmwaXzbnX4ANiiSJ4lJCuAXRbz66N4BGNvsFNQrYL3j34anQ', N'20201123144940', N'20201123164940', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2012, N'YTP01', N'c.wew22@gmail.com', N'8WMlPXPBPxyWOWvPKXVeWjVQED5JZGCvGjTwTFNtu8KDmc2xsMdi5QvYB5gMbFmJ', N'20201123144949', N'20201123164949', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2013, N'TUT01', N'fortunochristian@yahoo.com', N'5hDD5FILI58WUArbvKujOvfoYQ1EjWPtBsCsorI2U363MpCfEil5AwDJBYppOEmv', N'20201123145539', N'20201123165539', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2014, N'YTP01', N'c.wew22@gmail.com', N'8EnYeZxz4ONajr6hRSFBwLoK1bMdbwNgV6iorAcgoCKXx56ZAhBP5gawZdhDNUXH', N'20201123145548', N'20201123165548', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2015, N'YTP01', N'c.wew22@gmail.com', N'3nlCwnUGt64UxhlHZBqynAWgn2YOfIRk9vVx7oNO1lf9F1c3Ff4qnT13REY3PGll', N'20201123150643', N'20201123170643', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2016, N'YTP01', N'c.wew22@gmail.com', N'8F7EIFCusx3vHvxftYlIj8LNcEfqOasiJ9cQbtaCbkr0wtuw99Rt8OP94tlEkWfS', N'20201123151014', N'20201123171014', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2017, N'YTP01', N'c.wew22@gmail.com', N'Dqy43VkohYkR0h16sihx9ZYlPAD0SkDkkqheYZHGIUc8CQubZVf9oGyoRMp2RuV6', N'20201123151940', N'20201123171940', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2018, N'TUT01', N'fortunochristian@yahoo.com', N'XYiMi9uG76ql1dxqEZsENCdBRBjVxQ61K22LwbNF9fr9HmyLzhM1umzV5h4hZYSq', N'20201123152640', N'20201123172640', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2019, N'YTP01', N'c.wew22@gmail.com', N'ddUdldXxTmfZ9A5gylQWosKYElS4f18cKKDop7Bw6OiZGfr2AhgDzISvT5Z9LdSc', N'20201123152649', N'20201123172649', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2020, N'YTP01', N'c.wew22@gmail.com', N'5eDAPRBeLQobQT8qxGs5z4k8BmE4PxjX5cxYihXiEQXEdSK298IAklDA4E9nNGPY', N'20201123161628', N'20201123181628', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2021, N'YTP01', N'c.wew22@gmail.com', N'trTuaW1WhatGea8B3ljXG2PTFcYogeFDzShSKzh4BJbFYgEVb3wM9KG1cAo87M0C', N'20201123162419', N'20201123182419', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2022, N'YTP01', N'c.wew22@gmail.com', N'hoDYNTelajAus1CRhEF8BxEETEo2O6UznlQeV42wFSkoUr4t85Rnxfrtj9EIomWe', N'20201123164456', N'20201123184456', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2023, N'ON-BOARDING USER', N'1000011', N'XZytdZgnR0SyfFRWk3TWcH2sPK2yhx3ZWBKfJvmnMNlguXxFZppL3eXQgFeShrGe', N'20201123170025', N'20201124080025', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2024, N'ON-BOARDING USER', N'1000008', N'mvZSxRggIz6nYWM2vTfVeJ1UQ7pq9XVCr3IgyEbFDT26L5jMcPmfjkhc4FB0l0gv', N'20201123170224', N'20201124080224', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2025, N'HH195', N'fortunochristian22@gmail.com', N'axHBkBXwcS9Pgf5AvGYuXt1Q19miDya39TMKYIoal7Qu0SncSE0dXqxq50JkZyto', N'20201123170423', N'20201123190423', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2026, N'TUT01', N'fortunochristian@yahoo.com', N'XShhZMCAp5Mz5dNHODcZUNoGhHKnqxklgQiA9osg2P0e80wlBKbheieKnbARBZT2', N'20201123172143', N'20201123192143', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2027, N'HH195', N'fortunochristian22@gmail.com', N'DIY9uIN2xcezh6Wl3OTeCpIc13MOK8c5TJ7QZdjLskAVCc6Xg56UmYXYA6I059ry', N'20201123172152', N'20201123192152', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2028, N'YTP01', N'c.wew22@gmail.com', N'Cjdb6cXaQfdcxR00B6myJaZXdNV68kAdrokVPxjQfqB7TYcTvjywbIqmKF7sGbbp', N'20201123172317', N'20201123192317', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2029, N'HH195', N'fortunochristian22@gmail.com', N'2gWcaivrTAu3HRvSwK4j4zbPEbxsj6TmWGY4OnAfzGH8gpyiOlS82CzDEHY8vCIW', N'20201123172411', N'20201123192411', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1005, N'TUT01', N'fortunochristian@yahoo.com', N'qLHoxO7jnJH0Q6KVsx9A1tYWHJC6m2anq0pTC9NUkaPBZxb0pnkFuQ27wuhoLA7v', N'20201119122831', N'20201119142831', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1006, N'TUT01', N'fortunochristian@yahoo.com', N'z89FCorlenaywigNGeOoI0cB3S4jBbSjgjAcfUviNYOVCjzL5VOhCVX8f7LuuIkC', N'20201119122840', N'20201119142840', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1007, N'TUT01', N'fortunochristian@yahoo.com', N'U5ZGxC0FxBu5JQhoiiStwm3XZ1KkHudzGgks7RKyb5q3FoK1e1ilU8EsBCr62Q8C', N'20201119122846', N'20201119142846', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1008, N'ON-BOARDING USER', N'1000011', N'AcuidaxLMyrfcBMMuunWWrfbUhLg6JAeWhosSXN7DG5w6sw57coKQLjS6TO6PQPC', N'20201119124036', N'20201120034036', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1009, N'ON-BOARDING USER', N'1000011', N'62geP3iEqfuHuNsj5fMFpM5tROA9whjzvCyHlkBiiLaLk72b2c5pOW4u7xMA2jRJ', N'20201119131107', N'20201120041107', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1010, N'ON-BOARDING USER', N'1000011', N'DUcbewsCbp10Lra6FzvlazrJNLFwgfoAYycuCKcHFCgTVSjBJxw6buNNmTOSr5f5', N'20201119131658', N'20201120041658', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (1011, N'ON-BOARDING USER', N'1000011', N'9Dd2CD2vspFCnE0ppKAye1VH4B91U0cU46EZ6AIpGZoswpJ0yFxMCU9IrW8AEAa2', N'20201119132835', N'20201120042835', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2006, N'ON-BOARDING USER', N'1000011', N'9SY7dcWJW4Qjrusyqc6OSprBon2rsLa7ylQlX0Yje5Gk8k3mp76x4qqTkXS0joaL', N'20201123141648', N'20201124051648', N'1')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2030, N'YTP01', N'c.wew22@gmail.com', N'7z8MVRziGYfgQCPBNuQpNNyB4A5CIUnPryULWYCrh7cvNzHKegyKnbbuB6RUVxav', N'20201123172933', N'20201123192933', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2031, N'TUT01', N'fortunochristian@yahoo.com', N'YrEC1EOSHugAg5KkLR4RwesarRajVflSc25e55QIMRHDtSIo5hs8xtZ38nVjnp7l', N'20201123184430', N'20201123204430', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2032, N'YTP01', N'c.wew22@gmail.com', N'hK0uLpoJY6UpdEheLVnQogLrZx4UnFewovGuqjuQvrmMe0Ovmf8BCDlB8JUXvUfS', N'20201123184452', N'20201123204452', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2033, N'YTP01', N'c.wew22@gmail.com', N'DwjNRtCV1bvCFDg7vldh3xyIwHa94ya9Lwf1hwnBwQw5Z6tVA4Qo2q9P8bbKSCbD', N'20201123185255', N'20201123205255', N'0')
INSERT [dbo].[LRWEB_V1_AUTH] ([IntRecord], [GroupCode], [EmployeeEmail], [token], [timestamp], [timeexpiration], [logout]) VALUES (2034, N'HH195', N'fortunochristian22@gmail.com', N'CxT7Eu6RmeQEpibByYtqfpXNq8r6fkLnMr2i4In105b6OJ9yDQ744xNXaFQGCAwC', N'20201123185312', N'20201123205312', N'0')
SET IDENTITY_INSERT [dbo].[LRWEB_V1_AUTH] OFF
GO
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ON 

INSERT [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ([ID], [groupCode], [lastName], [firstName], [middleName], [contactNumber], [email], [password], [OTPCode], [OTPDateTime], [attempt], [sent_status], [status], [newPassword], [dateCreated], [dateUpdated]) VALUES (1000000, N'TUT01', N'FORTUNO', N'CHRISTIAN', N'ACE', N'639161844862', N'fortunochristian@yahoo.com', N'123456Abcde!', N'', N'', 0, N'1', N'ACTIVE', N'NO', CAST(N'2020-11-10T14:47:56.283' AS DateTime), CAST(N'2020-11-10T14:50:37.813' AS DateTime))
INSERT [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ([ID], [groupCode], [lastName], [firstName], [middleName], [contactNumber], [email], [password], [OTPCode], [OTPDateTime], [attempt], [sent_status], [status], [newPassword], [dateCreated], [dateUpdated]) VALUES (1000001, N'YTP01', N'FORTUNO', N'CHRISTIAN', N'O', N'639161844862', N'c.wew22@gmail.com', N'123456Abcde!', N'', N'', 0, N'1', N'ACTIVE', N'NO', CAST(N'2020-11-23T13:18:57.333' AS DateTime), CAST(N'2020-11-23T13:19:52.570' AS DateTime))
INSERT [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] ([ID], [groupCode], [lastName], [firstName], [middleName], [contactNumber], [email], [password], [OTPCode], [OTPDateTime], [attempt], [sent_status], [status], [newPassword], [dateCreated], [dateUpdated]) VALUES (1000002, N'HH195', N'QWESDA', N'QWE', N'QWE', N'639161844687', N'fortunochristian22@gmail.com', N'123456Abcde!', N'', N'', 0, N'1', N'ACTIVE', N'NO', CAST(N'2020-11-23T17:03:39.803' AS DateTime), CAST(N'2020-11-23T17:04:21.787' AS DateTime))
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CLIENT_ACCESS_ID] OFF
GO
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ON 

INSERT [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ([IntRecord], [GroupCode], [last_name], [first_name], [middle_name], [contactnum], [email], [Tokens], [tokenSender], [Expiration], [Status], [Client_Branch], [SubmitStatus], [scheme_code], [submit_cps], [reason_decline], [PDF_querystring], [branch_delivery]) VALUES (1500000, N'TUT01', N'LASTNAAME', N'FIRSTNAME', N'MIDDLE', N'639161844862', N'fortunochristian22@gmail.com', N'', N'', N'', 1, N'MIDDLE', N'0', N'AE101', N'NOT', NULL, NULL, N' TONDO METRO MANILA')
INSERT [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ([IntRecord], [GroupCode], [last_name], [first_name], [middle_name], [contactnum], [email], [Tokens], [tokenSender], [Expiration], [Status], [Client_Branch], [SubmitStatus], [scheme_code], [submit_cps], [reason_decline], [PDF_querystring], [branch_delivery]) VALUES (1500001, N'TUT01', N'WEW', N'CHRIS', N'O', N'639161844863', N'c.wew22@gmail.com', N'', N'', N'', 1, N'O', N'0', N'AE101', N'NOT', NULL, NULL, N' TONDO METRO MANILA')
INSERT [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ([IntRecord], [GroupCode], [last_name], [first_name], [middle_name], [contactnum], [email], [Tokens], [tokenSender], [Expiration], [Status], [Client_Branch], [SubmitStatus], [scheme_code], [submit_cps], [reason_decline], [PDF_querystring], [branch_delivery]) VALUES (1501001, N'HH195', N'LASTNAAME', N'CHRISTIAN', N'ODENA', N'639161844862', N'fortunochristian22@gmail.com', N'', N'', N'', 1, N'ODENA', N'0', N'AE102', N'NOT', NULL, NULL, N' Masbate Branch')
INSERT [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ([IntRecord], [GroupCode], [last_name], [first_name], [middle_name], [contactnum], [email], [Tokens], [tokenSender], [Expiration], [Status], [Client_Branch], [SubmitStatus], [scheme_code], [submit_cps], [reason_decline], [PDF_querystring], [branch_delivery]) VALUES (1501002, N'HH195', N'LASTNAAME', N'FIRST', N'SAMPLE', N'639161844862', N'fortunochristian@yahoo.com', N'', N'', N'', 1, N'SAMPLE', N'0', N'AE102', N'NOT', NULL, NULL, N' Ubay Ext. Office')
INSERT [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] ([IntRecord], [GroupCode], [last_name], [first_name], [middle_name], [contactnum], [email], [Tokens], [tokenSender], [Expiration], [Status], [Client_Branch], [SubmitStatus], [scheme_code], [submit_cps], [reason_decline], [PDF_querystring], [branch_delivery]) VALUES (1501003, N'YTP01', N'FORTUNOC', N'FIRST', N'QWE', N'639164845454', N'fortunochristian22@gmail.com', N'', N'', N'', 1, N'QWE', N'0', N'AE104', N'NOT', NULL, NULL, N' ESPANA METRO MANILA')
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CLIENT_EMPLOYEE] OFF
GO
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CLIENTS] ON 

INSERT [dbo].[LRWEB_V1_CLIENTS] ([ID], [AED], [clientName], [addressLine], [barangayId], [barangay], [provinceId], [province], [cityId], [city], [mobileNumber], [officeNumber], [emailAddress], [classification], [structure], [accountNumber], [customerID], [finacle_sol_id], [sol_id], [csbBranch], [schemeCode], [agencyCode], [groupCode], [status], [remarks], [admin_id], [emailFormat], [creditRatio], [dateCreated], [dateUpdated], [dateApproved], [approvedBy]) VALUES (1, CAST(N'2020-11-21T00:00:00.000' AS DateTime), N'TUTELA MARINE, INC.', N'MEGA STATE BUILDING ARANETA AVE COR AGNO EXT1100', N'1254', N' Bayanihan', N'48', N' Metro Manila', N'1178', N' Quezon City', N'639161844862', N'6573775', N'TUT01@tutela.com', N'Private Companies', N'One-person Corporation', NULL, NULL, N'458', N'PH0010608', N' Kalibo Branch', N'AE101', N'TUTELA01', N'TUT01', N'VERIFIED', NULL, N'1000011', N'@gmail.com', N'30', CAST(N'2020-11-10T14:44:10.783' AS DateTime), CAST(N'2020-11-19T13:28:59.230' AS DateTime), CAST(N'2020-11-10T14:46:43.377' AS DateTime), N'CHRISTIAN FORTUNO')
INSERT [dbo].[LRWEB_V1_CLIENTS] ([ID], [AED], [clientName], [addressLine], [barangayId], [barangay], [provinceId], [province], [cityId], [city], [mobileNumber], [officeNumber], [emailAddress], [classification], [structure], [accountNumber], [customerID], [finacle_sol_id], [sol_id], [csbBranch], [schemeCode], [agencyCode], [groupCode], [status], [remarks], [admin_id], [emailFormat], [creditRatio], [dateCreated], [dateUpdated], [dateApproved], [approvedBy]) VALUES (2, CAST(N'2020-10-09T00:00:00.000' AS DateTime), N'Yokogawa Techno Philippines, Inc.', N'ADDRESSLINE 1', N'2812', N' Butao', N'7', N' Apayao', N'293', N' Calanasan (bayag)', N'639161844862', N'6513773', N'sample@gmail.com', N'Private Companies', N'Corporation', NULL, NULL, N'960', N'PH0011509', N' ESPANA METRO MANILA', N'AE104', N'YTISVC01', N'YTP01', N'VERIFIED', NULL, N'1000011', N'@gmail.com', N'30', CAST(N'2020-11-19T13:18:22.573' AS DateTime), CAST(N'2020-11-23T14:18:03.037' AS DateTime), CAST(N'2020-11-23T13:18:04.677' AS DateTime), N'CHRISTIAN FORTUNO')
INSERT [dbo].[LRWEB_V1_CLIENTS] ([ID], [AED], [clientName], [addressLine], [barangayId], [barangay], [provinceId], [province], [cityId], [city], [mobileNumber], [officeNumber], [emailAddress], [classification], [structure], [accountNumber], [customerID], [finacle_sol_id], [sol_id], [csbBranch], [schemeCode], [agencyCode], [groupCode], [status], [remarks], [admin_id], [emailFormat], [creditRatio], [dateCreated], [dateUpdated], [dateApproved], [approvedBy]) VALUES (1002, CAST(N'2020-11-22T00:00:00.000' AS DateTime), N'BRILLIANT METAL CRAFT AND METAL DESIGN', N'ADDRESS', N'2781', N' Agtangao', N'1', N' Abra', N'151', N' Bangued (capital)', N'639161844875', N'6513454', N'sample@email.com', N'Private Schools, Universities and Colleges', N'Sole proprietorship', NULL, NULL, N'107', N'PH0010707', N' Ubay Ext. Office', N'AE102', N'BMCMD01', N'HH195', N'VERIFIED', NULL, N'1000011', N'@yahoo.com', N'30', CAST(N'2020-11-23T17:01:32.040' AS DateTime), NULL, CAST(N'2020-11-23T17:02:40.083' AS DateTime), N'CHRISTIAN FORTUNO')
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CLIENTS] OFF
GO
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ON 

INSERT [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ([ID], [application_number], [referenceNumber], [netIncome], [loanAmount], [loanTerms], [loanPurpose], [loanPurposeID], [loanPurposeOthers], [groupCode], [creditOption], [creditOptionID], [nameToDisplay], [accountNumber], [bankName], [attachment1], [attachment2], [attachment3], [attachment4], [attachment5], [legalID], [nameOnID], [documentName], [documentNameID], [issueAuth], [issueAuthID], [issueDate], [expirationDate], [sol_id], [submit_cps], [reason_decline], [status], [sent_status], [dateCreated], [dateUpdated], [hrID], [dateSubmitted], [token], [tokenExpire], [OTP], [OTPExpiration], [dateSigned], [dateApproved], [dateDeclined]) VALUES (1, N'238', N'1', N'30000', N'100000', N'24', N'Travel', N'Travel', NULL, N'TUT01', N'PAYROLL - UNIONBANK', N'payrollUBP', NULL, N'101011010101100', NULL, N'1-A1.jpg', N'1-A2.jpg', N'1-A3.jpg', N'1-B1.jpg', N'1-B2.jpg', N'LEGALID', N'NAMEONID', N' Driver''s License', N'Driver''s License', N' LTO', N'LTO', N'2020-03-21', NULL, N'PH0011510', N'0', NULL, N'FOR SIGNING', N'0', N'Nov 10 2020  3:31PM', N'Nov 13 2020  3:16PM', N'1000000', N'Nov 10 2020  5:41PM', N'2388C96AC5BEE346A057E17D0F1231047D7', N'20201120165201', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ([ID], [application_number], [referenceNumber], [netIncome], [loanAmount], [loanTerms], [loanPurpose], [loanPurposeID], [loanPurposeOthers], [groupCode], [creditOption], [creditOptionID], [nameToDisplay], [accountNumber], [bankName], [attachment1], [attachment2], [attachment3], [attachment4], [attachment5], [legalID], [nameOnID], [documentName], [documentNameID], [issueAuth], [issueAuthID], [issueDate], [expirationDate], [sol_id], [submit_cps], [reason_decline], [status], [sent_status], [dateCreated], [dateUpdated], [hrID], [dateSubmitted], [token], [tokenExpire], [OTP], [OTPExpiration], [dateSigned], [dateApproved], [dateDeclined]) VALUES (2, N'239', N'2', N'50000', N'99999', N'12', N'Others', N'Others', N'BILLING', N'TUT01', N'PAYROLL - OTHER BANKS', N'others', NULL, N'98654321654', N'BDO', N'2-A1.jpg', N'2-A2.jpg', N'2-A3.jpg', N'2-B1.jpg', N'2-B2.jpg', N'LEGALID', N'NAMEONID', N' GSIS ID', N'GSIS ID', N' GSIS', N'GSIS', N'2010-11-04', NULL, N'PH0011510', N'0', NULL, N'FOR SIGNING', N'0', N'Nov 17 2020 12:39PM', NULL, N'1000000', N'Nov 17 2020 12:39PM', N'2395417DAD62B7D9C8A274E36D567192001', N'20201124124944', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ([ID], [application_number], [referenceNumber], [netIncome], [loanAmount], [loanTerms], [loanPurpose], [loanPurposeID], [loanPurposeOthers], [groupCode], [creditOption], [creditOptionID], [nameToDisplay], [accountNumber], [bankName], [attachment1], [attachment2], [attachment3], [attachment4], [attachment5], [legalID], [nameOnID], [documentName], [documentNameID], [issueAuth], [issueAuthID], [issueDate], [expirationDate], [sol_id], [submit_cps], [reason_decline], [status], [sent_status], [dateCreated], [dateUpdated], [hrID], [dateSubmitted], [token], [tokenExpire], [OTP], [OTPExpiration], [dateSigned], [dateApproved], [dateDeclined]) VALUES (1002, N'539', N'1002', N'50000', N'70000', N'12', N'House Repair and Maintenance', N'House Repair and Maintenance', NULL, N'HH195', N'NEW CSB Savings Account', N'csbNew', N'NAME DISPLAY', NULL, NULL, N'1002-A1.jpg', N'1002-A2.jpg', N'1002-A3.jpg', N'1002-B1.jpg', N'1002-B2.jpg', N'LEGAL ID', N'NAME ON IN', N' Driver''s License', N'Driver''s License', N' PHILHEALTH', N'PHILHEALTH', N'2010-11-22', NULL, N'PH0011509', N'0', NULL, N'ONPROCESS', N'0', N'Nov 23 2020  2:25PM', NULL, N'1000002', N'Nov 23 2020  5:07PM', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ([ID], [application_number], [referenceNumber], [netIncome], [loanAmount], [loanTerms], [loanPurpose], [loanPurposeID], [loanPurposeOthers], [groupCode], [creditOption], [creditOptionID], [nameToDisplay], [accountNumber], [bankName], [attachment1], [attachment2], [attachment3], [attachment4], [attachment5], [legalID], [nameOnID], [documentName], [documentNameID], [issueAuth], [issueAuthID], [issueDate], [expirationDate], [sol_id], [submit_cps], [reason_decline], [status], [sent_status], [dateCreated], [dateUpdated], [hrID], [dateSubmitted], [token], [tokenExpire], [OTP], [OTPExpiration], [dateSigned], [dateApproved], [dateDeclined]) VALUES (1003, N'540', N'1003', N'70000', N'90500', N'12', N'House Repair and Maintenance', N'House Repair and Maintenance', NULL, N'HH195', N'NEW CSB Savings Account', N'csbNew', N'ANME TO DISPLAY', NULL, NULL, N'1003-A1.jpg', N'1003-A2.jpg', N'1003-A3.jpg', N'1003-B1.jpg', N'1003-B2.jpg', N'LEGAL ID', N'NAME IN ID', N' Company ID', N'Company ID', N' GSIS', N'GSIS', N'2015-10-28', NULL, N'PH0010707', N'0', NULL, N'FOR SIGNING', N'0', N'Nov 23 2020  5:21PM', NULL, N'1000002', N'Nov 23 2020  5:22PM', N'5404D3A985329D7CDE449F10E3646AFC465', N'20201230174709', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] ([ID], [application_number], [referenceNumber], [netIncome], [loanAmount], [loanTerms], [loanPurpose], [loanPurposeID], [loanPurposeOthers], [groupCode], [creditOption], [creditOptionID], [nameToDisplay], [accountNumber], [bankName], [attachment1], [attachment2], [attachment3], [attachment4], [attachment5], [legalID], [nameOnID], [documentName], [documentNameID], [issueAuth], [issueAuthID], [issueDate], [expirationDate], [sol_id], [submit_cps], [reason_decline], [status], [sent_status], [dateCreated], [dateUpdated], [hrID], [dateSubmitted], [token], [tokenExpire], [OTP], [OTPExpiration], [dateSigned], [dateApproved], [dateDeclined]) VALUES (1004, N'541', N'1004', N'50000', N'10000', N'12', N'House Repair and Maintenance', N'House Repair and Maintenance', NULL, N'YTP01', N'PAYROLL - UNIONBANK', N'payrollUBP', NULL, N'156485845', NULL, N'1004-A1.jpg', N'1004-A2.jpg', N'1004-A3.jpg', N'1004-B1.jpg', N'1004-B2.jpg', N'LEGAL', N'NAMEON', N' Passport', N'Passport', N' Post Office', N'Post Office', N'2013-10-29', NULL, N'PH0011509', N'0', NULL, N'ONPROCESS', N'0', N'Nov 23 2020  5:29PM', NULL, N'1000001', N'Nov 23 2020  5:30PM', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CUSTOMER_APPLICATION] OFF
GO
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CUSTOMER_INFO] ON 

INSERT [dbo].[LRWEB_V1_CUSTOMER_INFO] ([ID], [salutation], [firstName], [middleName], [lastName], [suffix], [birthPlace], [birthDate], [age], [gender], [religion], [citizenship], [civilStatus], [civilStatusID], [TIN], [SSS], [GSIS], [spouseSalutation], [spouseFirstname], [spouseMiddlename], [spouseLastname], [spouseSuffix], [spouseGender], [spouseBirthDate], [spouseAge], [dependents], [spouseEmployer], [spouseOccupation], [spouseMonthlyIncome], [spouseNumber], [Present_Address], [Present_Province], [Present_ProvinceID], [Present_City], [Present_CityID], [Present_Barangay], [Present_BarangayID], [Present_Country], [Present_Zipcode], [Present_Ownership], [Present_OwnershipID], [Present_Years], [Present_Months], [Present_LengthOfStay], [Present_Telephone], [Permanent_Address], [Permanent_Province], [Permanent_ProvinceID], [Permanent_City], [Permanent_CityID], [Permanent_Barangay], [Permanent_BarangayID], [Permanent_Country], [Permanent_Zipcode], [Permanent_Ownership], [Permanent_OwnershipID], [Permanent_Years], [Permanent_Months], [Permanent_LengthOfStay], [Permanent_Telephone], [dateHired], [tenure], [employeeNumber], [rank], [department], [monthlyAllowance], [occupation], [natureOfWork], [natureOfWorkID], [sourceOfIncomeOthers], [monthlyIncomeOthers], [Employer_Address], [Employer_Province], [Employer_ProvinceID], [Employer_City], [Employer_CityID], [Employer_Barangay], [Employer_BarangayID], [Employer_Country], [Employer_Zipcode], [Employer_Telephone], [employee_reference], [dateCreated]) VALUES (1, N'MR', N'FIRSTNAME', N'MIDDLE', N'LASTNAAME', NULL, N'ANGONO, RIZAL', N'1993-11-22', N'26 YRS AND 11 MONTHS OLD', N'MALE', N'CATH', N'FILIPINO', N'Single', N'Single', N'1111111111', N'321321321', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2ND FLOOR AVRE BUILDING P. OCAMPO ST CORNER D', N' Batanes', N'11', N' Ivana', N'652', N' Salagao', N'6152', N'PHILIPPINES', N'1906', N'Mortgaged', N'4', N'1', N'2', N'14', N'6574852', N'2ND FLOOR AVRE BUILDING P. OCAMPO ST CORNER D', N' Batanes', N'11', N' Ivana', N'652', N' Salagao', N'6152', N'PHILIPPINES', N'1906', N'Mortgaged', N'4', N'1', N'2', N'14', N'6574852', N'2010-02-02', N'10 YRS AND 9 MONTHS OLD', N'55555', N'RANK ONE', N'DEPARTMENT OF DEPARTMENT', N'40000', N'00373 - ACTUARIAN -STATISTICAL & RELATED ASSO PROFESSIONAL', N' Agriculture, Forestry and Fishing', N'Agriculture, Forestry and Fishing', NULL, NULL, N'MEGA STATE BUILDING ARANETA AVE COR AGNO EXT1100', N' Metro Manila', N'48', N' Quezon City', N'1178', N' Alicia', N'1241', N'PHILIPPINES', N'1950', N'654789', N'1500000', N'Nov 10 2020  3:31PM')
INSERT [dbo].[LRWEB_V1_CUSTOMER_INFO] ([ID], [salutation], [firstName], [middleName], [lastName], [suffix], [birthPlace], [birthDate], [age], [gender], [religion], [citizenship], [civilStatus], [civilStatusID], [TIN], [SSS], [GSIS], [spouseSalutation], [spouseFirstname], [spouseMiddlename], [spouseLastname], [spouseSuffix], [spouseGender], [spouseBirthDate], [spouseAge], [dependents], [spouseEmployer], [spouseOccupation], [spouseMonthlyIncome], [spouseNumber], [Present_Address], [Present_Province], [Present_ProvinceID], [Present_City], [Present_CityID], [Present_Barangay], [Present_BarangayID], [Present_Country], [Present_Zipcode], [Present_Ownership], [Present_OwnershipID], [Present_Years], [Present_Months], [Present_LengthOfStay], [Present_Telephone], [Permanent_Address], [Permanent_Province], [Permanent_ProvinceID], [Permanent_City], [Permanent_CityID], [Permanent_Barangay], [Permanent_BarangayID], [Permanent_Country], [Permanent_Zipcode], [Permanent_Ownership], [Permanent_OwnershipID], [Permanent_Years], [Permanent_Months], [Permanent_LengthOfStay], [Permanent_Telephone], [dateHired], [tenure], [employeeNumber], [rank], [department], [monthlyAllowance], [occupation], [natureOfWork], [natureOfWorkID], [sourceOfIncomeOthers], [monthlyIncomeOthers], [Employer_Address], [Employer_Province], [Employer_ProvinceID], [Employer_City], [Employer_CityID], [Employer_Barangay], [Employer_BarangayID], [Employer_Country], [Employer_Zipcode], [Employer_Telephone], [employee_reference], [dateCreated]) VALUES (2, N'MR', N'CHRIS', N'O', N'WEW', NULL, N'BINANGONAN', N'1996-11-22', N'23 YRS AND 11 MONTHS OLD', N'MALE', N'VATH', N'FILIPINO', N'Married', N'Married', N'1945687854', NULL, N'987987987', N'MS', N'FIRSTNAME', N'MIDDLENAME', N'LASTNAME', NULL, N'FEMALE', N'1994-11-22', N'25 YRS AND 11 MONTHS OLD', N'2', N'EMPNAMESPOUSE', N'00001 - ACCOUNTANT - ACCOUNTANTS AND AUDITORS', N'50000', N'6513773', N'BINANGONAN', N' Agusan Del Norte', N'2', N' Kitcharao', N'710', N' Mahayahay', N'38279', N'PHILIPPINES', N'1940', N'Owned', N'1', N'2', N'2', N'26', N'65471423', N'BINANGONAN', N' Agusan Del Norte', N'2', N' Kitcharao', N'710', N' Mahayahay', N'38279', N'PHILIPPINES', N'1940', N'Owned', N'1', N'2', N'2', N'26', N'65471423', N'2010-10-10', N'10 YRS AND 1 MONTHS OLD', N'EMP NO', N'RANK', N'DEPT', N'7000', N'00388 - WORKER - TABACCO PREPARER & TOBACCO PRODUCTS MAKER', N' Wholesale and Retail Trade, Repair of Motor Vehicles, Motorcycles and Personal and Household Goods', N'Wholesale and Retail Trade, Repair of Motor Vehicles, Motorcycles and Personal and Household Goods', N'LUMPIA', N'5000', N'MEGA STATE BUILDING ARANETA AVE COR AGNO EXT1100', N' Metro Manila', N'48', N' Quezon City', N'1178', N' Alicia', N'1241', N'PHILIPPINES', N'1850', N'6574123', N'1500001', N'Nov 17 2020 12:39PM')
INSERT [dbo].[LRWEB_V1_CUSTOMER_INFO] ([ID], [salutation], [firstName], [middleName], [lastName], [suffix], [birthPlace], [birthDate], [age], [gender], [religion], [citizenship], [civilStatus], [civilStatusID], [TIN], [SSS], [GSIS], [spouseSalutation], [spouseFirstname], [spouseMiddlename], [spouseLastname], [spouseSuffix], [spouseGender], [spouseBirthDate], [spouseAge], [dependents], [spouseEmployer], [spouseOccupation], [spouseMonthlyIncome], [spouseNumber], [Present_Address], [Present_Province], [Present_ProvinceID], [Present_City], [Present_CityID], [Present_Barangay], [Present_BarangayID], [Present_Country], [Present_Zipcode], [Present_Ownership], [Present_OwnershipID], [Present_Years], [Present_Months], [Present_LengthOfStay], [Present_Telephone], [Permanent_Address], [Permanent_Province], [Permanent_ProvinceID], [Permanent_City], [Permanent_CityID], [Permanent_Barangay], [Permanent_BarangayID], [Permanent_Country], [Permanent_Zipcode], [Permanent_Ownership], [Permanent_OwnershipID], [Permanent_Years], [Permanent_Months], [Permanent_LengthOfStay], [Permanent_Telephone], [dateHired], [tenure], [employeeNumber], [rank], [department], [monthlyAllowance], [occupation], [natureOfWork], [natureOfWorkID], [sourceOfIncomeOthers], [monthlyIncomeOthers], [Employer_Address], [Employer_Province], [Employer_ProvinceID], [Employer_City], [Employer_CityID], [Employer_Barangay], [Employer_BarangayID], [Employer_Country], [Employer_Zipcode], [Employer_Telephone], [employee_reference], [dateCreated]) VALUES (1002, N'MR', N'ISTIAN', N'DENA', N'STNAAME', N'O', N'BINANGONAN', N'1990-08-22', N'25 YRS AND 0 MONTHS OLD', N'MALE', N'CATHOLIC', N'FILIPINO', N'Separated', N'Separated', N'987987', N'654654', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'ADREESS LINE', N' Abra', N'1', N' Bangued (capital)', N'151', N' Agtangao', N'2781', N'PHILIPPINES', N'1950', N'Owned', N'1', N'8', N'8', N'104', N'654723', N'ADREESS LINE', N' Abra', N'1', N' Bangued (capital)', N'151', N' Agtangao', N'2781', N'PHILIPPINES', N'1950', N'Owned', N'1', N'8', N'8', N'104', N'654723', N'2010-11-22', N'10 YRS AND 0 MONTHS OLD', N'EMPLOYEE NUMBER', N'RANK ONE', N'DEPARTMENT', N'MONTLHY ALLOWANCE', N'00001 - ACCOUNTANT - ACCOUNTANTS AND AUDITORS', N' Accomodation and Food Service Activities', N'Accomodation and Food Service Activities', NULL, NULL, N'ADDRESSLINE 1', N' Apayao', N'7', N' Calanasan (bayag)', N'293', N' Butao', N'2812', N'PHILIPPINES', N'1940', N'TEL NO', N'1501001', N'Nov 23 2020  2:25PM')
INSERT [dbo].[LRWEB_V1_CUSTOMER_INFO] ([ID], [salutation], [firstName], [middleName], [lastName], [suffix], [birthPlace], [birthDate], [age], [gender], [religion], [citizenship], [civilStatus], [civilStatusID], [TIN], [SSS], [GSIS], [spouseSalutation], [spouseFirstname], [spouseMiddlename], [spouseLastname], [spouseSuffix], [spouseGender], [spouseBirthDate], [spouseAge], [dependents], [spouseEmployer], [spouseOccupation], [spouseMonthlyIncome], [spouseNumber], [Present_Address], [Present_Province], [Present_ProvinceID], [Present_City], [Present_CityID], [Present_Barangay], [Present_BarangayID], [Present_Country], [Present_Zipcode], [Present_Ownership], [Present_OwnershipID], [Present_Years], [Present_Months], [Present_LengthOfStay], [Present_Telephone], [Permanent_Address], [Permanent_Province], [Permanent_ProvinceID], [Permanent_City], [Permanent_CityID], [Permanent_Barangay], [Permanent_BarangayID], [Permanent_Country], [Permanent_Zipcode], [Permanent_Ownership], [Permanent_OwnershipID], [Permanent_Years], [Permanent_Months], [Permanent_LengthOfStay], [Permanent_Telephone], [dateHired], [tenure], [employeeNumber], [rank], [department], [monthlyAllowance], [occupation], [natureOfWork], [natureOfWorkID], [sourceOfIncomeOthers], [monthlyIncomeOthers], [Employer_Address], [Employer_Province], [Employer_ProvinceID], [Employer_City], [Employer_CityID], [Employer_Barangay], [Employer_BarangayID], [Employer_Country], [Employer_Zipcode], [Employer_Telephone], [employee_reference], [dateCreated]) VALUES (1003, N'MR', N'FIRST', N'SAMPLE', N'LASTNAAME', N'QWEASD', N'BINANGONAN', N'2000-08-08', N'20 YRS AND 3 MONTHS OLD', N'FEMALE', N'CATHOLIC', N'FILIPINO', N'Single', N'Single', N'111111', N'321321654', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'ADDRESS LINE 1', N' Abra', N'1', N' Bangued (capital)', N'151', N' Agtangao', N'2781', N'PHILIPPINES', N'1654', N'Owned', N'1', N'4', N'4', N'52', N'6513486', N'ADDRESS LINE 1', N' Abra', N'1', N' Bangued (capital)', N'151', N' Agtangao', N'2781', N'PHILIPPINES', N'1654', N'Owned', N'1', N'4', N'4', N'52', N'6513486', N'2010-01-01', N'10 YRS AND 10 MONTHS OLD', N'EMPLOYEE NUMBER', N'RANK', N'DEPARTMENT', N'5000', N'00429 - ASSEMBLER - WOOD AND RELATED PRODUCTS ASSEMBLERS', N' Water Supply, Sewerage, Waste Management and Remediation Activities', N'Water Supply, Sewerage, Waste Management and Remediation Activities', NULL, NULL, N'ADDRESS', N' Abra', N'1', N' Bangued (capital)', N'151', N' Agtangao', N'2781', N'PHILIPPINES', N'1940', N'65134784', N'1501002', N'Nov 23 2020  5:21PM')
INSERT [dbo].[LRWEB_V1_CUSTOMER_INFO] ([ID], [salutation], [firstName], [middleName], [lastName], [suffix], [birthPlace], [birthDate], [age], [gender], [religion], [citizenship], [civilStatus], [civilStatusID], [TIN], [SSS], [GSIS], [spouseSalutation], [spouseFirstname], [spouseMiddlename], [spouseLastname], [spouseSuffix], [spouseGender], [spouseBirthDate], [spouseAge], [dependents], [spouseEmployer], [spouseOccupation], [spouseMonthlyIncome], [spouseNumber], [Present_Address], [Present_Province], [Present_ProvinceID], [Present_City], [Present_CityID], [Present_Barangay], [Present_BarangayID], [Present_Country], [Present_Zipcode], [Present_Ownership], [Present_OwnershipID], [Present_Years], [Present_Months], [Present_LengthOfStay], [Present_Telephone], [Permanent_Address], [Permanent_Province], [Permanent_ProvinceID], [Permanent_City], [Permanent_CityID], [Permanent_Barangay], [Permanent_BarangayID], [Permanent_Country], [Permanent_Zipcode], [Permanent_Ownership], [Permanent_OwnershipID], [Permanent_Years], [Permanent_Months], [Permanent_LengthOfStay], [Permanent_Telephone], [dateHired], [tenure], [employeeNumber], [rank], [department], [monthlyAllowance], [occupation], [natureOfWork], [natureOfWorkID], [sourceOfIncomeOthers], [monthlyIncomeOthers], [Employer_Address], [Employer_Province], [Employer_ProvinceID], [Employer_City], [Employer_CityID], [Employer_Barangay], [Employer_BarangayID], [Employer_Country], [Employer_Zipcode], [Employer_Telephone], [employee_reference], [dateCreated]) VALUES (1004, N'MR', N'FIRST', N'QWE', N'FORTUNOC', NULL, N'BINANGONAN', N'1998-04-05', N'22 YRS AND 7 MONTHS OLD', N'MALE', N'CATHOLIC', N'FILIPINO', N'Single', N'Single', N'111111', N'222222', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'ADDRESS LINE 1', N' Abra', N'1', N' Bangued (capital)', N'151', N' Agtangao', N'2781', N'PHILIPPINES', N'1945', N'Owned', N'1', N'2', N'4', N'28', N'6513454', N'ADDRESS LINE 1', N' Abra', N'1', N' Bangued (capital)', N'151', N' Agtangao', N'2781', N'PHILIPPINES', N'1945', N'Owned', N'1', N'2', N'4', N'28', N'6513454', N'2018-05-05', N'2 YRS AND 6 MONTHS OLD', N'EMP NO', N'RTANK', N'DEPARTMENT', N'4000', N'00374 - ACTUARIAN - STATISTICIANS', N' Agriculture, Forestry and Fishing', N'Agriculture, Forestry and Fishing', NULL, NULL, N'ADDRESSLINE 1', N' Apayao', N'7', N' Calanasan (bayag)', N'293', N' Butao', N'2812', N'PHILIPPINES', N'1964', N'6574585', N'1501003', N'Nov 23 2020  5:29PM')
SET IDENTITY_INSERT [dbo].[LRWEB_V1_CUSTOMER_INFO] OFF
GO
SET IDENTITY_INSERT [dbo].[PASSWORD_HISTORY] ON 

INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (1, N'superadmin@citysavings.com.ph', N'123456Abcde!1', CAST(N'2020-07-06T15:03:10.553' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (2, N'superadmin@citysavings.com.ph', N'123456Abcde!12', CAST(N'2020-07-06T15:29:13.007' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (3, N'superadmin@citysavings.com.ph', N'123456Abcde!123', CAST(N'2020-07-14T14:01:29.140' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (4, N'sad@citysavings.com.ph', N'123456Abcde!', CAST(N'2020-07-14T14:03:18.343' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (5, N'sample@yahoo.com', N'123456Abcde@!', CAST(N'2020-07-29T00:05:23.567' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (6, N'sample@yahoo.com', N'123456Abcde!!', CAST(N'2020-07-29T00:15:25.787' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (7, N'sample@yahoo.com', N'123456Abcde@!', CAST(N'2020-07-29T17:31:45.930' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (8, N'sample@yahoo.com', N'123456Abcde@!', CAST(N'2020-07-29T17:33:18.617' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (9, N'sample@yahoo.com', N'123456Abcde!!', CAST(N'2020-07-29T17:49:02.303' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (10, N'fortunochristian@yahoo.com', N'123456Abcde!', CAST(N'2020-08-17T05:19:03.773' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (11, N'fortunochristian@yahoo.com', N'123456Abcde!', CAST(N'2020-08-17T05:19:51.320' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (12, N'fortunochristian@yahoo.com', N'123456Abcde!', CAST(N'2020-08-17T05:19:57.617' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (13, N'another1@citysavings.com.ph', N'123456Abcde!', CAST(N'2020-08-17T05:41:37.337' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (14, N'malouisa.madrid@citysavings.com.ph', N'Onetwothree@123', CAST(N'2020-08-17T16:40:24.780' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (15, N'sadlayp@citysavings.com.ph', N'123456Abcde!', CAST(N'2020-09-03T03:30:29.560' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (16, N'another1@citysavings.com.ph', N'123456Abcde!123', CAST(N'2020-09-03T04:25:05.347' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (17, N'malouisa.madrid@citysavings.com.ph', N'Sample@12345', CAST(N'2020-09-09T10:30:21.720' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (18, N'another1@citysavings.com.ph', N'123456Abcde!1', CAST(N'2020-09-09T13:43:23.697' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (19, N'another1@citysavings.com.ph', N'123456Abcde!12', CAST(N'2020-09-09T13:45:27.057' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (22, N'malouisa.madrid@citysavings.com.ph', N'Louisa@12345', CAST(N'2020-10-06T15:06:02.157' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (20, N'another1@citysavings.com.ph', N'123456Abcde!@', CAST(N'2020-09-09T13:53:18.743' AS DateTime))
INSERT [dbo].[PASSWORD_HISTORY] ([ID], [userName], [pastPassword], [dateUpdated]) VALUES (21, N'another1@citysavings.com.ph', N'123456Abcde!1231', CAST(N'2020-09-09T13:54:32.980' AS DateTime))
SET IDENTITY_INSERT [dbo].[PASSWORD_HISTORY] OFF
GO
SET IDENTITY_INSERT [dbo].[SCHEMECODE] ON 

INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (1, N'AE.101', N'Agency Loan', 18, 4)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (2, N'AE.102', N'Agency Loan', 18, 4)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (3, N'AE.103', N'Agency Loan', 18, 5)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (4, N'AE.104', N'Agency Loan', 18, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (5, N'AB.511', N'Agency Loan - AEV', 12, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (6, N'PSUC.01', N'PUC Loan', 12, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (7, N'PSUC.02', N'PUC Loan', 12, 7)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (8, N'PSUC.03', N'PUC Loan', 12, 8)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (9, N'PSUC.04', N'PUC Loan', 12, 9)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (10, N'PSUC.05', N'PUC Loan', 12, 10)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (11, N'PSUC.06', N'PUC Loan', 12, 11)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (12, N'PSUC.07', N'PUC Loan', 12, 12)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (13, N'PSUC.08', N'PUC Loan', 12, 5)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (14, N'AE101', N'Agency Loan', 18, 4)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (15, N'AE102', N'Agency Loan', 18, 4)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (16, N'AE103', N'Agency Loan', 18, 5)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (17, N'AE104', N'Agency Loan', 18, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (18, N'AB511', N'Agency Loan - AEV', 12, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (19, N'PSUC01', N'PUC Loan', 12, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (20, N'PSUC02', N'PUC Loan', 12, 7)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (21, N'PSUC03', N'PUC Loan', 12, 8)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (22, N'PSUC04', N'PUC Loan', 12, 9)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (23, N'PSUC05', N'PUC Loan', 12, 10)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (24, N'PSUC06', N'PUC Loan', 12, 11)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (25, N'PSUC07', N'PUC Loan', 12, 12)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (26, N'PSUC08', N'PUC Loan', 12, 5)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (27, N'AE105', N'Agency Loan', 12, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (28, N'AE106', N'Agency Loan', 12, 7)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (29, N'AE107', N'Agency Loan', 12, 8)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (30, N'AE108', N'Agency Loan', 13, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (31, N'AE109', N'Agency Loan', 14, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (32, N'AE110', N'Agency Loan', 15, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (33, N'AE111', N'Agency Loan', 16, 5)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (34, N'AE112', N'Agency Loan', 16, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (35, N'AE113', N'Agency Loan', 17, 5)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (36, N'AE114', N'Agency Loan', 17, 6)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (37, N'res', N'res', NULL, NULL)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (38, N'res', N'res', NULL, NULL)
INSERT [dbo].[SCHEMECODE] ([ID], [schemeCode], [product], [interestRate], [bankCharge]) VALUES (39, N'res', N'res', NULL, NULL)
SET IDENTITY_INSERT [dbo].[SCHEMECODE] OFF
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
/****** Object:  StoredProcedure [dbo].[ael_stat_change]    Script Date: 12/22/2020 1:25:37 PM ******/
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
