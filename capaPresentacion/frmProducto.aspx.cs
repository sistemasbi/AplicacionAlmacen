using capaNegocio;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace capaPresentacion
{
    public partial class frmProducto : System.Web.UI.Page
    {
        DataTable dtb;
        DataTable carrito = new DataTable();
        DataTable detalleObj = new DataTable();
        int nro_pol;
        public void CargarDetalle()
        {
            if (Session["pedido"] == null)
            {
                dtb = new DataTable("Carrito");
                dtb.Columns.Add("nro_kpi", System.Type.GetType("System.Int32"));
                dtb.Columns.Add("tipo_kpi", System.Type.GetType("System.String"));
                dtb.Columns.Add("sector", System.Type.GetType("System.String"));
                dtb.Columns.Add("tipo", System.Type.GetType("System.String"));
                dtb.Columns.Add("valor", System.Type.GetType("System.String"));
                dtb.Columns.Add("peso", System.Type.GetType("System.Int32"));

                Session["pedido"] = dtb;
                Session["prueba"] = dtb;
            }
            else
            {
                Session["pedido"] = Session["prueba"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            mostrar();
            selectSectorA();
            CargarDetalle();
            NroPolitica();
            if (!IsPostBack)
            {
                listarPolitica();
            }
        }

        protected void mostrar()
        {
            txtVigenciaDesde.Text = DateTime.Now.ToString("yyyy-MM-dd");
            
            string script = "var fachab = document.getElementById('txtVigenciaHasta'); fachab.disabled=true;";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        protected void listarPolitica()
        {
            ProductoKpi prod = new ProductoKpi();
            string filtro = txtBuscar.Text;
            gvPolitica.DataSource = prod.buscarPolitica(filtro);
            gvPolitica.DataBind();
        }

        protected void NroPolitica()
        {
            ProductoKpi prod = new ProductoKpi();
            lblNroPolitica.Text = "Nro: " + prod.nroPolitica();
            nro_pol = prod.nroPolitica();

        }
        public void cargarcarrito()
        {
            gvDetalle.DataSource = Session["pedido"];
            gvDetalle.DataBind();
        }

        protected int calcularTotal()
        {
            int total = 0;
            foreach(GridViewRow row in gvDetalle.Rows)
            {
                int peso = Convert.ToInt32(((TextBox)row.Cells[6].FindControl("txtpeso")).Text);
                total = total + peso;
            }
            return total;
        }

        protected int ordenTipo(string nameTipo)
        {
            int orden=0;
            switch (nameTipo)
            {
                case "DIVISION":
                    orden = 6;
                    break;
                case "SECTOR":
                    orden = 5;
                    break;
                case "MARCA":
                    orden=4;
                    break;
                case "CATEGORIA":
                    orden=3;
                    break;
                case "GRUPO":
                    orden = 2;
                    break;
                case "FAMILIA":
                    orden = 1;
                    break;
               default:orden = 0;
                    break;
            }
            return orden;
        }

        protected void messageAlert(string mensaje) {
            string script = "window.onload = function(){ alert('";
            script += mensaje;
            script += "');";
            script += "window.location = '";
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        protected void selectSectorA()
        {
            ProductoComision cat = new ProductoComision();
            if (IsPostBack == false)
            {
                cboSector.DataSource = cat.selectSector1();
                cboSector.DataValueField = "nombre";
                cboSector.DataTextField = "nombre";
                cboSector.DataBind();
                cboSector.Items.Insert(0, new ListItem("TODOS", "TODOS"));
            }
        }

        protected void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> data = new List<string>();
            foreach (ListItem item in cboSector.Items)
            {
                if (item.Selected)
                {
                    data.Add(Convert.ToString(item.Text));
                }
            }
            ProductoComision cat = new ProductoComision();
            string tipo = cboTipo.SelectedItem.ToString();
            if (tipo == "TODOS")
            {
                cboValor.Items.Insert(0, new ListItem("TODOS", "TODOS"));
                //string script = "var valor = document.getElementById('cboValor'); valor.disabled=true;";
                //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                string javaScript = "deshabilitarCboValor();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            }
            else
            {
                if (IsPostBack == true)
                {
                    //string script = "var valor = document.getElementById('cboValor'); valor.disabled=false;";
                    //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    string javaScript = "habilitarCboValor();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

                    cboValor.DataSource = cat.selectValor(data, tipo);
                    cboValor.DataValueField = "nombre";
                    cboValor.DataTextField = "nombre";
                    cboValor.DataBind();
                    cboValor.Items.Insert(0, new ListItem("TODOS", "TODOS"));
                }
            }
            
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            int kpi = -1;
            if (Int32.Parse(gvDetalle.Rows.Count.ToString()) == 0)
            {
                kpi = 0;
            }
            else
            {
                kpi = Int32.Parse(gvDetalle.Rows.Count.ToString());
            }


            carrito = (DataTable)Session["pedido"];
            DataRow fila = carrito.NewRow();
            string tipo_kpì = cboTipoKpi.SelectedValue.ToString();
            if (tipo_kpì == "EFECTIVIDAD X VISITA" || tipo_kpì == "TICKET PROMEDIO")
            {
                fila[0] = kpi + 1; //Nro kpi
                fila[1] = cboTipoKpi.SelectedValue.ToString(); //Tipo Kpi
                fila[2] = "TODOS"; //Sector
                fila[3] = cboTipo.SelectedItem.ToString(); //Tipo
                fila[4] = cboValor.SelectedValue.ToString();  //Valor
                fila[5] = 0; //Peso
            }
            else
            {
                string cadSector = "";
                foreach (ListItem item in cboSector.Items)
                {
                    if (item.Selected)
                    {
                        cadSector = cadSector + Convert.ToString(item.Text) + ",";
                    }
                }

                fila[0] = kpi + 1; //Nro kpi
                fila[1] = cboTipoKpi.SelectedValue.ToString(); //Tipo Kpi
                fila[2] = cadSector.TrimEnd(','); //Sector
                fila[3] = cboTipo.SelectedItem.ToString(); //Tipo
                fila[4] = cboValor.SelectedValue.ToString();  //Valor
                fila[5] = 0; //Peso
            }            
            carrito.Rows.Add(fila);
            Session["pedido"] = carrito;
            cargarcarrito();

            cboTipo.SelectedIndex = 0;
            cboValor.Items.Clear();
            cboValor.Items.Insert(0, new ListItem("TODOS", "TODOS"));
            cboSector.ClearSelection();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (calcularTotal() == 100)
            {
                ProductoComision cat = new ProductoComision();
                foreach (GridViewRow row in gvDetalle.Rows)
                {
                    ProductoKpi kpi = new ProductoKpi();
                    kpi.NroPolitica = nro_pol;
                    kpi.NombrePolitica = txtNombrePolitica.Text;
                    kpi.VigenciaDesde = Convert.ToDateTime(txtVigenciaDesde.Text);
                    kpi.NroKpi = Convert.ToInt32(row.Cells[1].Text);
                    kpi.TipoKpi = row.Cells[2].Text;
                    kpi.Sector = row.Cells[3].Text;
                    kpi.Tipo = row.Cells[4].Text;
                    kpi.Valor = row.Cells[5].Text;
                    kpi.Peso = Convert.ToInt32(((TextBox)row.Cells[6].FindControl("txtpeso")).Text);
                    kpi.Orden = ordenTipo(row.Cells[4].Text);
                    kpi.SP_Guardar();
                }
                NroPolitica();
                txtNombrePolitica.Text = "";
                txtVigenciaDesde.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtVigenciaHasta.Text = "";
                gvDetalle.DataBind();
                Session["pedido"] = null;
                Session["prueba"] = null;
                //messageAlert("Politica registrada...");

                string javaScript = "smsRegistrado();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
                listarPolitica();
            }
            else
            {
                //messageAlert("Revise el Peso Total");
                lblTotal.Text = "Total peso: "+calcularTotal()+" %";
                string javaScript = "smsErrorPeso();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombrePolitica.Text = "";
            txtIdPolitica.Text = "";
            txtVigenciaDesde.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtVigenciaHasta.Text = "";
            gvDetalle.DataBind();
            Session["pedido"] = null;
            Session["prueba"] = null;

            gvDetKpi.DataBind();
            //lblTotal.Text = "% " + calcularTotal();
            //messageAlert("Registro cancelado...");

            string javaScript = "nuevaPolitica();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }

    protected void cboTipoKpi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo_kpì = cboTipoKpi.SelectedValue.ToString();
            if (tipo_kpì == "EFECTIVIDAD X VISITA" || tipo_kpì == "TICKET PROMEDIO")
            {
                cboSector.ClearSelection();
                cboSector.SelectedIndex = 0;

                cboTipo.SelectedIndex = 0;

                cboValor.Items.Clear();
                cboValor.Items.Insert(0, new ListItem("TODOS", "TODOS"));

                //string script = "var sector = document.getElementById('cboSector'); sector.disabled=true; " +
                //    "var tipo = document.getElementById('cboTipo'); tipo.disabled=true; " +
                //    "var valor = document.getElementById('cboValor'); valor.disabled=true;";
                //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                string javaScript = "deshabilitarCbo();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

            }
            else
            {
                cboSector.ClearSelection();
                cboTipo.SelectedIndex = 0;
                cboValor.Items.Clear();
                cboValor.Items.Insert(0, new ListItem("TODOS", "TODOS"));

                //string script = "var sector = document.getElementById('cboSector'); sector.disabled=false; " +
                //    "var tipo = document.getElementById('cboTipo'); tipo.disabled=false; " +
                //    "var valor = document.getElementById('cboValor'); valor.disabled=false;";
                //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                string javaScript = "habilitarCbo();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            }
        }

        protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gvDetalle.SelectedRow.Cells[1].Text);
            var db = (DataTable)Session["pedido"];
            detalleObj = null;
            foreach (DataRow row in db.Rows)
            {
                if (detalleObj == null)
                {
                    detalleObj = new DataTable("Carrito");
                    detalleObj.Columns.Add("nro_kpi", System.Type.GetType("System.Int32"));
                    detalleObj.Columns.Add("tipo_kpi", System.Type.GetType("System.String"));
                    detalleObj.Columns.Add("sector", System.Type.GetType("System.String"));
                    detalleObj.Columns.Add("tipo", System.Type.GetType("System.String"));
                    detalleObj.Columns.Add("valor", System.Type.GetType("System.String"));
                    detalleObj.Columns.Add("peso", System.Type.GetType("System.Int32"));
                }
                int es = Convert.ToInt32(row["nro_kpi"].ToString());
                if (es != id)
                {
                    DataRow fila = detalleObj.NewRow();
                    fila[0] = Convert.ToInt32(row["nro_kpi"].ToString());
                    fila[1] = Convert.ToString(row["tipo_kpi"].ToString());
                    fila[2] = Convert.ToString(row["sector"].ToString());
                    fila[3] = Convert.ToString(row["tipo"].ToString());
                    fila[4] = Convert.ToString(row["valor"].ToString());
                    fila[5] = Convert.ToInt32(row["peso"].ToString());
                    detalleObj.Rows.Add(fila);
                }
            }

            gvDetalle.DataBind();

            Session["pedido"] = detalleObj;
            Session["prueba"] = detalleObj;
            gvDetalle.DataSource = detalleObj;
            gvDetalle.DataBind();
            //lblTotal.Text = "Total (%): " + Convert.ToString(calcularTotal());
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            listarPolitica();
        }

        protected void gvPolitica_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nro_politica = Convert.ToInt32(gvPolitica.SelectedRow.Cells[1].Text);
            lblNroPolitica.Text = "Nro: " + nro_politica;
            txtIdPolitica.Text=nro_politica.ToString();
            nro_pol = nro_politica;

            string nombre_politica = gvPolitica.SelectedRow.Cells[2].Text;

            txtNombrePolitica.Text = nombre_politica;
            DateTime fecha = Convert.ToDateTime(gvPolitica.SelectedRow.Cells[3].Text);
            txtVigenciaDesde.Text = fecha.ToString("yyyy-MM-dd");
            //txtVigenciaHasta.Text = (gvPolitica.SelectedRow.Cells[4].Text).ToString();

            string javaScript = "editarPolitica();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

            ProductoKpi prod = new ProductoKpi();
            gvDetKpi.DataSource = prod.editarPolitica(nro_politica);
            gvDetKpi.DataBind();

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            ProductoKpi prod = new ProductoKpi();
            prod.NroPolitica = Convert.ToInt32(txtIdPolitica.Text);
            DateTime fecha = Convert.ToDateTime(txtVigenciaHasta.Text);
            prod.VigenciaHasta = fecha;
            prod.modificarPolitica();
            listarPolitica();

            txtVigenciaHasta.Text = "";
            gvDetKpi.DataBind();
            //string sms = "Politica" + prod.NroPolitica + " modificada...";
            //messageAlert(sms);

            string javaScript = "smsEditado();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }
    }
}