using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
namespace Task_Management.Models;
public class Repository1 
{
    public static void TaskSubmit(UserTask task, string id)
    {
        
            using (SqlConnection Connection = new SqlConnection(getConnection()))
        {
            Connection.Open();
            UserModel userid = new UserModel();
            SqlCommand insertCommand = new SqlCommand("Insert into Tasks (AceId,Date,Project,Task,Workplace,Description) values ('" + id + "','" + task.Date + "','" + task.Project + "','" + task.Task + "','" + task.WorkPlace + "','" + task.Decription + "');", Connection);

            insertCommand.ExecuteNonQuery();
        }
    }
    
    public static string getConnection()
    {
        var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = build.Build();
        string connectionString = Convert.ToString(configuration.GetConnectionString("DefaultConnection"));
        return connectionString;
    }
    public static List<UserTask> ViewSubmit(string submit)
    {
 
        List<UserTask> TaskList = new List<UserTask>();
 
        try
        {
            using (SqlConnection connection = new SqlConnection(getConnection()))
            {
               
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Tasks where AceId=@ID", connection);
                sqlCommand.Parameters.AddWithValue("@ID", submit);
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
 
                while (sqlReader.Read())
                {
                    UserTask task = new UserTask();
                    task.AceId = sqlReader["AceId"].ToString();
                    task.Date = sqlReader["Date"].ToString();
                    task.Project=sqlReader["Project"].ToString();
                    task.Task=sqlReader["Task"].ToString();
                    task.WorkPlace=sqlReader["WorkPlace"].ToString();
                    task.Decription=sqlReader["Description"].ToString();

                    TaskList.Add(task);
                   
                }
                Console.WriteLine("Task Fetched");
               
            }
        }
        catch (SqlException exception)
        {
            Console.WriteLine("Database error: " + exception);
        }
   
 
        return TaskList;
    }
}