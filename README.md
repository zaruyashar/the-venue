# 🥂 THEVENUE Management System

## 📖 Description

THEVENUE is a comprehensive and luxurious venue and event management system. It features a beautifully designed, public-facing web landing page for clients to browse venues, view upcoming events, and submit inquiries. Behind the scenes, it is powered by a robust administrative dashboard for total operational control. 

The application architecture is decoupled into a **Web API** layer and an **MVC** client layer (acting as the API consumer). Built with performance in mind, the data access layer bypasses traditional Entity Framework Core in favor of **Dapper** and raw **T-SQL Stored Procedures**. It utilizes the Repository pattern and DTOs to ensure clean, maintainable code. Additionally, the public contact form is secured against spam using a Sliding Window Rate Limiting policy. 

**Upcoming Features:** Secure user signup and login utilizing the Microsoft Identity library.

## 🛠 Tech Stack

* **Backend:** C#, .NET Core, ASP.NET Core Web API, ASP.NET Core MVC
* **Data Access:** Dapper (Micro-ORM), T-SQL, Stored Procedures (No EF Core)
* **Architecture:** Repository Pattern, Data Transfer Objects (DTOs), API + Consumer Client
* **Frontend:** Razor Views, HTML5, CSS3, JavaScript, C3.js / D3.js (Data Visualization)
* **Security & Performance:** Sliding Window Rate Limiting (Contact Form)
* **Roadmap:** Authentication & Authorization via Microsoft Identity

## 📸 Screenshots

### Public Web Landing Page
Our luxurious public-facing web application allows clients to explore spaces and submit inquiries.
<img width="3028" height="1917" alt="1" src="https://github.com/user-attachments/assets/c08328ec-2764-48fa-aeda-9503da413bff" />
<img width="3030" height="1917" alt="2" src="https://github.com/user-attachments/assets/f09ebe6e-b5a2-4de4-9d28-508fcb9d2865" />
<img width="3024" height="1917" alt="3" src="https://github.com/user-attachments/assets/b36e2c99-f5ee-4f67-bad1-4d1295dc961b" />
<img width="3035" height="1917" alt="4" src="https://github.com/user-attachments/assets/2291b450-8e6a-4e99-ae80-287f5c9c8f57" />
<img width="3024" height="1917" alt="5" src="https://github.com/user-attachments/assets/ee3db18d-c330-4b6d-aa71-e664a8954f44" />
<img width="3024" height="1917" alt="6" src="https://github.com/user-attachments/assets/76563667-c2c3-4d55-872c-3fd00be6ffde" />


### Administrative Dashboard
The backend management system provides total control over venues, events, reservations, and client communications.

**7. Message Inbox:** View and manage client inquiries from the public site, complete with dynamic unread badges and status tracking.
<img width="3069" height="1917" alt="7" src="https://github.com/user-attachments/assets/295c5601-dbca-4351-8486-579db23cc3d1" />

**8. Reservations Management:** Handle bookings with quick-action status toggles (Pending, Confirmed, Cancelled) and a dedicated CSV export feature.
<img width="3015" height="1917" alt="8" src="https://github.com/user-attachments/assets/acd7b7a9-0aa2-41a3-86f7-18852a39852b" />

**9. Event Management:** Create, filter, and manage upcoming events with attendee counts and visibility controls.
<img width="3039" height="1917" alt="9" src="https://github.com/user-attachments/assets/4889fb10-60bf-46a4-8e2e-2a664aadaab4" />

**10. Venue Editing:** Update venue details including capacity, pricing, location, descriptions, and active status for the public site.
<img width="3035" height="1905" alt="10" src="https://github.com/user-attachments/assets/a2bfe20d-23ae-4b5f-89fd-6be217e855b3" />

**11. Dashboard KPI Overview:** High-level metrics at a glance, tracking active venues, total events, pending/confirmed reservations, and monthly revenue.
<img width="3017" height="1917" alt="11" src="https://github.com/user-attachments/assets/31d2933d-226b-48bd-a1bb-51254280bff4" />

**12. Dashboard Analytics (Trends & Types):** Visual insights featuring a line/bar combination chart for monthly revenue trends and a donut chart for event type distributions.
<img width="3028" height="1917" alt="12" src="https://github.com/user-attachments/assets/d3e5a99f-fda0-4c5f-9dd4-8b6f75b73838" />

**13. Revenue Analytics:** Detailed bar charts breaking down confirmed reservation revenue by specific venues, alongside the admin profile settings menu.
<img width="3024" height="1917" alt="13" src="https://github.com/user-attachments/assets/6337a2c3-01ed-4850-ac18-2d54907216c8" />

**14. Data Export:** Seamlessly exported CSV reservation data, perfectly formatted for analysis in Excel.
<img width="2911" height="1368" alt="14" src="https://github.com/user-attachments/assets/ebe8ceae-3084-4625-9c56-91d50195af9b" />
