<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobile.aspx.cs" Inherits="PortalOnDemand.mobile3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ADI - On Demand</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" href="/docs/4.0/assets/img/favicons/favicon.ico" />


    <link rel="canonical" href="https://getbootstrap.com/docs/4.0/examples/offcanvas/" />

    <!-- Bootstrap core CSS -->
    <link href="mobile/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="mobile/offcanvas.css" rel="stylesheet" />

</head>
<body class="bg-light">
    <nav class="navbar navbar-expand-md fixed-top navbar-dark bg-dark">
        <a class="navbar-brand" href="~/">ADI - On Demand</a>
        <button class="navbar-toggler p-0 border-0" type="button" data-toggle="offcanvas">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="navbar-collapse offcanvas-collapse" id="navbarsExampleDefault">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                    <a class="nav-link" href="#"><span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" href="~/">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" href="pipeline.aspx">Pipeline</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" href="monitor.aspx">Monitor Pipeline</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" href="mobile.aspx">Monitor Mobile</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" href="flatfile.aspx">Flat File</a>
                </li>
               
                
            </ul>

            <ul class="navbar-nav ml-auto">

                 <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownAjuda" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
          Ajuda
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdownAjuda">
          <a class="dropdown-item" runat="server" href="manual.aspx" target="_blank">Manual de Utilização</a>
          <a class="dropdown-item" runat="server" href="https://nestle.service-now.com/sp?id=sc_cat_item&sys_id=986cb28b1bb70850cb52773e0d4bcbee" target="_blank">Abertura de Incidentes</a>
             <a class="dropdown-item" runat="server" href="https://nestle.service-now.com/sp?id=sc_cat_item&sys_id=816afb231b37c010cb52773e0d4bcb03" target="_blank">Abertura de Requisições</a>
        </div>
      </li>
                 <%  
                            if (Session["idPerfil"].ToString() == "4" && !String.IsNullOrEmpty(Session["idPerfil"].ToString()))
                            {
                        %>
                 <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownConfig" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
          Configurações
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdownConfig">
          <a class="dropdown-item" href="consulta-usuario.aspx">Cadastro Usuário</a>
            <a class="dropdown-item" href="consulta-pipeline.aspx">Cadastro Pipeline</a>
            <a class="dropdown-item" href="consulta-acesso.aspx">Cadastro Acesso</a>
          <div class="dropdown-divider"></div>
          <a class="dropdown-item" href="consulta-log.aspx">Consultar Log</a>
            <a class="dropdown-item" href="upload-dq.aspx">Gestão do DataQuality</a>
        </div>
      </li>
                <%  }  %>
                 <li class="nav-link text-white"><% Response.Write(Session["usuario"].ToString()); %></li>
                    </ul>
                        
        </div>
    </nav>

    <form id="form1" runat="server">
        <main role="main" class="container">
            <h1 class="my-1 p-1" style="color:red; text-align:center;">Página em desenvolvimento</h1>
            <h4 class="my-1 p-1">Pipelines Runs</h4>
            <div class="row">

                <div class="col">
                    <asp:DropDownList ID="DropDownListPeriodo" runat="server" class="form-control form-control-sm">
                        <asp:ListItem>ultimas 24 horas</asp:ListItem>
                        <asp:ListItem>ultimos 7 dias</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <!--
                <asp:Label ID="lblPeriodo" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblInicioPeriodo" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="LalblFimPeriodobel" runat="server" Text="Label"></asp:Label>
                -->

                 <div class="col">
                    <asp:DropDownList ID="DropDownListPipelines" runat="server" class="form-control form-control-sm text-lowercase">
                        <asp:ListItem>Selecione Todos</asp:ListItem>
                        <asp:ListItem>pl_master-origenes_op_fechamento</asp:ListItem>
                        <asp:ListItem>MESTRE_PEC_VENDAS</asp:ListItem>
                        <asp:ListItem>ORIGENES_FATOS</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col">
                    <asp:Button ID="Button1" class="btn btn-info btn-xs" runat="server" Text="Filtrar"  OnClick="Button2_Click2"  UseSubmitBehavior="true" OnClientClick="javascript:ShowProgressBar();this.disabled='true'"  />   
                      </div>
            </div>
            <br />
     
            <div class="row">
                <div class="col text-center">
                    <!--<button type="button" class="btn btn-primary btn-sm">Refresh</button>-->

                    <asp:LinkButton ID="btnAtualizarParcial"
                    runat="server"
                    Text="Refresh"
                    OnClick="Atualizar_Click1"
                    UseSubmitBehavior="false" 
                    OnClientClick="javascript:ShowProgressBar();this.disabled='true'"
                    class="btn btn-info btn-xs">
                </asp:LinkButton>
                </div>
            </div>
            <hr />

            <!-- Laço -->
            <asp:PlaceHolder ID="div_pipeline" runat="server">
            </asp:PlaceHolder>
            
            <!--
            <div class="my-3 p-3 bg-white rounded box-shadow">
                <div class="row text-lowercase">
                    <div class="col-md-2">status:&nbsp;&nbsp;<img src="mobile/img/icon-success.svg" title="Succeeded" /></div>
                    <div class="col-md-4">name:&nbsp;&nbsp;pl_master-origenes_op_fechamento</div>
                    <div class="col-md-3">início:&nbsp;&nbsp;20/04/22&nbsp;08:23</div>
                    <div class="col-md-3">duração:&nbsp;&nbsp;00:23:00</div>
                </div>
            </div>
           
            <div class="my-3 p-3 bg-white rounded box-shadow">
                <div class="row text-lowercase">
                    <div class="col-md-2">status:&nbsp;&nbsp;<img src="mobile/img/icon-progress.svg" title="In progress"/></div>
                    <div class="col-md-4">name:&nbsp;&nbsp;MESTRE_PEC_VENDAS</div>
                    <div class="col-md-3">início:&nbsp;&nbsp;20/04/22&nbsp;08:23</div>
                    <div class="col-md-3">duração:&nbsp;&nbsp;00:23:00</div>
                </div>
            </div>
           
            <div class="my-3 p-3 bg-white rounded box-shadow">
                <div class="row text-lowercase">
                    <div class="col-md-2">status:&nbsp;&nbsp;<img src="mobile/img/icon-error.svg" title="Failed"/></div>
                    <div class="col-md-4">name:&nbsp;&nbsp;ORIGENES_FATOS</div>
                    <div class="col-md-3">início:&nbsp;&nbsp;20/04/22&nbsp;08:23</div>
                    <div class="col-md-3">duração:&nbsp;&nbsp;00:23:00</div>
                </div>
            </div>
                -->
        </main>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script>window.jQuery || document.write('<script src="mobile/assets/js/vendor/jquery-slim.min.js"><\/script>')</script>
    <script src="mobile/assets/js/vendor/popper.min.js"></script>
    <script src="mobile/dist/js/bootstrap.min.js"></script>
    <script src="mobile/assets/js/vendor/holder.min.js"></script>
    <script src="mobile/js/offcanvas.js"></script>
</body>
</html>
