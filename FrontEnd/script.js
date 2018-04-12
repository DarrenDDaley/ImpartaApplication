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
		appendToList(task);
     });
}

function appendToList(task) {	
	$("#task-items").append(createTaskHTML(task.id, task.description, task.done));
}

function postTask() {
	
  var task = {
    description: $('#task-input').val(),
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
			 description: $('#task-input').val(),
		     done: false,
			 id: id
		};
		$('#task-input').val('');
		appendToList(newTask);
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
	
	var editElements = '<div id=\''+ id + '\' class="task-list-item"><div class="task-description"><input type="text" class="task-textbox" id="editText-'+ id +'" value="'+ description +'">'
					    +'<div class="item-buttons"><button class="task-button" onclick="editTask(\''+ id + '\', \''+ description + '\', ' + done +')">Save</button>'
						+'<button class="task-button" onclick="placeTask(\''+ id + '\', \''+ description + '\', ' + done +')">Cancel</button></div></div>';
	
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
	
	var taskElement = '<div id=\''+ id + '\' class="task-list-item" ><div class="task-description">' + description + 
		'</span><div class="item-buttons"><button class="task-button" onclick="createEditHTML(\''+ id + '\', \''+ description + '\', ' + done + 
		')">Edit</button><button class="task-button" onclick="deleteTask(\''+ id + '\')">Delete</button>';
		
		if(done == true) {
			taskElement += '<input type="checkbox" class="task-done" id="checkbox-'+ id + '"onclick="taskDone(\''+ id + '\')" checked/><span>Done</span></div></div>';
		}
		else {
			taskElement += '<input type="checkbox" class="task-done" id="checkbox-'+ id + '"onclick="taskDone(\''+ id + '\')" /><span>Done</span></div></div>';
		}
	
	return taskElement;
}