// Site-wide JavaScript functionality

// Initialize tooltips
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

// Auto-dismiss alerts
setTimeout(function () {
    var alerts = document.querySelectorAll('.alert');
    alerts.forEach(function (alert) {
        var bsAlert = new bootstrap.Alert(alert);
        bsAlert.close();
    });
}, 5000);

// Global form validation
function validateNumberInput(input, min, max) {
    const value = parseFloat(input.value);
    if (isNaN(value) || value < min || value > max) {
        input.classList.add('is-invalid');
        return false;
    } else {
        input.classList.remove('is-invalid');
        return true;
    }
}

// File upload validation
function validateFileUpload(fileInput, maxSizeMB = 5) {
    const files = fileInput.files;
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const fileSizeMB = file.size / 1024 / 1024;

        if (fileSizeMB > maxSizeMB) {
            alert(`File "${file.name}" exceeds ${maxSizeMB}MB limit.`);
            fileInput.value = '';
            return false;
        }
    }
    return true;
}

// Format currency
function formatCurrency(amount) {
    return 'R ' + parseFloat(amount).toLocaleString('en-ZA', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}

// Calculate claim totals
function calculateClaimTotal(hours, rate) {
    return hours * rate;
}

// Export functions to global scope
window.CMCS = {
    validateNumberInput,
    validateFileUpload,
    formatCurrency,
    calculateClaimTotal
};