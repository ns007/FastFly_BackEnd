
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/30/2017 20:11:09
-- Generated from EDMX file: C:\Users\netanel\Source\Repos\FastFly_BackEnd\FastFly.BeckEnd\FastFlyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FlyFastDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DepartmentUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_DepartmentUser];
GO
IF OBJECT_ID(N'[dbo].[FK_FacultyUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_FacultyUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationRoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_ApplicationRoleUser];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryFlight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Flights] DROP CONSTRAINT [FK_CountryFlight];
GO
IF OBJECT_ID(N'[dbo].[FK_UserApplyDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ApplyDocuments] DROP CONSTRAINT [FK_UserApplyDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryDestinationPeriods]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DestinationPeriods] DROP CONSTRAINT [FK_CountryDestinationPeriods];
GO
IF OBJECT_ID(N'[dbo].[FK_UserReckoningDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReckoningDocuments] DROP CONSTRAINT [FK_UserReckoningDocument];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Faculties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Faculties];
GO
IF OBJECT_ID(N'[dbo].[ApplicationRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationRoles];
GO
IF OBJECT_ID(N'[dbo].[CarRents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarRents];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[TestReplacements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestReplacements];
GO
IF OBJECT_ID(N'[dbo].[StayExpenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StayExpenses];
GO
IF OBJECT_ID(N'[dbo].[ReckoningDocuments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReckoningDocuments];
GO
IF OBJECT_ID(N'[dbo].[OtherOxpenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OtherOxpenses];
GO
IF OBJECT_ID(N'[dbo].[LectureReplacements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LectureReplacements];
GO
IF OBJECT_ID(N'[dbo].[Flights]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Flights];
GO
IF OBJECT_ID(N'[dbo].[AccommodationAbroads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccommodationAbroads];
GO
IF OBJECT_ID(N'[dbo].[ApplyDocuments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplyDocuments];
GO
IF OBJECT_ID(N'[dbo].[DestinationPeriods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DestinationPeriods];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] nvarchar(9)  NOT NULL,
    [Password] nvarchar(10)  NOT NULL,
    [EmailAddress] nvarchar(100)  NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Role] nvarchar(20)  NOT NULL,
    [PercentageJob] int  NULL,
    [CellNum] nvarchar(10)  NULL,
    [DepartmentId] int  NOT NULL,
    [FacultyId] int  NOT NULL,
    [ApplicationRoleId] int  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(50)  NULL
);
GO

-- Creating table 'Faculties'
CREATE TABLE [dbo].[Faculties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FacultyName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ApplicationRoles'
CREATE TABLE [dbo].[ApplicationRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'CarRents'
CREATE TABLE [dbo].[CarRents] (
    [DocId] int  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [TotalDays] int  NULL,
    [Cost] decimal(18,0)  NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryCode] nvarchar(3)  NOT NULL,
    [CountryName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'TestReplacements'
CREATE TABLE [dbo].[TestReplacements] (
    [DocId] int  NOT NULL,
    [CourseName] nvarchar(50)  NOT NULL,
    [TestDate] datetime  NOT NULL,
    [SubstituteLucterer] nvarchar(100)  NOT NULL,
    [ExamPeriod] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'StayExpenses'
CREATE TABLE [dbo].[StayExpenses] (
    [DocId] int  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [TotalDays] int  NOT NULL,
    [FeePerDay] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'ReckoningDocuments'
CREATE TABLE [dbo].[ReckoningDocuments] (
    [DocId] int  NOT NULL,
    [UserId] nvarchar(9)  NOT NULL,
    [ConcentrationExpenses] nvarchar(1)  NULL,
    [AccommodationAbroad] nvarchar(1)  NULL,
    [StayExpense] nvarchar(1)  NULL,
    [CarRent] nvarchar(1)  NULL,
    [OtherOxpense] nvarchar(1)  NULL,
    [AdvanceGiven] nvarchar(1)  NULL,
    [AdvanceAmount] decimal(18,0)  NULL,
    [SignPresididnt] nvarchar(50)  NULL,
    [Date] datetime  NULL,
    [Sign] nvarchar(50)  NULL
);
GO

-- Creating table 'OtherOxpenses'
CREATE TABLE [dbo].[OtherOxpenses] (
    [DocId] int  NOT NULL,
    [ExpenseEssence] nvarchar(100)  NOT NULL,
    [Cost] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'LectureReplacements'
CREATE TABLE [dbo].[LectureReplacements] (
    [DocId] int  NOT NULL,
    [CourseName] nvarchar(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [FromHour] time  NOT NULL,
    [ToHour] time  NOT NULL,
    [CompleteBy] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Flights'
CREATE TABLE [dbo].[Flights] (
    [DocId] int  NOT NULL,
    [FlightDate] datetime  NOT NULL,
    [Class] nvarchar(25)  NOT NULL,
    [Cost] decimal(18,0)  NOT NULL,
    [CountryCodeDest] nvarchar(3)  NOT NULL
);
GO

-- Creating table 'AccommodationAbroads'
CREATE TABLE [dbo].[AccommodationAbroads] (
    [DocId] int  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [TotalNights] int  NOT NULL,
    [Cost] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'ApplyDocuments'
CREATE TABLE [dbo].[ApplyDocuments] (
    [DocId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(9)  NOT NULL,
    [ColleagueType] nvarchar(50)  NULL,
    [DepartureDate] datetime  NULL,
    [ReturnDate] datetime  NULL,
    [TotalDays] int  NULL,
    [TravelPurpose] nvarchar(50)  NULL,
    [TotalRequsetAmount] int  NULL,
    [TotalEselDays] int  NULL,
    [LastReturnDate] datetime  NULL,
    [TeacheDuringTravel] bit  NULL,
    [ReplacingInTests] bit  NULL,
    [ResearchTraining] bit  NULL,
    [AboveWeek] bit  NULL,
    [MoreThenOneTravel] bit  NULL,
    [AbsenceTestA] bit  NULL,
    [ExceptionRequstExplain] nvarchar(250)  NULL,
    [PlusOne] bit  NULL,
    [ApplicantSign] nvarchar(50)  NULL,
    [ApplyDate] datetime  NULL,
    [DepartmentHeadApprove] datetime  NULL,
    [ReadPaperApprove] datetime  NULL,
    [ReplacementApprove] bit  NULL,
    [DepartmentHeadFullname] nvarchar(100)  NULL,
    [DepartmentHeadSign] nvarchar(50)  NULL,
    [DepartmentHeadSignDate] datetime  NULL,
    [WastedYear] nvarchar(5)  NULL,
    [WastedAmount] decimal(18,0)  NULL,
    [Sign1] nvarchar(50)  NULL,
    [Date1] datetime  NULL,
    [Sign2] nvarchar(50)  NULL,
    [Date2] datetime  NULL,
    [Sign3] nvarchar(50)  NULL,
    [Date3] datetime  NULL,
    [Sign4] nvarchar(50)  NULL,
    [Date4] datetime  NULL,
    [Sign5] nvarchar(50)  NULL,
    [Date5] datetime  NULL
);
GO

-- Creating table 'DestinationPeriods'
CREATE TABLE [dbo].[DestinationPeriods] (
    [DocId] int  NOT NULL,
    [CountryCode] nvarchar(3)  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [Flight] nvarchar(20)  NOT NULL,
    [Hotel] nvarchar(50)  NOT NULL,
    [RideBy] nvarchar(50)  NOT NULL,
    [ParticipationFee] decimal(18,0)  NOT NULL,
    [TotalAmountRequested] decimal(18,0)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Faculties'
ALTER TABLE [dbo].[Faculties]
ADD CONSTRAINT [PK_Faculties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ApplicationRoles'
ALTER TABLE [dbo].[ApplicationRoles]
ADD CONSTRAINT [PK_ApplicationRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [DocId], [FromDate], [ToDate] in table 'CarRents'
ALTER TABLE [dbo].[CarRents]
ADD CONSTRAINT [PK_CarRents]
    PRIMARY KEY CLUSTERED ([DocId], [FromDate], [ToDate] ASC);
GO

-- Creating primary key on [CountryCode] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryCode] ASC);
GO

-- Creating primary key on [CourseName], [TestDate] in table 'TestReplacements'
ALTER TABLE [dbo].[TestReplacements]
ADD CONSTRAINT [PK_TestReplacements]
    PRIMARY KEY CLUSTERED ([CourseName], [TestDate] ASC);
GO

-- Creating primary key on [DocId], [FromDate], [ToDate] in table 'StayExpenses'
ALTER TABLE [dbo].[StayExpenses]
ADD CONSTRAINT [PK_StayExpenses]
    PRIMARY KEY CLUSTERED ([DocId], [FromDate], [ToDate] ASC);
GO

-- Creating primary key on [DocId] in table 'ReckoningDocuments'
ALTER TABLE [dbo].[ReckoningDocuments]
ADD CONSTRAINT [PK_ReckoningDocuments]
    PRIMARY KEY CLUSTERED ([DocId] ASC);
GO

-- Creating primary key on [DocId], [ExpenseEssence] in table 'OtherOxpenses'
ALTER TABLE [dbo].[OtherOxpenses]
ADD CONSTRAINT [PK_OtherOxpenses]
    PRIMARY KEY CLUSTERED ([DocId], [ExpenseEssence] ASC);
GO

-- Creating primary key on [CourseName], [Date], [FromHour] in table 'LectureReplacements'
ALTER TABLE [dbo].[LectureReplacements]
ADD CONSTRAINT [PK_LectureReplacements]
    PRIMARY KEY CLUSTERED ([CourseName], [Date], [FromHour] ASC);
GO

-- Creating primary key on [FlightDate] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [PK_Flights]
    PRIMARY KEY CLUSTERED ([FlightDate] ASC);
GO

-- Creating primary key on [DocId], [FromDate], [ToDate] in table 'AccommodationAbroads'
ALTER TABLE [dbo].[AccommodationAbroads]
ADD CONSTRAINT [PK_AccommodationAbroads]
    PRIMARY KEY CLUSTERED ([DocId], [FromDate], [ToDate] ASC);
GO

-- Creating primary key on [DocId] in table 'ApplyDocuments'
ALTER TABLE [dbo].[ApplyDocuments]
ADD CONSTRAINT [PK_ApplyDocuments]
    PRIMARY KEY CLUSTERED ([DocId] ASC);
GO

-- Creating primary key on [FromDate], [CountryCode] in table 'DestinationPeriods'
ALTER TABLE [dbo].[DestinationPeriods]
ADD CONSTRAINT [PK_DestinationPeriods]
    PRIMARY KEY CLUSTERED ([FromDate], [CountryCode] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DepartmentId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_DepartmentUser]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentUser'
CREATE INDEX [IX_FK_DepartmentUser]
ON [dbo].[Users]
    ([DepartmentId]);
GO

-- Creating foreign key on [FacultyId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_FacultyUser]
    FOREIGN KEY ([FacultyId])
    REFERENCES [dbo].[Faculties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FacultyUser'
CREATE INDEX [IX_FK_FacultyUser]
ON [dbo].[Users]
    ([FacultyId]);
GO

-- Creating foreign key on [ApplicationRoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_ApplicationRoleUser]
    FOREIGN KEY ([ApplicationRoleId])
    REFERENCES [dbo].[ApplicationRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationRoleUser'
CREATE INDEX [IX_FK_ApplicationRoleUser]
ON [dbo].[Users]
    ([ApplicationRoleId]);
GO

-- Creating foreign key on [CountryCodeDest] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [FK_CountryFlight]
    FOREIGN KEY ([CountryCodeDest])
    REFERENCES [dbo].[Countries]
        ([CountryCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryFlight'
CREATE INDEX [IX_FK_CountryFlight]
ON [dbo].[Flights]
    ([CountryCodeDest]);
GO

-- Creating foreign key on [UserId] in table 'ApplyDocuments'
ALTER TABLE [dbo].[ApplyDocuments]
ADD CONSTRAINT [FK_UserApplyDocument]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserApplyDocument'
CREATE INDEX [IX_FK_UserApplyDocument]
ON [dbo].[ApplyDocuments]
    ([UserId]);
GO

-- Creating foreign key on [CountryCode] in table 'DestinationPeriods'
ALTER TABLE [dbo].[DestinationPeriods]
ADD CONSTRAINT [FK_CountryDestinationPeriods]
    FOREIGN KEY ([CountryCode])
    REFERENCES [dbo].[Countries]
        ([CountryCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryDestinationPeriods'
CREATE INDEX [IX_FK_CountryDestinationPeriods]
ON [dbo].[DestinationPeriods]
    ([CountryCode]);
GO

-- Creating foreign key on [UserId] in table 'ReckoningDocuments'
ALTER TABLE [dbo].[ReckoningDocuments]
ADD CONSTRAINT [FK_UserReckoningDocument]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserReckoningDocument'
CREATE INDEX [IX_FK_UserReckoningDocument]
ON [dbo].[ReckoningDocuments]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------