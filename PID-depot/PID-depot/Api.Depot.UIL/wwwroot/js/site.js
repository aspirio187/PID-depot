// Write your Javascript code.

function makeAvailable(day) {

	$('#' + day.id + ' > input').each(function () {

		if ($(this).is('input[type="checkbox"]') && $(this).is(':checked')) {
			console.log("is checked");
		}

		if (!$(this).is('input[type="checkbox"]') && $(this).attr('name').indexOf('.Day') < 0) {

			if ($(this).prop('disabled')) {
				$(this).prop('disabled', false);
			} else {
				$(this).prop('disabled', true);
			}
		}
	});
}