using capaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaNegocio
{
    public class ProductoKpi:clsConexion
    {
        private int id;
        private int nro_politica;
        private string nombre_politica;
        private DateTime vigencia_desde;
        private DateTime vigencia_hasta;
        private int nro_kpi;
        private string tipo_kpi;
        private string sector;
        private string tipo;
        private string valor;
        private int peso;
        private int orden;
        private string cj_division;
        private string cj_sector;
        private string cj_marca;
        private string cj_categoria;
        private string cj_grupo;
        private string cj_familia;
        private int cj_coddivision;
        private int cj_codsector;
        private int cj_codmarca;
        private int cj_codcategoria;
        private int cj_codgrupo;
        private int cj_codfamilia;

        public ProductoKpi()
        {
            id=0;
            nro_politica=0;
            nombre_politica = "";
            vigencia_desde = DateTime.Today.Date;
            vigencia_hasta = DateTime.Today.Date;
            nro_kpi = 0;
            tipo_kpi = "";
            sector = "";
            tipo = "";
            valor = "";
            peso = 0;
            orden = 0;
            cj_division = "";
            cj_sector = "";
            cj_marca = "";
            cj_categoria = "";
            cj_grupo = "";
            cj_familia = "";
            cj_coddivision = 0;
            cj_codsector = 0;
            cj_codmarca = 0;
            cj_codcategoria = 0;
            cj_codgrupo = 0;
            cj_codfamilia = 0;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int NroPolitica
        {
            get { return this.nro_politica; }
            set { this.nro_politica = value; }
        }

        public string NombrePolitica
        {
            get { return this.nombre_politica; }
            set { this.nombre_politica = value; }

        }
        public DateTime VigenciaDesde
        {
            get { return this.vigencia_desde; }
            set { this.vigencia_desde = value; }
        }

        public DateTime VigenciaHasta
        {
            get { return this.vigencia_hasta; }
            set { this.vigencia_hasta = value; }
        }

        public int NroKpi
        {
            get { return this.nro_kpi; }
            set { this.nro_kpi = value; }
        }

        public string TipoKpi
        {
            get { return this.tipo_kpi; }
            set { this.tipo_kpi = value; }
        }

        public string Sector
        {
            get { return this.sector; }
            set { this.sector = value; }
        }

        public string Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; }
        }

        public string Valor
        {
            get { return this.valor; }
            set { this.valor = value; }
        }

        public int Peso
        {
            get { return this.peso; }
            set { this.peso = value; }
        }

        public int Orden
        {
            get { return this.orden; }
            set { this.orden = value; }
        }

        public string CjDivision
        {
            get { return this.cj_division; }
            set { this.cj_division = value; }
        }

        public string CjSector
        {
            get { return this.cj_sector; }
            set { this.cj_sector = value; }
        }

        public string CjMarca
        {
            get { return this.cj_marca; }
            set { this.cj_marca = value; }
        }

        public string CjCategoria
        {
            get { return this.cj_categoria; }
            set { this.cj_categoria = value; }
        }

        public string CjGrupo
        {
            get { return this.cj_grupo; }
            set { this.cj_grupo = value; }
        }

        public string CjFamilia
        {
            get { return this.cj_familia; }
            set { this.cj_familia = value; }
        }

        public int CjCodDivision
        {
            get { return this.cj_coddivision; }
            set { this.cj_coddivision = value; }
        }

        public int CjCodSector
        {
            get { return this.cj_codsector; }
            set { this.cj_codsector = value; }
        }

        public int CjCodMarca
        {
            get { return this.cj_codmarca; }
            set { this.cj_codmarca = value; }
        }

        public int CjCodCategoria
        {
            get { return this.cj_codcategoria; }
            set { this.cj_codcategoria = value; }
        }

        public int CjCodGrupo
        {
            get { return this.cj_codgrupo; }
            set { this.cj_codgrupo = value; }
        }

        public int CjCodFamilia
        {
            get { return this.cj_codfamilia; }
            set { this.cj_codfamilia = value; }
        }

        public DataTable buscarPolitica(string filtro)
        {
            string sql = "select distinct nro_politica, nombre_politica, vigencia_desde, vigencia_hasta from producto_kpi where nro_politica LIKE '%"+filtro+"%' OR nombre_politica LIKE '%"+filtro+"%' order by nro_politica desc";
            return ejecutarSQL(sql);
        }

        public DataTable editarPolitica(int filtro)
        {
            iniciarSP("grillaAgrupadaXKpi");
            parametroInt(filtro, "nro_politica");
            return mostrarData();
        }

        public bool modificarPolitica()
        {
            iniciarSP("modificarVigencia");
            parametroInt(NroPolitica, "nro_politica");
            parametroFecha(VigenciaHasta, "vigencia_hasta");
            if (ejecutarSP() == true) { return true; } else { return false; }
        }

        public int nroPolitica()
        {
            string sql = "select top 1 nro_politica from producto_kpi order by id desc";
            DataTable data= ejecutarSQL(sql);
            if (data.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                int id_politica = int.Parse(data.Rows[0]["nro_politica"].ToString());
                return (id_politica + 1);
            }
            
        }

        public bool SP_Guardar()
        {
            iniciarSP("guardarProductoKpi");
            parametroInt(NroPolitica, "nro_politica");
            parametroVarchar(NombrePolitica, "nombre_politica", 100);
            parametroFecha(VigenciaDesde, "vigencia_desde");
            parametroInt(NroKpi, "nro_kpi");
            parametroVarchar(TipoKpi, "tipo_kpi", 100);
            parametroVarchar(Sector, "sector", 100);
            parametroVarchar(Tipo, "tipo", 100);
            parametroVarchar(Valor, "valor", 100);
            parametroInt(Peso, "peso");
            parametroInt(Orden, "orden");
            if (ejecutarSP() == true) { return true; } else { return false; }
        }

    }
}
