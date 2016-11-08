CREATE TABLE [dbo].[Asset] (
    [InternalAssetId] INT          IDENTITY (1, 1) NOT NULL,
    [ExternalAssetId] VARCHAR (50) NOT NULL,
    [LogicalName]     VARCHAR (50) NOT NULL,
    [Type]            VARCHAR (20) NOT NULL,
    [AssetSampleType] VARCHAR (20) DEFAULT ('status') NOT NULL,
    [Category]        VARCHAR (30) NULL,
    [SubCategory]     VARCHAR (30) NULL,
    [AreaId]          VARCHAR (20) NULL,
    [FloorId]         VARCHAR (20) NULL,
    [BuildingId]      VARCHAR (20) NULL,
    [LastUpdated]     DATETIME     DEFAULT (getdate()) NOT NULL,
    [UtcHourOffset]   INT          CONSTRAINT [DF_Asset_UtcHourOffset] DEFAULT ((2)) NOT NULL,
    PRIMARY KEY CLUSTERED ([InternalAssetId] ASC),
    CONSTRAINT [UQ_Asset_ExternalAssetId] UNIQUE NONCLUSTERED ([ExternalAssetId] ASC)
);

