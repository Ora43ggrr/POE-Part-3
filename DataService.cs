using POE_Part_3.Models;
using System.Linq.Expressions;

namespace POE_Part_3.Services
{
    public class DataService
    {
        // In-memory data storage (replace with database in production)
        public List<User> Users { get; set; } = new List<User>();
        public List<Claim> Claims { get; set; } = new List<Claim>();
        public List<Document> Documents { get; set; } = new List<Document>();

        public DataService()
        {
            // Initialize with sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Clear existing data to avoid duplicates
            Users.Clear();
            Claims.Clear();
            Documents.Clear();

            // Add sample users with different roles including HR
            Users.Add(new User
            {
                Id = 1,
                Username = "Dr. Sarah Johnson",
                Email = "lecturer@university.com",
                Password = "password123",
                Role = "Lecturer"
            });

            Users.Add(new User
            {
                Id = 2,
                Username = "Prof. Michael Brown",
                Email = "coordinator@university.com",
                Password = "password123",
                Role = "ProgrammeCoordinator"
            });

            Users.Add(new User
            {
                Id = 3,
                Username = "Dr. Emily Wilson",
                Email = "manager@university.com",
                Password = "password123",
                Role = "AcademicManager"
            });

            // Add HR user
            Users.Add(new User
            {
                Id = 4,
                Username = "HR Manager",
                Email = "hr@university.com",
                Password = "password123",
                Role = "HR"
            });

            Users.Add(new User
            {
                Id = 5,
                Username = "Dr. Robert Davis",
                Email = "robert@university.com",
                Password = "password123",
                Role = "Lecturer"
            });

            Users.Add(new User
            {
                Id = 6,
                Username = "Prof. Jennifer Lee",
                Email = "jennifer@university.com",
                Password = "password123",
                Role = "Lecturer"
            });

            // Add sample claims with enhanced automation features
            var claim1 = new Claim
            {
                Id = 1,
                ClaimId = "CLM-2025-001",
                LecturerName = "Dr. Sarah Johnson",
                Month = 9,
                Year = 2025,
                HoursWorked = 45,
                HourlyRate = 350,
                Description = "September teaching hours - Advanced Mathematics",
                Status = "Submitted",
                SubmissionDate = DateTime.Now.AddDays(-1)
            };
            claim1.CalculateAmounts();
            Claims.Add(claim1);

            var claim2 = new Claim
            {
                Id = 2,
                ClaimId = "CLM-2025-002",
                LecturerName = "Dr. Sarah Johnson",
                Month = 8,
                Year = 2025,
                HoursWorked = 40,
                HourlyRate = 350,
                Description = "August teaching hours - Statistics",
                Status = "Approved",
                SubmissionDate = DateTime.Now.AddDays(-10),
                ApprovalDate = DateTime.Now.AddDays(-8),
                ApprovedBy = "Prof. Michael Brown"
            };
            claim2.CalculateAmounts();
            Claims.Add(claim2);

            // Add more sample claims with automation features
            var claim3 = new Claim
            {
                Id = 3,
                ClaimId = "CLM-2025-003",
                LecturerName = "Dr. Robert Davis",
                Month = 9,
                Year = 2025,
                HoursWorked = 42,
                HourlyRate = 380,
                Description = "September teaching hours - Computer Science",
                Status = "Approved",
                SubmissionDate = DateTime.Now.AddDays(-2),
                ApprovalDate = DateTime.Now.AddDays(-1),
                ApprovedBy = "System Auto-Approval"
            };
            claim3.CalculateAmounts();
            Claims.Add(claim3);

            // Add a claim that's approved but not paid (for HR testing)
            var claim4 = new Claim
            {
                Id = 4,
                ClaimId = "CLM-2025-004",
                LecturerName = "Prof. Jennifer Lee",
                Month = 9,
                Year = 2025,
                HoursWorked = 35,
                HourlyRate = 400,
                Description = "September teaching hours - Business Management",
                Status = "Approved",
                SubmissionDate = DateTime.Now.AddDays(-3),
                ApprovalDate = DateTime.Now.AddDays(-1),
                ApprovedBy = "Dr. Emily Wilson"
            };
            claim4.CalculateAmounts();
            Claims.Add(claim4);

            // Add sample documents
            Documents.Add(new Document
            {
                Id = 1,
                FileName = "September_Timesheet.pdf",
                StoredFileName = "doc1.pdf",
                DocumentType = "Supporting Document",
                Description = "Supporting document for CLM-2025-001",
                FileSize = 2400000,
                UploadDate = DateTime.Now.AddDays(-2),
                ClaimId = 1
            });

            Documents.Add(new Document
            {
                Id = 2,
                FileName = "Qualification_Certificate.jpg",
                StoredFileName = "doc2.jpg",
                DocumentType = "Qualification",
                Description = "PhD Qualification Certificate",
                FileSize = 1800000,
                UploadDate = DateTime.Now.AddDays(-5)
            });
        }

        // AUTOMATION: Advanced claim validation and processing
        public (bool isValid, List<string> errors) ValidateClaimAutomation(Claim claim)
        {
            var errors = new List<string>();

            // Automated policy checks
            if (claim.HoursWorked > 200)
                errors.Add("Hours worked cannot exceed 200 per month");

            if (claim.HourlyRate > 1000)
                errors.Add("Hourly rate cannot exceed R1000");

            if (claim.TotalAmount > 50000)
                errors.Add("Total claim amount cannot exceed R50,000");

            // Check for duplicate claims in same month
            var duplicateClaims = Claims.Any(c =>
                c.LecturerName == claim.LecturerName &&
                c.Month == claim.Month &&
                c.Year == claim.Year &&
                c.Id != claim.Id);

            if (duplicateClaims)
                errors.Add("A claim for this month already exists");

            return (!errors.Any(), errors);
        }

        // AUTOMATION: Automated approval workflow
        public bool ProcessAutomatedApproval(Claim claim)
        {
            // Auto-approve claims under certain conditions
            if (claim.TotalAmount <= 5000 && claim.IsWithinPolicyLimits())
            {
                claim.Status = "Approved";
                claim.ApprovalDate = DateTime.Now;
                claim.ApprovedBy = "System Auto-Approval";
                return true;
            }

            return false;
        }

        // AUTOMATION: HR Payment processing
        public string ProcessPayment(Claim claim)
        {
            if (claim.Status == "Approved")
            {
                claim.Status = "Paid";
                claim.PaymentDate = DateTime.Now;
                claim.PaymentReference = $"PAY-{DateTime.Now:yyyyMMdd}-{claim.ClaimId}";

                // Generate automated payment report
                GeneratePaymentReport(claim);

                return claim.PaymentReference;
            }
            return null;
        }

        // AUTOMATION: Generate payment reports
        private void GeneratePaymentReport(Claim claim)
        {
            // In a real system, this would generate PDF reports
            // For now, we'll log the report generation
            Console.WriteLine($"Payment Report Generated for {claim.ClaimId}");
            Console.WriteLine($"Lecturer: {claim.LecturerName}");
            Console.WriteLine($"Amount: R{claim.TotalAmount:N2}");
            Console.WriteLine($"Payment Reference: {claim.PaymentReference}");
        }

        // AUTOMATION: Bulk operations for HR
        public List<Claim> GetClaimsForPaymentProcessing()
        {
            return Claims.Where(c => c.Status == "Approved" && c.PaymentDate == null).ToList();
        }

        public decimal CalculateMonthlyTotal(string lecturerName, int month, int year)
        {
            return Claims
                .Where(c => c.LecturerName == lecturerName && c.Month == month && c.Year == year)
                .Sum(c => c.TotalAmount);
        }

        // AUTOMATION: Advanced reporting
        public object GetMonthlyReport(int month, int year)
        {
            var monthlyClaims = Claims.Where(c => c.Month == month && c.Year == year).ToList();

            return new
            {
                TotalClaims = monthlyClaims.Count,
                TotalAmount = monthlyClaims.Sum(c => c.TotalAmount),
                ApprovedClaims = monthlyClaims.Count(c => c.Status == "Approved"),
                PendingClaims = monthlyClaims.Count(c => c.Status == "Submitted"),
                PaidClaims = monthlyClaims.Count(c => c.Status == "Paid"),
                ClaimsByStatus = monthlyClaims.GroupBy(c => c.Status)
                    .ToDictionary(g => g.Key, g => g.Count())
            };
        }

        // AUTOMATION: Lecturer data management for HR
        public List<User> GetLecturersWithActiveClaims()
        {
            var lecturerNames = Claims.Select(c => c.LecturerName).Distinct();
            return Users.Where(u => lecturerNames.Contains(u.Username)).ToList();
        }

        // AUTOMATION: Notification system
        public void CreateNotification(string userId, string message, string type)
        {
            // In a real system, this would save to a notifications table
            Console.WriteLine($"Notification for {userId}: {message}");
        }

        // Existing helper methods...
        public User GetUserById(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return Users.FirstOrDefault(u => u.Email == email);
        }

        public List<Claim> GetClaimsByLecturer(string lecturerName)
        {
            return Claims.Where(c => c.LecturerName == lecturerName).ToList();
        }

        public List<Claim> GetPendingClaims()
        {
            return Claims.Where(c => c.Status == "Submitted").ToList();
        }

        public List<Claim> GetClaimsByStatus(string status)
        {
            return Claims.Where(c => c.Status == status).ToList();
        }

        public Claim GetClaimById(int id)
        {
            return Claims.FirstOrDefault(c => c.Id == id);
        }

        public bool UpdateClaimStatus(int claimId, string status)
        {
            var claim = GetClaimById(claimId);
            if (claim != null)
            {
                claim.Status = status;
                return true;
            }
            return false;
        }

        public void AddDocument(Document document)
        {
            document.Id = Documents.Count + 1;
            Documents.Add(document);
        }

        public List<Document> GetDocumentsByType(string documentType)
        {
            return Documents.Where(d => d.DocumentType == documentType).ToList();
        }

        public List<Document> GetDocumentsByClaimId(int claimId)
        {
            return Documents.Where(d => d.ClaimId == claimId).ToList();
        }

        public int GetPendingClaimsCount()
        {
            return Claims.Count(c => c.Status == "Submitted");
        }

        public decimal GetTotalApprovedAmount()
        {
            return Claims.Where(c => c.Status == "Approved" || c.Status == "Paid")
                        .Sum(c => c.TotalAmount);
        }

        public int GetTotalClaimsCount()
        {
            return Claims.Count;
        }

        public int GetClaimsCountByStatus(string status)
        {
            return Claims.Count(c => c.Status == status);
        }

        public List<Claim> GetRecentClaims(int count = 5)
        {
            return Claims.OrderByDescending(c => c.SubmissionDate)
                        .Take(count)
                        .ToList();
        }

        public string GenerateNextClaimId(int year)
        {
            var nextNumber = Claims.Count + 1;
            return $"CLM-{year}-{nextNumber.ToString("D3")}";
        }

        public bool ValidateFileUpload(IFormFile file, out string errorMessage)
        {
            errorMessage = null;

            if (file == null || file.Length == 0)
            {
                errorMessage = "Please select a file to upload";
                return false;
            }

            if (file.Length > 5 * 1024 * 1024)
            {
                errorMessage = "File size exceeds 5MB limit";
                return false;
            }

            var allowedExtensions = new[] { ".pdf", ".jpg", ".png", ".docx", ".xlsx" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                errorMessage = "File type not supported. Please upload PDF, JPG, PNG, DOCX, or XLSX files.";
                return false;
            }

            return true;
        }
    }
}