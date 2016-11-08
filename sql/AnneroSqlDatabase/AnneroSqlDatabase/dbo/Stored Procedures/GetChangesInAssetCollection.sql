

CREATE PROCEDURE [dbo].[GetChangesInAssetCollection]
AS
	SET NOCOUNT ON;

	DECLARE @lastDateUpdated datetime;

	SELECT @lastDateUpdated = CONVERT(datetime, [Value])
	FROM dbo.ValuesStore
	WHERE Id = 'lastuppdatedinasset'

	IF (SELECT COUNT(*) FROM dbo.Asset WHERE LastUpdated > @lastDateUpdated) > 0 BEGIN
	
		UPDATE dbo.ValuesStore
			SET [Value] = CONVERT(varchar, GETDATE())
		WHERE Id = 'lastuppdatedinasset';

		SELECT InternalAssetId, ExternalAssetId, LogicalName, Type, Category, SubCategory, AreaId, FloorId, BuildingId, LastUpdated FROM dbo.Asset
		RETURN 0; 
	END
	ELSE BEGIN
		RETURN -1;
	END
