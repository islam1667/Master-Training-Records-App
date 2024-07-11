using System;
using System.ComponentModel;

namespace MasterTrainingRecordsApp
{
    public class TrainingRecord
    {
        public TrainingRecord(string reference, string task, string category, string type, string startTime, string endTime, string trainerInitials, string certifierInitials, int? certifierScore, int? requiredScore, int? scoreCategory1, int? scoreCategory2, int? scoreCategory3, int? scoreCategory4)
        {
            Reference = reference ?? default;
            Task = task ?? default;
            Category = category ?? default;
            Type = type ?? default;
            StartTime = startTime ?? default;
            EndTime = endTime ?? default;
            TrainerInitials = trainerInitials ?? default;
            CertifierInitials = certifierInitials ?? default;
            CertifierScore = certifierScore;
            RequiredScore = requiredScore;
            ScoreCategory1 = scoreCategory1;
            ScoreCategory2 = scoreCategory2;
            ScoreCategory3 = scoreCategory3;
            ScoreCategory4 = scoreCategory4;
        }

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

        // This will be set to ScoreCategory1 by default
        [DisplayName("Required Score")]
        public int? RequiredScore { get; set; }

        [Browsable(false)]
        public int? ScoreCategory1 { get; set; }

        [Browsable(false)]
        public int? ScoreCategory2 { get; set; }

        [Browsable(false)]
        public int? ScoreCategory3 { get; set; }

        [Browsable(false)]
        public int? ScoreCategory4 { get; set; }

        
        // Default constructor initializes properties with default values
        //public TrainingRecord()
        //{
        //    this.Reference = default;
        //    this.Task = default;
        //    this.Category = default;
        //    this.Type = default;
        //    this.StartTime = default;
        //    this.EndTime = default;
        //    this.TrainerInitials = default;
        //    this.CertifierInitials = default;
        //    this.CertifierScore = default;
        //    this.RequiredScore = default;
        //}

        //public TrainingRecord()
        //{
        //    this.Reference = default;
        //    this.Task = default;
        //    this.Category = default;
        //    this.Type = default;
        //    this.StartTime = default;
        //    this.EndTime = default;
        //    this.TrainerInitials = default;
        //    this.CertifierInitials = default;
        //    this.CertifierScore = default;
        //    this.RequiredScore = default;
        //    this.ScoreCategory1 = default;
        //    this.ScoreCategory2 = default;
        //    this.ScoreCategory3 = default;
        //    this.ScoreCategory4 = default;
        //}



        // Method to create a copy of the TrainingRecord object
        public TrainingRecord Clone()
        {
            return new TrainingRecord(
                reference: this.Reference,
                task: this.Task,
                category: this.Category,
                type: this.Type,
                startTime: this.StartTime,
                endTime: this.EndTime,
                trainerInitials: this.TrainerInitials,
                certifierInitials: this.CertifierInitials,
                certifierScore: CertifierScore,
                requiredScore: RequiredScore,
                scoreCategory1: this.ScoreCategory1,
                scoreCategory2: this.ScoreCategory2,
                scoreCategory3: this.ScoreCategory3,
                scoreCategory4: this.ScoreCategory4
            );
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
