CREATE TABLE [dbo].[Income] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [BudgetId]    INT             NOT NULL,
    [CategoryId]  INT             NOT NULL,
    [Amount]      DECIMAL (20, 2) NULL,
    [Date]        DATE            NULL,
    [Description] NVARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([BudgetId]) REFERENCES [dbo].[Budget] ([Id]),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);

