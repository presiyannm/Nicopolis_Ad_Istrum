document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const inputs = form.querySelectorAll("input, textarea, select");
    const errorClass = "is-invalid"; // Bootstrap's error styling class

    form.addEventListener("submit", function (event) {
        let isValid = true;

        inputs.forEach((input) => {
            const errorSpan = input.nextElementSibling; // Get the validation span
            const value = input.value.trim();

            // Check for empty or null fields
            if (!value || value === "") {
                isValid = false;
                input.classList.add(errorClass); // Add error styling
                if (errorSpan && errorSpan.classList.contains("text-danger")) {
                    errorSpan.textContent = "Това поле е задължително.";
                }
            } else {
                input.classList.remove(errorClass); // Remove error styling
                if (errorSpan && errorSpan.classList.contains("text-danger")) {
                    errorSpan.textContent = "";
                }
            }
        });

        // If any field is invalid, prevent form submission
        if (!isValid) {
            event.preventDefault();
        }
    });

    // Remove error styling on input change
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
