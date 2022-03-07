<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmProducto.aspx.cs" Inherits="capaPresentacion.frmProducto" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>KPI PRODUCTO</title>
    <link href="public/css/style.css" rel="stylesheet"/>
    <link href="public/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="public/js/jquery-3.6.0.min.js"></script>
    <script src="public/js/kpi.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <div class="navbar navbar-inverse navbar-fixed-top" style="background-color: #46546C;">
        <div class="container">
            <div class="navbar-header">
                <h3 style="color:white">PARAMETRIZACION DE CARGOS PARA COMISIONES</h3>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-xs-9">
            <br />
            <br />
            <div class="form-group row">
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtBuscar" class="form-control" placeholder="Nombre politica.." runat="server"></asp:TextBox>
                      <asp:Button ID="btnBuscar" class="btn btn-info text-white position-relative" runat="server" Text="Buscar" OnClick="btnBuscar_Click"/>
                </div>
            </div>
            <div style="height:400px; overflow:auto">
                <asp:GridView ID="gvPolitica" runat="server" 
                    AutoGenerateColumns="false" 
                    ClientIDMode="Static"
                    CssClass="mGrid" 
                    AutoPostBack="true"
                    OnSelectedIndexChanged="gvPolitica_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" HeaderText="Opcion" SelectText="Editar"/>
                        <asp:BoundField DataField="nro_politica" HeaderText="Nro Politica"/>
                        <asp:BoundField DataField="nombre_politica" HeaderText="Politica"/>
                        <asp:BoundField DataField="vigencia_desde" DataFormatString="{0:d}" HeaderText="Vigencia desde"/>
                        <asp:BoundField DataField="vigencia_hasta" DataFormatString="{0:d}" HeaderText="Vigencia hasta"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-md-8 col-xs-12">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-body">       
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="exampleInputEmail1" class="form-label">Nombre Política</label>
                                <div class="input-group mb-3">
                                    <span class="input-group-text" id="basic-addon2"><asp:Label ID="lblNroPolitica" runat="server" Text=""></asp:Label></span>
                                    <asp:TextBox ID="txtNombrePolitica" class="form-control" placeholder="Nombre politica.." runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtIdPolitica" class="form-control" Type="hidden"  runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label for="exampleInputPassword1" class="form-label">Fecha desde...</label>
                                <asp:TextBox ID="txtVigenciaDesde" class="form-control" type="date" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <label for="exampleInputPassword1" class="form-label">Fecha hasta...</label>
                                <asp:TextBox ID="txtVigenciaHasta" class="form-control" type="date" runat="server"></asp:TextBox>
                            </div>

                            <hr />
                            <div class="col-md-3">
                                <label for="exampleInputPassword1" class="form-label">Tipo KPI</label>
                                <asp:DropDownList ID="cboTipoKpi" class="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTipoKpi_SelectedIndexChanged">
                                    <asp:ListItem Text="VENTAS" Value="VENTAS" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="COBERTURA" Value="COBERTURA"></asp:ListItem>
                                    <asp:ListItem Text="TICKET PROMEDIO" Value="TICKET PROMEDIO"></asp:ListItem>
                                    <asp:ListItem Text="EFECTIVIDAD X VISITA" Value="EFECTIVIDAD X VISITA"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label for="exampleInputPassword1" class="form-label">Sector</label>
                                <asp:ListBox ID="cboSector" class="form-select" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputPassword1" class="form-label">Tipo</label>
                                <asp:DropDownList ID="cboTipo" class="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTipo_SelectedIndexChanged">
                                    <asp:ListItem Text="TODOS" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="DIVISION" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="SECTOR" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="MARCA" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="CATEGORIA" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="GRUPO" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="FAMILIA" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label for="exampleInputPassword1" class="form-label">Valor</label>
                                <asp:DropDownList ID="cboValor" class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                            </div>

                            <div class="col-md-12">
                                <asp:Button ID="btnagregar" class="btn btn-info text-white position-relative" runat="server" Text="Agregar" OnClick="btnagregar_Click"/>
                            </div>
                            <hr />
                        </div>
                        <div class="form-group row">
                            <asp:GridView ID="gvDetalle" runat="server" 
                                AutoGenerateColumns="false" 
                                GridLines="None" 
                                AllowPaging="true" 
                                CssClass="mGrid" 
                                PagerStyle-CssClass="pgr" 
                                AlternatingRowStyle-CssClass="alt" OnSelectedIndexChanged="gvDetalle_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" HeaderText="Opcion" SelectText="Quitar"/>
                                    <asp:BoundField DataField="nro_kpi" HeaderText="Nro KPI"/>
                                    <asp:BoundField DataField="tipo_kpi" HeaderText="Tipo KPI"/>
                                    <asp:BoundField DataField="sector" HeaderText="Sector"/>
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo"/>
                                    <asp:BoundField DataField="valor" HeaderText="Valor"/>
                                    <asp:TemplateField HeaderText="Peso(%)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtpeso" TextMode="Number" class="form-control" runat="server">0</asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvDetKpi" runat="server" 
                                AutoGenerateColumns="false" 
                                GridLines="None" 
                                AllowPaging="True" 
                                CssClass="mGrid" 
                                PagerStyle-CssClass="pgr" 
                                AlternatingRowStyle-CssClass="alt" OnSelectedIndexChanged="gvDetalle_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="nro_kpi" HeaderText="Nro KPI"/>
                                    <asp:BoundField DataField="tipo_kpi" HeaderText="Tipo KPI"/>
                                    <asp:BoundField DataField="sector" HeaderText="Sector"/>
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo"/>
                                    <asp:BoundField DataField="valor" HeaderText="Valor"/>
                                    <asp:BoundField DataField="peso" HeaderText="Peso"/>
                                </Columns>
                            </asp:GridView>
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="100px"></asp:TableCell>
                                    <asp:TableCell Width="100px"></asp:TableCell>
                                    <asp:TableCell Width="100px"></asp:TableCell>
                                    <asp:TableCell Width="100px"></asp:TableCell>
                                    <asp:TableCell Width="100px">
                                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table> 
                        </div>
                        <div class="d-grid gap-2 d-md-flex justify-content-md-end" style='width:96%;margin-left: 2.2%'>
                            <asp:Button ID="btnCancelar" class="btn btn-success me-md-2 text-white" runat="server" Text="Nuevo" OnClick="btnCancelar_Click"/>
                            <asp:Button ID="btnGuardar" class="btn btn-info btn-lg text-white" runat="server" Text="Guardar" OnClick="btnGuardar_Click"/>
                            <asp:Button ID="btnModificar" class="btn btn-warning btn-lg text-white" runat="server" Text="Modificar" OnClick="btnModificar_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</div>
 </form>
</body>
</html>
