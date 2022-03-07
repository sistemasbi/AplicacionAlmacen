using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaDatos;

namespace capaNegocio
{
    public class ProductoComision: clsConexion
    {
        private int id_producto;
        private string division;
        private string cmdSql;

        public ProductoComision()
        {
            id_producto = 0;
            division = "";
        }

        public int IdProducto
        {
            get { return this.id_producto; }
            set { this.id_producto = value; }
        }

        public string Division
        {
            get { return this.division; }
            set { this.division = value; }
        }

        public DataTable selectSector1()
        {
            iniciarSP("sp_sector1");
            return mostrarData();
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public DataTable selectComision(string sector, string tipo, string valor)
        {
            this.cmdSql = "";
            if(tipo == "TODOS" || valor == "TODOS")
            {
                this.cmdSql = "select distinct Division, Sector, Marca, Categoria, Grupo, Familia, CodDivision, CodSector, CodMarca, CodCategoria, CodGrupo, CodFamilia from producto_comision where Sector = '" + sector + "'";
            }
            else
            {

                this.cmdSql = "select distinct Division, Sector, Marca, Categoria, Grupo, Familia, CodDivision, CodSector, CodMarca, CodCategoria, CodGrupo, CodFamilia from producto_comision where Sector = '" + sector + "' and "+ UppercaseFirst(tipo.ToLower())+"='"+valor+"'";
            }
            return ejecutarSQL(this.cmdSql);
        }

        public DataTable selectValor(List<string> sector,string tipo)
        {
            this.cmdSql="";
            switch (sector.Count())
            {
                case 1:
                    switch (tipo)
                    {
                        case "DIVISION":
                            this.cmdSql = "select distinct Division as nombre from producto_comision where Sector in ('" + sector[0] + "')";
                            break;
                        case "SECTOR":
                            this.cmdSql = "select distinct Sector as nombre from producto_comision where Sector in ('" + sector[0] + "')";
                            break;
                        case "MARCA":
                            this.cmdSql = "select distinct Marca as nombre from producto_comision where Sector in ('" + sector[0] + "')";
                            break;
                        case "CATEGORIA":
                            this.cmdSql = "select distinct Categoria as nombre from producto_comision where Sector in ('" + sector[0] + "')";
                            break;
                        case "GRUPO":
                            this.cmdSql = "select distinct Grupo as nombre from producto_comision where Sector in ('" + sector[0] + "')";
                            break;
                        case "FAMILIA":
                            this.cmdSql = "select distinct Familia as nombre from producto_comision where Sector in ('" + sector[0] + "')";
                            break;
                    }
                    break;
                    
                case 2:
                    switch (tipo)
                    {
                        case "DIVISION":
                            this.cmdSql = "select distinct Division as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "')";
                            break;
                        case "SECTOR":
                            this.cmdSql = "select distinct Sector as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "')";
                            break;
                        case "MARCA":
                            this.cmdSql = "select distinct Marca as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "')";
                            break;
                        case "CATEGORIA":
                            this.cmdSql = "select distinct Categoria as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "')";
                            break;
                        case "GRUPO":
                            this.cmdSql = "select distinct Grupo as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "')";
                            break;
                        case "FAMILIA":
                            this.cmdSql = "select distinct Familia as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "')";
                            break;
                    }                    
                    break;
                
                case 3:
                    switch (tipo)
                    {
                        case "DIVISION":
                            this.cmdSql = "select distinct Division as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "')";
                            break;
                        case "SECTOR":
                            this.cmdSql = "select distinct Sector as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "')";
                            break;
                        case "MARCA":
                            this.cmdSql = "select distinct Marca as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "')";
                            break;
                        case "CATEGORIA":
                            this.cmdSql = "select distinct Categoria as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "')";
                            break;
                        case "GRUPO":
                            this.cmdSql = "select distinct Grupo as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "')";
                            break;
                        case "FAMILIA":
                            this.cmdSql = "select distinct Familia as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "')";
                            break;
                    }                    
                    break;

                case 4:
                    switch (tipo)
                    {
                        case "DIVISION":
                            this.cmdSql = "select distinct Division as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "')";
                            break;
                        case "SECTOR":
                            this.cmdSql = "select distinct Sector as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "')";
                            break;
                        case "MARCA":
                            this.cmdSql = "select distinct Marca as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "')";
                            break;
                        case "CATEGORIA":
                            this.cmdSql = "select distinct Categoria as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "')";
                            break;
                        case "GRUPO":
                            this.cmdSql = "select distinct Grupo as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "')";
                            break;
                        case "FAMILIA":
                            this.cmdSql = "select distinct Familia as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "')";
                            break;
                    }                    
                    break;
                case 5:
                    switch (tipo)
                    {
                        case "DIVISION":
                            this.cmdSql = "select distinct Division as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "','" + sector[4] + "')";
                            break;
                        case "SECTOR":
                            this.cmdSql = "select distinct Sector as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "','" + sector[4] + "')";
                            break;
                        case "MARCA":
                            this.cmdSql = "select distinct Marca as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "','" + sector[4] + "')";
                            break;
                        case "CATEGORIA":
                            this.cmdSql = "select distinct Categoria as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "','" + sector[4] + "')";
                            break;
                        case "GRUPO":
                            this.cmdSql = "select distinct Grupo as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "','" + sector[4] + "')";
                            break;
                        case "FAMILIA":
                            this.cmdSql = "select distinct Familia as nombre from producto_comision where Sector in ('" + sector[0] + "','" + sector[1] + "','" + sector[2] + "','" + sector[3] + "','" + sector[4] + "')";
                            break;
                    }                    
                    break;
                default:
                    /*Caso por añadir*/
                    break;
            }
            return ejecutarSQL(this.cmdSql);
        }
    }
}
