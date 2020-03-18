using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiYo.Models;

namespace WebApiYo.Data
{
    public class MaspeRepository
    {
        private readonly string _connectionString;

        public MaspeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionBaseDatos");
        }

        public async Task<List<Maspe>> ListarPersonal(string Cip, string Paterno, string Materno, string Nombres, string Dni)
        {
            List<Maspe> Lista = new List<Maspe>();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                //using (SqlConnection sql = new SqlConnection(_connectionString))
                //{
                //    using (SqlCommand cmd = new SqlCommand("GetAllValues", sql))
                //    {
                //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //        var response = new List<Maspe>();
                //        await sql.OpenAsync();

                //        using (var reader = await cmd.ExecuteReaderAsync())
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                response.Add(MapToValue(reader));
                //            }
                //        }

                //        return response;
                //    }
                //}

                //==============================================
                //con = Conexion.getInstance().ConexionBD();
                con = new SqlConnection(_connectionString);

                cmd = new SqlCommand("AscensoListarPersonal2", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cip", Cip);
                cmd.Parameters.AddWithValue("@ApPaterno", Paterno);
                cmd.Parameters.AddWithValue("@ApMaterno", Materno);
                cmd.Parameters.AddWithValue("@Nombres", Nombres);
                cmd.Parameters.AddWithValue("@Dni", Dni);

                //con.Open();
                await con.OpenAsync();
                //reader = cmd.ExecuteReader();
                reader = await cmd.ExecuteReaderAsync();

                //while (await reader.Read())
                while (await reader.ReadAsync())
                {
                    // Crear objetos de tipo maspe
                    Maspe objMaspe = new Maspe();
                    objMaspe.Fila = Convert.ToInt32(reader["RowNum"].ToString());
                    objMaspe.MASPE_CARNE = Convert.ToInt32(reader["MASPE_CARNE"]);
                    objMaspe.MASPE_FASCEN = reader["MASPE_FASCEN"].ToString();
                    objMaspe.TGRAD_DES = reader["TGRAD_DES"].ToString();
                    objMaspe.TSITUA_DESL = reader["TSITUA_DESL"].ToString();
                    objMaspe.MASPE_PATER = reader["MASPE_PATER"].ToString();
                    objMaspe.MASPE_MATER = reader["MASPE_MATER"].ToString();
                    objMaspe.MASPE_NOMB = reader["MASPE_NOMB"].ToString();
                    objMaspe.MASPE_DNI = reader["MASPE_DNI"].ToString();
                    objMaspe.Total = Convert.ToInt32(reader["Total"].ToString());
                    objMaspe.Filtro = Convert.ToInt32(reader["TotalCount"].ToString());
                    Lista.Add(objMaspe);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Lista;

        }
    }
}


