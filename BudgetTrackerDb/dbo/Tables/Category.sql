﻿CREATE TABLE [dbo].[Category] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId] INT            NOT NULL,
    [Name]   NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

