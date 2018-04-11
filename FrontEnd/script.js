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
	$("#taskList").append(createTaskHTML(task.id, task.description, task.done));
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
	var checkedElement = '#checkbox-'+id;
	
	if($(checkedElement).is(':checked') == true) {
		done = true;
	}
	
	$.ajax({
	type: 'PUT',
	data: { "done": done },
	url: rootURL + '/done/'+ id,
	});
}

function createEditHTML(id, description, done) {
	
	var editElements = '<div id=\''+ id + '\'><input type="text" name="editText" id="editText-'+ id +'" value="'+ description +'">'
					    +'<button onclick="editTask(\''+ id + '\', \''+ description + '\', ' + done +')">Save</button>'
						+'<button onclick="placeTask(\''+ id + '\', \''+ description + '\', ' + done +')">Cancel</button><br><br>';
	
	$("#" + id).replaceWith(editElements);
}

function editTask(id, originalDesc, done) {
	
	var task = {
    description: $('#editText-'+id).val(),
    done: done
  };
  
   $.ajax({
	type: 'PUT',
	url: rootURL + '/edit/'+id,
	data: JSON.stringify(task),
	contentType: 'application/json',
	dataType: 'json',
	success: placeTask(id, task.description, done)
  });	
}

function placeTask(id, description, done) {
	$("#" + id).replaceWith(createTaskHTML(id, description, done));
}

function createTaskHTML(id, description, done) {
	
	var taskElement = '<div id=\''+ id + '\'><p>' + description + 
		'</p><button onclick="createEditHTML(\''+ id + '\', \''+ description + '\', ' + done + 
		')">Edit</button><button onclick="deleteTask(\''+ id + '\')">Delete</button>';
		
		if(done == true) {
			taskElement += '<input type="checkbox" id="checkbox-'+ id + '"onclick="taskDone(\''+ id + '\')" checked/> Done</div>';
		}
		else {
			taskElement += '<input type="checkbox" id="checkbox-'+ id + '"onclick="taskDone(\''+ id + '\')" /> Done</div>';
		}
	
	return taskElement;
}





