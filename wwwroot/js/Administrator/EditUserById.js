document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");

    if (form) {
        form.addEventListener("submit", function (event) {
            event.preventDefault();
            let isValid = true;

            const errorMessages = form.querySelectorAll(".error-message");
            errorMessages.forEach(msg => msg.remove());

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

                    const errorMessage = document.createElement("span");
                    errorMessage.className = "error-message";
                    errorMessage.style.color = "red";
                    errorMessage.style.fontSize = "12px";
                    errorMessage.textContent = field.message;

                    input.insertAdjacentElement("afterend", errorMessage);
                }
            });

            if (isValid) {
                form.submit();
            }
        });
    }
});
