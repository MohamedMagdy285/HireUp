
        // Password Toggle
        //function togglePassword(fieldId) {
        //    const field = document.getElementById(fieldId);
        //    const button = event.target.closest('.password-toggle');
        //    if (field.type === 'password') {
        //        field.type = 'text';
        //        button.innerHTML = '<i class="fas fa-eye-slash"></i>';
        //    } else {
        //        field.type = 'password';
        //        button.innerHTML = '<i class="fas fa-eye"></i>';
        //    }
        //}

        // Validation Rules
        const validators = {
            NationalNumber: {
                validate: (value) => /^\d{14}$/.test(value),
                message: 'National ID must be exactly 14 digits'
            },
            Email: {
                validate: (value) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value),
                message: 'Please enter a valid email address'
            },
            FirstName: {
                validate: (value) => value.length > 0 && value.length <= 50,
                message: 'First name is required'
            },
            LastName: {
                validate: (value) => value.length > 0 && value.length <= 50,
                message: 'Last name is required'
            },
            Country: {
                validate: (value) => value.length > 0,
                message: 'Please select a country'
            },
            Location: {
                validate: (value) => value.length > 0,
                message: 'City/Location is required'
            },
            Password: {
                validate: (value) => value.length >= 8,
                message: 'Password must be at least 8 characters'
            },
            ConfirmPassword: {
                validate: (value) => {
                    const password = document.getElementById('Password').value;
                    return value === password && value.length > 0;
                },
                message: 'Passwords do not match'
            },
            terms: {
                validate: (value) => document.getElementById('Terms').checked,
                message: 'You must agree to the terms and conditions'
            }
        };

        // Real-time Validation
        Object.keys(validators).forEach(fieldId => {
            const field = document.getElementById(fieldId);
            if (field) {
                field.addEventListener('blur', () => validateField(fieldId));
                field.addEventListener('change', () => validateField(fieldId));
                field.addEventListener('input', () => {
                    if (field.classList.contains('is-invalid')) {
                        validateField(fieldId);
                    }
                });
            }
        });

        function validateField(fieldId) {
            const field = document.getElementById(fieldId);
            const errorSpan = document.getElementById(fieldId + '-error');
            const value = field.type === 'checkbox' ? field.checked : field.value.trim();

            if (validators[fieldId].validate(value)) {
                field.classList.remove('is-invalid');
                field.classList.add('is-valid');
                if (errorSpan) errorSpan.style.display = 'none';
                return true;
            } else {
                field.classList.remove('is-valid');
                field.classList.add('is-invalid');
                if (errorSpan) {
                    errorSpan.textContent = validators[fieldId].message;
                    errorSpan.style.display = 'block';
                }
                return false;
            }
        }

        // Form Submission
        //document.getElementById('jobSeekerForm').addEventListener('submit', function(e) {
        //    e.preventDefault();

        //    let isValid = true;
        //    Object.keys(validators).forEach(fieldId => {
        //        if (!validateField(fieldId)) {
        //            isValid = false;
        //        }
        //    });

        //    if (isValid) {
        //        alert('Registration successful! Redirecting to job listings...');
        //        // In your MVC project, submit the form to your controller
        //        // this.submit();
        //        // Or redirect to next page
        //        // window.location.href = 'jobs-listing.html';
        //    }
        //    //else {
        //    //    alert('Please fix the errors above');
        //    //}
//});
document.getElementById('jobSeekerForm').addEventListener('submit', function (e) {
    let isValid = true;

    Object.keys(validators).forEach(fieldId => {
        if (!validateField(fieldId)) {
            isValid = false;
        }
    });

    if (!isValid) {
        e.preventDefault(); // منع الإرسال لو فيه أخطاء
    //    alert('Please fix the errors above');
    }
    //// لو كل شيء صحيح، الفورم يرسل البيانات للـ controller بشكل طبيعي
});
