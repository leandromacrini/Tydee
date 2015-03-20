




-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/18/2015 17:38:47
-- Generated from EDMX file: C:\Users\esd81leamacr.ICC\Documents\Visual Studio 2013\Projects\Tydee\Tydee\Tydee\DataModels\TydeeDataModel.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Users` DROP CONSTRAINT `FK_UserUserOption`;
--    ALTER TABLE `Collections` DROP CONSTRAINT `FK_CollectionCollectionOption`;
--    ALTER TABLE `CollectionOptions` DROP CONSTRAINT `FK_UserOptionCollectionOption`;
--    ALTER TABLE `Collections` DROP CONSTRAINT `FK_UserCollection`;
--    ALTER TABLE `Shops` DROP CONSTRAINT `FK_UserShop`;
--    ALTER TABLE `Items` DROP CONSTRAINT `FK_ItemCollection`;
--    ALTER TABLE `Pictures` DROP CONSTRAINT `FK_ItemPicture`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Collections`;
    DROP TABLE IF EXISTS `Users`;
    DROP TABLE IF EXISTS `Shops`;
    DROP TABLE IF EXISTS `UserOptions`;
    DROP TABLE IF EXISTS `CollectionOptions`;
    DROP TABLE IF EXISTS `Items`;
    DROP TABLE IF EXISTS `Pictures`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Collections`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`EBayName` longtext, 
	`ShopInfo` longtext, 
	`UserId` int NOT NULL, 
	`CollectionOption_Id` int NOT NULL);

ALTER TABLE `Collections` ADD PRIMARY KEY (Id);




CREATE TABLE `Users`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Email` longtext NOT NULL, 
	`UserName` longtext NOT NULL, 
	`Password` longtext NOT NULL, 
	`CreationDate` datetime NOT NULL, 
	`LastAccessDate` datetime NOT NULL, 
	`FirstName` longtext NOT NULL, 
	`LastName` longtext NOT NULL, 
	`SocialCreated` bool NOT NULL, 
	`ImportId` longtext, 
	`UserOption_Id` int NOT NULL);

ALTER TABLE `Users` ADD PRIMARY KEY (Id);




CREATE TABLE `Shops`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Enabled` bool NOT NULL, 
	`PaymentInfo` longtext, 
	`ShipmentInfo` longtext, 
	`ShowOwned` bool NOT NULL, 
	`ShowSearched` bool NOT NULL, 
	`ShowSalable` bool NOT NULL, 
	`User_Id` int NOT NULL);

ALTER TABLE `Shops` ADD PRIMARY KEY (Id);




CREATE TABLE `UserOptions`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ShowPhoto` bool NOT NULL, 
	`CollectionOrder` int NOT NULL, 
	`DialogOnNew` bool NOT NULL);

ALTER TABLE `UserOptions` ADD PRIMARY KEY (Id);




CREATE TABLE `CollectionOptions`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ShowExchanged` bool NOT NULL, 
	`ShowArchived` bool NOT NULL, 
	`ShowSold` bool NOT NULL, 
	`ShowSearched` bool NOT NULL, 
	`AlphaOrder` bool NOT NULL, 
	`FavoriteFirst` bool NOT NULL, 
	`OwnedFirst` bool NOT NULL, 
	`UserOptionCollectionOption_CollectionOption_Id` int NOT NULL);

ALTER TABLE `CollectionOptions` ADD PRIMARY KEY (Id);




CREATE TABLE `Items`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`PublicInfo` longtext, 
	`PrivateInfo` longtext, 
	`BuyPrice` decimal( 10, 2 ) , 
	`SellPrice` decimal( 10, 2 ) , 
	`Owned` bool NOT NULL, 
	`Searched` bool NOT NULL, 
	`Sold` bool NOT NULL, 
	`Exchanged` bool NOT NULL, 
	`Archived` bool NOT NULL, 
	`Salable` bool NOT NULL, 
	`Favorite` bool NOT NULL, 
	`Conditions` int NOT NULL, 
	`Damage` int NOT NULL, 
	`Collection_Id` int NOT NULL);

ALTER TABLE `Items` ADD PRIMARY KEY (Id);




CREATE TABLE `Pictures`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`MainFile` longtext, 
	`ThumbFile` longtext, 
	`ItemId` int NOT NULL);

ALTER TABLE `Pictures` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `UserOption_Id` in table 'Users'

ALTER TABLE `Users`
ADD CONSTRAINT `FK_UserUserOption`
    FOREIGN KEY (`UserOption_Id`)
    REFERENCES `UserOptions`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserOption'

CREATE INDEX `IX_FK_UserUserOption` 
    ON `Users`
    (`UserOption_Id`);

-- Creating foreign key on `CollectionOption_Id` in table 'Collections'

ALTER TABLE `Collections`
ADD CONSTRAINT `FK_CollectionCollectionOption`
    FOREIGN KEY (`CollectionOption_Id`)
    REFERENCES `CollectionOptions`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CollectionCollectionOption'

CREATE INDEX `IX_FK_CollectionCollectionOption` 
    ON `Collections`
    (`CollectionOption_Id`);

-- Creating foreign key on `UserOptionCollectionOption_CollectionOption_Id` in table 'CollectionOptions'

ALTER TABLE `CollectionOptions`
ADD CONSTRAINT `FK_UserOptionCollectionOption`
    FOREIGN KEY (`UserOptionCollectionOption_CollectionOption_Id`)
    REFERENCES `UserOptions`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOptionCollectionOption'

CREATE INDEX `IX_FK_UserOptionCollectionOption` 
    ON `CollectionOptions`
    (`UserOptionCollectionOption_CollectionOption_Id`);

-- Creating foreign key on `UserId` in table 'Collections'

ALTER TABLE `Collections`
ADD CONSTRAINT `FK_UserCollection`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCollection'

CREATE INDEX `IX_FK_UserCollection` 
    ON `Collections`
    (`UserId`);

-- Creating foreign key on `User_Id` in table 'Shops'

ALTER TABLE `Shops`
ADD CONSTRAINT `FK_UserShop`
    FOREIGN KEY (`User_Id`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserShop'

CREATE INDEX `IX_FK_UserShop` 
    ON `Shops`
    (`User_Id`);

-- Creating foreign key on `Collection_Id` in table 'Items'

ALTER TABLE `Items`
ADD CONSTRAINT `FK_ItemCollection`
    FOREIGN KEY (`Collection_Id`)
    REFERENCES `Collections`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCollection'

CREATE INDEX `IX_FK_ItemCollection` 
    ON `Items`
    (`Collection_Id`);

-- Creating foreign key on `ItemId` in table 'Pictures'

ALTER TABLE `Pictures`
ADD CONSTRAINT `FK_ItemPicture`
    FOREIGN KEY (`ItemId`)
    REFERENCES `Items`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemPicture'

CREATE INDEX `IX_FK_ItemPicture` 
    ON `Pictures`
    (`ItemId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
