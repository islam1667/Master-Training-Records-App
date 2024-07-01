namespace MasterTrainingRecordsApp
{
    internal class MemberInfo
    {
        // Properties for Course Managing information
        public string Trainee { get; set; }
        public string Position { get; set; }
        public string Course { get; set; }
        public string Manager { get; set; }

        public MemberInfo()
        {
            Trainee = string.Empty;
            Position = string.Empty;
            Course = string.Empty;
            Manager = string.Empty;
        }
    }
}
