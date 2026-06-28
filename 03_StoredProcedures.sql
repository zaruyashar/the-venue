CREATE OR ALTER PROCEDURE sp_GetAllVenues
    @IsActive BIT = NULL
AS
BEGIN
    SELECT VenueId, Name, Description, Capacity, PricePerHour,
           Location, ImageUrl, IsActive, CreatedAt
    FROM   Venues
    WHERE  (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER  BY CreatedAt DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetVenueById
    @VenueId INT
AS
BEGIN
    SELECT VenueId, Name, Description, Capacity, PricePerHour,
           Location, ImageUrl, IsActive, CreatedAt
    FROM   Venues
    WHERE  VenueId = @VenueId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetPublicVenues
AS
BEGIN
    SELECT VenueId, Name, Description, Capacity, PricePerHour,
           Location, ImageUrl, CreatedAt
    FROM   Venues
    WHERE  IsActive = 1
    ORDER  BY Name;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetVenueLookups
AS
BEGIN
    SELECT VenueId, Name
    FROM   Venues
    WHERE  IsActive = 1
    ORDER  BY Name;
END;
GO

CREATE OR ALTER PROCEDURE sp_CreateVenue
    @Name         NVARCHAR(150),
    @Description  NVARCHAR(MAX),
    @Capacity     INT,
    @PricePerHour DECIMAL(10,2),
    @Location     NVARCHAR(250),
    @ImageUrl     NVARCHAR(500),
    @IsActive     BIT
AS
BEGIN
    INSERT INTO Venues (Name, Description, Capacity, PricePerHour, Location, ImageUrl, IsActive)
    VALUES (@Name, @Description, @Capacity, @PricePerHour, @Location, @ImageUrl, @IsActive);
    SELECT SCOPE_IDENTITY();
END;
GO

CREATE OR ALTER PROCEDURE sp_UpdateVenue
    @VenueId      INT,
    @Name         NVARCHAR(150),
    @Description  NVARCHAR(MAX),
    @Capacity     INT,
    @PricePerHour DECIMAL(10,2),
    @Location     NVARCHAR(250),
    @ImageUrl     NVARCHAR(500),
    @IsActive     BIT
AS
BEGIN
    UPDATE Venues
    SET    Name         = @Name,
           Description  = @Description,
           Capacity     = @Capacity,
           PricePerHour = @PricePerHour,
           Location     = @Location,
           ImageUrl     = @ImageUrl,
           IsActive     = @IsActive
    WHERE  VenueId = @VenueId;
END;
GO

CREATE OR ALTER PROCEDURE sp_DeleteVenue
    @VenueId INT
AS
BEGIN
    DELETE FROM Venues WHERE VenueId = @VenueId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetAllEvents
AS
BEGIN
    SELECT e.EventId, e.VenueId, v.Name AS VenueName,
           e.Title, e.Description, e.EventType,
           e.StartDate, e.EndDate, e.ExpectedAttendees,
           e.IsPublic, e.CreatedAt
    FROM   Events e
    INNER  JOIN Venues v ON e.VenueId = v.VenueId
    ORDER  BY e.StartDate DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetEventById
    @EventId INT
AS
BEGIN
    SELECT e.EventId, e.VenueId, v.Name AS VenueName,
           e.Title, e.Description, e.EventType,
           e.StartDate, e.EndDate, e.ExpectedAttendees,
           e.IsPublic, e.CreatedAt
    FROM   Events e
    INNER  JOIN Venues v ON e.VenueId = v.VenueId
    WHERE  e.EventId = @EventId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetEventsByVenue
    @VenueId INT
AS
BEGIN
    SELECT e.EventId, e.VenueId, v.Name AS VenueName,
           e.Title, e.Description, e.EventType,
           e.StartDate, e.EndDate, e.ExpectedAttendees,
           e.IsPublic, e.CreatedAt
    FROM   Events e
    INNER  JOIN Venues v ON e.VenueId = v.VenueId
    WHERE  e.VenueId = @VenueId
    ORDER  BY e.StartDate;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetUpcomingPublicEvents
AS
BEGIN
    SELECT TOP 6
           e.EventId, e.VenueId, v.Name AS VenueName,
           e.Title, e.EventType, e.StartDate, e.EndDate,
           e.ExpectedAttendees
    FROM   Events e
    INNER  JOIN Venues v ON e.VenueId = v.VenueId
    WHERE  e.IsPublic = 1
    AND    e.StartDate >= GETDATE()
    ORDER  BY e.StartDate;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetEventLookups
AS
BEGIN
    SELECT e.EventId, e.Title, v.Name AS VenueName, e.StartDate
    FROM   Events e
    INNER  JOIN Venues v ON e.VenueId = v.VenueId
    ORDER  BY e.StartDate DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_CreateEvent
    @VenueId           INT,
    @Title             NVARCHAR(200),
    @Description       NVARCHAR(MAX),
    @EventType         NVARCHAR(80),
    @StartDate         DATETIME,
    @EndDate           DATETIME,
    @ExpectedAttendees INT,
    @IsPublic          BIT
AS
BEGIN
    INSERT INTO Events (VenueId, Title, Description, EventType, StartDate, EndDate, ExpectedAttendees, IsPublic)
    VALUES (@VenueId, @Title, @Description, @EventType, @StartDate, @EndDate, @ExpectedAttendees, @IsPublic);
    SELECT SCOPE_IDENTITY();
END;
GO

CREATE OR ALTER PROCEDURE sp_UpdateEvent
    @EventId           INT,
    @VenueId           INT,
    @Title             NVARCHAR(200),
    @Description       NVARCHAR(MAX),
    @EventType         NVARCHAR(80),
    @StartDate         DATETIME,
    @EndDate           DATETIME,
    @ExpectedAttendees INT,
    @IsPublic          BIT
AS
BEGIN
    UPDATE Events
    SET    VenueId           = @VenueId,
           Title             = @Title,
           Description       = @Description,
           EventType         = @EventType,
           StartDate         = @StartDate,
           EndDate           = @EndDate,
           ExpectedAttendees = @ExpectedAttendees,
           IsPublic          = @IsPublic
    WHERE  EventId = @EventId;
END;
GO

CREATE OR ALTER PROCEDURE sp_DeleteEvent
    @EventId INT
AS
BEGIN
    DELETE FROM Events WHERE EventId = @EventId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetAllReservations
AS
BEGIN
    SELECT r.ReservationId, r.VenueId, v.Name AS VenueName,
           r.EventId, e.Title AS EventTitle,
           r.GuestName, r.GuestEmail, r.GuestPhone,
           r.GuestCount, r.Status, r.TotalAmount, r.Notes, r.CreatedAt
    FROM   Reservations r
    INNER  JOIN Venues v ON r.VenueId = v.VenueId
    INNER  JOIN Events e ON r.EventId = e.EventId
    ORDER  BY r.CreatedAt DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetReservationById
    @ReservationId INT
AS
BEGIN
    SELECT r.ReservationId, r.VenueId, v.Name AS VenueName,
           r.EventId, e.Title AS EventTitle,
           r.GuestName, r.GuestEmail, r.GuestPhone,
           r.GuestCount, r.Status, r.TotalAmount, r.Notes, r.CreatedAt
    FROM   Reservations r
    INNER  JOIN Venues v ON r.VenueId = v.VenueId
    INNER  JOIN Events e ON r.EventId = e.EventId
    WHERE  r.ReservationId = @ReservationId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetReservationsByVenue
    @VenueId INT
AS
BEGIN
    SELECT r.ReservationId, r.VenueId, v.Name AS VenueName,
           r.EventId, e.Title AS EventTitle,
           r.GuestName, r.GuestEmail, r.GuestPhone,
           r.GuestCount, r.Status, r.TotalAmount, r.Notes, r.CreatedAt
    FROM   Reservations r
    INNER  JOIN Venues v ON r.VenueId = v.VenueId
    INNER  JOIN Events e ON r.EventId = e.EventId
    WHERE  r.VenueId = @VenueId
    ORDER  BY r.CreatedAt DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetReservationsByStatus
    @Status NVARCHAR(30)
AS
BEGIN
    SELECT r.ReservationId, r.VenueId, v.Name AS VenueName,
           r.EventId, e.Title AS EventTitle,
           r.GuestName, r.GuestEmail, r.GuestPhone,
           r.GuestCount, r.Status, r.TotalAmount, r.Notes, r.CreatedAt
    FROM   Reservations r
    INNER  JOIN Venues v ON r.VenueId = v.VenueId
    INNER  JOIN Events e ON r.EventId = e.EventId
    WHERE  r.Status = @Status
    ORDER  BY r.CreatedAt DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_CreateReservation
    @VenueId      INT,
    @EventId      INT,
    @GuestName    NVARCHAR(150),
    @GuestEmail   NVARCHAR(200),
    @GuestPhone   NVARCHAR(30),
    @GuestCount   INT,
    @Status       NVARCHAR(30),
    @TotalAmount  DECIMAL(10,2),
    @Notes        NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Reservations
        (VenueId, EventId, GuestName, GuestEmail, GuestPhone, GuestCount, Status, TotalAmount, Notes)
    VALUES
        (@VenueId, @EventId, @GuestName, @GuestEmail, @GuestPhone, @GuestCount, @Status, @TotalAmount, @Notes);
    SELECT SCOPE_IDENTITY();
END;
GO

CREATE OR ALTER PROCEDURE sp_UpdateReservation
    @ReservationId INT,
    @VenueId       INT,
    @EventId       INT,
    @GuestName     NVARCHAR(150),
    @GuestEmail    NVARCHAR(200),
    @GuestPhone    NVARCHAR(30),
    @GuestCount    INT,
    @Status        NVARCHAR(30),
    @TotalAmount   DECIMAL(10,2),
    @Notes         NVARCHAR(MAX)
AS
BEGIN
    UPDATE Reservations
    SET    VenueId      = @VenueId,
           EventId      = @EventId,
           GuestName    = @GuestName,
           GuestEmail   = @GuestEmail,
           GuestPhone   = @GuestPhone,
           GuestCount   = @GuestCount,
           Status       = @Status,
           TotalAmount  = @TotalAmount,
           Notes        = @Notes
    WHERE  ReservationId = @ReservationId;
END;
GO

CREATE OR ALTER PROCEDURE sp_UpdateReservationStatus
    @ReservationId INT,
    @Status        NVARCHAR(30)
AS
BEGIN
    UPDATE Reservations
    SET    Status = @Status
    WHERE  ReservationId = @ReservationId;
END;
GO

CREATE OR ALTER PROCEDURE sp_DeleteReservation
    @ReservationId INT
AS
BEGIN
    DELETE FROM Reservations WHERE ReservationId = @ReservationId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetDashboardStats
AS
BEGIN
    SELECT
        (SELECT COUNT(*) FROM Venues WHERE IsActive = 1)                         AS TotalVenues,
        (SELECT COUNT(*) FROM Events)                                            AS TotalEvents,
        (SELECT COUNT(*) FROM Reservations)                                      AS TotalReservations,
        (SELECT COUNT(*) FROM Reservations WHERE Status = 'Pending')               AS PendingReservations,
        (SELECT COUNT(*) FROM Reservations WHERE Status = 'Confirmed')             AS ConfirmedReservations,
        (SELECT COUNT(*) FROM Reservations WHERE Status = 'Cancelled')             AS CancelledReservations,
        (SELECT ISNULL(SUM(TotalAmount), 0) FROM Reservations
         WHERE Status = 'Confirmed')                                             AS TotalRevenue,
        (SELECT ISNULL(SUM(TotalAmount), 0) FROM Reservations
         WHERE Status = 'Confirmed'
         AND   MONTH(CreatedAt) = MONTH(GETDATE())
         AND   YEAR(CreatedAt)  = YEAR(GETDATE()))                               AS RevenueThisMonth;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetRevenueByVenue
AS
BEGIN
    SELECT v.Name AS VenueName,
           ISNULL(SUM(r.TotalAmount), 0) AS TotalRevenue,
           COUNT(r.ReservationId)         AS ReservationCount
    FROM   Venues v
    LEFT   JOIN Reservations r ON v.VenueId = r.VenueId AND r.Status = 'Confirmed'
    WHERE  v.IsActive = 1
    GROUP  BY v.VenueId, v.Name
    ORDER  BY TotalRevenue DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetReservationsByMonth
AS
BEGIN
    SELECT YEAR(CreatedAt)               AS Year,
           MONTH(CreatedAt)              AS Month,
           DATENAME(MONTH, CreatedAt)    AS MonthName,
           COUNT(*)                      AS ReservationCount,
           ISNULL(SUM(TotalAmount), 0)   AS Revenue
    FROM   Reservations
    WHERE  CreatedAt >= DATEADD(MONTH, -11, GETDATE())
    GROUP  BY YEAR(CreatedAt), MONTH(CreatedAt), DATENAME(MONTH, CreatedAt)
    ORDER  BY Year, Month;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetEventTypeBreakdown
AS
BEGIN
    SELECT ISNULL(EventType, 'Other') AS EventType,
           COUNT(*)                   AS EventCount
    FROM   Events
    GROUP  BY EventType
    ORDER  BY EventCount DESC;
END;
GO