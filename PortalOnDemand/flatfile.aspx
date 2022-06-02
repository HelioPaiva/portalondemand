<%@ Page Title="flatfile" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="flatfile.aspx.cs" Inherits="PortalOnDemand.flatfile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
        }

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }

        function VerificaTamanhoArquivo() {

            var fi = document.getElementById('<%= FileUpload1.ClientID %>');
            var maxFileSize = 104857600; // 100MB -> 100 * 1024 * 1024

            if (fi.files.length > 0) {

                for (var i = 0; i <= fi.files.length - 1; i++) {

                    var fsize = fi.files.item(i).size;

                    if (fsize < maxFileSize) {
                        //alert("arquivo ok");
                    }
                    else {
                        alert("Erro - O sistema só aceita arquivos com tamanho máximo de 100mb");
                        fi.value = null;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content">
        <h3>Selecione Datasource.</h3>
        <div class="row">
            <div class="col-md-4">
                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" onchange="Javascript: VerificaTamanhoArquivo();" />
                 <small id="pipelineHelp" class="form-text text-muted">Arquivos .csv,.txt,.xls,.xlsx tamanho máximo permitido 100mb</small>
            </div>
             <div class="col-md-6">
                <br />
                <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Upload Arquivo" OnClick="Button1_Click1" UseSubmitBehavior="false" OnClientClick="javascript:ShowProgressBar();this.disabled='true'" Style="margin-top: -20px;" />
            </div>
        </div>
        <!--
        <div class="row">
            <div class="col-md-6">
                <br />
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Upload Arquivo" OnClick="Button1_Click1" UseSubmitBehavior="false" OnClientClick="javascript:ShowProgressBar();this.disabled='true'" />
            </div>
        </div>
        -->
        <br />

        <div id="dvProgressBar" style="float: left; color:red; visibility: hidden;">
            <img src="img/process.gif" style="width: 50px; height: 50px;" /><br />
            Arquivo em processamento, não fecha esta janela. aguarde...
        </div>

        <div class="alert alert-danger" role="alert" id="mensagemErroExtensaoInvalida" runat="server">
            Erro - O sistema só aceita arquivos com a extensão (.csv) (.txt) (.xls) (.xlsx)
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>

        <div class="alert alert-danger" role="alert" id="mensagemErroSemArquivo" runat="server">
            Erro - Nenhum arquivo foi selecionado..
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="alert alert-warning" role="alert" id="mensagemErroNaoAutorizado" runat="server">
            Alerta - Este arquivo não foi localizado na base de dados data quality para upload.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroNoCaminho" runat="server">
            Erro - Caminho não encontrado dentro do azure
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroProcessamentoFileFlat" runat="server">
            Erro - Não foi possível carregar o arquivo
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="alert alert-success" role="alert" id="mensagemOK" runat="server">
            Sucesso - Seu arquivo foi enviado com sucesso.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="alert alert-danger" role="alert" id="ErroDQ" runat="server" style="text-align:center">
            <asp:Label ID="LabelErroDQ" runat="server" Text="Label" style="text-align:center"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
         <div class="alert alert-warning" role="alert" id="mensagemAlertaFlatFile" runat="server">
            Alerta - Você não tem acesso a esta fucnionalidade.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>

         <div class="alert alert-warning" role="alert" id="alertaInProgress" runat="server">
            Alerta - Este pipeline já está em execução. Verifique o status no painel abaixo.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>

        <!--Novo código monitor de acompanhamento-->
        <div id="painelMonitor" runat="server">

            <hr />
            <h3 style="text-align: center">Validação datasource em execução</h3>

            <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

            <asp:LinkButton ID="btnAtualizarParcial"
                runat="server"
                Text="Refresh"
                OnClick="Atualizar_Click"
                class="btn btn-info btn-xs">
               <span class="glyphicon glyphicon-repeat"></span>
            </asp:LinkButton>&nbsp;Refresh

            <br />
            <br />

            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="row">
                        <div class="form-group col-md-4"></div>
                        <div class="form-group col-md-4">
                            <div id="dvProgressBar" style="float: left; color: red;">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="img/process.gif" style="width: 50px; height: 50px;" /><br />
                                Processando aguarde...
                            </div>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>


            <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Table ID="Table1" runat="server" class="table">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>DataSource</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Início</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Duração</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Erro</asp:TableHeaderCell>
                            <asp:TableHeaderCell>ID Pipeline</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblPipeline" runat="server" Text="-"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblInicioExecucao" runat="server" Text="-"></asp:Label>
                                <asp:Label ID="lblInicioExecucaoVisual" runat="server" Text="-"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblDuracao" runat="server" Text="-"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>

                                <asp:Label ID="lblStatus" runat="server" Text="-"></asp:Label>

                                <div id="statusEmProgresso" runat="server" style="width: 200px;" class="rounded-circle">
                                    <a href="#" class="btn btn-primary btn-xs">
                                        <span class="glyphicon glyphicon-refresh"></span>
                                    </a>
                                    &nbsp;&nbsp;&nbsp;Em processamento
                                </div>
                                <div id="statusOK" runat="server" style="width: 200px;">
                                    <a href="#" class="btn btn-success btn-xs">
                                        <span class="glyphicon glyphicon-ok"></span>
                                    </a>
                                    &nbsp;&nbsp;&nbsp;Concluído
                                </div>

                                <div id="statusEmFile" runat="server" style="width: 200px;">
                                    <a href="#" class="btn btn-info btn-xs">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </a>
                                    &nbsp;&nbsp;&nbsp;Em Fila
                                </div>

                                <div id="statusCancelado" runat="server" style="width: 200px;">
                                    <a href="#" class="btn btn-warning btn-xs">
                                        <span class="glyphicon glyphicon-ban-circle"></span>
                                    </a>
                                    &nbsp;&nbsp;&nbsp;Cancelado
                                </div>
                                <div id="statusErro" runat="server" style="width: 200px;">
                                    <a href="#" class="btn btn-danger btn-xs">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </a>
                                    &nbsp;&nbsp;&nbsp;Falhou
                                </div>
                            </asp:TableCell>
                            <asp:TableCell>
                                <div id="iconMsgErro" runat="server">
                                    <a href="#" data-toggle="modal" data-target=".bd-example-modal-lg"><span class="glyphicon glyphicon-comment" runat="server"></span></a>
                                </div>

                                <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <b>Erro Pipeline</b>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Label ID="idMsgErro" runat="server" Text="-"></asp:Label>
                                            </div>
                                            <div class="modal-footer">
                                                <button class="btn btn-danger btn-md" data-dismiss="modal">OK</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="statusErroMsg" runat="server" style="width: 200px;">
                                    <a href="#" class="btn btn-danger btn-xs" data-toggle="modal" data-target="#myModal">
                                        <span class="glyphicon glyphicon-alert"></span>
                                    </a>
                                </div>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblIdPipeline" runat="server" Text="-"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAtualizarParcial" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!-- Fim do Monitor-->



    </div>
</asp:Content>
