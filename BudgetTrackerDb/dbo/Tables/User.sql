CREATE TABLE [dbo].[User] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (255) NOT NULL,
    [PasswordHash] VARCHAR (255)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Username] UNIQUE NONCLUSTERED ([Username] ASC)
);

