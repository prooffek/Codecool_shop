create table [Address Data]
(
    Id      int identity
        constraint [Address Data_pk]
            primary key nonclustered,
    Country nvarchar(255),
    City    nvarchar(255),
    ZipCode nvarchar(255),
    Street  nvarchar(255)
)
go

create unique index [Address Data_Id_uindex]
    on [Address Data] (Id)
go

create table Cart
(
    Id   int identity
        constraint Cart_pk
            primary key nonclustered,
    Date datetime
)
go

create unique index Cart_Id_uindex
    on Cart (Id)
go

create table Country
(
    Id          int identity
        constraint Country_pk
            primary key nonclustered,
    Name        nvarchar(255),
    Description nvarchar(255)
)
go

create unique index Country_Id_uindex
    on Country (Id)
go

create table OrderStatus
(
    Id          int identity
        constraint OrderStatus_pk
            primary key nonclustered,
    Name        nvarchar(255),
    Description nvarchar(255)
)
go

create unique index OrderStatus_Id_uindex
    on OrderStatus (Id)
go

create table ProductCategory
(
    Id          int identity
        constraint ProductCategory_pk
            primary key nonclustered,
    Name        nvarchar(255),
    Description nvarchar(255)
)
go

create unique index ProductCategory_Id_uindex
    on ProductCategory (Id)
go

create table TravelAgency
(
    Id          int identity
        constraint TravelAgency_pk
            primary key nonclustered,
    Name        nvarchar(255),
    Description nvarchar(255)
)
go

create table Product
(
    Id                int identity
        constraint Product_pk
            primary key nonclustered,
    Name              nvarchar(255),
    Description       nvarchar(255),
    DefaultPrice      decimal,
    LengthOfStay      int,
    City              nvarchar(255),
    ImgName           nvarchar(255),
    ProductCategoryId int
        constraint Product_ProductCategory_Id_fk
            references ProductCategory,
    TravelAgencyId    int
        constraint Product_TravelAgency_Id_fk
            references TravelAgency,
    CountryId         int
        constraint Product_Country_Id_fk
            references Country,
    Currency nvarchar(255) default 'PLN',
)
go

create table CartItem
(
    Id        int identity
        constraint CartItem_pk
            primary key nonclustered,
    ProductId int
        constraint CartItem_Product_Id_fk
            references Product,
    Quantity  int,
    CartId    int
)
go

create unique index CartItem_Id_uindex
    on CartItem (Id)
go

create unique index Product_Id_uindex
    on Product (Id)
go

create unique index TravelAgency_Id_uindex
    on TravelAgency (Id)
go

create table [User]
(
    Id            int identity
        constraint User_pk
            primary key nonclustered,
    FirstName     nvarchar(255),
    LastName      nvarchar(255),
    Email         nvarchar(255),
    Password      nvarchar(255),
    AddressDataId int
        constraint [User_Address Data_Id_fk]
            references [Address Data],
    PhoneNumber   nvarchar(255)
)
go

create table [Order]
(
    Id       int identity
        constraint Order_pk
            primary key nonclustered,
    Date     datetime,
    CartId   int
        constraint Order_Cart_Id_fk
            references Cart,
    UserId   int
        constraint Order_User_Id_fk
            references [User],
    StatusId int
        constraint Order_OrderStatus_Id_fk
            references OrderStatus
)
go

create unique index Order_Id_uindex
    on [Order] (Id)
go

create unique index User_Id_uindex
    on [User] (Id)
go


