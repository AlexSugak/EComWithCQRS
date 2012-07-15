CREATE TABLE [dbo].[Events]
(
	AggregateId NVARCHAR(50) NOT NULL, 
	[Version] int NOT NULL,
	[Event] varbinary(max) NOT NULL,
	[Date] datetime NOT NULL,
	[User] nvarchar(100) NULL
)
