ALTER TABLE [dbo].[Events]
	ADD CONSTRAINT [FK_Events_Aggregates] 
	FOREIGN KEY (AggregateId)
	REFERENCES [Aggregates] (AggregateId)	

