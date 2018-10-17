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
values (1, 'First area of first layout', 1, 1),
(1, 'Second area of first layout', 1, 1),
(2, 'First area of second layout', 2, 2),
(3, 'Area withount seats', 3, 3)

--- Seat
insert into dbo.Seat
values (1, 1, 1),
(1, 1, 2),
(1, 1, 3),
(1, 1, 4),
(1, 1, 5),
(1, 2, 1),
(1, 2, 2),
(1, 2, 3),
(1, 2, 4),
(1, 2, 5),
(2, 1, 1),
(2, 2, 1),
(3, 1, 1)

--- Event(for test)
insert into dbo.Event
values ('ForTestEvent', 'For test', '2010-06-05 15:32:11.037', '2010-06-05 15:32:11.037','image', 1)

--- Role
insert into dbo.Role
values ('event_manager'),
('user')

--- User
insert into dbo.[User]
values ('341bea99-c093-4f7b-bd3a-d06948c890d3', 'manager', 'ivan', 'ivanov', 'manager@mail.ru', 'AJmwAup4nW7gd0S6qH3Ppb0PGbN8dcsNg0vd8MpmnM9bnoDGIqr8ZYwjVMQjEKAGYw==', 0, '(UTC+03:00)', 'en')

--- UserRole
insert into dbo.UserRole
values (1,'341bea99-c093-4f7b-bd3a-d06948c890d3')
