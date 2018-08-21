
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the HealthTrackDB (IF EXISTS)
IF EXISTS(select * from sys.databases where name='HealthTrackDB')
DROP DATABASE HealthTrackDB;
GO

-- Create a new HealthTrackDB
CREATE DATABASE HealthTrackDB;
GO

-- Switch to the HealthTrackDB
USE HealthTrackDB;
GO

-- Begin a TRANSACTION that must complete with no errors
BEGIN TRANSACTION;


CREATE TABLE users (
	userId integer							IDENTITY(1,1),
	userName varchar(20)					NOT NULL,
	email varchar(50)						NOT NULL,
	[password] varchar(70)					NOT NULL,
	salt varchar(50)						NOT NULL,
	[role] varchar(50)						default('user'),
	userFirstName varchar(20)				NOT NULL,
	userLastName varchar(20)				NOT NULL,
	birthday date							NOT NULL,
	userAge integer							,
	userHeight integer						,
	userCurrentWeight integer				,
	userDesiredWeight integer				,
	recommendedDailyCaloricIntake integer	,
	mealStreak integer						,
	meal1 integer							,
	meal2 integer							,
	meal3 integer							,
	meal4 integer							,
	meal5 integer							,
	mealOrder varchar(20)					,

	CONSTRAINT pk_users PRIMARY KEY (userId),
	CONSTRAINT users_email UNIQUE (email),
	CONSTRAINT users_userName UNIQUE (userName)
);


CREATE TABLE foods (
	foodId integer							IDENTITY(1,1),
	foodName varchar(200)					,
	servingQuantity float					,
	servingUnit varchar(50)					,
	calories float							,
	totalFat float							,
	saturatedFat float						,
	cholesterol float						,
	sodium		float						,
	totalCarbohydrate float					,
	dietaryFiber	float					,
	sugars float							,
	protein float							,
	potassium float							,
	imgurl text								,
	

	CONSTRAINT pk_foods PRIMARY KEY (foodName)
);



CREATE TABLE meals (
	mealId integer							IDENTITY(1,1),
	mealName varchar(50)					not null,

	CONSTRAINT pk_meals PRIMARY KEY (mealId)
);

CREATE TABLE users_foods (
rowId integer				IDENTITY(1,1),
userId integer				,
dateOfEntry date			,
mealId integer				,
caloriesPerServing float	,
numberOfServings decimal	,
servingQuantity decimal		,
servingUnit varchar(20)		,
--foodId integer				,
foodName varchar(200)		,
mealClassification int		,

CONSTRAINT pk_users_foods PRIMARY KEY (rowId),
CONSTRAINT fk_users_foods_userId FOREIGN KEY (userId) REFERENCES users(userId),
CONSTRAINT fk_users_foods_foodName FOREIGN KEY (foodName) REFERENCES foods(foodName),
--CONSTRAINT fk_users_foods_mealId FOREIGN KEY (mealId) REFERENCES meals(mealId)
);

CREATE TABLE dailyWeight (
	rowId integer							IDENTITY(1,1),
	userId integer							not null,
	dateOfEntry date						not null,
	todaysWeight integer					not null,

	CONSTRAINT pk_dailyWeight PRIMARY KEY (rowId),
	CONSTRAINT fk_dailyWeight_users FOREIGN KEY (userId) REFERENCES users(userId)
);

--CREATE TABLE quickMeals (
--	rowId integer							IDENTITY(1,1),
--	quickMealId integer						not null,
--	userId integer							not null,
--	foodId integer							not null,
--	numberOfServings decimal				not null,
--	lastUsed datetime						not null,

--	CONSTRAINT pk_quickMeals PRIMARY KEY (rowId),
--	CONSTRAINT fk_quickMeals_userId FOREIGN KEY (userId) REFERENCES users(userId),
--	CONSTRAINT fk_quickMeals_foodId FOREIGN KEY (foodId) REFERENCES foods(foodId),
--);

COMMIT TRANSACTION;
