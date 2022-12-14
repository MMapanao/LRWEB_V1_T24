USE [BankAlerts]
GO
/****** Object:  Table [dbo].[PIS_COUNTRY]    Script Date: 1/7/2021 5:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIS_COUNTRY](
	[CountryCode] [varchar](50) NULL,
	[CountryDesc] [varchar](80) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCity]    Script Date: 1/7/2021 5:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCity](
	[code] [nchar](10) NULL,
	[name] [varchar](50) NULL,
	[province_code] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNatureofWork]    Script Date: 1/7/2021 5:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNatureofWork](
	[Code] [nchar](10) NULL,
	[NatureOfWork] [varchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOccupation]    Script Date: 1/7/2021 5:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOccupation](
	[code] [nchar](10) NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProvince]    Script Date: 1/7/2021 5:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProvince](
	[code] [varchar](10) NULL,
	[name] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSourceofFunds]    Script Date: 1/7/2021 5:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSourceofFunds](
	[Code] [nchar](10) NULL,
	[SourceOfFunds] [varchar](100) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'021', N' AMERICAN PACIFIC ISL')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'022', N'CANADA')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'023', N'GUAM (MARIANAS IS) ')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'024', N'PANAMA CANAL ZONE ')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'025', N'PUERTO RICO')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'026', N'UNITED STATES')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'027', N'VIRGIN ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'028', N'CAYMAN ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'030', N'ARUBA')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'031', N'CHANNEL ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'032', N'CURACAO')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'033', N'MONACO')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'034', N'AMERICAN SAMOA')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'035', N'LEEWARD WINDWARD')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'036', N'AZORES')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'037', N'ZIMBABWE')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'038', N'SOCIETY ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'039', N'CANARY ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'040', N'CAPE VERDE ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'041', N'CAROLINE ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'042', N'CHRISTMAS ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'043', N'COMORES ARCHIPELAGO')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'044', N'TURKS CAICOS IS.')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'045', N'RODRIGUES ISLAND')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'046', N'ISLE OF MAN')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'047', N'MACAU')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'048', N'MADEIRA')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'049', N'MALDIVE ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'050', N'MARSHALL ISLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'051', N'AUSTRIA')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'052', N'BELGIUM')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'053', N'DENMARK')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'054', N'FRANCE')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'055', N'GERMANY FEDERAL REP')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'056', N'GREECE')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'057', N'ICELAND')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'058', N'IRELAND REP OF')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'059', N'ITALY')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'060', N'JAPAN')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'061', N'LIECHTENSTEIN')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'062', N'LUXEMBOURG')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'063', N'NETHERLANDS')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'064', N'NORWAY')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'065', N'PORTUGAL')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'066', N'SPAIN')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'067', N'SWEDEN')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'068', N'SWITZERLAND')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'069', N'TURKEY')
INSERT [dbo].[PIS_COUNTRY] ([CountryCode], [CountryDesc]) VALUES (N'070', N'UNITED KINGDOM')
INSERT [dbo].[tblCity] ([code], [name], [province_code]) VALUES (N'1147A     ', N'NOVALICHES P.O BOX', N'110')
INSERT [dbo].[tblCity] ([code], [name], [province_code]) VALUES (N'1128B     ', N'VASRA', N'110')
INSERT [dbo].[tblCity] ([code], [name], [province_code]) VALUES (N'1200A     ', N'MAKATI CITY', N'120')
INSERT [dbo].[tblCity] ([code], [name], [province_code]) VALUES (N'1203A     ', N'SAN ANTONIO VILLAGE', N'120')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'A0000     ', N'Agriculture, forestry and fishing')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'B0000     ', N'Mining and quarrying')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'C0000     ', N'Manufacturing')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'D0000     ', N'Electricity, gas, steam and air conditioning supply')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'E0000     ', N'Water supply; sewerage, waste management and remediation activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'F0000     ', N'Construction')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'G0000     ', N'Wholesale and retail trade; repair of motor vehicles and motorcycles')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'H0000     ', N'Transportation and storage')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'I0000     ', N'Accommodation and food service activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'J0000     ', N'Information and communication')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'K0000     ', N'Financial and insurance activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'L0000     ', N'Real estate activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'M0000     ', N'Professional, scientific and technical activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'N0000     ', N'Administrative and support service activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'O0000     ', N'Public administration and defense; compulsory social security')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'P0000     ', N'Education')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'Q0000     ', N'Human health and social work activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'R0000     ', N'Arts, entertainment and recreation')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'S0000     ', N'Other service activities')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'T0000     ', N'Activities of households as employers or Activities for own use')
INSERT [dbo].[tblNatureofWork] ([Code], [NatureOfWork]) VALUES (N'U0000     ', N'Activities of extraterritorial organization and bodies')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'00        ', N'UNSKILLED WORKER')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'01        ', N'BUSINESSWOMAN')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'02        ', N'SR. EXECUTIVE')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'03        ', N'JR. EXECUTIVE')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'04        ', N'ACCOUNTANT')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'05        ', N'ACCOUNT EXECUTIVE')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'06        ', N'SALES REPRESENTATIVE')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'07        ', N'STOCKHOLDER')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'08        ', N'OFFICE PERSONNEL')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'09        ', N'FINANCIAL ANALYSTS')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'16        ', N'ACTUARIAN')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'17        ', N'INSURANCE AGENT ')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'18        ', N'INSURANCE UNDERWRITER')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'19        ', N'HEAD OF COLLEGES')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'1A        ', N'ABLE SEAMAN')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'20        ', N'FINANCIER')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'26        ', N'PILOT')
INSERT [dbo].[tblOccupation] ([code], [name]) VALUES (N'27        ', N'FLIGHT ATTENDANT')
INSERT [dbo].[tblProvince] ([code], [name]) VALUES (N'110', N'QUEZON CITY')
INSERT [dbo].[tblProvince] ([code], [name]) VALUES (N'120', N'MAKATI')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'A1000     ', N'Income from Business')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'B1000     ', N'Employment / Salary')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'C1000     ', N'Savings / Investments')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'D1000     ', N'Allowance')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'E1000     ', N'Remittance')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'F1000     ', N'Pension')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'G1000     ', N'Inheritance')
INSERT [dbo].[tblSourceofFunds] ([Code], [SourceOfFunds]) VALUES (N'H1000     ', N'Others')
