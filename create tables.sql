drop table if exists Items;
drop table if exists Orders;
drop table if exists Customer;
drop table if exists Locations;


create table Customer 
(
	Username nvarchar(80)  primary key,
	Name nvarchar(80)
);

create table Locations
(
	LocationId int identity primary key,
	LocationName nvarchar(80),
	LocationAddress nvarchar(100),
);

create table Orders 
(
	OrderId int identity primary key,
	CUsername nvarchar(80) references Customer(Username) on delete cascade null,
	Orderdate date default CURRENT_TIMESTAMP,
	total decimal(38,36)
);

create table Items
(
	ItemId int identity primary key,
	LocationId int references Locations(LocationId) on delete cascade null,
	OrderId int references Orders(OrderId) default null,
	ProductName nvarchar(100),
	Price decimal(38,36),
	Quantity int
);

insert into Customer (Username, Name) values 
('johndoe123', 'John Doe'), ('janedoe123', 'Jane Doe'), ('amanboru123', 'Aman Boru'), ('amanuelboru123', 'Amanuel Boru');

select * from Customer;

insert into Locations (LocationName, LocationAddress) values
('cool store', '123 cool rd Baltimore MD'), ('cooler store', '123 cooler rd Baltimore MD'), ('coolest store', '123 coolest rd Baltimore MD');

select * from Locations;

-- location Inventory
insert into Items (LocationId, OrderId, ProductName, Price, Quantity) values
(1, null, 'Rose', 12.50, 10), (2, null, 'Rose', 12.50, 10), (3, null, 'Rose', 12.50, 10),
(1, null, 'Tulip', 9.99, 10), (2, null, 'Tulip', 9.99, 10), (3, null, 'Tulip', 9.99, 10),
(1, null, 'Sunflower', 5.00, 10), (2, null, 'Sunflower', 5.00, 10), (3, null, 'Sunflower', 5.00, 10),
(1, null, 'Daisy', 5.00, 10), (2, null, 'Daisy', 5.00, 10), (3, null, 'Daisy', 5.00, 10);

-- orders
insert into Orders (CUsername, Orderdate, total) values
('johndoe123', '2015-06-24', 12.50), ('janedoe123', '1998-04-16', 19.98), ('amanboru123', '2011-05-27', 15.00), ('amanuelboru123', '1996-12-20', 32.49);

select * from Orders;

insert into Items (LocationId, OrderId, ProductName, Price, Quantity) values (1, 1, 'Rose', 12.50, 1);
insert into Items (LocationId, OrderId, ProductName, Price, Quantity) values (1, 2, 'Tulip', 9.99, 2);
insert into Items (LocationId, OrderId, ProductName, Price, Quantity) values (1, 3, 'Sunflower', 5.00, 3);
insert into Items (LocationId, OrderId, ProductName, Price, Quantity) values (1, 4, 'Rose', 12.50, 1), (1, 4, 'Tulip', 9.99, 1), (1, 4, 'Sunflower', 5.00, 1), (1, 4, 'Daisy', 5.00, 1);

select * from Items;