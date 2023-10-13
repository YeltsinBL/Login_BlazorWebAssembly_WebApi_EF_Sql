/*create DATABASE LoginApp
GO
use LoginApp
GO
*/
create TABLE Roles(
Id NVARCHAR(450) PRIMARY KEY,
[Name] NVARCHAR(256),
NormalizedName NVARCHAR(256),
ConcurrencyStamp NVARCHAR(MAX) 
)
GO
create table Users(
Id NVARCHAR(450) PRIMARY KEY,
UserName NVARCHAR(256),
NormalizedUserName NVARCHAR(256),
Email NVARCHAR(256),
NormalizedEmail NVARCHAR(256),
EmailConfirmed BIT not NULL,
PasswordHash NVARCHAR(MAX),
SecurityStamp NVARCHAR(MAX),
ConcurrencyStamp NVARCHAR(MAX),
PhoneNumber NVARCHAR(MAX),
PhoneNumberConfirmed BIT not NULL,
TwoFactorEnabled BIT NOT NULL,
LockoutEnd DATETIME,
LockoutEnabled BIT NOT NULL,
AccessFailedCount int NOT NULL
)
GO
create TABLE RoleClaims(
Id int PRIMARY KEY IDENTITY,
RoleId NVARCHAR(450) NOT NULL,
ClaimType NVARCHAR(MAX),
ClaimValue NVARCHAR(MAX),
CONSTRAINT FK_RoleClaims_Roles_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(Id)
)
GO
CREATE TABLE UserClaims(
Id int PRIMARY KEY IDENTITY,
UserId NVARCHAR(450) not NULL,
ClaimType NVARCHAR(MAX),
ClaimValue NVARCHAR(MAX),
CONSTRAINT FK_UserClaims_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(Id)
)
GO
CREATE TABLE UserLogins(
LoginProvider NVARCHAR(128) PRIMARY KEY,
ProviderKey NVARCHAR(128) not NULL,
ProviderDisplayName NVARCHAR(MAX),
UserId NVARCHAR(450) not NULL,
CONSTRAINT FK_UserLogins_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(Id)
)
GO
create TABLE UserRoles(
UserId NVARCHAR(450) NOT NULL,
RoleId NVARCHAR(450) NOT NULL,
CONSTRAINT FK_UserRoles_Roles_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(Id),
CONSTRAINT FK_UserRoles_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(Id)
)
GO
CREATE TABLE UserTokens(
UserId NVARCHAR(450) not NULL,
LoginProvider NVARCHAR(128) not NULL,
[Name] NVARCHAR(128) NOT NULL,
[Value] NVARCHAR(MAX),
CONSTRAINT FK_UserTokens_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(Id)
)
GO
