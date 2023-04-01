IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Todos] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Priority] smallint NOT NULL,
    CONSTRAINT [PK_Todos] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230401162703_initial', N'6.0.15');
GO

COMMIT;
GO

