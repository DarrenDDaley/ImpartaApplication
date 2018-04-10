var rootURL = 'http://localhost:52370/api/tasks';
$.ajaxSetup({ cache: false });

$(document).ready(function() {
$.ajax({
	    type: 'GET',	
		url: rootURL,
        dataType: 'json',
        success: createTaskList
	});
});

function createTaskList(taskList)
{
	$.each(taskList, function(i, task) {
		AppenedToList(task);
     });
}

function AppenedToList(task) {
	
	var taskElement = '<div id=\''+ task.id + '\'><p>' + task.description + 
		'</p><button onclick="deleteTask(\''+ task.id + '\')">Delete</button>' +
		'<input type="checkbox" name="terms" checked=\''+ task.done + '\'/> Done</div>';
		
	$("#taskList").append(taskElement);
}


function PrependToList(task) {
		var taskElement = '<div id=\''+ task.id + '\'><p>' + task.description + 
		'</p><button onclick="deleteTask(\''+ task.id + '\')">Delete</button>' +
		'<input type="checkbox" name="terms" checked=\''+ task.done + '\'/> Done</div>';
		
	$("#taskList").prepend(taskElement);
}

function postTask() {
	
  var task = {
    description: $('#task').val(),
    done: false
  };

  $.ajax({
	type: 'POST',
	url: rootURL + '/add',
	data: JSON.stringify(task),
	contentType: 'application/json',
	dataType: 'json',
	success: function(id) {
		var newTask = {
			 description: $('#task').val(),
		     done: false,
			 id: id
		};
		$('#task').val('');
		PrependToList(newTask);
	  }
  });
}

function deleteTask(id) {
	
	$.ajax({
	type: 'DELETE',
	url: rootURL + '/delete/'+ id,
	success: function() {
		$('#'+ id).remove();
	  }
  });
}