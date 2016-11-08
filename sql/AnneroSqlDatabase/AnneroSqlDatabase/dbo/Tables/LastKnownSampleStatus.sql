CREATE TABLE [dbo].[LastKnownSampleStatus] (
    [InternalAssetId] INT             NOT NULL,
    [StatusChar]      VARCHAR (20)    NULL,
    [StatusInt]       INT             NULL,
    [StatusDecimal]   DECIMAL (10, 4) NULL,
    [SampleTime]      DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([InternalAssetId] ASC)
);


GO
create trigger [dbo].[SampleInsertTrigger] on [dbo].[LastKnownSampleStatus]
instead of insert
as begin
	declare @internalAssetId int
	declare @statusChar varchar(20)
	declare @statusInt int
	declare @statusDecimal decimal (10,4)
	declare @sampleTime datetime

	declare @idx int
	declare @numrows int

	select @idx = 1
	select @numrows = count(*) from inserted;

	with InsertedRows as (
		select ROW_NUMBER() over (order by inserted.InternalAssetId) as Row, inserted.*	 from inserted
	)
	select * into #InsertedRows from InsertedRows

	while (@idx <= @numrows)
	begin
		select @internalAssetId = InternalAssetId,
			   @statusChar = StatusChar,
			   @statusInt = StatusInt,
			   @statusDecimal = StatusDecimal,
			   @sampleTime = SampleTime
		from #InsertedRows where Row = @idx

		if @statusInt is not null
			select @statusDecimal = null
		if @statusChar = 'occupied'
			select @statusInt = 1
		else if @statusChar = 'free'
			select @statusInt = 0

		select @idx = @idx+1

		update LastKnownSampleStatus
		set
			StatusChar = @statusChar,
			StatusInt = @statusInt,
			StatusDecimal = @statusDecimal,
			SampleTime = @sampleTime
		where
			InternalAssetId = @internalAssetid AND SampleTime < @sampleTime

		if @@ROWCOUNT = 0 begin
			-- Something has happened
			-- 1. SampleTime is older than the last recorded SampleTime
			-- 2. This is the first time a sample is recorded for a InternalAssetId
		
			if (select count(InternalAssetId) from LastKnownSampleStatus where InternalAssetId = @internalAssetid) = 0 begin
				insert into LastKnownSampleStatus (
					InternalAssetId,
					StatusChar,
					StatusInt,
					StatusDecimal,
					SampleTime)
				values (
					@internalAssetid,
					@statusChar,
					@statusInt,
					@statusDecimal,
					@sampleTime)
			end
		end
	end
	drop table #InsertedRows
end

