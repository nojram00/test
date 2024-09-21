using game_sdk;
using game_sdk.Services;
using game_sdk.Structures;
using System.Text.Json;
public class Program
{
    private const string mainUrl = "https://game-server-nu.vercel.app";

    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("\n\n****************\n\n");

            GetStudents().Wait();

            Console.WriteLine("\n\n****************\n\n");

            GetSections().Wait();

            Console.WriteLine("\n\n****************\n\n");

            UpdateScore().Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection Timed Out: {ex.Message}");
        }
        
    }

    public static async Task GetStudents()
    {
        var client = new Client(mainUrl);
        var studentService = new StudentService(client);


        var students = await studentService.GetStudents();

        if(students != null && students.Count > 0)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"Name : {student.name}, \nSection: {(student.section != null ? student.section.sectionName : "No Section")} \n");
                Console.WriteLine($"Pre Test Score: {(student.score != null ? student.score?.preTest : 0)} \nPost Test Score: {(student.score != null ? student.score?.postTest : 0)}");
                Console.WriteLine("\n===============");
            }
        }
    }

    public static async Task GetSections()
    {
        var client = new Client(mainUrl);
        var sectionService = new SectionService(client);

        var sections = await sectionService.GetSections();

        if (sections != null && sections.Count > 0)
        {
            foreach( Section section in sections)
            {
                Console.WriteLine($"Section Name: {section.sectionName}, \nTeacher: {(section.teacher != null ? section.teacher.name : "No Teacher Assigned")}");
            }
        }
    }

    public static async Task UpdateScore()
    {
        var client = new Client(mainUrl);
        var scoreService = new ScoreService(client);

        var scoreData = await scoreService.UpdateScore(3, new Score(20, 13)); 

        Console.WriteLine(scoreData.message);
        Console.WriteLine(scoreData.data);
    }

}
