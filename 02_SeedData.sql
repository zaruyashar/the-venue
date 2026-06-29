INSERT INTO Venues (Name, Description, Capacity, PricePerHour, Location, ImageUrl, IsActive)
VALUES
('The Grand Hall',    'A majestic ballroom with crystal chandeliers and marble floors.',  400, 850.00, 'İstanbul, Beşiktaş',  'https://images.unsplash.com/photo-1519167758481-83f550bb49b3?auto=format&fit=crop&w=800&q=80',    1),
('The Garden Terrace','An open-air rooftop terrace with panoramic Bosphorus views.',      150, 450.00, 'İstanbul, Üsküdar',   'https://images.unsplash.com/photo-1533105079780-92b9be482077?auto=format&fit=crop&w=800&q=80', 1),
('The Studio Loft',  'A minimalist industrial space perfect for creative events.',        80,  300.00, 'İstanbul, Karaköy',   'https://images.unsplash.com/photo-1513694203232-719a280e022f?auto=format&fit=crop&w=800&q=80',    1);

INSERT INTO Events (VenueId, Title, Description, EventType, StartDate, EndDate, ExpectedAttendees, IsPublic)
VALUES
(1, 'Gala Night 2026',        'Annual black-tie gala dinner.',           'Corporate',  '2026-09-15 19:00', '2026-09-15 23:59', 350, 1),
(1, 'Autumn Wedding',         'Intimate wedding reception.',             'Wedding',    '2026-10-04 17:00', '2026-10-04 23:00', 200, 1),
(2, 'Bosphorus Jazz Evening', 'Live jazz under the stars.',              'Concert',    '2026-08-22 20:00', '2026-08-22 23:00', 120, 1),
(3, 'Product Launch — Nexus', 'Tech product reveal and press event.',    'Corporate',  '2026-07-10 14:00', '2026-07-10 18:00', 70,  0);

INSERT INTO Reservations (VenueId, EventId, GuestName, GuestEmail, GuestPhone, GuestCount, Status, TotalAmount, Notes)
VALUES
(1, 1, 'Ahmet Yılmaz',   'ahmet@example.com',  '+90 532 111 2233', 4,  'Confirmed', 3400.00, 'VIP table requested.'),
(1, 2, 'Leila Mansouri', 'leila@example.com',  '+90 545 222 3344', 80, 'Pending',   12000.00, NULL),
(2, 3, 'Marco Rossi',    'marco@example.com',  NULL,               2,  'Confirmed', 900.00,  'Outdoor seating preferred.'),
(3, 4, 'Sara Demir',     'sara@example.com',   '+90 533 444 5566', 15, 'Cancelled', 0.00,    'Event postponed indefinitely.');