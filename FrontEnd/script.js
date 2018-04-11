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
		appenedToList(task);
     });
}

function appenedToList(task) {
	
	var taskElement = '<div id=\''+ task.id + '\'><p>' + task.description + 
		'</p><button onclick="deleteTask(\''+ task.id + '\')">Delete</button>';
		
		if(task.done == true) {
			taskElement += '<input type="checkbox" id="checkbox-'+ task.id + '" onclick="taskDone(\''+ task.id + '\')" checked/> Done</div>';
		}
		else {
			taskElement += '<input type="checkbox" id="checkbox-'+ task.id + '" onclick="taskDone(\''+ task.id + '\')" /> Done</div>';
		}
		
	$("#taskList").append(taskElement);
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
		appenedToList(newTask);
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

function taskDone(id) {
	
	var done = false;
	var checkedElement = 'checkbox-'+id;
	
	if($(checkedElement).is(':checked') == true) {
		done = true;
	}
	
	$.ajax({
	type: 'PUT',
	data: done,
	url: rootURL + '/done/'+ id,
	});
}





