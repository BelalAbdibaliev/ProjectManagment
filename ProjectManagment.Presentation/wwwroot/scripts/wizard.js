$(document).ready(function () {
    let uploadedFiles = [];

    $('.next').click(function () {
        let currentStep = $(this).closest('.step');
        let nextStep = currentStep.next('.step');
        let inputs = currentStep.find('input[required], select[required]');
        let valid = true;

        inputs.each(function () {
            if ($(this).val() === '') {
                valid = false;
                $(this).css('border-color', 'red');
            } else {
                $(this).css('border-color', '#ddd');
            }
        });

        if (valid) {
            currentStep.removeClass('active');
            nextStep.addClass('active');
            let stepIndex = $('.step').index(nextStep);
            $('.progress-step').removeClass('active');
            $('.progress-step').eq(stepIndex).addClass('active');
        }
    });

    $('.prev').click(function () {
        let currentStep = $(this).closest('.step');
        let prevStep = currentStep.prev('.step');
        currentStep.removeClass('active');
        prevStep.addClass('active');
        let stepIndex = $('.step').index(prevStep);
        $('.progress-step').removeClass('active');
        $('.progress-step').eq(stepIndex).addClass('active');
    });

    const dropzone = document.getElementById('dropzone');
    const fileInput = document.getElementById('file-input');

    dropzone.addEventListener('click', () => {
        fileInput.click();
    });

    fileInput.addEventListener('change', (e) => {
        handleFiles(e.target.files);
    });

    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dropzone.addEventListener(eventName, preventDefaults, false);
    });

    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    ['dragenter', 'dragover'].forEach(eventName => {
        dropzone.addEventListener(eventName, highlight, false);
    });

    ['dragleave', 'drop'].forEach(eventName => {
        dropzone.addEventListener(eventName, unhighlight, false);
    });

    function highlight() {
        dropzone.classList.add('highlight');
    }

    function unhighlight() {
        dropzone.classList.remove('highlight');
    }

    dropzone.addEventListener('drop', (e) => {
        const dt = e.dataTransfer;
        const files = dt.files;
        handleFiles(files);
    });

    function handleFiles(files) {
        files = [...files];
        files.forEach(uploadFile);
        renderFileList();
    }

    function uploadFile(file) {
        uploadedFiles.push(file);
    }

    function renderFileList() {
        const fileList = document.getElementById('file-list');
        fileList.innerHTML = '';

        uploadedFiles.forEach((file, index) => {
            const fileItem = document.createElement('div');
            fileItem.className = 'file-item';

            const fileName = document.createElement('div');
            fileName.textContent = file.name;

            const removeBtn = document.createElement('button');
            removeBtn.textContent = 'Remove';
            removeBtn.addEventListener('click', () => {
                uploadedFiles.splice(index, 1);
                renderFileList();
            });

            fileItem.appendChild(fileName);
            fileItem.appendChild(removeBtn);
            fileList.appendChild(fileItem);
        });
    }

    $('.next').on('click', function () {
        if ($(this).closest('.step').attr('id') === 'step4') {
            updateDebugInfo();
        }
    });

    function updateDebugInfo() {
        let debugInfo = 'Current form values:<br>';
        debugInfo += 'Project Name: ' + $('#project-name').val() + '<br>';
        debugInfo += 'Start Date: ' + $('#start-date').val() + '<br>';
        debugInfo += 'End Date: ' + $('#end-date').val() + '<br>';
        debugInfo += 'Priority: ' + $('#priority').val() + '<br>';
        debugInfo += 'Client ID: ' + $('#client-id').val() + '<br>';
        debugInfo += 'Supplier ID: ' + $('#supplier-id').val() + '<br>';
        debugInfo += 'Project Manager ID: ' + $('#project-manager').val() + '<br>';
        debugInfo += 'Employee ID: ' + $('#employee-id').val() + '<br>';

        $('#debug-info').html(debugInfo);
    }

    $('#project-wizard').on('submit', function (e) {
        e.preventDefault();
        updateDebugInfo();
        let startDate = new Date($('#start-date').val());
        let endDate = new Date($('#end-date').val());
        let formData = {
            name: $('#project-name').val(),
            priority: Number($('#priority').val()),
            clientId: Number($('#client-id').val()),
            supplierId: Number($('#supplier-id').val()),
            projectLeadId: Number($('#project-manager').val()),
            employeeId: Number($('#employee-id').val()),
            startDate: startDate.toISOString(),
            endDate: endDate.toISOString()
        };

        console.log('Submitting data:', formData);

        $.ajax({
            url: "http://localhost:5054/projects/create",
            method: "POST",
            data: JSON.stringify(formData),
            contentType: "application/json",
            complete: function (xhr, status) {
                if (xhr.status >= 200 && xhr.status < 300) {
                    alert('Задача успешно создана!');
                    window.location.href = 'index.html';
                } else {
                    console.error('Детали ошибки:', xhr.responseText);
                    alert('Ошибка при создании задачи. Пожалуйста, попробуйте снова.');
                }
            }
        });

        if (uploadedFiles.length > 0) {
            let fileFormData = new FormData();
            uploadedFiles.forEach(file => {
                fileFormData.append('files[]', file);
            });
            fileFormData.append('projectName', $('#project-name').val());

            $.ajax({
                url: 'upload-files.php',
                method: 'POST',
                data: fileFormData,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log('Files uploaded successfully');
                },
                error: function (xhr, status, error) {
                    console.error('Error uploading files:', xhr.responseText);
                }
            });
        }
    });
});