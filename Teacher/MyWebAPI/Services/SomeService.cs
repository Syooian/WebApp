namespace MyWebAPI.Services
{
    //8.1.2 在資料夾中建立SomeService.cs類別並實作內容
    public class SomeService
    {
        string[] Id = { "A01", "A02", "A03" };

        string[] Name = { "王小明", "王中明", "王大明" };

        int[] Age = { 14, 15, 16 };


        public string[] getAllStudents()
        {
            string[] students =new string[Id.Length];

            for (int i=0; i<Id.Length; i++)
            {
                students[i] = $"{Id[i]} - {Name[i]} - {Age[i]}";
            }
            return students;
        }

        public string gelStudent(string id)
        {

            int i = Array.IndexOf(Id, id);

            if (i < 0 || i >= Id.Length)
            {
                return "沒有找到任何資料";
            }


            return $"{Id[i]} - {Name[i]} - {Age[i]}";
            
            
        }


    }
}
