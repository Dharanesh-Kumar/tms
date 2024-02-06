using System.Data.SqlClient;
namespace Task_Management.Models;
public class Repository
{
    public static List<UserModel> employeeDetails = new List<UserModel>();

    public static List<UserModel> GetDetails()
    {
        try
        {
            using (SqlConnection Connection = new SqlConnection(getConnection()))
            {
                Connection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Employee", Connection);
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    UserModel user = new UserModel();
                    user.AceId = sqlReader["ACE"].ToString();
                    user.Username = sqlReader["UserName"].ToString();
                    user.Password = sqlReader["Password"].ToString();
                        employeeDetails.Add(user);
                    
                }
            }
        }

        catch (SqlException exception)
        {
            Console.WriteLine("Datebase error" + exception);
        }
        return employeeDetails;
    }



    public static int Login(UserModel user)
    {
        string AceId = user.AceId;
        string password = user.Password;
        List<UserModel> Detail = GetDetails();
        foreach (UserModel Data in Detail)
        {
            Console.WriteLine("Login Validation");
            if (String.Equals(AceId, Data.AceId) && String.Equals(password, Data.Password))
            {
                return 1;
            }
        }
        return 2;
    }

    public static int SignUp(UserModel sign)

    {
        Console.WriteLine("Inside Signup Model");
        bool userNameflag = true;
        List<UserModel> details = GetDetails();
        foreach (UserModel data in details)
        {
            Console.WriteLine("Inside Verification");
            if (String.Equals(sign.AceId, data.AceId))
            {
                userNameflag = false;
            }
        }

        if (userNameflag == true)
        {
            using (SqlConnection Connection = new SqlConnection(getConnection()))
            {
                Connection.Open();

                SqlCommand insertCommand = new SqlCommand("Insert into Employee (ACE,UserName,Password,MailId) values ('" + sign.AceId + "','" + sign.Username + "','" + sign.Password + "','" + sign.Email + "');", Connection);

                insertCommand.ExecuteNonQuery();
            }
            System.Console.WriteLine("inserted");
            return 4;
        }
        System.Console.WriteLine("UserName Exist");
        return 1;
    }
    public static int ForgetPass(UserModel Forget)
    {

        Console.WriteLine("Inside Forget Model");
        string AceId = Forget.AceId;
        
        bool userNameflag = true;
        List<UserModel> details = GetDetails();
        foreach (UserModel data in details)
        {
            Console.WriteLine("Inside Verification");
            if (String.Equals(AceId, data.AceId))
            {
                userNameflag = false;
            }
        }

        if (userNameflag == true)
        {
            using (SqlConnection Connection = new SqlConnection(getConnection()))
            {
                Connection.Open();
                 
                SqlCommand updateCommand = new SqlCommand("Update Employee Set (Password) = ('" + Forget.NewPassword + "') Where (ACE) = ('" + Forget.AceId + "');", Connection);

                updateCommand.ExecuteNonQuery();
            }
            System.Console.WriteLine("Updated");
            return 4;
        }
        System.Console.WriteLine("Invalid AceId");
        return 1;
    }
    public static string getConnection()
    {
        var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = build.Build();
        string connectionString = Convert.ToString(configuration.GetConnectionString("DefaultConnection"));
        return connectionString;
    }
}

