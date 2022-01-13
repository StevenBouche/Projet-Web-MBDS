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

CREATE TABLE [CourseImages] (
    [Id] int NOT NULL IDENTITY,
    [Extention] nvarchar(max) NOT NULL,
    [Data] varbinary(max) NOT NULL,
    [CourseId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_CourseImages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserProfilImages] (
    [Id] int NOT NULL IDENTITY,
    [Extention] nvarchar(max) NOT NULL,
    [Data] varbinary(max) NOT NULL,
    [UserId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_UserProfilImages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Password] nvarchar(255) NOT NULL,
    [Role] int NOT NULL,
    [ImageId] int NULL,
    [RefreshTokenId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_UserProfilImages_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [UserProfilImages] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [UserId] int NOT NULL,
    [ImageId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Courses_CourseImages_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [CourseImages] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RefreshTokens] (
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(450) NOT NULL,
    [ExpireAt] bigint NOT NULL,
    [UserId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Assignments] (
    [Id] int NOT NULL IDENTITY,
    [Label] nvarchar(max) NOT NULL,
    [State] int NOT NULL,
    [DelivryDate] datetime2 NOT NULL,
    [CourseId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Assignments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Assignments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [WorkSubmits] (
    [Id] int NOT NULL IDENTITY,
    [Label] nvarchar(max) NOT NULL,
    [Grade] float NOT NULL,
    [Comment] nvarchar(max) NOT NULL,
    [State] int NOT NULL,
    [UserId] int NOT NULL,
    [AssignmentId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_WorkSubmits] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkSubmits_Assignments_AssignmentId] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignments] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_WorkSubmits_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Assignments_CourseId] ON [Assignments] ([CourseId]);
GO

CREATE UNIQUE INDEX [IX_Courses_ImageId] ON [Courses] ([ImageId]) WHERE [ImageId] IS NOT NULL;
GO

CREATE INDEX [IX_Courses_UserId] ON [Courses] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_RefreshTokens_Token] ON [RefreshTokens] ([Token]);
GO

CREATE UNIQUE INDEX [IX_RefreshTokens_UserId] ON [RefreshTokens] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Users_ImageId] ON [Users] ([ImageId]) WHERE [ImageId] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Users_Name] ON [Users] ([Name]);
GO

CREATE INDEX [IX_WorkSubmits_AssignmentId] ON [WorkSubmits] ([AssignmentId]);
GO

CREATE UNIQUE INDEX [IX_WorkSubmits_UserId_AssignmentId] ON [WorkSubmits] ([UserId], [AssignmentId]) WHERE [AssignmentId] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220112173936_InitialCreate', N'6.0.1');
GO

COMMIT;
GO

