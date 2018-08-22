
$(".edit-btn").click(editFood);

function editFood() {
    var parentForm = $(this).parent();
    $(this).remove();
    parentForm.append("<label>Meal Classification</label><select name=\"mealId\"><option value=\"1\">Breakfast</option><option value=\"2\">Lunch</option><option value=\"3\">Dinner</option><option value=\"4\">Snack</option><option value=\"5\">Dessert</option></select>");
    parentForm.append("<label>Number of Servings: </label><select name=\"numberOfServings\"><option value =\"1\">1</option><option value=\"2\">2</option><option value=\"3\">3</option><option value=\"4\">4</option><option value=\"5\">5</option><option value=\"6\">6</option><option value=\"7\">7</option><option value=\"8\">8</option><option value=\"9\">9</option><option value=\"10\">10</option></select > ");
    parentForm.append("<button class=\"btn update-btn\" type=\"submit\" value=\"submit\">Update</button>")
    return false;
}

