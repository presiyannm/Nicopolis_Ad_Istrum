document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const inputs = form.querySelectorAll("input, textarea, select");
    const errorClass = "is-invalid";

    form.addEventListener("submit", function (event) {
        let isValid = true;

        inputs.forEach((input) => {
            const errorSpan = input.nextElementSibling;
            const value = input.value.trim();

            // Check for empty or null fields
            if (!value || value === "") {
                isValid = false;
                input.classList.add(errorClass);
                if (errorSpan && errorSpan.classList.contains("text-danger")) {
                    errorSpan.textContent = "Това поле е задължително.";
                }
            } else {
                input.classList.remove(errorClass);
                if (errorSpan && errorSpan.classList.contains("text-danger")) {
                    errorSpan.textContent = "";
                }
            }
        });

        if (!isValid) {
            event.preventDefault();
        }
    });

    inputs.forEach((input) => {
        input.addEventListener("input", () => {
            if (input.value.trim() !== "") {
                input.classList.remove(errorClass);
                const errorSpan = input.nextElementSibling;
                if (errorSpan && errorSpan.classList.contains("text-danger")) {
                    errorSpan.textContent = "";
                }
            }
        });
    });
});
