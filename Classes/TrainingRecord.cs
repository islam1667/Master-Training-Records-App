using System.ComponentModel;

namespace MasterTrainingRecordsApp
{
    public class TrainingRecord
    {
        // Properties for training record information
        public string Reference { get; set; }
        public string Task { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }

        [DisplayName("Start Time")]
        public string StartTime { get; set; }

        [DisplayName("End Time")]
        public string EndTime { get; set; }

        [DisplayName("Trainer Initials")]
        public string TrainerInitials { get; set; }

        [DisplayName("Certifier Initials")]
        public string CertifierInitials { get; set; }

        [DisplayName("Certifier Score")]
        public int? CertifierScore { get; set; }

        [DisplayName("Required Score")]
        public int? RequiredScore { get; set; }

        // Default constructor initializes properties with default values
        public TrainingRecord()
        {
            this.Reference = default;
            this.Task = default;
            this.Category = default;
            this.Type = default;
            this.StartTime = default;
            this.EndTime = default;
            this.TrainerInitials = default;
            this.CertifierInitials = default;
            this.CertifierScore = default;
            this.RequiredScore = default;
        }

        // Parameterized constructor to initialize properties with provided values
        public TrainingRecord(string Reference, string TaskInfo, string CatagoryInfo, string TypeInfo, string StartTimeInfo, string EndTimeInfo, string TrainerInitialsInfo, string CertifierInitialsInfo, int? CertifierScoreInfo, int? RequiredScoreInfo)
        {
            this.Reference = Reference ?? default;
            this.Task = TaskInfo ?? default;
            this.Category = CatagoryInfo ?? default;
            this.Type = TypeInfo ?? default;
            this.StartTime = StartTimeInfo ?? default;
            this.EndTime = EndTimeInfo ?? default;
            this.TrainerInitials = TrainerInitialsInfo ?? default;
            this.CertifierInitials = CertifierInitialsInfo ?? default;
            this.CertifierScore = CertifierScoreInfo ?? default;
            this.RequiredScore = RequiredScoreInfo ?? default;
        }

        // Method to create a copy of the TrainingRecord object
        public TrainingRecord Clone()
        {
            return new TrainingRecord(Reference, Task, Category, Type, StartTime, EndTime, TrainerInitials, CertifierInitials, CertifierScore, RequiredScore);
        }

        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(Reference) &&
                string.IsNullOrEmpty(Task) &&
                string.IsNullOrEmpty(Category) &&
                string.IsNullOrEmpty(Type) &&
                string.IsNullOrEmpty(StartTime) &&
                string.IsNullOrEmpty(EndTime) &&
                string.IsNullOrEmpty(TrainerInitials) &&
                string.IsNullOrEmpty(CertifierInitials)) return true;
            return false;
        }

        public string GetFullName()
        {
            return
                Reference + " | " + Task + " | " + Category + " | " + Type + " | " + StartTime + " | " + EndTime + " | " + TrainerInitials + " | " + CertifierInitials + " | " + CertifierScore + " | " + RequiredScore;
        }
    }
}
