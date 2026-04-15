CREATE TABLE [dbo].[Industry] (
    [IND_CODE] INT NOT NULL PRIMARY KEY,       -- Код галузі [cite: 27]
    [IND_NAME] NVARCHAR(5) NULL,                -- Коротка назва [cite: 27]
    [LONG_NAME] NVARCHAR(20) NULL               -- Повна назва [cite: 27]
);