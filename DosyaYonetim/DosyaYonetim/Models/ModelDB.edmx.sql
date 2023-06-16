
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/12/2023 02:44:15
-- Generated from EDMX file: C:\Users\alkan\OneDrive\Masaüstü\DosyaYonetim\DosyaYonetim\DosyaYonetim\Models\ModelDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DosyaYonetimDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Dosya_ToKategori]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dosya] DROP CONSTRAINT [FK_Dosya_ToKategori];
GO
IF OBJECT_ID(N'[dbo].[FK_Dosya_Uye]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dosya] DROP CONSTRAINT [FK_Dosya_Uye];
GO
IF OBJECT_ID(N'[dbo].[FK_DosyaEtiket_Dosya]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DosyaEtiket] DROP CONSTRAINT [FK_DosyaEtiket_Dosya];
GO
IF OBJECT_ID(N'[dbo].[FK_DosyaEtiket_Etiket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DosyaEtiket] DROP CONSTRAINT [FK_DosyaEtiket_Etiket];
GO
IF OBJECT_ID(N'[dbo].[FK_Yorum_Dosya]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Yorum] DROP CONSTRAINT [FK_Yorum_Dosya];
GO
IF OBJECT_ID(N'[dbo].[FK_Yorum_Uye]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Yorum] DROP CONSTRAINT [FK_Yorum_Uye];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Dosya]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dosya];
GO
IF OBJECT_ID(N'[dbo].[DosyaEtiket]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DosyaEtiket];
GO
IF OBJECT_ID(N'[dbo].[Etiket]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Etiket];
GO
IF OBJECT_ID(N'[dbo].[Kategori]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kategori];
GO
IF OBJECT_ID(N'[dbo].[Uye]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Uye];
GO
IF OBJECT_ID(N'[dbo].[Yorum]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Yorum];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Dosya'
CREATE TABLE [dbo].[Dosya] (
    [DosyaId] int IDENTITY(1,1) NOT NULL,
    [Adi] nvarchar(100)  NULL,
    [Uzanti] nvarchar(max)  NULL,
    [Foto] nvarchar(100)  NULL,
    [Tarih] datetime  NULL,
    [KategoriId] int  NOT NULL,
    [UyeId] int  NOT NULL,
    [Goruntulenme] int  NOT NULL,
);
GO

-- Creating table 'Etiket'
CREATE TABLE [dbo].[Etiket] (
    [EtiketId] int IDENTITY(1,1) NOT NULL,
    [EtiketAdi] nvarchar(50)  NULL
);
GO

-- Creating table 'Kategori'
CREATE TABLE [dbo].[Kategori] (
    [KategoriId] int IDENTITY(1,1) NOT NULL,
    [KategoriAdi] nchar(100)  NULL
);
GO

-- Creating table 'Uye'
CREATE TABLE [dbo].[Uye] (
    [UyeId] int IDENTITY(1,1) NOT NULL,
    [KullaniciAdi] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Sifre] nvarchar(50)  NULL,
    [AdSoyad] nvarchar(50)  NULL,
    [Foto] nchar(100)  NULL,
    [UyeAdmin] int  NOT NULL
);
GO

-- Creating table 'Yorum'
CREATE TABLE [dbo].[Yorum] (
    [YorumId] int IDENTITY(1,1) NOT NULL,
    [YorumIcerik] nchar(500)  NULL,
    [UyeId] int  NOT NULL,
    [DosyaId] int  NOT NULL,
    [Tarih] datetime  NULL
);
GO

-- Creating table 'DosyaEtiket'
CREATE TABLE [dbo].[DosyaEtiket] (
    [Dosya_DosyaId] int  NOT NULL,
    [Etiket_EtiketId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DosyaId] in table 'Dosya'
ALTER TABLE [dbo].[Dosya]
ADD CONSTRAINT [PK_Dosya]
    PRIMARY KEY CLUSTERED ([DosyaId] ASC);
GO

-- Creating primary key on [EtiketId] in table 'Etiket'
ALTER TABLE [dbo].[Etiket]
ADD CONSTRAINT [PK_Etiket]
    PRIMARY KEY CLUSTERED ([EtiketId] ASC);
GO

-- Creating primary key on [KategoriId] in table 'Kategori'
ALTER TABLE [dbo].[Kategori]
ADD CONSTRAINT [PK_Kategori]
    PRIMARY KEY CLUSTERED ([KategoriId] ASC);
GO

-- Creating primary key on [UyeId] in table 'Uye'
ALTER TABLE [dbo].[Uye]
ADD CONSTRAINT [PK_Uye]
    PRIMARY KEY CLUSTERED ([UyeId] ASC);
GO

-- Creating primary key on [YorumId] in table 'Yorum'
ALTER TABLE [dbo].[Yorum]
ADD CONSTRAINT [PK_Yorum]
    PRIMARY KEY CLUSTERED ([YorumId] ASC);
GO

-- Creating primary key on [Dosya_DosyaId], [Etiket_EtiketId] in table 'DosyaEtiket'
ALTER TABLE [dbo].[DosyaEtiket]
ADD CONSTRAINT [PK_DosyaEtiket]
    PRIMARY KEY CLUSTERED ([Dosya_DosyaId], [Etiket_EtiketId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [KategoriId] in table 'Dosya'
ALTER TABLE [dbo].[Dosya]
ADD CONSTRAINT [FK_Dosya_ToKategori]
    FOREIGN KEY ([KategoriId])
    REFERENCES [dbo].[Kategori]
        ([KategoriId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Dosya_ToKategori'
CREATE INDEX [IX_FK_Dosya_ToKategori]
ON [dbo].[Dosya]
    ([KategoriId]);
GO

-- Creating foreign key on [UyeId] in table 'Dosya'
ALTER TABLE [dbo].[Dosya]
ADD CONSTRAINT [FK_Dosya_Uye]
    FOREIGN KEY ([UyeId])
    REFERENCES [dbo].[Uye]
        ([UyeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Dosya_Uye'
CREATE INDEX [IX_FK_Dosya_Uye]
ON [dbo].[Dosya]
    ([UyeId]);
GO

-- Creating foreign key on [DosyaId] in table 'Yorum'
ALTER TABLE [dbo].[Yorum]
ADD CONSTRAINT [FK_Yorum_Dosya]
    FOREIGN KEY ([DosyaId])
    REFERENCES [dbo].[Dosya]
        ([DosyaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Yorum_Dosya'
CREATE INDEX [IX_FK_Yorum_Dosya]
ON [dbo].[Yorum]
    ([DosyaId]);
GO

-- Creating foreign key on [UyeId] in table 'Yorum'
ALTER TABLE [dbo].[Yorum]
ADD CONSTRAINT [FK_Yorum_Uye]
    FOREIGN KEY ([UyeId])
    REFERENCES [dbo].[Uye]
        ([UyeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Yorum_Uye'
CREATE INDEX [IX_FK_Yorum_Uye]
ON [dbo].[Yorum]
    ([UyeId]);
GO

-- Creating foreign key on [Dosya_DosyaId] in table 'DosyaEtiket'
ALTER TABLE [dbo].[DosyaEtiket]
ADD CONSTRAINT [FK_DosyaEtiket_Dosya]
    FOREIGN KEY ([Dosya_DosyaId])
    REFERENCES [dbo].[Dosya]
        ([DosyaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Etiket_EtiketId] in table 'DosyaEtiket'
ALTER TABLE [dbo].[DosyaEtiket]
ADD CONSTRAINT [FK_DosyaEtiket_Etiket]
    FOREIGN KEY ([Etiket_EtiketId])
    REFERENCES [dbo].[Etiket]
        ([EtiketId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DosyaEtiket_Etiket'
CREATE INDEX [IX_FK_DosyaEtiket_Etiket]
ON [dbo].[DosyaEtiket]
    ([Etiket_EtiketId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------