using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace LucasNotes.UserService.Repositories
{
    public class DbHelper
    {
        private readonly string connectionString;
        public DbHelper(IConfiguration configuration)
        {
            //connectionString = configuration["ConnectionStrings:UserDatabase"];
            var ip = configuration["dbAddress"];
            var pwd = configuration["dbPwd"];

            connectionString = $"Data Source={ip};database=LucasNotes.UserDb;uid=sa;pwd={pwd};";
        }

        public async Task<List<T>> Query<T>(string sql, SqlParameter[] paramArray = null) where T : new()
        {
            var ds = await GetDataSetAsync(sql, paramArray);
            return ConvertDataTableToList<T>(ds.Tables[0]);
        }

        public async Task<DataSet> GetDataSetAsync(string sql, SqlParameter[] paramArray = null )
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    if (paramArray != null)
                    {
                        cmd.Parameters.AddRange(paramArray);
                    }
                    try
                    {

                        var ds = new DataSet();
                        var adapter = new SqlDataAdapter(cmd);
                        await con.OpenAsync();
                        cmd.CommandText = sql;
                        adapter.Fill(ds);
                        return ds;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Method:GetDataSet, DateTime:{DateTime.Now}, ConnectString:{connectionString},sql:{sql}"); ;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

        }

        public async Task<int> ExecuteNonQueryAsync(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        await con.OpenAsync();
                        
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Method:GetDataSet, DateTime:{DateTime.Now}");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        private static List<T> ConvertDataTableToList<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
                {
                    if (dataTable.Columns.Contains(propertyInfo.Name))
                    {
                        if (row[propertyInfo.Name] != DBNull.Value)
                        {
                            propertyInfo.SetValue(obj, row[propertyInfo.Name], null);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}