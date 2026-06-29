USE THEVENUE;
GO

-- 1. Create Contacts Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Contacts')
BEGIN
    CREATE TABLE Contacts (
        ContactId  INT            IDENTITY(1,1) PRIMARY KEY,
        Name       NVARCHAR(150)  NOT NULL,
        Email      NVARCHAR(200)  NOT NULL,
        Phone      NVARCHAR(30)   NULL,
        Subject    NVARCHAR(200)  NULL,
        Message    NVARCHAR(MAX)  NOT NULL,
        IsRead     BIT            NOT NULL DEFAULT 0,
        CreatedAt  DATETIME       NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 2. Stored Procedures for Contacts
CREATE OR ALTER PROCEDURE sp_GetAllContacts
AS
BEGIN
    SELECT ContactId, Name, Email, Phone, Subject, Message, IsRead, CreatedAt
    FROM   Contacts
    ORDER  BY CreatedAt DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetContactById
    @ContactId INT
AS
BEGIN
    SELECT ContactId, Name, Email, Phone, Subject, Message, IsRead, CreatedAt
    FROM   Contacts
    WHERE  ContactId = @ContactId;
END;
GO

CREATE OR ALTER PROCEDURE sp_CreateContact
    @Name     NVARCHAR(150),
    @Email    NVARCHAR(200),
    @Phone    NVARCHAR(30) = NULL,
    @Subject  NVARCHAR(200) = NULL,
    @Message  NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Contacts (Name, Email, Phone, Subject, Message, IsRead, CreatedAt)
    VALUES (@Name, @Email, @Phone, @Subject, @Message, 0, GETDATE());
    
    SELECT CAST(SCOPE_IDENTITY() as INT);
END;
GO

CREATE OR ALTER PROCEDURE sp_DeleteContact
    @ContactId INT
AS
BEGIN
    DELETE FROM Contacts
    WHERE  ContactId = @ContactId;
END;
GO

CREATE OR ALTER PROCEDURE sp_MarkContactAsRead
    @ContactId INT
AS
BEGIN
    UPDATE Contacts
    SET    IsRead = 1
    WHERE  ContactId = @ContactId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetUnreadContactCount
AS
BEGIN
    SELECT COUNT(*) FROM Contacts WHERE IsRead = 0;
END;
GO

-- 3. Update sp_GetUpcomingPublicEvents to filter by EndDate >= GETDATE() instead of StartDate
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
    AND    e.EndDate >= GETDATE()
    ORDER  BY e.StartDate;
END;
GO
