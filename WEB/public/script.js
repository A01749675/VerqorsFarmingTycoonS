function toggleForms(fromForgotPassword = false) {
    var registerForm = document.getElementById("register-form");
    var loginForm = document.getElementById("login-form");
    var forgotPasswordForm = document.getElementById("forgot-password-form");

    if (fromForgotPassword) {
        registerForm.classList.add("hidden");
        forgotPasswordForm.classList.add("hidden");
        loginForm.classList.remove("hidden");
    } else {
        registerForm.classList.toggle("hidden");
        loginForm.classList.toggle("hidden");
        forgotPasswordForm.classList.add("hidden");
    }
}

function showForgotPasswordForm() {
    var loginForm = document.getElementById("login-form");
    var registerForm = document.getElementById("register-form");
    var forgotPasswordForm = document.getElementById("forgot-password-form");

    loginForm.classList.add("hidden");
    registerForm.classList.add("hidden");
    forgotPasswordForm.classList.remove("hidden");
}
