
CREATE PROCEDURE [dbo].[CreateSnapshotOfCurrentStatusOfAllSamples]
	@sampleCollectedTime datetime
AS
	SET NOCOUNT ON

	--- Events
	INSERT INTO dbo.Sample (
		InternalAssetId,
		SampleCollectedTime,
		SampleTime,
		StatusChar,
		StatusInt,
		StatusDecimal)
	SELECT L.InternalAssetId, @sampleCollectedTime, SampleTime, StatusChar, StatusInt, StatusDecimal
	FROM dbo.LastKnownSampleStatus L
	JOIN dbo.Asset A ON L.InternalAssetId = A.InternalAssetId
	WHERE A.AssetSampleType = 'event'

/*
	--- Status
	INSERT INTO dbo.Sample (
		InternalAssetId,
		SampleCollectedTime,
		SampleTime,
		StatusChar,
		StatusInt,
		StatusDecimal)
	SELECT L.InternalAssetId, @sampleCollectedTime, SampleTime, StatusChar, StatusInt, StatusDecimal
	FROM dbo.LastKnownSampleStatus L
	JOIN dbo.Asset A ON L.InternalAssetId = A.InternalAssetId
	WHERE A.AssetSampleType = 'status' AND (DATEDIFF(MINUTE, @sampleCollectedTime, SampleTime) > -10 OR DATEDIFF(MINUTE, @sampleCollectedTime, SampleTime) < 10)
*/

RETURN 0
