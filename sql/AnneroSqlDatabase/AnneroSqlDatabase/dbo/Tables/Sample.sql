CREATE TABLE [dbo].[Sample] (
    [InternalAssetId]     INT             NOT NULL,
    [SampleCollectedTime] DATETIME        NOT NULL,
    [StatusChar]          VARCHAR (20)    NULL,
    [StatusInt]           INT             NULL,
    [StatusDecimal]       DECIMAL (10, 4) NULL,
    [SampleTime]          DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([InternalAssetId] ASC, [SampleCollectedTime] ASC)
);


GO
CREATE NONCLUSTERED INDEX [nci_wi_Sample_BC6AB451B62CCF171B26747F226916D7]
    ON [dbo].[Sample]([StatusChar] ASC)
    INCLUDE([InternalAssetId], [SampleCollectedTime]);


GO
CREATE NONCLUSTERED INDEX [nci_wi_Sample_FB873EA5D8FB6C14DFF9E517413ECD7F]
    ON [dbo].[Sample]([StatusChar] ASC)
    INCLUDE([SampleTime]);

