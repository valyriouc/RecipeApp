DROP DATABASE IF EXISTS recipeAppDb;
CREATE DATABASE recipeAppDb;
USE recipeAppDb;

CREATE TABLE User(
    userId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password TEXT NOT NULL
);

CREATE TABLE Recipe(
    recipeId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(100) NOT NULL, 
    createdAt DATETIME NOT NULL,
    description TEXT NOT NULL,
    imageUrl TEXT NOT NULL,
    fuserId INTEGER REFERENCES User(userId)
);

CREATE TABLE Ingredient(
    ingredientId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100)
);

CREATE TABLE UnitOfMeasurement(
    measureId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100)
);

CREATE TABLE IngredUnitMapping(
    ingredUnitMappingId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    fIngredientId INTEGER REFERENCES Ingredient(ingredientId),
    fUnitId INTEGER REFERENCES UnitOfMeasurement(measureId)
);

CREATE TABLE RecipeIngredList(
    recipIngredId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    frecipeId INTEGER REFERENCES Recipe(recipeId),
    fIngredUnitMap INTEGER REFERENCES IngredUnitMapping(ingredUnitMappingId)
);