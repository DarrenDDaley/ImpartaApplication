$(document).ready(function(){
	
var rootURL = "http://localhost:52370/api/tasks/";
	
var task ={
  Description: $('#task').val(),
  Done: false
};
	
  $('#add').on('click', function() {
	$.ajax({
		type: 'POST',
		url: rootURL + 'add',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		data: task,
		success:  function (data) { 
			alert('DONE');
			}
		});
	});
});