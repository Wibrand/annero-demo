CREATE PROCEDURE [dbo].[GetLatestStatusForAssets]

AS
	SET NOCOUNT ON

	SELECT A.LogicalName, A.BuildingId, A.AreaId, A.FloorId, A.Type, L.StatusChar, L.SampleTime
	FROM LastKnownSampleStatus L
	JOIN Asset A ON L.InternalAssetId = A.InternalAssetId
	WHERE A.Type IN('desk', 'conferenceRoom', 'area', 'toilet') 

RETURN 0