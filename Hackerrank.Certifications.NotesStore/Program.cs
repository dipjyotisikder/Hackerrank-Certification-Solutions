namespace Solution
{

    public class NotesStore
    {
        private List<KeyValuePair<string, string>> store = new List<KeyValuePair<string, string>>();

        private readonly List<string> StatesStore = new List<string>
        {
            "completed",
            "active",
            "others"
        };

        public NotesStore() { }
        public void AddNote(String state, String name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) throw new Exception("Name cannot be empty");
            if (!string.IsNullOrEmpty(state) && !string.IsNullOrWhiteSpace(state))
            {
                if (!StatesStore.Contains(state)) throw new Exception($"Invalid state {state}");
            }

            store.Add(new KeyValuePair<string, string>(name, state));
        }

        public List<String> GetNotes(String state)
        {
            if (!string.IsNullOrEmpty(state) && !string.IsNullOrWhiteSpace(state))
            {
                if (!StatesStore.Contains(state)) throw new Exception($"Invalid state {state}");
            }
            var names = store.Where(x => x.Value == state).Select(x => x.Key);

            return names.ToList();
        }
    }

    public class Solution
    {
        public static void Main()
        {
            var notesStoreObj = new NotesStore();
            var n = int.Parse(Console.ReadLine());
            for (var i = 0; i < n; i++)
            {
                var operationInfo = Console.ReadLine().Split(' ');
                try
                {
                    if (operationInfo[0] == "AddNote")
                        notesStoreObj.AddNote(operationInfo[1], operationInfo.Length == 2 ? "" : operationInfo[2]);
                    else if (operationInfo[0] == "GetNotes")
                    {
                        var result = notesStoreObj.GetNotes(operationInfo[1]);
                        if (result.Count == 0)
                            Console.WriteLine("No Notes");
                        else
                            Console.WriteLine(string.Join(",", result));
                    }
                    else
                    {
                        Console.WriteLine("Invalid Parameter");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
}