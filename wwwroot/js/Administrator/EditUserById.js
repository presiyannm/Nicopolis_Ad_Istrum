document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");

    if (form) {
        form.addEventListener("submit", function (event) {
            // Prevent submission by default
            event.preventDefault();
            let isValid = true;

            // Clear existing error messages
            const errorMessages = form.querySelectorAll(".error-message");
            errorMessages.forEach(msg => msg.remove());

            // Validate fields
            const fields = [
                { id: "FirstName", message: "Полето е задължително" },
                { id: "LastName", message: "Полето е задължително" },
                { id: "Email", message: "Полето е задължително" },
                { name: "SelectedRoles", message: "Полето е задължително" }
            ];

            fields.forEach(field => {
                const input = field.id ? document.getElementById(field.id) : form.querySelector(`select[name='${field.name}']`);
                if (input && input.value.trim() === "") {
                    isValid = false;

                    // Create error message
                    const errorMessage = document.createElement("span");
                    errorMessage.className = "error-message";
                    errorMessage.style.color = "red";
                    errorMessage.style.fontSize = "12px";
                    errorMessage.textContent = field.message;

                    // Append the error message
                    input.insertAdjacentElement("afterend", errorMessage);
                }
            });

            // Submit the form if all fields are valid
            if (isValid) {
                form.submit();
            }
        });
    }
});
