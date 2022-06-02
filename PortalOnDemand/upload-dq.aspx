<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="upload-dq.aspx.cs" Inherits="PortalOnDemand.upload_dq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
        }

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }
        function submitMigrationNew() {
            project = document.getElementById('<%=DropDownListProject.ClientID%>');
            projectId = project.value;
            projectName = project.options[project.selectedIndex].innerHTML;
            migrationFull = document.getElementById('<%=checkBoxMigrationFull.ClientID%>').checked;
            migrationType = document.getElementById('<%=SourceDB.ClientID%>').textContent;
            openmodal = document.getElementById('<%=openModal.ClientID%>');
            dbs = migrationType.split(",");
            sourceDb = dbs[0];
            targetDb = dbs[1];
            migrafull = ""
            list_id = ""
            if (!migrationFull) {
                migrafull = "Não"
                datasources = document.getElementById('<%=PnlDatasource.ClientID%>');
                inputs = datasources.querySelectorAll("input[type=checkbox]")
                for (i = 0; i < inputs.length; i++) {
                    var parts = inputs[i].id.split("_");
                    id_datasource = parts[parts.length - 1]
                    if (inputs[i].checked) {
                        list_id = list_id + id_datasource + ','
                    }
                }
                msglistIds = list_id
            }
            else {
                migrafull = "Sim"
                msglistIds = "Todos"
            }
            if ((list_id.substring(list_id.length - 1) == ',') && (list_id.length > 0)) {
                list_id = list_id.substring(0, list_id.length - 1);
            }
            openmodal.click();
            messagebody = document.getElementById('<%=idMsgErro.ClientID%>');
            message = "<p><h2> Migração do ambiente de <b><font color='red'>" + sourceDb + "</font></b> para o ambiente <b><font color='red'>" + targetDb + "</font></b></h2></p>"
            message = message + "<p><h2> Migração completa: " + migrafull + "</h2></p>"
            message = message + "<p><h2> Projeto que será migrado: <b><font color='red'>" + projectName + "</font></b></p></h2>"
            message = message + "<p><h2> Lista de Ids que serão migrados: <b><font color='red'>" + msglistIds + "</font></b></p></h2>"
            messagebody.outerHTML = message;
            modal = document.getElementById('<%=modalConfirm.ClientID%>').getElementsByTagName("button")
            var confirm = {}
            for (var i = 0; i < modal.length; i++) {
                if (modal[i].id == "ConfirmMigration") {
                    confirm = modal[i];
                    break;
                }
            }
            confirm.addEventListener("click", event => {
                if (PageMethods.get_path().toString().includes('.aspx')) {
                    PageMethods.set_path(PageMethods.get_path());
                }
                else {
                    PageMethods.set_path(PageMethods.get_path() + '.aspx');
                }
                PageMethods.submitMigration(parseInt(projectId), list_id, sourceDb, targetDb, OnSuccess, Failure);
            })

        }
        function OnSuccess(result) {
            alert(result);
        }
        function Failure(error) {
            alert(error)
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />

    <div class="card">
        <div class="card-header">
            Preenchimento Tabelas Data Quality
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col">
                    <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                    <small id="uploadHelp" class="form-text text-muted">Selecione a planilha para cadastro ou atualização do projeto no data quality</small>
                </div>
                <div class="col">
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Upload Arquivo" OnClick="Button1_Click1" UseSubmitBehavior="false" OnClientClick="javascript:ShowProgressBar();this.disabled='true'" />
                </div>

                <div class="col">
                    <asp:Button ID="Button2" class="btn btn-danger" runat="server" Text="Download" OnClick="Button2_Click" /><br />
                    <small id="uploadHelp2" class="form-text text-muted">Download Template padrão</small>

                </div>
            </div>
        </div>
    </div>

    <br>

    <div class="card">
        <div class="card-header">
            Migrar de projetos ou datasources
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col">
                    <asp:Button ID="btnMovDevProd" class="btn btn-primary" runat="server" Text="Migrar Dev para Prod" OnClick="move_db" CommandArgument="dev,prod" />
                    <br />
                </div>
                <div class="col">
                    <asp:Button ID="btnMovProdDev" class="btn btn-primary" runat="server" Text="Migrar Prod para Dev" OnClick="move_db" CommandArgument="prod,dev" />
                    <br />
                </div>
            </div>

            <br>

            <!-- parte da migrãção de dev para prod -->
            <div class="col-md-12" id="divDropDownDev4prod" runat="server" style="text-align: center;">
                <div class="row">
                    <div class="col">
                        <div class="alert-danger" id="Migration">
                            <label id="migrationMessage" runat="server" autopostback="true">Migração será de desenvolvimento para produção</label>
                        </div>
                    </div>
                </div>

                <br>

                <div class="row">
                    <div class="col">
                        <label id="textInfo" class="form-text">Informe o projeto que será migrado</label>
                        <asp:DropDownList ID="DropDownListProject" runat="server" Enabled="True" OnSelectedIndexChanged="loadDatasource" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col">
                        <label>Migração do projeto completa</label>
                        <asp:CheckBox ID="checkBoxMigrationFull" runat="server" CssClass="checkbox"
                            AutoPostBack="True"
                            TextAlign="Right"
                            Checked="true"
                            OnCheckedChanged="loadDatasource"></asp:CheckBox>
                    </div>
                    <asp:Label ID="SourceDB" runat="server" Style="visibility: hidden;" AutoPostBack="True" Text="dev,prod">
                    </asp:Label>
                </div>

                <br>

                <div class="row text-left">
                    <div class="col">
                        <asp:Label runat="server">Lista de datasources para o projeto selecionado</asp:Label>
                        <asp:Panel ID="PnlDatasource" CssClass="col-md-6 checkbox" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <!-- button submit -->
                </div>
                <div class="panel-footer" id="divSentMigration" runat="server" style="text-align: left;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
                    <asp:Button ID="bntSubmitMigration" class="btn btn-success" runat="server" Text="Submit" ClientIDMode="Static" UseSubmitBehavior="false" OnClientClick="javascript:submitMigrationNew();return false;" /><br />
                </div>
            </div>
        </div>
    </div>
    </div>




    <h3></h3>
    <a id="openModal" runat="server" data-target=".bd-example-modal-lg" href="#" data-toggle="modal"></a>
    <div id="modalConfirm" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" runat="server">
        <div id="modalbody" class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h3><b>Confirmação de migração</b></h3>
                </div>
                <div class="modal-body">
                    <asp:Label ID="idMsgErro" runat="server" Text="-"></asp:Label>
                    <p style=""></p>
                </div>
                <div class="modal-footer">
                    <button id="ConfirmMigration" class="btn btn-success btn-md" data-dismiss="modal">OK</button>
                    <button id="CancelMigration" class="btn btn-danger btn-md" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <div id="dvProgressBar" style="float: left; color: red; visibility: hidden;">
        <img src="img/process.gif" style="width: 50px; height: 50px;" /><br />
        Arquivo em processamento, não fecha esta janela. aguarde...
    </div>

    <div class="alert alert-danger" role="alert" id="mensagemErroExtensaoInvalida" runat="server">
        Erro - Seu arquivo não foi carregado, ele não possui extensão (.xls) ou (.xlsx).
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
    <div class="alert alert-danger" role="alert" id="ErroDQ" runat="server" style="text-align: center">
        <asp:Label ID="LabelErroDQ" runat="server" Text="Label" Style="text-align: center"></asp:Label>
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
</asp:Content>
