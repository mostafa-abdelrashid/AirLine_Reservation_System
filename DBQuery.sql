
CREATE TABLE Passenger (
PassengerID   INT IDENTITY(1,1) PRIMARY KEY,
first_name    VARCHAR(50)  NOT NULL,
Last_name     VARCHAR(50)  NOT NULL,
Passport_No   VARCHAR(50)  NOT NULL,
email         VARCHAR(50)  NOT NULL,
date_of_birth DATETIME     NOT NULL,
Nationality   VARCHAR(50)  NOT NULL
);

CREATE TABLE PassengerPhone (
PassengerID INT NOT NULL,
Phone       VARCHAR(50) NOT NULL,
PRIMARY KEY (PassengerID, Phone),
FOREIGN KEY (PassengerID) REFERENCES Passenger(PassengerID) ON DELETE CASCADE
);

CREATE TABLE Booking (
BookingID    INT IDENTITY(1,1) PRIMARY KEY,
Booking_date DATETIME    NOT NULL,
Status       VARCHAR(50) NOT NULL,
Totalamount  INT         NOT NULL,
PassengerID  INT         NOT NULL,
FOREIGN KEY (PassengerID) REFERENCES Passenger(PassengerID) ON DELETE CASCADE
);

CREATE TABLE Payment (
PaymentID      INT IDENTITY(1,1) PRIMARY KEY,
BookingID      INT         NOT NULL,
Amount         INT         NOT NULL,
Payment_date   DATETIME    NOT NULL,
Payment_method VARCHAR(50) NOT NULL,
FOREIGN KEY (BookingID) REFERENCES Booking(BookingID) ON DELETE CASCADE
);

CREATE TABLE Airline (
AirlineID INT IDENTITY(1,1) PRIMARY KEY,
Name      NVARCHAR(255) NOT NULL,
City      NVARCHAR(100) NOT NULL,
Country   NVARCHAR(100) NOT NULL,
Code      NVARCHAR(10)  NOT NULL UNIQUE
);

CREATE TABLE Aircraft (
AircraftID   INT IDENTITY(1,1) PRIMARY KEY,
Model        NVARCHAR(100) NOT NULL,
Capacity     INT           NOT NULL,
Manufacturer NVARCHAR(100) NOT NULL,
AirlineID    INT           NOT NULL,
FOREIGN KEY (AirlineID) REFERENCES Airline(AirlineID) ON DELETE CASCADE
);

CREATE TABLE Airport (
AirportID INT IDENTITY(1,1) PRIMARY KEY,
Name      NVARCHAR(255) NOT NULL,
City      NVARCHAR(100) NOT NULL,
Country   NVARCHAR(100) NOT NULL,
Code      NVARCHAR(10)  NOT NULL UNIQUE
);

CREATE TABLE Flight (
FlightID             INT IDENTITY(1,1) PRIMARY KEY,
Flight_Number        VARCHAR(20) NOT NULL UNIQUE,
Departure_Time       DATETIME    NOT NULL,
Arrival_Time         DATETIME    NOT NULL,
Status               VARCHAR(50) NOT NULL DEFAULT 'Scheduled',
AircraftID           INT NOT NULL,
AirlineID            INT NOT NULL,
Departure_Airport_ID INT NOT NULL,
Arrival_Airport_ID   INT NOT NULL,
FOREIGN KEY (AircraftID) REFERENCES Aircraft(AircraftID) ON DELETE CASCADE,
FOREIGN KEY (AirlineID) REFERENCES Airline(AirlineID) ON DELETE CASCADE,
FOREIGN KEY (Departure_Airport_ID) REFERENCES Airport(AirportID) ON DELETE CASCADE,
FOREIGN KEY (Arrival_Airport_ID)   REFERENCES Airport(AirportID) ON DELETE CASCADE
);

CREATE TABLE BookingFlight (
BookingID   INT NOT NULL,
FlightID    INT NOT NULL,
Travel_date DATETIME NOT NULL,
Seat_class  VARCHAR(50) NOT NULL,
PRIMARY KEY (BookingID, FlightID),
FOREIGN KEY (BookingID) REFERENCES Booking(BookingID) ON DELETE CASCADE,
FOREIGN KEY (FlightID)  REFERENCES Flight(FlightID) ON DELETE CASCADE
);

CREATE TABLE Ticket (
TicketID    INT IDENTITY(1,1) PRIMARY KEY,
Seatnumber  VARCHAR(50) NOT NULL,
Class       VARCHAR(50) NOT NULL,
Price       INT NOT NULL,
BookingID   INT NOT NULL,
FlightID    INT NOT NULL,
PassengerID INT NOT NULL,
FOREIGN KEY (BookingID)   REFERENCES Booking(BookingID) ON DELETE CASCADE,
FOREIGN KEY (FlightID)    REFERENCES Flight(FlightID) ON DELETE CASCADE,
FOREIGN KEY (PassengerID) REFERENCES Passenger(PassengerID) ON DELETE CASCADE
);

-- =====================
-- INSERT DATA
-- =====================

-- Airlines
INSERT INTO Airline (Name, City, Country, Code) VALUES
(N'EgyptAir',N'Cairo',N'Egypt',N'MS'),
(N'Emirates',N'Dubai',N'UAE',N'EK'),
(N'Turkish Airlines',N'Istanbul',N'Turkey',N'TK'),
(N'Qatar Airways',N'Doha',N'Qatar',N'QR'),
(N'Lufthansa',N'Frankfurt',N'Germany',N'LH'),
(N'Air France',N'Paris',N'France',N'AF'),
(N'British Airways',N'London',N'UK',N'BA'),
(N'American Airlines',N'Dallas',N'USA',N'AA'),
(N'Delta Airlines',N'Atlanta',N'USA',N'DL'),
(N'Saudia',N'Jeddah',N'Saudi Arabia',N'SV');

-- Airports
INSERT INTO Airport (Name, City, Country, Code) VALUES
(N'Cairo International Airport',N'Cairo',N'Egypt',N'CAI'),
(N'Dubai International Airport',N'Dubai',N'UAE',N'DXB'),
(N'Istanbul Airport',N'Istanbul',N'Turkey',N'IST'),
(N'Hamad International Airport',N'Doha',N'Qatar',N'DOH'),
(N'Frankfurt Airport',N'Frankfurt',N'Germany',N'FRA'),
(N'Charles de Gaulle Airport',N'Paris',N'France',N'CDG'),
(N'Heathrow Airport',N'London',N'UK',N'LHR'),
(N'Dallas/Fort Worth Airport',N'Dallas',N'USA',N'DFW'),
(N'Hartsfield-Jackson Atlanta',N'Atlanta',N'USA',N'ATL'),
(N'King Abdulaziz Airport',N'Jeddah',N'Saudi Arabia',N'JED');

-- Aircraft
INSERT INTO Aircraft (Model, Capacity, Manufacturer, AirlineID) VALUES
(N'Boeing 737',180,N'Boeing',1),
(N'Airbus A380',525,N'Airbus',2),
(N'Boeing 777',396,N'Boeing',3),
(N'Airbus A350',440,N'Airbus',4),
(N'Boeing 747',416,N'Boeing',5),
(N'Airbus A320',180,N'Airbus',6),
(N'Boeing 787',335,N'Boeing',7),
(N'Boeing 737 MAX',200,N'Boeing',8),
(N'Airbus A330',250,N'Airbus',9),
(N'Boeing 777X',426,N'Boeing',10);

-- Flights
INSERT INTO Flight (Flight_Number, Departure_Time, Arrival_Time, Status, AircraftID, AirlineID, Departure_Airport_ID, Arrival_Airport_ID) VALUES
(N'MS401','2025-06-01 08:00','2025-06-01 14:00','Scheduled',1,1,1,2),
(N'EK201','2025-06-05 10:30','2025-06-05 15:00','Scheduled',2,2,2,3),
(N'TK780','2025-06-10 22:00','2025-06-11 02:30','Scheduled',3,3,3,1),
(N'QR100','2025-06-12 09:00','2025-06-12 13:00','Scheduled',4,4,4,1),
(N'LH200','2025-06-15 07:00','2025-06-15 11:00','Scheduled',5,5,5,2),
(N'AF300','2025-06-18 14:00','2025-06-18 18:00','Scheduled',6,6,6,3),
(N'BA400','2025-06-20 10:00','2025-06-20 16:00','Scheduled',7,7,7,4),
(N'AA500','2025-06-22 12:00','2025-06-22 20:00','Scheduled',8,8,8,5),
(N'DL600','2025-06-25 08:00','2025-06-25 14:00','Scheduled',9,9,9,6),
(N'SV700','2025-06-28 06:00','2025-06-28 10:00','Scheduled',10,10,10,1);

-- Passengers
INSERT INTO Passenger (first_name, Last_name, Passport_No, email, date_of_birth, Nationality) VALUES
(N'Ahmed',N'Hassan',N'A12345678',N'ahmed.hassan@email.com)','1990-03-15',N'Egyptian'),
(N'Sara',N'Ali',N'B98765432',N'sara.ali@email.com)','1995-07-22',N'Egyptian'),
(N'Michael',N'Johnson',N'US1122334',N'michael.j@email.com)','1988-11-05',N'American'),
(N'Omar',N'Khaled',N'C12345678',N'omar.k@email.com)','1992-01-10',N'Egyptian'),
(N'Fatma',N'Mahmoud',N'D98765432',N'fatma@email.com)','1998-05-18',N'Egyptian'),
(N'John',N'Smith',N'US998877',N'john.s@email.com)','1985-09-12',N'American'),
(N'Anna',N'Muller',N'DE112233',N'anna@email.com)','1991-04-25',N'German'),
(N'Pierre',N'Dubois',N'FR445566',N'pierre@email.com','1987-02-14',N'French'),
(N'James',N'Brown',N'UK778899',N'james@email.com','1993-08-30',N'British'),
(N'Ali',N'Alharbi',N'SA334455',N'ali@email.com','1996-12-11',N'Saudi');

-- Passenger Phones
INSERT INTO PassengerPhone VALUES
(1,'+201001234567'),(2,'+201009876543'),(3,'+12125551234'),
(4,'+201112223334'),(5,'+201223334445'),(6,'+14155552671'),
(7,'+4915123456789'),(8,'+33123456789'),(9,'+447911123456'),
(10,'+966512345678');

-- Bookings
INSERT INTO Booking (Booking_date, Status, Totalamount, PassengerID) VALUES
('2025-05-01 12:00','Confirmed',850,1),
('2025-05-03 14:30','Confirmed',1200,2),
('2025-05-05 09:00','Pending',450,3),
('2025-05-06 10:00','Confirmed',900,4),
('2025-05-07 11:00','Confirmed',1100,5),
('2025-05-08 12:00','Pending',700,6),
('2025-05-09 13:00','Confirmed',1300,7),
('2025-05-10 14:00','Confirmed',1500,8),
('2025-05-11 15:00','Pending',600,9),
('2025-05-12 16:00','Confirmed',1000,10);

-- BookingFlight
INSERT INTO BookingFlight VALUES
(1,1,'2025-06-01','Business'),
(2,2,'2025-06-05','Economy'),
(3,3,'2025-06-10','Economy'),
(4,4,'2025-06-12','Business'),
(5,5,'2025-06-15','Economy'),
(6,6,'2025-06-18','Economy'),
(7,7,'2025-06-20','Business'),
(8,8,'2025-06-22','First'),
(9,9,'2025-06-25','Economy'),
(10,10,'2025-06-28','Business');

-- Tickets
INSERT INTO Ticket (Seatnumber, Class, Price, BookingID, FlightID, PassengerID) VALUES
('12A','Business',850,1,1,1),
('34B','Economy',1200,2,2,2),
('22C','Economy',450,3,3,3),
('14A','Business',900,4,4,4),
('20B','Economy',1100,5,5,5),
('18C','Economy',700,6,6,6),
('2A','Business',1300,7,7,7),
('1A','First',1500,8,8,8),
('25D','Economy',600,9,9,9),
('10F','Business',1000,10,10,10);

-- Payments
INSERT INTO Payment (BookingID, Amount, Payment_date, Payment_method) VALUES
(1,850,'2025-05-01 12:05','Credit Card'),
(2,1200,'2025-05-03 14:40','PayPal'),
(3,450,'2025-05-05 09:15','Debit Card'),
(4,900,'2025-05-06 10:10','Credit Card'),
(5,1100,'2025-05-07 11:10','Debit Card'),
(6,700,'2025-05-08 12:10','PayPal'),
(7,1300,'2025-05-09 13:10','Credit Card'),
(8,1500,'2025-05-10 14:10','Credit Card'),
(9,600,'2025-05-11 15:10','Debit Card'),
(10,1000,'2025-05-12 16:10','PayPal');
