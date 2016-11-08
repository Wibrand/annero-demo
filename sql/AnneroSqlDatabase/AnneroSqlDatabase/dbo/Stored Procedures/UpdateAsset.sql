CREATE PROCEDURE [dbo].[UpdateAsset]
	@externalAssetId varchar(50),
	@logicalName varchar(50),
	@type varchar(20),
	@category varchar(20),
	@subCategory varchar(30),
	@areaId varchar(20),
	@floorId varchar(20),
	@buildingId varchar(20)
AS
	SET NOCOUNT ON;

	DECLARE @intId int;
	DECLARE @sampleType varchar(20);

	SELECT @sampleType = 'status';
	IF @type = 'desk' OR @type = 'conferenceRoom' OR @type = 'area' OR @type = 'toilet' BEGIN
		SELECT @sampleType = 'event';
	END

	SELECT @intId = InternalAssetId FROM dbo.Asset WHERE @externalAssetId = ExternalAssetId;

	IF @intId > 0 BEGIN
		UPDATE dbo.Asset
		SET
			LogicalName =	  COALESCE(@logicalName, LogicalName),
			[Type] =		  COALESCE(@type, [Type]),
			AssetSampleType = @sampleType,
			Category =		  COALESCE(@category, Category),
			SubCategory =	  COALESCE(@subCategory, SubCategory),
			AreaId =		  COALESCE(@areaId, AreaId),
			FloorId =		  COALESCE(@floorId, FloorId),
			BuildingId =	  COALESCE(@buildingId, BuildingId),
			LastUpdated =	  GETDATE()
		WHERE
			InternalAssetId = @intId; 
	END
	ELSE BEGIN
		INSERT INTO dbo.Asset (
			ExternalAssetId,
			LogicalName,
			[Type],
			AssetSampleType,
			Category,
			SubCategory,
			AreaId,
			FloorId,
			BuildingId,
			LastUpdated)
		VALUES (
			@externalAssetId,
			@logicalName,
			@type,
			@sampleType,
			@category,
			@subCategory,
			@areaId,
			@floorId,
			@buildingId,
			GETDATE())

			IF @sampleType = 'event' BEGIN
				INSERT INTO dbo.LastKnownSampleStatus (
					InternalAssetId,
					StatusChar,
					SampleTime)
				VALUES (
					SCOPE_IDENTITY(),
					'free',
					GETDATE())
			END
	END

RETURN 0