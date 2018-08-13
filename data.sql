
INSERT INTO users (userName, [password], userFirstName, userLastName, birthday, userAge, userHeight, userCurrentWeight, userDesiredWeight, recommendedDailyCaloricIntake, mealStreak, meal1, meal2, meal3, meal4, meal5, mealOrder)
	VALUES ('jdean', 'iLoveFood', 'James', 'Dean', '19280810', 90, 72, 200, 180, 1500, 4, 1, 2, 3, 0, 0, '1,2,3');
INSERT INTO users (userName, [password], userFirstName, userLastName, birthday, userAge, userHeight, userCurrentWeight, userDesiredWeight, recommendedDailyCaloricIntake, mealStreak, meal1, meal2, meal3, meal4, meal5, mealOrder)
	VALUES ('revans', 'iLoveBreakfast', 'Robert', 'Evans', '19580707', 60, 65, 190, 170, 1600, 3, 1, 2, 0, 0, 0, '1,2');

INSERT INTO foods (foodName, servingSize, calories, foodGroup) VALUES ('egg', 1, 78, 'protein');
INSERT INTO foods (foodName, servingSize, calories, foodGroup) VALUES ('bacon', 1, 43, 'protein');
INSERT INTO foods (foodName, servingSize, calories, foodGroup) VALUES ('sausage', 1, 229, 'protein');

INSERT INTO meals (mealName) VALUES ('Breakfast');
INSERT INTO meals (mealName) VALUES ('Lunch');
INSERT INTO meals (mealName) VALUES ('Dinner');
INSERT INTO meals (mealName) VALUES ('Snack');
INSERT INTO meals (mealName) VALUES ('Dessert');

INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (1, '20180811', 1, 2, 1);
INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (1, '20180811', 1, 2, 2);
INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (2, '20180811', 1, 1, 1);
INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (2, '20180811', 1, 1, 3);
INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (1, '20180812', 1, 1, 1);
INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (1, '20180812', 1, 2, 2);
INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (2, '20180812', 1, 2, 1);
INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, foodId)
	VALUES (2, '20180812', 1, 2, 3);

INSERT INTO dailyWeight (userId, dateOfEntry, todaysWeight) VALUES (1, '20180811', 200);
INSERT INTO dailyWeight (userId, dateOfEntry, todaysWeight) VALUES (1, '20180812', 198);
INSERT INTO dailyWeight (userId, dateOfEntry, todaysWeight) VALUES (2, '20180811', 190);
INSERT INTO dailyWeight (userId, dateOfEntry, todaysWeight) VALUES (2, '20180812', 189);

INSERT INTO quickMeals (userId, foodId, numberOfServings, quickMealId, lastUsed)
	VALUES (1, 1, 2, 1, '20180812')
INSERT INTO quickMeals (userId, foodId, numberOfServings, quickMealId, lastUsed)
	VALUES (1, 2, 2, 1, '20180812')
INSERT INTO quickMeals (userId, foodId, numberOfServings, quickMealId, lastUsed)
	VALUES (2, 1, 2, 1, '20180812')
INSERT INTO quickMeals (userId, foodId, numberOfServings, quickMealId, lastUsed)
	VALUES (2, 3, 2, 1, '20180812')