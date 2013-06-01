



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 06/01/2013 11:12:46
-- Generated from EDMX file: C:\Users\Steven\Documents\GitHub\coffeebean\src\datastore\datastore\MBSDataStoreModel.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `mobilespy`;
CREATE DATABASE `mobilespy`;
USE `mobilespy`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `mbs_callset` DROP CONSTRAINT `FK_MBS_SessionMBS_Call`;
--    ALTER TABLE `mbs_contactpropertyset` DROP CONSTRAINT `FK_MBS_ContactMBS_ContactProperty`;
--    ALTER TABLE `mbs_contactset` DROP CONSTRAINT `FK_MBS_SessionMBS_Contact`;
--    ALTER TABLE `mbs_sessionset` DROP CONSTRAINT `FK_MBS_DeviceMBS_Session`;
--    ALTER TABLE `mbs_filesystemset` DROP CONSTRAINT `FK_MBS_SessionMBS_FileSystem`;
--    ALTER TABLE `mbs_sessionset` DROP CONSTRAINT `FK_MBS_SessionMBS_Session`;
--    ALTER TABLE `mbs_smsset` DROP CONSTRAINT `FK_MBS_SessionMBS_SMS`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `mbs_callset`;
    DROP TABLE IF EXISTS `mbs_contactpropertyset`;
    DROP TABLE IF EXISTS `mbs_contactset`;
    DROP TABLE IF EXISTS `mbs_deviceset`;
    DROP TABLE IF EXISTS `mbs_filesystemset`;
    DROP TABLE IF EXISTS `mbs_sessionset`;
    DROP TABLE IF EXISTS `mbs_smsset`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `mbs_callset`(
	`Id` bigint NOT NULL AUTO_INCREMENT UNIQUE, 
	`Number` nvarchar (1000) NOT NULL, 
	`Duration` int NOT NULL, 
	`Status` int NOT NULL, 
	`StartTimeStamp` datetime NOT NULL, 
	`StopTimeStamp` datetime NOT NULL, 
	`MBS_SessionId` bigint NOT NULL, 
	`Class` int NOT NULL);

ALTER TABLE `mbs_callset` ADD PRIMARY KEY (Id);




CREATE TABLE `mbs_contactpropertyset`(
	`Id` bigint NOT NULL AUTO_INCREMENT UNIQUE, 
	`Property` int NOT NULL, 
	`Text` nvarchar (1000), 
	`Datetime` datetime, 
	`Binary` longblob, 
	`MBS_ContactId` bigint NOT NULL);

ALTER TABLE `mbs_contactpropertyset` ADD PRIMARY KEY (Id);




CREATE TABLE `mbs_contactset`(
	`Id` bigint NOT NULL AUTO_INCREMENT UNIQUE, 
	`Label` nvarchar (1000), 
	`MBS_SessionId` bigint NOT NULL, 
	`Class` int NOT NULL);

ALTER TABLE `mbs_contactset` ADD PRIMARY KEY (Id);




CREATE TABLE `mbs_deviceset`(
	`UID` nvarchar (200) NOT NULL, 
	`Type` nvarchar (1000) NOT NULL, 
	`Label` nvarchar (1000), 
	`Manufacturer` nvarchar (1000), 
	`Product` nvarchar (1000), 
	`ESN` nvarchar (1000), 
	`Lac` bigint, 
	`Cid` bigint, 
	`HWRevision` nvarchar (1000), 
	`IMEI` nvarchar (1000), 
	`Phone` bigint, 
	`Platform` nvarchar (1000), 
	`ReturnedIMEI` nvarchar (1000), 
	`SWRevision` nvarchar (1000), 
	`IMSI` nvarchar (1000), 
	`ICCID` nvarchar (1000), 
	`LAI` nvarchar (1000), 
	`Phrase` nvarchar (1000));

ALTER TABLE `mbs_deviceset` ADD PRIMARY KEY (UID);




CREATE TABLE `mbs_filesystemset`(
	`Id` bigint NOT NULL AUTO_INCREMENT UNIQUE, 
	`MappingRootPath` nvarchar (1000) NOT NULL, 
	`MBS_SessionId` bigint NOT NULL);

ALTER TABLE `mbs_filesystemset` ADD PRIMARY KEY (Id);




CREATE TABLE `mbs_sessionset`(
	`Id` bigint NOT NULL AUTO_INCREMENT UNIQUE, 
	`Timestamp` datetime NOT NULL, 
	`MBS_DeviceUID` nvarchar (200) NOT NULL, 
	`ParentSessionId` bigint, 
	`OwnerName` nvarchar (1000), 
	`OwnerID` nvarchar (1000), 
	`Description` nvarchar (1000));

ALTER TABLE `mbs_sessionset` ADD PRIMARY KEY (Id);




CREATE TABLE `mbs_smsset`(
	`Id` bigint NOT NULL AUTO_INCREMENT UNIQUE, 
	`FromNumber` nvarchar (1000), 
	`ReceivedTimeStamp` datetime, 
	`SentTimeStamp` datetime, 
	`Head` nvarchar (1000), 
	`ReceivedTimeZone` bigint, 
	`ServiceCenter` nvarchar (1000), 
	`State` smallint, 
	`Status` tinyint, 
	`Text` nvarchar (1000), 
	`ToNumber` nvarchar (1000), 
	`Type` smallint, 
	`MBS_SessionId` bigint NOT NULL, 
	`Class` int NOT NULL);

ALTER TABLE `mbs_smsset` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `MBS_SessionId` in table 'mbs_callset'

ALTER TABLE `mbs_callset`
ADD CONSTRAINT `FK_MBS_SessionMBS_Call`
    FOREIGN KEY (`MBS_SessionId`)
    REFERENCES `mbs_sessionset`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MBS_SessionMBS_Call'

CREATE INDEX `IX_FK_MBS_SessionMBS_Call` 
    ON `mbs_callset`
    (`MBS_SessionId`);

-- Creating foreign key on `MBS_ContactId` in table 'mbs_contactpropertyset'

ALTER TABLE `mbs_contactpropertyset`
ADD CONSTRAINT `FK_MBS_ContactMBS_ContactProperty`
    FOREIGN KEY (`MBS_ContactId`)
    REFERENCES `mbs_contactset`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MBS_ContactMBS_ContactProperty'

CREATE INDEX `IX_FK_MBS_ContactMBS_ContactProperty` 
    ON `mbs_contactpropertyset`
    (`MBS_ContactId`);

-- Creating foreign key on `MBS_SessionId` in table 'mbs_contactset'

ALTER TABLE `mbs_contactset`
ADD CONSTRAINT `FK_MBS_SessionMBS_Contact`
    FOREIGN KEY (`MBS_SessionId`)
    REFERENCES `mbs_sessionset`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MBS_SessionMBS_Contact'

CREATE INDEX `IX_FK_MBS_SessionMBS_Contact` 
    ON `mbs_contactset`
    (`MBS_SessionId`);

-- Creating foreign key on `MBS_DeviceUID` in table 'mbs_sessionset'

ALTER TABLE `mbs_sessionset`
ADD CONSTRAINT `FK_MBS_DeviceMBS_Session`
    FOREIGN KEY (`MBS_DeviceUID`)
    REFERENCES `mbs_deviceset`
        (`UID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MBS_DeviceMBS_Session'

CREATE INDEX `IX_FK_MBS_DeviceMBS_Session` 
    ON `mbs_sessionset`
    (`MBS_DeviceUID`);

-- Creating foreign key on `MBS_SessionId` in table 'mbs_filesystemset'

ALTER TABLE `mbs_filesystemset`
ADD CONSTRAINT `FK_MBS_SessionMBS_FileSystem`
    FOREIGN KEY (`MBS_SessionId`)
    REFERENCES `mbs_sessionset`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MBS_SessionMBS_FileSystem'

CREATE INDEX `IX_FK_MBS_SessionMBS_FileSystem` 
    ON `mbs_filesystemset`
    (`MBS_SessionId`);

-- Creating foreign key on `ParentSessionId` in table 'mbs_sessionset'

ALTER TABLE `mbs_sessionset`
ADD CONSTRAINT `FK_MBS_SessionMBS_Session`
    FOREIGN KEY (`ParentSessionId`)
    REFERENCES `mbs_sessionset`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MBS_SessionMBS_Session'

CREATE INDEX `IX_FK_MBS_SessionMBS_Session` 
    ON `mbs_sessionset`
    (`ParentSessionId`);

-- Creating foreign key on `MBS_SessionId` in table 'mbs_smsset'

ALTER TABLE `mbs_smsset`
ADD CONSTRAINT `FK_MBS_SessionMBS_SMS`
    FOREIGN KEY (`MBS_SessionId`)
    REFERENCES `mbs_sessionset`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MBS_SessionMBS_SMS'

CREATE INDEX `IX_FK_MBS_SessionMBS_SMS` 
    ON `mbs_smsset`
    (`MBS_SessionId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
