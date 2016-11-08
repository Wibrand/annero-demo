CREATE PROCEDURE [dbo].[UpdateLatestSampleStatus]
	@internalAssetid int,
	@sampleTime DateTime,
	@statusChar varchar(20),
	@statusInt int,
	@statusDecimal decimal

AS
	SET NOCOUNT ON
	     
	UPDATE dbo.LastKnownSampleStatus
	SET
		SampleTime = @sampleTime,
		StatusChar = @statusChar,
		StatusInt = @statusInt,
		StatusDecimal = @statusDecimal
	WHERE
		InternalAssetId = @internalAssetid AND SampleTime < @sampleTime

	IF @@ROWCOUNT = 0 BEGIN
		-- Something has happened
		-- 1. SampleTime is older than the last recorded SampleTime
		-- 2. This is the first time a sample is recorded for a InternalAssetId
		
		IF (SELECT COUNT(InternalAssetId) FROM dbo.LastKnownSampleStatus WHERE InternalAssetId = @internalAssetid) = 0 BEGIN
			INSERT INTO dbo.LastKnownSampleStatus (
				InternalAssetId,
				StatusChar,
				StatusInt,
				StatusDecimal,
				SampleTime)
			VALUES (
				@internalAssetid,
				@statusChar,
				@statusInt,
				@statusDecimal,
				@sampleTime)
		END
	END

RETURN 0