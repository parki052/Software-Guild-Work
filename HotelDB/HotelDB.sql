USE master
IF EXISTS(select * from sys.databases where name='Hotel')
DROP DATABASE Hotel

CREATE DATABASE Hotel
GO
 
USE Hotel;
GO





CREATE TABLE AddOn (
	AddOnId INT PRIMARY KEY,
	Item VARCHAR (100) NOT NULL,
)
GO

CREATE TABLE AddOnPrice (
	
	AddOnPriceId INT PRIMARY KEY,
	AddOnId INT FOREIGN KEY REFERENCES AddOn(AddOnId) NOT NULL,
    Price DECIMAL NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL
)
GO



CREATE TABLE RoomType (
	RoomTypeId INT PRIMARY KEY,
	RoomType VARCHAR (50) NOT NULL
)
GO

CREATE TABLE RoomRate (
	RoomRateId INT PRIMARY KEY,
	RoomTypeId INT FOREIGN KEY REFERENCES RoomType(RoomTypeId),
	Rate DECIMAL NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL
)
GO

CREATE TABLE Room (

	RoomId INT PRIMARY KEY,
	RoomRateId INT FOREIGN KEY REFERENCES RoomRate(RoomRateId),
	FloorNumber INT NOT NULL,
	OccupancyLimit INT NOT NULL
)
GO

CREATE TABLE Amenity (
	AmenityId INT PRIMARY KEY,
	Amenity VARCHAR (50) NOT NULL
)
GO

CREATE TABLE RoomAmenity (
	RoomId INT NOT NULL,
	AmenityId INT NOT NULL,
	CONSTRAINT PK_RoomAmenity
		PRIMARY KEY (RoomId, AmenityId),
	CONSTRAINT FK_RoomId_RoomAmenity
		FOREIGN KEY (RoomId) REFERENCES Room(RoomId),
	CONSTRAINT FK_AmenityId_RoomAmenity
		FOREIGN KEY (AmenityId) REFERENCES Amenity(AmenityId)
)
GO


CREATE TABLE Customer (
	CustomerId INT PRIMARY KEY,
	FirstName VARCHAR (30) NOT NULL,
	LastName VARCHAR (30) NOT NULL,
	Phone VARCHAR (30) NOT NULL,
	Email VARCHAR (50) NOT NULL
)
GO

CREATE TABLE Promotion (
	PromotionId INT PRIMARY KEY,
	PercentDiscount DECIMAL,
	FlatDiscount DECIMAL
)
GO


CREATE TABLE Reservation (
	ReservationId INT PRIMARY KEY,
	PromotionId INT FOREIGN KEY REFERENCES Promotion(PromotionId),
	CustomerId INT FOREIGN KEY REFERENCES Customer(CustomerId),
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
)
GO

CREATE TABLE RoomReservation (
	RoomId INT NOT NULL,
	ReservationId INT NOT NULL,
	CONSTRAINT PK_RoomReservation
		PRIMARY KEY (RoomId, ReservationId),
	CONSTRAINT FK_RoomId_RoomReservation
		FOREIGN KEY (RoomId) REFERENCES Room(RoomId),
	CONSTRAINT FK_ReservationId_RoomReservation
		FOREIGN KEY (ReservationId) REFERENCES Reservation(ReservationId)
)
GO


CREATE TABLE Guest (
	GuestId INT PRIMARY KEY,
	ReservationId INT FOREIGN KEY REFERENCES Reservation(ReservationId),
	FirstName VARCHAR (30) NOT NULL,
	LastName VARCHAR (30) NOT NULL,
	Age INT NOT NULL
)
GO

CREATE TABLE Bill (
	BillId INT PRIMARY KEY,
	RoomId INT FOREIGN KEY REFERENCES Room(RoomId),
	RoomRate DECIMAL NOT NULL,
	RoomCharge DECIMAL NOT NULL,
	Total DECIMAL NOT NULL,
	Tax DECIMAL NOT NULL
)
GO

CREATE TABLE BillDetails (
	BillDetailsId INT PRIMARY KEY,
	BillId INT FOREIGN KEY REFERENCES Bill(BillId),
	AddOnPriceId INT FOREIGN KEY REFERENCES AddOnPrice(AddOnPriceId),


)
GO



