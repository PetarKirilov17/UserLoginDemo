using Microsoft.Data.SqlClient;
using RegisterLoginDemo.ViewModels;

namespace RegisterLoginDemo.Helpers
{
    public class CommonHelper
    {
        private IConfiguration _configuration;
        public CommonHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool UserAlreadyExists(string query) {
            bool isUsed = false;
            string connectionStr = _configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionStr)) { 
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) { 
                    isUsed = true;
                }
                connection.Close();
            }
            return isUsed;
        }

        public int DMLTransaction(string query) {
            int result;
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString)) { 
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        public UserViewModel GetUserByUsername(string query) 
        {
            UserViewModel model = new UserViewModel();

            string connectionStr = _configuration["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection( connectionStr)) 
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) 
                {
                    while (dataReader.Read()) 
                    {
                        model.UserId = Convert.ToInt32(dataReader["UserId"]);
                        model.Username = dataReader["Username"].ToString();
                        model.FirstName = dataReader["FirstName"].ToString();
                        model.LastName = dataReader["LastName"].ToString();
                        model.Password = dataReader["Password"].ToString();
                    }
                }
                connection.Close();
            }
            return model;
        }
    }
}
