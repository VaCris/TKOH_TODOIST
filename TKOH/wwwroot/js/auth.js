const PasswordModule = (function () {
    function initPasswordToggle() {
        const toggleButtons = document.querySelectorAll('.auth-password-toggle');

        toggleButtons.forEach(button => {
            button.addEventListener('click', function () {
                const passwordInput = this.parentElement.querySelector('input');
                const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
                passwordInput.setAttribute('type', type);
                this.textContent = type === 'password' ? 'SHOW' : 'HIDE';
            });
        });
    }

    function validatePasswordStrength(password) {
        return password.length >= 8;
    }

    return {
        init: initPasswordToggle,
        validateStrength: validatePasswordStrength
    };
})();


const loginForm = document.getElementById('loginForm');
if (loginForm) {
    loginForm.addEventListener('submit', function (e) {
        const email = document.querySelector('input[name="Email"]');
        const password = document.querySelector('input[name="Password"]');

        let valid = true;

        if (!email.value || !FormValidationModule.validateEmail(email.value)) {
            e.preventDefault();
            FormValidationModule.showError(email, 'Introduce un email válido');
            valid = false;
        }

        if (!password.value) {
            e.preventDefault();
            FormValidationModule.showError(password, 'La contraseña es obligatoria');
            valid = false;
        }

        if (!valid) return false;
    });
}

const FormValidationModule = (function () {
    function validateEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }

    function validatePasswordMatch(password, confirmPassword) {
        return password === confirmPassword;
    }

    function showError(field, message) {
        let errorElement = field.parentElement.querySelector('.auth-text-danger');
        if (!errorElement) {
            errorElement = document.createElement('span');
            errorElement.className = 'auth-text-danger';
            field.parentElement.appendChild(errorElement);
        }
        errorElement.textContent = message;
        field.style.borderColor = '#dc3545';
    }

    function clearError(field) {
        const errorElement = field.parentElement.querySelector('.auth-text-danger');
        if (errorElement) {
            errorElement.textContent = '';
        }
        field.style.borderColor = '#ddd';
    }

    function initRealTimeValidation() {
        const emailFields = document.querySelectorAll('input[type="email"]');
        const passwordFields = document.querySelectorAll('input[type="password"]');

        emailFields.forEach(field => {
            field.addEventListener('blur', function () {
                if (this.value && !validateEmail(this.value)) {
                    showError(this, 'Por favor, introduce un email válido');
                } else {
                    clearError(this);
                }
            });
        });

        passwordFields.forEach(field => {
            field.addEventListener('input', function () {
                clearError(this);
            });
        });
    }

    return {
        init: initRealTimeValidation,
        validateEmail: validateEmail,
        validatePasswordMatch: validatePasswordMatch,
        showError: showError,
        clearError: clearError
    };
})();

const AuthModule = (function () {
    function init() {
        PasswordModule.init();
        FormValidationModule.init();

        const registerForm = document.getElementById('registerForm');
        if (registerForm) {
            registerForm.addEventListener('submit', function (e) {
                const password = document.querySelector('input[name="Password"]');
                const confirmPassword = document.querySelector('input[name="ConfirmPassword"]');

                if (password && confirmPassword && password.value !== confirmPassword.value) {
                    e.preventDefault();
                    FormValidationModule.showError(confirmPassword, 'Las contraseñas no coinciden');
                    return false;
                }
            });
        }

        console.log('Auth module initialized successfully');
    }

    return {
        init: init
    };
})();

document.addEventListener('DOMContentLoaded', function () {
    AuthModule.init();
});

