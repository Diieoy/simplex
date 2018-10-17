--- Venue
insert into dbo.Venue
values ('Arena' ,'First venue', 'Amazing streat 12/4', '22-44-11')

--- Layout
insert into dbo.Layout
values ('Layout 1', 1, 'First layout'),
('Layout 2', 1, 'Second layout'),
('Layout 3', 1, 'Third layout')

--- Area
insert into dbo.Area
values (1, 'First area of first layout', 2, 3),
(1, 'Second area of first layout', 3, 3),
(2, 'First area of second layout', 4, 4),
(3, 'Area withount seats', 3, 3)

--- Seat
insert into dbo.Seat
values (1,1,1),
(1,1,2),
(1,2,1),
(1,2,2),
(1,3,1),
(1,3,2),

(2,1,1),
(2,1,2),
(2,1,3),
(2,2,1),
(2,2,2),
(2,2,3),
(2,3,1),
(2,3,2),
(2,3,3),

(3,1,1),
(3,1,2),
(3,1,3),
(3,1,-1),
(3,2,1),
(3,2,2),
(3,2,-1),
(3,2,3),
(3,3,1),
(3,3,-1),
(3,3,2),
(3,3,3),
(3,4,-1),
(3,4,1),
(3,4,2),
(3,4,3)

--- Event(for test)
insert into dbo.Event
values ('ForTestEvent', 'For test', '2010-06-05 15:32:11.037', '2010-06-05 15:32:11.037','image', 1)

--- Role
insert into dbo.Role
values ('event_manager'),
('user'),
('venue_manager')

--- User
insert into dbo.[User]
values ('341bea99-c093-4f7b-bd3a-d06948c890d3', 'manager', 'ivan', 'ivanov', 'manager@mail.ru', 'AJmwAup4nW7gd0S6qH3Ppb0PGbN8dcsNg0vd8MpmnM9bnoDGIqr8ZYwjVMQjEKAGYw==', 1000, '(UTC+03:00)', 'en'),
('1b81e6f1-91a3-4166-b59e-9e09d9756c88', 'user', 'oleg', 'olegov', 'oleg@mail.ru', 'AJWsqdXJpXDfBgpZQuk95/JIk5ZghEiMf8Kj0Q2vu6gYThp9sQHCZEMnVuF4PjI1JQ==', 200, '(UTC+03:00)', 'en'),
('9024dcfb-8a98-4003-9934-e0145252a28b','anna','anna','petrova','anna@mail.ru','AQAAAAEAACcQAAAAEBc3d8YfWSu3d6DdFenaQZ4P4pGu82QfLTyoddZ6t953/mXKNGMBoZCO5z4OvS8E/w==',1000,'(UTC+03:00)','en')

--- UserRole
insert into dbo.UserRole
values (1,'341bea99-c093-4f7b-bd3a-d06948c890d3'),
(2,'1b81e6f1-91a3-4166-b59e-9e09d9756c88'),
(3,'9024dcfb-8a98-4003-9934-e0145252a28b')