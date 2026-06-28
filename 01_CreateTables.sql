CREATE TABLE Venues (
    VenueId       INT            IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(150)  NOT NULL,
    Description   NVARCHAR(MAX)  NULL,
    Capacity      INT            NOT NULL,
    PricePerHour  DECIMAL(10,2)  NOT NULL,
    Location      NVARCHAR(250)  NOT NULL,
    ImageUrl      NVARCHAR(500)  NULL,
    IsActive      BIT            NOT NULL DEFAULT 1,
    CreatedAt     DATETIME       NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Events (
    EventId            INT            IDENTITY(1,1) PRIMARY KEY,
    VenueId            INT            NOT NULL REFERENCES Venues(VenueId),
    Title              NVARCHAR(200)  NOT NULL,
    Description        NVARCHAR(MAX)  NULL,
    EventType          NVARCHAR(80)   NULL,
    StartDate          DATETIME       NOT NULL,
    EndDate            DATETIME       NOT NULL,
    ExpectedAttendees  INT            NULL,
    IsPublic           BIT            NOT NULL DEFAULT 1,
    CreatedAt          DATETIME       NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Reservations (
    ReservationId  INT            IDENTITY(1,1) PRIMARY KEY,
    VenueId        INT            NOT NULL REFERENCES Venues(VenueId),
    EventId        INT            NOT NULL REFERENCES Events(EventId),
    GuestName      NVARCHAR(150)  NOT NULL,
    GuestEmail     NVARCHAR(200)  NOT NULL,
    GuestPhone     NVARCHAR(30)   NULL,
    GuestCount     INT            NOT NULL,
    Status         NVARCHAR(30)   NOT NULL DEFAULT 'Pending',
    TotalAmount    DECIMAL(10,2)  NOT NULL DEFAULT 0,
    Notes          NVARCHAR(MAX)  NULL,
    CreatedAt      DATETIME       NOT NULL DEFAULT GETDATE()
);