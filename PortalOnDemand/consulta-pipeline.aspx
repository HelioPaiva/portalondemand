<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="consulta-pipeline.aspx.cs" Inherits="PortalOnDemand.consulta_pipeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container body-content">
        <br />
        <h2>Pipeline</h2>
        <div class="col text-right">
            <a href="cadastro-pipeline.aspx" class="btn btn-primary">Novo Pipeline</a>
        </div>
        <br />


         <!--novo trecho com filtros -->
        <div class="row">
            <div class="form-group col-md-4">
                <asp:DropDownList ID="DropDownListPipeline" runat="server" class="form-control"></asp:DropDownList>
            </div>
            <div class="form-group col-md-3">
                <asp:Button ID="Button1" class="btn btn-info btn-sm" runat="server" Text="Filtrar" OnClick="Button1_Click" />
            </div>
        </div>
        <br />
        <!--Fim do novo trecho -->




        <div class="table-responsive">
            <asp:Table ID="Table1" runat="server" class="table">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Pipeline</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Perfil</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Projeto Data Quality</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Execução Manual </asp:TableHeaderCell>
                    <asp:TableHeaderCell>Cadastrado Em</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Ação</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel"><strong> ADI - On Demand</strong></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Pipeline Cadastrado Com Sucesso!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <% 
      
        if (Request.QueryString["status"] == "ok")
        {

        
   %>
    <script>
        $(document).ready(function (){
            $('#exampleModal').modal('show');
        });
    </script>
    <% }; %>

</asp:Content>



