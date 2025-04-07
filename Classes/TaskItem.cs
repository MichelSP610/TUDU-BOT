namespace TUDU_BOT.Classes
{
    public class TaskItem
    {

        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem() { }

        public TaskItem(string Description)
        {
            this.Description = Description;
            this.IsCompleted = false;
        }

        public void MarkAsCompleted()
        {
            this.IsCompleted = true;
        }

        public void MarkAsIncompleted()
        {
            this.IsCompleted = false;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "[X]" : "[ ]";
            return $"{status} {Description}";
        }

    }
}
