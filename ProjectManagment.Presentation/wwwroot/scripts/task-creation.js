$(document).ready(function () {
    $('#task-form input, #task-form select, #task-form textarea').on('change', function () {
        updateDebugInfo();
    });

    function updateDebugInfo() {
        let debugInfo = 'Текущие значения формы:<br>';
        debugInfo += 'Название: ' + $('#task-name').val() + '<br>';
        debugInfo += 'Описание: ' + $('#task-description').val() + '<br>';
        debugInfo += 'Приоритет: ' + $('#priority').val() + '<br>';
        debugInfo += 'Статус: ' + $('#status').val() + '<br>';
        debugInfo += 'ID автора: ' + $('#author-id').val() + '<br>';
        debugInfo += 'ID ответственного: ' + $('#manager-id').val() + '<br>';
        debugInfo += 'ID проекта: ' + $('#project-id').val() + '<br>';

        $('#debug-info').html(debugInfo);
    }

    $('#task-form').on('submit', function (e) {
        e.preventDefault();

        updateDebugInfo();

        let isValid = true;
        $('#task-form input[required], #task-form select[required], #task-form textarea[required]').each(function () {
            if ($(this).val() === '') {
                isValid = false;
                $(this).css('border-color', 'red');
            } else {
                $(this).css('border-color', '#ddd');
            }
        });

        if (!isValid) {
            alert('PLEASE, FILL ALL THE FIELDS');
            return;
        }

        let formData = {
            name: $('#task-name').val(),
            description: $('#task-description').val(),
            priority: Number($('#priority').val()),
            status: Number($('#status').val()),
            authorId: Number($('#author-id').val()),
            managerId: Number($('#manager-id').val()),
            projectId: Number($('#project-id').val())
        };

        console.log('SENDING DATA:', formData);

        $.ajax({
            url: 'http://localhost:5054/tasks/create',
            method: 'POST',
            data: JSON.stringify(formData),
            contentType: 'application/json',
            complete: function (xhr, status) {
                if (xhr.status >= 200 && xhr.status < 300) {
                    alert('TASK CREATED SUCCESSFULLY');
                    window.location.href = 'index.html';
                } else {
                    console.error('ERROR DETAILS;', xhr.responseText);
                    alert('ERROR OCCURED');
                }
            }
        });
    });
});