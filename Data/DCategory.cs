using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Model;
using System.Threading.Tasks;

namespace Data
{
    public class DCategory
    {

        public async Task<DataTable> List()
        {
            MySqlDataReader result;
            DataTable table = new DataTable();

            //SqlConnection sqlCon = new SqlConnection();
            MySqlConnection mySql = new MySqlConnection();
            
            try
            {
                
                mySql = ConnectionMysql.getInstancia().CrearConexion();
                MySqlCommand command = new MySqlCommand("sp_categories_list", mySql);
                command.CommandType = CommandType.StoredProcedure;
                await mySql.OpenAsync();
                //result = command.ExecuteReader();
                result = (MySqlDataReader)await command.ExecuteReaderAsync();
               // await result2.ReadAsync();
                table.Load(result);
                return table;

            }
            catch(Exception e) {
                throw e;
            } 
            finally
            {
                if (mySql.State == ConnectionState.Open) mySql.Close();
            }

        }

        public DataTable Search(string value)
        {

            SqlDataReader result;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Connection.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categories_search", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@value", SqlDbType.VarChar).Value = value;
                SqlCon.Open();
                result = Comando.ExecuteReader();
                Tabla.Load(result);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

        }

        public async Task<string> Insert(Category category)
        {
            string Rpta = "";
            MySqlConnection mySql = new MySqlConnection();
            try
            {
                mySql = ConnectionMysql.getInstancia().CrearConexion();
                MySqlCommand Comando = new MySqlCommand("category_insert", mySql);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("_parente", MySqlDbType.Int64).Value = category.Parent_id;
                Comando.Parameters.Add("_name", MySqlDbType.VarChar).Value = category.Name;
                Comando.Parameters.Add("_description", MySqlDbType.VarChar).Value = category.Description;
                await mySql.OpenAsync();
                Rpta = await Comando.ExecuteNonQueryAsync() == 1 ? "OK" : "No se pudo ingresar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (mySql.State == ConnectionState.Open) mySql.Close();
            }

            return Rpta;
        }

        public string Update(Category category)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Connection.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("category_update", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@id", SqlDbType.Int).Value = category.Id;
                Comando.Parameters.Add("@name", SqlDbType.VarChar).Value = category.Name;
                Comando.Parameters.Add("@description", SqlDbType.VarChar).Value = category.Description;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string Delete(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Connection.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("category_delete", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string Active(int id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Connection.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("category_active", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string Desactive(int id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Connection.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("category_desactive", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public async Task<string> Exist(string Value)
        {
            string Rpta = "";
            MySqlConnection mySql = new MySqlConnection();
            try
            {
                mySql = ConnectionMysql.getInstancia().CrearConexion();
                MySqlCommand Comando = new MySqlCommand("sp_existe_category", mySql);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("_name", MySqlDbType.VarChar).Value = Value;
                MySqlParameter parameterExist = new MySqlParameter();
                parameterExist.ParameterName = "_exist";
                parameterExist.MySqlDbType = MySqlDbType.Int64;
                parameterExist.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(parameterExist);
                mySql.Open();
                await Comando.ExecuteNonQueryAsync();
                Rpta = Convert.ToString(parameterExist.Value);
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (mySql.State == ConnectionState.Open) mySql.Close();
            }
            return Rpta;
        }


    }
}
