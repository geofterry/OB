--DROP TABLE
DROP TABLE Employee
DROP TABLE Generation
DROP TABLE Tada
DROP TABLE TopUp
DROP TABLE OrderVoucher
DROP TABLE DetailOrderVoucher
DROP TABLE OrderItem
DROP TABLE DetailOrderItems
DROP TABLE RedeemCash
DROP TABLE DetailRedeemCash
DROP TABLE Donate
DROP TABLE DetailDonate


--CREATE TABLE
CREATE TABLE Employee(
	Username varchar(255) primary key not null,
	EmployeeName varchar (255) not null,
	EmployeePassword varchar (255) not null,
	EmployeeRole varchar (255) not null
)

CREATE TABLE Generation(
	GenerationID varchar(255) primary key not null,
	NominalGeneration int not null,
	JumlahGeneration int not null,
	TanggalGeneration date not null,
	InvoiceNo varchar(255) not null,
	BrandName varchar(255) not null,
	Username varchar(255) not null,
	--foreign key (Username) references Employee on update cascade on delete cascade
)

CREATE TABLE Tada(
	TadaNo varchar(255) primary key not null,
	NominalTada int not null,
	GenerationID varchar(255) not null,
	--foreign key (GenerationID) references Generation on update cascade on delete cascade
)

CREATE TABLE TopUp(
	TopUpID varchar(255)primary key not null,
	JumlahTopUp int not null,
	TanggalTopUp datetime not null,
	TadaNo varchar(255) not null,
	Username varchar(255) not null,
	foreign key (TadaNo) references Tada on update cascade on delete cascade,
	--foreign key (Username) references Employee on update cascade on delete cascade
)

CREATE TABLE OrderVoucher(
	NoOrder varchar(255) primary key not null,
	Tanggal date not null,
	NamaVoucher varchar(255) not null,
	Claimer varchar(255) not null,
	Jumlah int not null,
	Pecahan int not null,
	Delivery int not null,
	Email varchar(255) not null,
	Telepon varchar(255) not null,
	Alamat varchar(255) not null,
	StatusOrderVoucher varchar (255) not null,
	Username varchar(255) not null,
	--foreign key (Username) references Employee on update cascade on delete cascade
)

CREATE TABLE DetailOrderVoucher(
	TadaNo varchar(255) not null,
	NoOrder varchar(255) not null,
	Terpakai int not null,
	--primary key(TadaNo, NoOrder),
	--foreign key (TadaNo) references Tada on update cascade on delete cascade,
	--foreign key (NoOrder) references OrderVoucher on update cascade on delete cascade
)

CREATE TABLE OrderItem(
	NoOrder varchar(255) primary key not null,
	Claimer varchar(255) not null,
	Tanggal date not null,
	Kategori varchar(255) not null,
	Barang varchar(255) not null,
	Jumlah int not null,
	Harga int not null,
	Delivery int not null,
	Email varchar(255) not null,
	Telepon varchar(255) not null,
	Alamat varchar(255) not null,
	StatusOrderItem varchar(255) not null,
	Username varchar(255) not null,
	--foreign key (Username) references Employee on update cascade on delete cascade
)

CREATE TABLE DetailOrderItems(
	TadaNo varchar(255) not null,
	NoOrder varchar(255) not null,
	Terpakai int not null,
	--primary key(TadaNo, NoOrder),
	--foreign key (TadaNo) references Tada on update cascade on delete cascade,
	--foreign key (NoOrder) references OrderItem on update cascade on delete cascade
)

CREATE TABLE RedeemCash(
	RedeemNo varchar(255) primary key not null,
	Tanggal date not null,
	Claimer varchar(255) not null,
	NamaBank varchar (255) not null,
	Telepon varchar(255) not null,
	NamaRekening varchar(255) not null,
	NoRekening varchar(255) not null,
	NamaCabang varchar(255) not null,
	StatusRedeemCash varchar(255) not null,
	Username varchar(255) not null,
	--foreign key (Username) references Employee on update cascade on delete cascade
)

CREATE TABLE DetailRedeemCash(
	TadaNo varchar(255) not null,
	RedeemNo varchar(255) not null,
	Terpakai int not null,
	--primary key(TadaNo, RedeemNo),
	--foreign key (TadaNo) references Tada on update cascade on delete cascade,
	--foreign key (RedeemNo) references RedeemCash on update cascade on delete cascade
)

CREATE TABLE Donate(
	RedeemNo varchar(255) primary key not null,
	Yayasan varchar(255) not null,
	Tanggal date not null,
	Claimer varchar(255) not null,
	StatusDonate varchar (255) not null,
	Username varchar(255) not null,
	--foreign key (Username) references Employee on update cascade on delete cascade
)

CREATE TABLE DetailDonate(
	TadaNo varchar(255) not null,
	RedeemNo varchar(255) not null,
	Terpakai int not null
	--primary key(TadaNo, RedeemNo),
	--foreign key (TadaNo) references Tada on update cascade on delete cascade,
	--foreign key (RedeemNo) references Donate on update cascade on delete cascade
)