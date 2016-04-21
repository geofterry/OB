CREATE TABLE Tada(
	TadaNo char(23) primary key not null,
	NominalTada int not null,
	BrandName varchar(255) not null,
	GenerationID char(10) not null,
	foreign key (GenerationID) references Generation on update cascade on delete cascade
)