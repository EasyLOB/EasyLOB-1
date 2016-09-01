/*
USE EasyLOBSecurity
GO
*/

-- AspNetRoles

INSERT INTO AspNetRoles
	(Id, Name, Discriminator)
	VALUES
	('7698dd46-3905-4869-8d10-0428b70c5af7', 'Administrators', 'ApplicationRole')
INSERT INTO AspNetRoles
	(Id, Name, Discriminator)
	VALUES
	('2f6635db-4e44-4228-bd03-c31c830caad3', 'Users', 'ApplicationRole')
    
-- AspNetUsers

-- administrator / administrator@gmail.com / P@ssw0rd
INSERT INTO AspNetUsers
	(Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName)
	VALUES
	('ced7e48f-682c-4903-8c4f-33b31c239a0e', 'administrator@gmail.com', 1, 'AHWnRIT3YsO2xsSg2bVVyohuOJxv3k9lYBEPxFCt8ohJ83Y9Nop7+xptCHk8Dcnrwg==', '76f53491-f82c-41da-a8d2-7c3c17ddd09e', NULL, 0, 0, NULL, 0, 0, 'administrator')
-- user / user@gmail.com / P@ssw0rd
INSERT INTO AspNetUsers
	(Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName)
	VALUES
	('35b60d61-d4ab-4eef-b8b7-963da3979835', 'user@gmail.com', 1, 'AHWnRIT3YsO2xsSg2bVVyohuOJxv3k9lYBEPxFCt8ohJ83Y9Nop7+xptCHk8Dcnrwg==', '76f53491-f82c-41da-a8d2-7c3c17ddd09e', NULL, 0, 0, NULL, 0, 0, 'user')

-- AspNetUserRoles

INSERT INTO AspNetUserRoles
	(UserId, RoleId)
	VALUES
	('ced7e48f-682c-4903-8c4f-33b31c239a0e', '7698dd46-3905-4869-8d10-0428b70c5af7')
INSERT INTO AspNetUserRoles
	(UserId, RoleId)
	VALUES
	('35b60d61-d4ab-4eef-b8b7-963da3979835', '2f6635db-4e44-4228-bd03-c31c830caad3')

-- AspNetActivity

INSERT INTO AspNetActivity VALUES ('fae6a311-6725-4603-87ec-62563ae8a326', 'Chinook-Album')
INSERT INTO AspNetActivity VALUES ('acb53387-66c7-4cd8-8535-fb3bdf3d669f', 'Chinook-Artist')
INSERT INTO AspNetActivity VALUES ('7ef5d02d-345a-4969-af58-091cc96a4abd', 'Chinook-Customer')
INSERT INTO AspNetActivity VALUES ('b205c2fa-66b2-4272-81a8-4aecc39a43f7', 'Chinook-CustomerDocument')
INSERT INTO AspNetActivity VALUES ('540b323d-f256-4ee9-9a56-57f89e8c246e', 'Chinook-Employee')
INSERT INTO AspNetActivity VALUES ('13fb1928-ddfe-49b0-b228-dadabc7babb6', 'Chinook-Genre')
INSERT INTO AspNetActivity VALUES ('c45725c6-416b-434b-ac59-1ba55af02742', 'Chinook-Invoice')
INSERT INTO AspNetActivity VALUES ('a76a1d24-2f05-4578-a56d-ae57b0794732', 'Chinook-InvoiceLine')
INSERT INTO AspNetActivity VALUES ('b2899640-409a-40ff-93e5-a50bd7dfa888', 'Chinook-MediaType')
INSERT INTO AspNetActivity VALUES ('4aef215a-0130-4e16-8ec4-24b2b4dba0e5', 'Chinook-Playlist')
INSERT INTO AspNetActivity VALUES ('9222b814-6808-4c10-b8c4-e0dc336f3a67', 'Chinook-PlaylistTrack')
INSERT INTO AspNetActivity VALUES ('a4618e0a-f051-4c7f-ab96-bbea3c7e0665', 'Chinook-Track')

-- AspNetActivityRoles

INSERT INTO AspNetActivityRoles VALUES ('fae6a311-6725-4603-87ec-62563ae8a326', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('acb53387-66c7-4cd8-8535-fb3bdf3d669f', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('7ef5d02d-345a-4969-af58-091cc96a4abd', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('b205c2fa-66b2-4272-81a8-4aecc39a43f7', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('540b323d-f256-4ee9-9a56-57f89e8c246e', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('13fb1928-ddfe-49b0-b228-dadabc7babb6', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('c45725c6-416b-434b-ac59-1ba55af02742', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('a76a1d24-2f05-4578-a56d-ae57b0794732', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('b2899640-409a-40ff-93e5-a50bd7dfa888', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('4aef215a-0130-4e16-8ec4-24b2b4dba0e5', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('9222b814-6808-4c10-b8c4-e0dc336f3a67', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
INSERT INTO AspNetActivityRoles VALUES ('a4618e0a-f051-4c7f-ab96-bbea3c7e0665', '2f6635db-4e44-4228-bd03-c31c830caad3', 'SCRUD')
