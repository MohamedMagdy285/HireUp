// Wait for DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function () {
    console.log('Login script loaded');

    // Password Toggle Function
    function togglePassword(fieldId) {
        const field = document.getElementById(fieldId);
        if (!field) return;

        const button = field.parentElement.querySelector('.password-toggle');
        const icon = button.querySelector('i');

        if (field.type === 'password') {
            field.type = 'text';
            icon.classList.remove('fa-eye');
            icon.classList.add('fa-eye-slash');
        } else {
            field.type = 'password';
            icon.classList.remove('fa-eye-slash');
            icon.classList.add('fa-eye');
        }
    }

    // Attach toggle functionality to all password toggle buttons
    document.querySelectorAll('.password-toggle').forEach(button => {
        button.addEventListener('click', function () {
            const fieldId = this.getAttribute('data-field');
            togglePassword(fieldId);
        });
    });

    // Validation Rules
    const validators = {
        Email: {
            validate: (value) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value.trim()),
            message: 'Please enter a valid email address'
        },
        Password: {
            validate: (value) => value.trim().length > 0,
            message: 'Password is required'
        }
    };

    // Real-time Validation
    Object.keys(validators).forEach(fieldId => {
        const field = document.getElementById(fieldId);
        if (field) {
            field.addEventListener('blur', () => validateField(fieldId));
            field.addEventListener('input', () => {
                if (field.classList.contains('is-invalid')) {
                    validateField(fieldId);
                }
            });
        }
    });

    function validateField(fieldId) {
        const field = document.getElementById(fieldId);
        if (!field) return false;

        const errorSpan = document.getElementById(fieldId + '-error');
        const value = field.value.trim();

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
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        console.log('Login form found, attaching submit handler');

        loginForm.addEventListener('submit', function (e) {
            console.log('Form submit triggered');

            let isValid = true;
            Object.keys(validators).forEach(fieldId => {
                if (!validateField(fieldId)) {
                    isValid = false;
                }
            });

            if (!isValid) {
                e.preventDefault();
                console.log('Form validation failed, preventing submission');

                // Show all errors
                Object.keys(validators).forEach(fieldId => {
                    const field = document.getElementById(fieldId);
                    if (field && !field.classList.contains('is-valid')) {
                        validateField(fieldId);
                    }
                });

                // Show alert only if you want
                // alert('Please fix the errors above');
            } else {
                console.log('Form validation passed, submitting...');
                // You can show loading indicator here if needed
                // document.getElementById('loading').style.display = 'block';
            }
        });
    } else {
        console.error('Login form not found!');
    }

    // Social Login
    window.socialLogin = function (provider) {
        console.log(`Social login with ${provider}`);
        // In production, implement OAuth flow
        // window.location.href = `/Account/ExternalLogin?provider=${provider}&returnUrl=${encodeURIComponent(window.location.pathname)}`;
    };

    // Remember Me - Store preference
    const rememberMeCheckbox = document.getElementById('RememberMe');
    if (rememberMeCheckbox) {
        rememberMeCheckbox.addEventListener('change', function () {
            const email = document.getElementById('Email').value.trim();
            if (this.checked && email) {
                localStorage.setItem('rememberedEmail', email);
            } else {
                localStorage.removeItem('rememberedEmail');
            }
        });
    }

    // Load remembered email on page load
    const rememberedEmail = localStorage.getItem('rememberedEmail');
    if (rememberedEmail) {
        const emailField = document.getElementById('Email');
        if (emailField) {
            emailField.value = rememberedEmail;
            if (rememberMeCheckbox) {
                rememberMeCheckbox.checked = true;
            }
        }
    }

    // Enter key to submit
    const passwordField = document.getElementById('Password');
    if (passwordField) {
        passwordField.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                e.preventDefault();
                loginForm.dispatchEvent(new Event('submit'));
            }
        });
    }
});