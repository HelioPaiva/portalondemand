<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PortalOnDemand._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content">
        
        <img src="img/nestle-logo_2.png" class="img-responsive" alt="Responsive image" style="padding-top: 35px;" />

        <br />

         <div class="alert alert-warning" role="alert" id="mensagemAlertaAcesso" runat="server">
            <center><h4>Sem acesso - Você ainda não tem acesso ao portal, Solicite a equipe IT ADI.</h4></center>
        </div>

        <div id="acessoLiberado" runat="server">

        <div class="row" style="padding-top: 35px;">
            <div class="col-md-4">
                <h2>Pipeline</h2>
                <p>
                    Pelo portal ADI - On Demand, você consegue executar o processo de atualização de suas bases de forma manual.
                    Os pipelines são disponibilizados conforme regra de acesso por usuário aplicada pela equipe de ADI.
                </p>
                <p><a class="btn btn-primary" href="pipeline.aspx">Executar Pipeline</a></p>
            </div>
            
            <div class="col-md-4">
                <h2>Monitor</h2>
                <p>
                    Através do monitor é possível acompanhar o status de todos os pipelines inclusive os que são executados de forma automática.
                    Os pipelines são disponibilizados conforme regra de acesso por usuário.
                </p>
                 <p><a class="btn btn-primary" href="monitor.aspx">Visualizar Pipelines</a></p>
            </div>
            
            <div class="col-md-4">
                <h2>Flat File</h2>
                <p>
                    Com o Flat file você consegue fazer o upload dos arquivos (.txt) ou (.csv) que serão usados na atualização dos relatórios.
                    Os arquivos para upload são validados atraves do usuário logado.
                </p>
                <p><a class="btn btn-primary" href="flatfile.aspx">Fazer Upload </a></p>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
