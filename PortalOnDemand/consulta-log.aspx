<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="consulta-log.aspx.cs" Inherits="PortalOnDemand.consulta_log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content">
         <br />
        <h2>Log</h2>
        <br />
         <!--novo trecho com filtros -->
        <div class="row">
            <div class="form-group col-md-4">
                <asp:DropDownList ID="DropDownListUsuario" runat="server" class="form-control"></asp:DropDownList>
            </div>
            <div class="form-group col-md-3">
                <asp:Button ID="Button1" class="btn btn-info btn-sm" runat="server" Text="Filtrar" OnClick="Button1_Click" />
            </div>
        </div>
        <br />
        <!--Fim do novo trecho -->
        <div class="table-responsive">
            <small id="emailHelp" class="form-text text-muted" style="color:red">Registros dos últimos 30 dias</small>
            <asp:Table ID="Table1" runat="server" class="table">
                <asp:TableHeaderRow>
                    
                    <asp:TableHeaderCell>Usuário</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Processo</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Nome</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Início Execução</asp:TableHeaderCell>
                    <asp:TableHeaderCell>ID Pipeline</asp:TableHeaderCell>
                   
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </div>
</asp:Content>
