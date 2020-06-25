Drop database QuanLyBanHang_DoAn
create database QuanLyBanHang_DoAn
go
use QuanLyBanHang_DoAn
go
create table Warehouses
(
	WH_id int identity primary key,
	WH_name nvarchar(max) not null,
	WH_address nvarchar(max) not null,
	WH_phone varchar(12) not null,
)
create table Category
(
	Cate_id int identity primary key,
	Cate_name nvarchar(max) not null,
)
create table Level
(
	Cus_level int identity primary key,
	Level_name nvarchar(max),
)
create table [Provider]
(
	Provider_id int identity primary key,
	Provider_name nvarchar(max) not null,
	Provider_address nvarchar(max) not null,
	Provider_phone varchar(12) not null,
)
create table Customers
(
	Cus_id int identity primary key,
	Cus_name nvarchar(max) not null,
	Cus_phone varchar(12) not null,
	Cus_address nvarchar(max) not null,
	Cus_email varchar(max) not null,
	Cus_Point float,
	Cus_level int,
	Cus_DOB date,
	foreign key (Cus_level) references Level(Cus_level),
)
create table Products
(
	Pro_id int identity primary key,
	Pro_name nvarchar(max),
	Pro_detail nvarchar(max),
	Pro_status bit,
	Cate_id int,
	Pro_Price float,
	Provider_id int,
	Pro_quantity int,
	foreign key (Cate_id) references Category(Cate_id),
	foreign key (Provider_id) references [Provider](Provider_id),
)
create table Position 
(
	Pos_id int identity primary key,
	Pos_name nvarchar(max),
)
create table [Login]
(
	Account_id int identity primary key,
	Username nvarchar(max),
	Password nvarchar(max),
)
create table Account 
(
	Account_id int,
	Account_name nvarchar(max),
	Pos_id int,
	Account_address nvarchar(max),
	Account_phone varchar(12),
	Account_DOB date,
	Account_Email varchar(max),
	Account_status bit,
	Account_avatar nvarchar(max) default 'default-avatar.jpg',
	primary key (Account_id),
	foreign key (Account_id) references [Login](Account_id),
	FOREIGN KEY (Pos_id) REFERENCES Position(Pos_id) ON DELETE CASCADE ON UPDATE CASCADE,
)

create table [Order]
(
	Order_id int identity primary key,
	Order_note nvarchar(max),
	Cus_id int,
	WH_id int,
	Account_id int,
	Quantity int,
	Order_day date,
	Total_Price float,
	foreign key (Cus_id) references Customers(Cus_id),
	foreign key (WH_id) references Warehouses(WH_id),
	foreign key (Account_id) references Account(Account_id),
)
select * from [Order]
create table Receipt
(
	Receipt_id int identity primary key,
	Provider_id int,
	Account_id int,
	Receipt_date date,
	Total_Receipt float,
	FOREIGN KEY (Account_id) REFERENCES Account(Account_id) ON DELETE CASCADE ON UPDATE CASCADE,
)
create table Receipt_Detail
(
	Receipt_id int,
	Pro_id int,
	Quantity int,
	Pro_Price float,
	Sum_Price float,
	foreign key (Receipt_id) references Receipt(Receipt_id),
	foreign key (Pro_id) references Products(Pro_id),
)

create table Order_detail (
	order_detail_id int identity primary key,
	Order_id int,
	Quantity int,
	Pro_id int,
	order_note nvarchar(MAX),
	order_total_price float,
	foreign key (Order_id) references [Order](Order_id),
	foreign key (Pro_id) references Products(Pro_id),
)
insert Login values ('vietdd', 'admin123')
insert Level values (N'Đồng')
insert Level values (N'Bạc')
insert Level values (N'Vàng')
insert Level values (N'Bạch Kim')
insert Level values (N'Kim Cương')

insert Position values (N'Nhân Viên Kho')
insert Position values (N'Bán Hàng')
insert Position values (N'Quản Lý')
insert Account values (1,N'Đinh Doãn Việt',1,N'Hà Nội','0965062715','1999-01-02','doanvietcntt99@gmail.com',1)

insert into Warehouses values ('Kho V1' , '225 - Quan Hoa - Cau Giay - Ha Noi', '0824744871')
insert into Account values ('2', 'Le Tung', 2, 'Ha Noi' , '0824744871', '11/27/1999', 'lonthong79@gmail.com', 'True')
insert into Customers values ('KH1', 'Tung', '0824744871', '225-QuanHoa', 'letungcntt@gmail.com', '100', '4', '11/27/1999')

insert into Category values ('Crikle')
insert into Category values ('VinMart')

insert into Provider values ('DHD', '235-Hoang Quoc Viet', '0824744871')
insert into Provider values ('DHT', '234-Hoang Quoc Viet', '0824544871')

insert into Products values ('Banh my', 'Banh my sieu to khong lo', '1', '1' , '20000','1','5')
insert into Products values ('Banh bao', 'Banh bao sieu to khong lo', '1', '2' , '20000','1','5')
insert into Products values ('Banh chuoi', '', '0', '1' , '20000','1','5')
insert into Category values (N'Laptop')
insert into Category values (N'CPU')
insert into Category values (N'Mouse')
insert into Category values (N'Keyboard')
insert into Category values (N'USB')
insert into [Provider] values (N'Hanoi Computer','Ha Noi','0965062715')

