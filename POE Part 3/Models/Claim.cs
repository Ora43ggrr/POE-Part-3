namespace POE_Part_3.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string ClaimId { get; set; }
        public string LecturerName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectionReason { get; set; }
        public string PaymentReference { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsUrgent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public List<string> SupportingDocuments { get; set; } = new List<string>();

        // Automated calculation methods
        public void CalculateAmounts()
        {
            TotalAmount = HoursWorked * HourlyRate;
            TaxAmount = TotalAmount * 0.15m; // 15% tax
            NetAmount = TotalAmount - TaxAmount;
        }

        public bool IsWithinPolicyLimits()
        {
            return HoursWorked <= 200 && HourlyRate <= 1000 && TotalAmount <= 50000;
        }

        public string GetPaymentStatus()
        {
            return Status == "Paid" ? "Paid" : "Pending";
        }
    }
}