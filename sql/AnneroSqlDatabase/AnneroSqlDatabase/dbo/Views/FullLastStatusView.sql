

CREATE VIEW [dbo].[FullLastStatusView]
	AS 
	SELECT
		L.InternalAssetId,
		A.ExternalAssetId,
		A.[Type],
		A.LogicalName,
		L.SampleTime,
		L.StatusChar,
		L.StatusInt,
		L.StatusDecimal,
		A.Category,
		A.SubCategory,
		A.AreaId,
		A.FloorId,
		A.BuildingId
	FROM dbo.LastKnownSampleStatus L
	JOIN dbo.Asset A ON L.InternalAssetId = A.InternalAssetId

