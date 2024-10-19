
//$(document).ready(function () {

//    function showError(input, message) {
//        $(input).next('.error-message').text(message);
//        $(input).addClass('error');
//    }

//    function removeError(input) {
//        $(input).next('.error-message').text('');
//        $(input).removeClass('error');
//    }

//    $('#fullName').on('blur', function () {
//        let fullName = $(this).val().trim();
//        if (fullName === '') {
//            showError(this, 'Full name is required');
//        } else if (!/^[a-zA-Z]+$/.test(fname)) {
//            showError(this, 'Full name must contain only letters');
//        } else {
//            removeError(this);
//        }
//    });

   

//    $('form').on('submit', function (event) {
//        let hasError = false;
//        if ($('#fullName').val().trim() === '') {
//            showError('#fullName', 'Full name is required');
//            hasError = true;
//        } else if (!/^[a-zA-Z]+$/.test($('#fullName').val())) {
//            showError('#fullName', 'Full name must contain only letters');
//            hasError = true;
//        } else {
//            removeError('#fullName');
//        }
        

//        if (hasError) {
//            event.preventDefault();
//        }
//    });
//});

//$(document).ready(function () {
//    function showError(input, message) {
//        $(input).next('.error-message').text(message);
//        $(input).addClass('error');
//    }
//    function removeError(input) {
//        $(input).next('.error-message').text('');
//        $(input).removeClass('error');
//    }
//    $('#fullName').on('input', function () {
//        let fullName = $(this).val();
//        if (fullName.trim() === '') {
//            showError(this, 'Full name is required');
//        } else if (!/^[a-zA-Z]+$/.test(fullName)) {
//            showError(this, 'Full name must contain only letters');
//        } else {
//            removeError(this);
//        }
//    });

//    $('#dateOfBirth').on('change', function () {
//        let dateOfBirth = $(this).val();
//        if (!dateOfBirth) {
//            showError(this, 'Date of birth is required');
//        } else {
//            removeError(this);
//        }
//    });
//    $('input[name="gender"]').on('change', function () {
//        if ($('input[name="gender"]:checked').length === 0) {
//            showError('#gender', 'Gender is required');
//        } else {
//            removeError('#gender');
//        }
//    });

//    $('#phoneNumber').on('input', function () {
//        let phoneNumber = $(this).val();
//        let phonePattern = /^\+1\s\(\d{3}\)\s\d{3}-\d{4}$/;
//        if (!phonePattern.test(phoneNumber)) {
//            showError(this, 'Please enter a valid phone number in the format +1 (555) 123-4567');
//        } else {
//            removeError(this);
//        }
//    });

//    // Validate Email
//    $('#email').on('input', function () {
//        let email = $(this).val();
//        let emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
//        if (!emailPattern.test(email)) {
//            showError(this, 'Please enter a valid email address');
//        } else {
//            removeError(this);
//        }
//    });
//    $('#address').on('input', function () {
//        let address = $(this).val();
//        if (address.trim() === '') {
//            showError(this, 'Address is required');
//        } else {
//            removeError(this);
//        }
//    });

//    $('#state').on('change', function () {
//        if ($(this).val() === '') {
//            showError(this, 'Please select a state');
//        } else {
//            removeError(this);
//        }
//    });

//    $('#city').on('change', function () {
//        if ($(this).val() === '') {
//            showError(this, 'Please select a city');
//        } else {
//            removeError(this);
//        }
//    });
//    $(document).ready(function () {

//        $('#userName').on('input', function () {
//            let userName = $(this).val();
//            if (userName === '') {
//                $('#userName-error').text('Username is required');
//            } else {
//                $('#userName-error').text('');
//            }
//        });

//        $('#password').on('input', function () {
//            let password = $(this).val();
//            if (password === '') {
//                $('#password-error').text('Password is required');
//            } else {
//                $('#password-error').text('');
//            }

//            validateConfirmPassword();
//        });

//        $('#CPassword').on('input', function () {
//            validateConfirmPassword();
//        });

//        function validateConfirmPassword() {
//            let password = $('#password').val();
//            let CPassword = $('#CPassword').val();
//            if (CPassword === '') {
//                $('#CPassword-error').text('Please confirm your password');
//            } else if (password !== CPassword) {
//                $('#CPassword-error').text('Passwords do not match');
//            } else {
//                $('#CPassword-error').text('');
//            }
//        }

//        $('form').on('submit', function (event) {
//            let hasError = false;


//            if ($('#userName').val() === '') {
//                $('#userName-error').text('Username is required');
//                hasError = true;
//            }

//            if ($('#password').val() === '') {
//                $('#password-error').text('Password is required');
//                hasError = true;
//            }

//            if ($('#CPassword').val() === '') {
//                $('#CPassword-error').text('Please confirm your password');
//                hasError = true;
//            } else if ($('#password').val() !== $('#CPassword').val()) {
//                $('#CPassword-error').text('Passwords do not match');
//                hasError = true;
//            }


//            if (hasError) {
//                event.preventDefault();
//            }
//        });
//    });


//    $('form').on('submit', function (event) {
//        let hasError = false;
//        $('input, select').each(function () {
//            $(this).trigger('input');
//            $(this).trigger('change');
//        });

//        $('.error-message:visible').each(function () {
//            if ($(this).text() !== '') {
//                hasError = true;
//            }
//        });

//        if (hasError) {
//            event.preventDefault();
//            $('#global-error').text('Please correct the errors in the form.');
//        } else {
//            $('#global-error').text('');
//        }
//    });
//});


function validateDOB() {
    var todayDate = new Date();
    var selectedDate = new Date(document.getElementById("date").value);
    var year = todayDate.getFullYear();
    var month = todayDate.getMonth() + 1;
    var tdate = todayDate.getDate();

    // Format month and date to have leading zeros if necessary
    if (month < 10) {
        month = "0" + month;
    }
    if (tdate < 10) {
        tdate = "0" + tdate;
    }

    var maxDate = year + "-" + month + "-" + tdate;

    // Set max attribute to today's date
    document.getElementById("date").setAttribute("max", maxDate);

    // Calculate the minimum valid date (18 years ago)
    var minDate = new Date();
    minDate.setFullYear(todayDate.getFullYear() - 18);
    minDate.setMonth(todayDate.getMonth());
    minDate.setDate(todayDate.getDate());
    minDate.setHours(0, 0, 0, 0); // Ensure time is set to start of the day

    // Display an error if the selected date is less than 18 years ago or in the future
    if (selectedDate > todayDate) {
        document.getElementById("message").textContent = "Date of Birth cannot be in the future.";
        document.getElementById("date").value = ""; // Clear the input
    } else if (selectedDate > minDate) {
        document.getElementById("message").textContent = "You must be at least 18 years old.";
        document.getElementById("date").value = ""; // Clear the input
    } else {
        document.getElementById("message").textContent = ""; // Clear any error message
    }
}

