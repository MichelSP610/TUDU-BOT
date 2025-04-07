using System.Text.Json;

namespace TUDU_BOT.Classes
{
    internal class TaskManager
    {

        private static readonly string SavePath = "tasks.json";
        private static Dictionary<ulong, List<TaskItem>> userTasks = new();

        static TaskManager()
        {
            LoadTasks();
        }

        public static void AddTask(ulong userId, string description)
        {
            if (!userTasks.ContainsKey(userId))
                userTasks[userId] = new List<TaskItem>();

            userTasks[userId].Add(new TaskItem(description));
            SaveTasks();
        }

        public static void RemoveTask(ulong userId, int index)
        {
            userTasks[userId].RemoveAt(index - 1);
            SaveTasks();
        }

        public static List<TaskItem> GetTasks(ulong userId)
        {
            return userTasks.TryGetValue(userId, out var tasks) ? tasks : new List<TaskItem>();
        }

        public static bool CompleteTask(ulong userId, int index)
        {
            if (userTasks.TryGetValue(userId, out var tasks) && index >= 0 && index < tasks.Count)
            {
                tasks[index].MarkAsCompleted();
                SaveTasks();
                return true;
            }

            return false;
        }

        public static bool UnCheckTask(ulong userId, int index)
        {
            if(userTasks.TryGetValue(userId,out var tasks) && index >= 0 && (index < tasks.Count - 1))
            {
                tasks[index].MarkAsIncompleted();
                SaveTasks();
                return true;
            }

            return false;
        }


        //Save tasks to 
        private static void SaveTasks()
        {
            var json = JsonSerializer.Serialize(userTasks, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(SavePath, json);
        }

        private static void LoadTasks()
        {
            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                userTasks = JsonSerializer.Deserialize<Dictionary<ulong, List<TaskItem>>>(json) ?? new Dictionary<ulong, List<TaskItem>>();
            }
        }


    }
}
