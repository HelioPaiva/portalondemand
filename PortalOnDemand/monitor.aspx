<%@ Page Title="monitor" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="monitor.aspx.cs" Inherits="PortalOnDemand.monitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            document.getElementById('dvProgressBar').style.display = 'none';
        }
        function Mudarestado(el) {
            var display = document.getElementById(el).style.display;
            if (display == "none")
                document.getElementById(el).style.display = 'block';
            else
                document.getElementById(el).style.display = 'none';
        }
    </script>

     <script type="text/javascript">
         function ShowProgressBar() {
             document.getElementById('dvProgressBar').style.display = 'block';
         }

         function HideProgressBar() {
             document.getElementById('dvProgressBar').style.display = 'none';
         }
     </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--<div class="container body-content">-->
    <!--ok -->
        <br />
        <h4>Pipeline Runs</h4>
        <div id="painelMonitor" runat="server">
            <div class="row">
                <div class="form-group col-md-2" runat="server">
                    <asp:DropDownList ID="DropDownListPeriodo" runat="server" class="form-control">
                        <asp:ListItem>Ultimas 24 horas</asp:ListItem>
                        <asp:ListItem>Ultimos 7 dias</asp:ListItem>
                    </asp:DropDownList>
                 </div>
                 <div class="form-group col-md-3">
                    <h6>
                        <asp:Label ID="lblPeriodo" runat="server" Text="Label"></asp:Label>
                        <b>
                            (<asp:Label ID="lblInicioPeriodo" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="LalblFimPeriodobel" runat="server" Text="Label"></asp:Label>)
                        </b>
                    </h6>
                </div>
                <div class="form-group col-md-4">
                    <asp:DropDownList ID="DropDownListPipeline" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    <asp:Button ID="Button1" class="btn btn-primary btn-sm" runat="server" Text="Filtrar"  OnClick="Button1_Click1"  UseSubmitBehavior="false" OnClientClick="javascript:ShowProgressBar();this.disabled='true'"  />   
                </div>
            </div>
            <br />
            <div class="row">
                <div class="form-group col-md-2">
                    <asp:LinkButton ID="btnAtualizarParcial"
                        runat="server"
                        Text="Refresh"
                        OnClick="Atualizar_Click"
                        UseSubmitBehavior="false" 
                        OnClientClick="javascript:ShowProgressBar();this.disabled='true'"
                        class="btn btn-info btn-xs">
                        <span class="glyphicon glyphicon-repeat"></span>
                    </asp:LinkButton>&nbsp;Refresh
                </div>   
            </div>
            </div>

            <div class="row">
                <div class="form-group col-md-4"></div>
                <div class="form-group col-md-4">
                    <div id="dvProgressBar" style="float: left; color:red;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="img/process.gif" style="width: 50px; height: 50px;" /><br />
                        Processando aguarde...
                    </div>
                </div>
            </div>
            
            <div id="tabela" visibility: hidden;">
                <asp:Panel ID="Panel1" runat="Server"
                             ScrollBars="Auto">

                <asp:Table ID="Table1" runat="server" class="table">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Pipeline</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Início</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Duração</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Erro</asp:TableHeaderCell>
                        <asp:TableHeaderCell>ID Pipeline</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Usuário</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
                    </asp:Panel>
            </div>
        <!--</div>-->
</asp:Content>
