<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobile2.aspx.cs" Inherits="PortalOnDemand.mobile2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ADI - On Demand</title>
    <link rel="icon" href="img/iconePaginaNestle.jpg" type="imagem/gif">
    <link rel="canonical" href="https://getbootstrap.com/docs/4.6/examples/navbar-fixed/">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" integrity="sha384-B0vP5xmATw1+K9KRQjQERJvTumQW0nPEzvF6L/Z6nronJ3oUOFUFpCjEUQouq2+l" crossorigin="anonymous">
    <style>
        .bd-placeholder-img {
            font-size: 1.125rem;
            text-anchor: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

        @media (min-width: 768px) {
            .bd-placeholder-img-lg {
                font-size: 3.5rem;
            }
        }

        /* Show it is fixed to the top */
        body {
            min-height: 75rem;
            padding-top: 4.5rem;
        }
    </style>

</head>
<body>
    <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <a class="navbar-brand" href="index.php">ADI - On Demand
        </a>
        <!--
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
        
  <div class="collapse navbar-collapse" id="navbarCollapse">
  <ul class="navbar-nav mr-auto">
    <li class="nav-item active">
          <a class="nav-link" href="~/">Home<span class="sr-only">(current)</span></a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="pipeline.aspx">Pipeline</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="monitor.aspx">Monitor Pipeline</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="flatfile.aspx">Flat File</a>
        </li>
      </ul>
  </div>
        -->
    </nav>
    <form id="form1" runat="server">
        <div>
            <table class="table">
                <thead>
                    <tr style="text-align: center;">
                        <th scope="col">Pipeline</th>
                        <th scope="col">Início</th>
                        <th scope="col">Duração</th>
                        <th scope="col">Status</th>
                        <th scope="col">Erro</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style="text-align: center;">
                        <td style="vertical-align: middle;"><strong>rank</strong></td>
                        <td style="vertical-align: middle;">nonono</td>
                        <td style="vertical-align: middle;"><strong>nonono</strong></td>
                        <td style="vertical-align: middle;">nonono</td>
                        <td style="vertical-align: middle;">nonono</td>

                        <tr style="text-align: center; font-size: 12px; border-bottom: 6pt solid #DCDCDC;">
                            <td colspan="5">Qtd.Primeiro Da Rodada: <strong>nonono</strong></td>
                        </tr>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
