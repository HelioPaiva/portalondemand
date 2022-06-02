<%@ Page EnableEventValidation="false" Title="pipeline" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="pipeline.aspx.cs" Inherits="PortalOnDemand.pipeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-3.4.1.min.js"></script>
    <script>
        function apenasNumeros(evt) {
            // Apenas números
            if (window.event) { // IE
                var charCode = evt.keyCode;
            } else if (evt.which) { // Safari 4, Firefox 3.0.4
                var charCode = evt.which
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content">
        <h3></h3>
        <div class="row">
            <div class="form-group col-md-4">
                <label>Selecione Pipeline</label>
                <asp:DropDownList ID="DropDownListPipeline" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPipeline_SelectedIndexChanged"></asp:DropDownList>
                <small id="pipelineHelp" class="form-text text-muted">pipeline disponiveis para este usuário.</small>
            </div>

            <!--
            <div id="dataVDE" runat="server">
                <div class="form-group col-md-3">
                    <label>Período Retroativo (YYYYMM)</label>
                    <asp:TextBox ID="txtData" runat="server" class="form-control" MaxLength="6" onkeypress="return apenasNumeros(event);"></asp:TextBox>
                    <small style="color:red;" id="pipelineVDE" class="form-text text-muted">Apenas para pipeline VDE_MESTRE</small>
                </div>
            </div>
            -->

            <!--
             <div id="dataSelloutDataLab" runat="server">
                <div class="form-group col-md-3">
                    <label>Período Retroativo (YYYYMM)</label>
                    <asp:TextBox ID="txtDataSellout" runat="server" class="form-control" MaxLength="6" onkeypress="return apenasNumeros(event);"></asp:TextBox>
                    <small style="color:red;" id="pipelineSelloutDataLab" class="form-text text-muted">Apenas para pipeline pl_master_sellout-b2c_online</small>
                </div>
            </div>
            -->

        </div>
        <div class="row">
            <div class="form-group col-md-12">
                <hr />
                <label id="lineParam0" runat="server">Parâmetros</label>
                <asp:Table runat="server" ID="idTableParametro" Width="802px">
                    <asp:TableRow ID="lineParam1">
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="namePara1" BorderStyle="None" ReadOnly="true" Enabled="False" Width="80%" /></asp:TableCell><asp:TableCell>
                                <asp:TextBox ID="Param_1" runat="server" class="form-control" Width="100%"></asp:TextBox></asp:TableCell><asp:TableCell>
                                    <asp:TextBox runat="server" ID="ajudaPara1" BorderStyle="None" ReadOnly="true" Enabled="False" Width="100%" Style="color: red;" Font-Size="12px" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="lineParam2">
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="namePara2" BorderStyle="None" ReadOnly="true" Enabled="False" Width="80%" /></asp:TableCell><asp:TableCell>
                                <asp:TextBox ID="Param_2" runat="server" class="form-control" Width="100%"></asp:TextBox></asp:TableCell><asp:TableCell>
                                    <asp:TextBox runat="server" ID="ajudaPara2" BorderStyle="None" ReadOnly="true" Enabled="False" Width="100%" Style="color: red;" Font-Size="12px" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="lineParam3">
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="namePara3" BorderStyle="None" ReadOnly="true" Enabled="False" Width="80%" /></asp:TableCell><asp:TableCell>
                                <asp:TextBox ID="Param_3" runat="server" class="form-control" Width="100%"></asp:TextBox></asp:TableCell><asp:TableCell>
                                    <asp:TextBox runat="server" ID="ajudaPara3" BorderStyle="None" ReadOnly="true" Enabled="False" Width="100%" Style="color: red;" Font-Size="12px" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="lineParam4">
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="namePara4" BorderStyle="None" ReadOnly="true" Enabled="False" Width="80%" /></asp:TableCell><asp:TableCell>
                                <asp:TextBox ID="Param_4" runat="server" class="form-control" Width="100%"></asp:TextBox></asp:TableCell><asp:TableCell>
                                    <asp:TextBox runat="server" ID="ajudaPara4" BorderStyle="None" ReadOnly="true" Enabled="False" Width="100%" Style="color: red;" Font-Size="12px" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="lineParam5">
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="namePara5" BorderStyle="None" ReadOnly="true" Enabled="False" Width="80%" /></asp:TableCell><asp:TableCell>
                                <asp:TextBox ID="Param_5" runat="server" class="form-control" Width="100%"></asp:TextBox></asp:TableCell><asp:TableCell>
                                    <asp:TextBox runat="server" ID="ajudaPara5" BorderStyle="None" ReadOnly="true" Enabled="False" Width="100%" Style="color: red;" Font-Size="12px" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <div class="form-group col-md-2">
                    <label></label>
                    <br />
                    <asp:Button ID="Button1" class="btn btn-primary btn-sm" runat="server" Text="Processar" OnClick="Button1_Click" Style="margin-top: 7px;" />
                </div>
            </div>

        </div>

        <div class="alert alert-success" role="alert" id="mensagemOkPipeline" runat="server">
            Sucesso - Pipeline em execução. Acompanhe os logs no seu e-mail ou através do monitor abaixo.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div class="alert alert-warning" role="alert" id="mensagemAlertaPipeline" runat="server">
            Alerta - Você não tem pipelines disponíveis.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroListaPipeline" runat="server">
            Erro - Selecione um pipeline.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroProcessamentoPipeline" runat="server">
            Erro - Não foi possivel processar o pipeline.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroConexao" runat="server">
            Erro - Não foi possível obter a conexão.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div class="alert alert-warning" role="alert" id="alertaInProgress" runat="server">
            Alerta - Este pipeline já está em execução. Verifique o status no painel abaixo.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div class="alert alert-danger" role="alert" id="PeridoVDE" runat="server">
            Erro - Preencha o campo Período Retroativo.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div class="alert alert-danger" role="alert" id="alertaParametro" runat="server">
            Erro - Preencha todos os parâmetros para execução deste pipeline.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
        </div>
        <div id="painelMonitor" runat="server">

            <hr />
            <h3 style="text-align: center">Pipeline em execução</h3>
            <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

            <asp:LinkButton ID="btnAtualizarParcial"
                runat="server"
                OnClick="Atualizar_Click"
                class="btn btn-info btn-xs">
                <div id="btnRefresh" runat="server" style="width: 100px;" class="rounded-circle">
                    Refresh
                </div>
            </asp:LinkButton>
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
                            <asp:TableHeaderCell>Pipeline</asp:TableHeaderCell><asp:TableHeaderCell>Início</asp:TableHeaderCell><asp:TableHeaderCell>Duração</asp:TableHeaderCell><asp:TableHeaderCell>Status</asp:TableHeaderCell><asp:TableHeaderCell>Erro</asp:TableHeaderCell><asp:TableHeaderCell>ID Pipeline</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblPipeline" runat="server" Text="-"></asp:Label>
                            </asp:TableCell><asp:TableCell>
                                <asp:Label ID="lblInicioExecucao" runat="server" Text="-"></asp:Label>
                                <asp:Label ID="lblInicioExecucaoVisual" runat="server" Text="-"></asp:Label>
                            </asp:TableCell><asp:TableCell>
                                <asp:Label ID="lblDuracao" runat="server" Text="-"></asp:Label>
                            </asp:TableCell><asp:TableCell>

                                <asp:Label ID="lblStatus" runat="server" Text="-"></asp:Label>

                                <div id="statusEmProgresso" runat="server" style="width: 200px;" class="rounded-circle">
                                    <img src='mobile/img/icon-progress.svg' title='In progress' />
                                    &nbsp;&nbsp;&nbsp;Em processamento
                                </div>

                                <div id="statusOK" runat="server" style="width: 200px;">
                                    <img src='mobile/img/icon-success.svg' title='Succeeded' />
                                    &nbsp;&nbsp;&nbsp;Concluído
                                </div>

                                <div id="statusEmFile" runat="server" style="width: 200px;">
                                    <img src='mobile/img/icon-fila.svg' title='fila' />
                                    &nbsp;&nbsp;&nbsp;Em Fila
                                </div>

                                <div id="statusCancelado" runat="server" style="width: 200px;">
                                    <img src='mobile/img/icon-cancel.svg' title='Cancelled' />
                                    &nbsp;&nbsp;&nbsp;Cancelado
                                </div>

                                <div id="statusErro" runat="server" style="width: 200px;">
                                    <img src='mobile/img/icon-error.svg' title='Failed' />
                                    &nbsp;&nbsp;&nbsp;Falhou
                                </div>

                            </asp:TableCell><asp:TableCell>
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
                            </asp:TableCell><asp:TableCell>
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
    </div>
</asp:Content>


