namespace LucasNotes.UserService.Repositories
{
    public class DbHelper
    {
        private readonly string connectionString;
        public DbHelper(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:UserDatabase"];
        }

        public T Query<T>(string sql) where T : new()
        {
            //using (var con = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand cmd = con.CreateCommand())
            //    {
            //        cmd.CommandText = sql;
            //        con.Open();

            //    }
            //}
            return new T();
        }
    }
}
