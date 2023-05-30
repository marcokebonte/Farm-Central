--Creating the farm central database
CREATE DATABASE farm_central;

--Switching to the new database
USE farm_central;


--Creating the farmer table
CREATE TABLE farmer(
	farmer_id INT PRIMARY KEY IDENTITY (1,1),
	fullname VARCHAR(255) NOT NULL,
	email VARCHAR(255) NOT NULL,
	fPassword CHAR(64),
	contact_number VARCHAR(20),
	location VARCHAR(255) NOT NULL
	);


--Creating the farm table 
CREATE TABLE farm(
	farm_id INT PRIMARY KEY IDENTITY (1,1),
	farmer_id INT NOT NULL,
	name VARCHAR(255) NOT NULL,
	location VARCHAR(255) NOT NULL,
	FOREIGN KEY (farmer_id) REFERENCES farmer (farmer_id)
);



--Creating the product table 
CREATE TABLE product (
	product_id INT PRIMARY KEY IDENTITY(1,1),
	farm_id INT NOT NULL,
	name VARCHAR(255) NOT NULL,
	type VARCHAR(255) NOT NULL,
	price DECIMAL(10,2),
	FOREIGN KEY (farm_id) REFERENCES farm(farm_id)
);


--Creating the Orders table 
CREATE TABLE orders (
	order_id INT PRIMARY KEY IDENTITY(1,1),
	customer_id INT NOT NULL,
	product_id INT NOT NULL,
	quantity INT NOT NULL,
	FOREIGN KEY (customer_id) REFERENCES customer (customer_id),
	FOREIGN KEY (product_id) REFERENCES product (product_id)
);


--Creating the Customer table 
CREATE TABLE customer (
	customer_id INT PRIMARY KEY IDENTITY (1,1),
	fullname VARCHAR(255),
	email VARCHAR(255),
	address VARCHAR(255),
	contact_number VARCHAR(20)
	);


--Creating the Employee table
CREATE TABLE employee(
	employee_id INT PRIMARY KEY IDENTITY(1,1),
	fullname VARCHAR(255),
	email VARCHAR(255),
	emPassword CHAR(64),
	contact_number VARCHAR(20)
	);



--Altering Product Table
USE farm_central;
ALTER TABLE product
ADD date_range date;


--Altering Employee and Farmer tables
USE farm_central
ALTER TABLE employee
DROP COLUMN emPassword;


USE farm_central
ALTER TABLE farmer
DROP COLUMN fPassword;



--Populating Employee, Farmer, and Farm table for sample data
--Employee
USE farm_central
INSERT INTO employee (fullname, email, contact_number)
VALUES ('Marco Kebonte', 'marco@farm-central.com', 0662380093);


INSERT INTO employee (fullname, email, contact_number)
VALUES ('Johnathan Doe', 'johnathan@farm-central.com', 0723451234);


INSERT INTO employee (fullname, email, contact_number)
VALUES ('Mary Poppins', 'mary@farm-central.com', 0897865432);

--Farm
USE farm_central
INSERT INTO farm (farmer_id, name, location)
VALUES (1, 'Jojo Farm', 'Cape Town');


INSERT INTO farm (farmer_id, name, location)
VALUES (2, 'J-App Farm', 'Cape Town');

INSERT INTO farm (farmer_id, name, location)
VALUES (3, 'Merc Farm', 'Johannesburg');


--Farmer
USE farm_central
INSERT INTO farmer (fullname, email, location, contact_number )
VALUES ('Farmer Joe', 'farmerJ@farm-central.com', 'Cape Town', 0823456789);


INSERT INTO farmer (fullname, email, location, contact_number )
VALUES ('Josephina Apples', 'josephinaApples@farm-central.com', 'Cape Town', 0824567890);

INSERT INTO farmer (fullname, email, location, contact_number )
VALUES ('Brandon Mercedes', 'branmercedes@farm-central.com', 'Cape Town', 0829871234);






--Altering Employee table 
-- Insert a new row with the desired primary key value



