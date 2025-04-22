namespace TUDU_BOT.Classes
{
    public class TaskItem
    {

        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }

        public TaskItem() { }

        public TaskItem(string Description, DateTime? dueDate = null)
        {
            this.Description = Description;
            this.IsCompleted = false;
            this.DueDate = dueDate ?? DateTime.MaxValue;
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
            string dueDateStr = DueDate == DateTime.MaxValue ? "No due date" : DueDate.ToString("g");
            return $"{status} {Description}";
        }

    }
}
