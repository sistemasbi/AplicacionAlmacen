using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace capaDatos
{
    public class clsConexion
    {
        private String servidor;
        private String usuario;
        private String password;
        private String basedatos;
        private SqlCommand cmd;

        public clsConexion()
        {
            this.servidor = "ERWINBARRIENTOS";
            this.usuario = "sa";
            this.password = "123456789";
            this.basedatos = "db_venado";
            this.cmd = new SqlCommand();
        }

        public SqlConnection conectar()
        {
            SqlConnection cnx = new SqlConnection();

            cnx.ConnectionString = "data source =" + this.servidor + "; initial catalog =" + this.basedatos + "; user id =" + this.usuario + "; password =" + this.password;
            cnx.Open();
            return cnx;
        }

        public void desconectar()
        {
            SqlConnection cnx = this.conectar();
            cnx.Close();
        }

        public void iniciarSP(string nombreSP)
        {
            this.cmd.Connection = conectar();
            this.cmd.CommandText = nombreSP;
            this.cmd.CommandType = CommandType.StoredProcedure;
        }

        public bool ejecutarSP()
        {
            bool res;
            //this.cmd.Connection = conectar();
            if (cmd.ExecuteNonQuery() == 1) { res = true; }
            else { res = false; }
            this.desconectar();
            return res;
        }

        public void parametroInt(int valor, string param)
        {
            SqlParameter Par = new SqlParameter();
            Par.ParameterName = param;
            Par.SqlDbType = SqlDbType.Int;
            Par.Value = valor;
            cmd.Parameters.Add(Par);
        }

        public void parametroDecimal(decimal valor, string param)
        {
            SqlParameter Par = new SqlParameter();
            Par.ParameterName = param;
            Par.SqlDbType = SqlDbType.Decimal;
            Par.Value = valor;
            cmd.Parameters.Add(Par);
        }

        public void parametroVarchar(string valor, string param, int dimension)
        {
            SqlParameter Par = new SqlParameter();
            Par.ParameterName = param;
            Par.SqlDbType = SqlDbType.VarChar;
            Par.Size = dimension;
            Par.Value = valor;
            cmd.Parameters.Add(Par);
        }
        public void parametroFecha(DateTime valor, string param)
        {
            SqlParameter Par = new SqlParameter();
            Par.ParameterName = param;
            Par.SqlDbType = SqlDbType.DateTime;
            Par.Value = valor;
            cmd.Parameters.Add(Par);
        }

        public DataTable mostrarData()
        {
            DataTable DtResultado = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            da.Fill(DtResultado);
            this.desconectar();
            return DtResultado;
        }

        public DataTable ejecutarSQL(String sql)
        {
            DataTable DtResultado = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conectar());
            da.Fill(DtResultado);
            this.desconectar();
            return DtResultado;
        }
    }
}
