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
	Level_name nvarchar(max)not null,
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
	Cus_phone varchar(12) not null unique,
	Cus_address nvarchar(max) not null,
	Cus_email varchar(max) not null,
	Cus_Point float not null,
	Cus_level int not null,
	Cus_DOB date not null,
	foreign key (Cus_level) references Level(Cus_level),
)
create table Products
(
	Pro_id int identity primary key,
	Pro_name nvarchar(max) not null,
	Pro_detail nvarchar(max) not null,
	Pro_status bit not null,
	Cate_id int not null,
	Pro_Price float not null, 
	Provider_id int not null, 
	Pro_quantity int not null,
	foreign key (Cate_id) references Category(Cate_id),
	foreign key (Provider_id) references [Provider](Provider_id),
)
create table Position 
(
	Pos_id int identity primary key,
	Pos_name nvarchar(max) not null,
)
create table [Login]
(
	Account_id int identity primary key,
	Username nvarchar(max) not null,
	Password nvarchar(max) not null,
)
create table Account 
(
	Account_id int not null,
	Account_name nvarchar(max) not null,
	Pos_id int not null,
	Account_address nvarchar(max) not null,
	Account_phone varchar(12) not null unique,
	Account_DOB date not null,
	Account_Email varchar(max) not null,
	Account_status bit not null,
	Account_avatar nvarchar(max) default 'default-avatar.jpg',
	primary key (Account_id),
	foreign key (Account_id) references [Login](Account_id),
	FOREIGN KEY (Pos_id) REFERENCES Position(Pos_id) ON DELETE CASCADE ON UPDATE CASCADE,
)

create table [Order]
(
	Order_id int identity primary key,
	Order_note nvarchar(max),
	Cus_id int not null,
	WH_id int not null,
	Account_id int not null,
	Quantity int not null,
	Order_day date not null,
	Total_Price float not null,
	foreign key (Cus_id) references Customers(Cus_id),
	foreign key (WH_id) references Warehouses(WH_id),
	foreign key (Account_id) references Account(Account_id),
)
create table Receipt
(
	Receipt_id int identity primary key,
	Provider_id int not null,
	Account_id int not null,
	Receipt_date date not null,
	Total_Receipt float not null,
	FOREIGN KEY (Account_id) REFERENCES Account(Account_id) ON DELETE CASCADE ON UPDATE CASCADE,
)
create table Receipt_Detail
(
	Receipt_Detail_id int identity primary key,
	Receipt_id int not null,
	Pro_id int not null,
	Quantity int not null,
	Pro_Price float not null,
	Sum_Price float not null,
	foreign key (Receipt_id) references Receipt(Receipt_id),
	foreign key (Pro_id) references Products(Pro_id),
)

create table Order_detail (
	order_detail_id int identity primary key,
	Order_id int not null,
	Quantity int not null,
	Pro_id int not null,
	order_note nvarchar(MAX) not null,
	order_total_price float not null,
	foreign key (Order_id) references [Order](Order_id),
	foreign key (Pro_id) references Products(Pro_id),
)


insert Position values (N'Nhân Viên Kho')
insert Position values (N'Bán Hàng')
insert Position values (N'Quản Lý')
insert Login values ('vietdd', '0192023a7bbd73250516f069df18b500')
insert Account values (1,N'Đinh Doãn Việt',3,N'Hà Nội','0965062715','1999-01-02','doanvietcntt99@gmail.com',1, 'default-avatar.jpg')
insert Login values ('cuongmd','0192023a7bbd73250516f069df18b500')
insert Account values (2,N'Mai Việt Cường',1,N'Hà Nội','086837141','1999-01-02','cuongmv@gmail.com',1, 'default-avatar.jpg')
insert Login values ('tunglt','0192023a7bbd73250516f069df18b500')
insert Account values (3,N'Lê Thanh Tùng',2,N'Thanh Hóa','098765423','1999-05-05','tunglt@gmail.com',1, 'default-avatar.jpg')
insert Login values ('admin','0192023a7bbd73250516f069df18b500')
insert Account values (4,N'Adminstator', 3, N'Hà Nội','0986678636','2020-06-27','adminstator@gmail.com',1, 'default-avatar.jpg')
insert into Warehouses values ('Kho V1' , '225 - Quan Hoa - Cau Giay - Ha Noi', '0824744871')
insert into Warehouses values ('Kho V2' , '226 - Quan Hoa - Cau Giay - Ha Noi', '0824744872')
insert into Warehouses values ('Kho V2' , '227 - Quan Hoa - Cau Giay - Ha Noi', '0824744873')
insert into Category values ('USB')
insert into Category values ('Laptop')
insert into Category values ('Keybroad')
insert into Category values ('CPU')
insert into Category values ('RAM')
insert into [Provider] values ('DHD', '235-Hoang Quoc Viet', '0824744876')
insert into [Provider] values ('DHT', '234-Hoang Quoc Viet', '0824544877')
insert into [Provider] values (N'Hanoi Computer','Ha Noi','0965062715')
insert into Products values (N'USB Kingston 64GB',N'USB Kingston dung lượng 64GB',1,1,105000,3,10)
insert into Products values (N'USB Kingston 32GB',N'USB Kingston dung lượng 32GB',1,1,95000,3,20)
insert into Products values (N'USB Kingston 16GB',N'USB Kingston dung lượng 16GB',1,1,75000,3,30)
insert into Products values (N'USB Kingston 8GB',N'USB Kingston dung lượng 8GB',1,1,55000,3,50)
insert into Products values (N'USB Kingston 4GB',N'USB Kingston dung lượng 4GB',1,1,35000,3,10)
insert into Products values (N'Laptop Asus VivoBook A512DA-EJ421T',N'Laptop Asus VivoBook A512DA-EJ421T (R3 3200U/4GB RAM/256GB SSD/15.6 inch FHD/Win 10/Bạc)',1,2,10999000,2,5)
insert into Products values (N'Laptop Asus X409MA-BV031T Silver',N'Laptop Asus VivoBook Asus X409MA-BV031T Silver',1,2,6190000,2,5)
insert into Products values (N'Laptop Lenovo IdeaPad S145-15API (81UT00DMVN)',N'Laptop Lenovo IdeaPad S145-15API (81UT00DMVN) (R3 3200U/4GB RAM/256GB SSD/15.6 FHD/Win10/Grey)',1,2,0699000,2,10)
insert into Products values (N'Laptop Asus D509DA-EJ285T',N'Laptop Asus D509DA-EJ285T (R3 3200U/4GB RAM/256GB SSD/15.6 inch FHD/Win 10/Bạc)',1,2,9799000,2,7)
insert into Products values (N'Laptop Asus X409JA-EK014T',N'Laptop Asus X409JA-EK014T i5-1035G1U/ 4GB/ 512GB/ 14" FHD/ Win 10',1,2,13990000,2,2)
insert into Products values (N'Bàn phím cơ E-DRA EK387 Brown Switch',N'Bàn phím cơ E-DRA EK387 Brown Switch Bảo hành 24 Tháng',1,3,549000,3,10)
insert into Products values (N'Bàn phím cơ Bluetooth Keychron K4',N'Bàn phím cơ Bluetooth Keychron K4 Nhôm Led RGB Red Switch',1,3,2190000,3,5)
insert into Products values (N'CPU Intel Core i9-9900K',N'CPU Intel Core i9-9900K (3.6GHz turbo up to 5.0GHz, 8 nhân 16 luồng, 16MB Cache, 95W) - Socket Intel LGA 1151-v2',1,4,12299000,1,10)
insert into Products values (N'CPU Intel Core i7-9700',N'CPU Intel Core i7-9700 (3.0GHz turbo up to 4.7Ghz, 8 nhân 8 luồng, 12MB Cache, 65W) - Socket Intel LGA 1151-v2',1,4,8499000,1,2)
insert into Products values (N'Ram PC Kingston HyperX',N'Ram PC Kingston HyperX Predator RGB 16GB 3200MHz DDR4 (8GBx2) HX432C16PB3AK2/16',1,5,2290000,1,10)
insert into Products values (N'RAM Desktop KINGSTON HyperX Predator RGB',N'RAM Desktop KINGSTON HyperX Predator RGB (HX432C16PB3AK2/16) 16GB (2x8GB) DDR4 3200MHz',1,5,2599000,1,10)
insert Level values (N'Đồng')
insert Level values (N'Bạc')
insert Level values (N'Vàng')
insert Level values (N'Bạch Kim')
insert Level values (N'Kim Cương')
insert Customers values (N'Nguyễn Thùy Miên','0912344567',N'Thái Bình','miennt@gmail.com',5.5,1,'1999-01-09')
insert Customers values (N'Đỗ Hồng Đức','0912344568',N'Thanh Hóa','ducdh@gmail.com',6.5,2,'1999-02-07')
insert Customers values (N'Nguyễn Duy Phong','0912344562',N'Thanh Hóa','phongnd@gmail.com',5.5,1,'1999-01-12')
insert Customers values (N'Vũ Thanh Bình','0912344569',N'Thanh Hóa','binhvt@gmail.com',7.5,3,'1999-01-15')
select * from Login as a, Account as b where a.Account_id = b.Account_id
