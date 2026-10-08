<h2 style={"text-align": center;}><b>Dayaratne Holdings ERP Module</b></h2>
<hr/>
<img src="https://omniaccounts.co.za/wp-content/uploads/2021/03/The-History-of-Enterprise-Resource-Planning.jpg" />

https://youtu.be/SDNwwTkceAY
https://www.learnentityframeworkcore.com/relationships/referential-constraint-action-options

https://www.ezzylearning.net/tutorial/building-asp-net-core-apps-with-clean-architecture

https://www.thomasclaudiushuber.com/2021/02/25/c-9-0-pattern-matching-in-switch-expressions/

save => edit => update 

//https://code-maze.com/dotnetcore-secure-microservices-jwt-ocelot/ and https://learn.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot

// https://blog.devops.dev/microservices-async-communication-using-ocelot-gateway-rabbitmq-docker-and-angular-14-b3716e8503e4

// https://www.learmoreseekmore.com/2022/03/dotnet6-part4-asynchronous-data-communication-between-microservices-using-rabbitmq-message-broker-with-masstransit.html

// https://auth0.com/blog/implementing-api-gateway-in-aspnet-core-with-ocelot/

// https://www.youtube.com/watch?v=P2osfctiHAc&t=653s

//https://www.infoworld.com/article/3669188/how-to-implement-jwt-authentication-in-aspnet-core.html

//https://juldhais.net/secure-way-to-store-passwords-in-database-using-sha256-asp-net-core-898128d1c4ef

Unit of Work Is Even Better With MediatR + TransactionScope

https://www.sqlservercentral.com/articles/docker-desktop-on-windows-10-for-sql-server-step-by-step

https://www.google.com/search?q=docker+hub+upload+image&rlz=1C1CHBF_enLK1096LK1096&oq=docker+hub+upl&gs_lcrp=EgZjaHJvbWUqBwgAEAAYgAQyBwgAEAAYgAQyBggBEEUYOTIICAIQABgWGB4yDQgDEAAYhgMYgAQYigUyDQgEEAAYhgMYgAQYigUyDQgFEAAYhgMYgAQYigUyCggGEAAYgAQYogQyCggHEAAYgAQYogSoAgiwAgE&sourceid=chrome&ie=UTF-8#fpstate=ive&vld=cid:7a0ab999,vid:zO0O-EzXYhk,st:0

Sql server terminal login    -	docker exec -it coredb /opt/mssql-tools/bin/sqlcmd -S localhost -U sa

EF Core Migration Command    -	dotnet ef migrations add initial5 -c ApplicationDbContext --project Core.Persistence\Core.Persistence.csproj -o Migrations --startup-project Core.WebAPI\Core.WebAPI.csproj

                             -  dotnet ef database update -c ApplicationDbContext --project Core.Persistence\Core.Persistence.csproj --startup-project Core.WebAPI\Core.WebAPI.csproj


D:\GitHub\DHERPSOLUATION\src\Services\HCM\HCM.Application
D:\GitHub\DHERPSOLUATION\src\Services\HCM\HCM.Domain
D:\GitHub\DHERPSOLUATION\src\Services\HCM\HCM.Shared
D:\GitHub\DHERPSOLUATION\src\Services\HCM\HCM.Infrastructure
D:\GitHub\DHERPSOLUATION\src\Services\HCM\HCM.Persistence
D:\GitHub\DHERPSOLUATION\src\Services\HCM\HCM.WebAPI


Application

Common
DTOs
Extensions
Features
Helper
Interfaces
Requests
Responses


Infrastructure

Extensions
Services

Persistence

Contexts
DbContextInterceptors
Extensions
Repositories

Shared

Constants
Extensions
Services
Interfaces

USE [CoreDB]
GO

USE [CoreDB]
GO

/****** Object:  Sequence [dbo].[RTID]    Script Date: 6/2/2024 8:02:58 PM ******/
CREATE SEQUENCE [dbo].[RTID] 
 AS [int]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO

docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build

docker compose build

https://medium.com/@komalminhas.96/a-step-by-step-guide-to-build-and-push-your-own-docker-images-to-dockerhub-709963d4a8bc
https://medium.com/@kesaralive/diving-deeper-into-docker-networking-with-docker-compose-737e3b8a3c8c

docker run -d --net earth -p 6003:8000 --name corewebapifromhub kokilasanjeewa/corewebapi:latest

https://www.freecodecamp.org/news/where-are-docker-images-stored-docker-container-paths-explained/


To extend the JWT token invalidation logic using a SQL table, you'll need to store invalidated tokens in a database and check this list during the token validation process. Here’s a detailed guide on how to implement this in an ASP.NET Core application:

  1. Steps to Implement JWT Token Invalidation with SQL Storage
  2. Set up the database.
  3. Create a table for storing invalidated tokens.
  4. Implement the ITokenService to interact with the database.
  5. Configure the JWT token validation to check the database.
  6. Modify the logout endpoint to store the invalidated token in the database.

Install tzdata (if it's not installed):

bash
Copy code
apt-get update && apt-get install -y tzdata
Set the timezone to Asia/Colombo:

bash
Copy code
ln -sf /usr/share/zoneinfo/Asia/Colombo /etc/localtime
echo "Asia/Colombo" > /etc/timezone



USE [CoreDB]
GO

/****** Object:  Table [Core].[Button1]    Script Date: 8/4/2024 6:15:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Core].[Button1](
	[BtnSerialID] [int] IDENTITY(1,1) NOT NULL,
	[BtnID] [int] NOT NULL,
	[MnuSerialID] [int] NOT NULL,
	[ButtonName] [nvarchar](15) NOT NULL,
	[ButtonText] [nvarchar](15) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[Active] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[MnuLevel] [int] NOT NULL,
 CONSTRAINT [PK_Button] PRIMARY KEY CLUSTERED 
(
	[BtnSerialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Core].[Button1] ADD  DEFAULT (NEXT VALUE FOR [dbo].[BtnID]) FOR [BtnID]
GO

ALTER TABLE [Core].[Button1] ADD  DEFAULT ((0)) FOR [MnuLevel]
GO

ALTER TABLE [Core].[Button1]  WITH CHECK ADD  CONSTRAINT [FK_Button_Menu_MnuSerialID] FOREIGN KEY([MnuSerialID])
REFERENCES [Core].[Menu] ([MnuSerialID])
ON DELETE CASCADE
GO

ALTER TABLE [Core].[Button1] CHECK CONSTRAINT [FK_Button_Menu_MnuSerialID]
GO
