<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="editar-pipeline.aspx.cs" Inherits="PortalOnDemand.editar_pipeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content">
        <br />
        <h3>Atualizar Pipeline</h3>
        <div class="row">
            <div class="form-group col-md-3">
                <label>Pipeline</label>
                <asp:TextBox ID="txtPipeline" runat="server" class="form-control" required=""></asp:TextBox>
            </div>
            <div class="form-group col-md-2">
                <label>Perfil</label>
                <asp:DropDownList ID="DropDownListPerfil" runat="server" class="form-control" required=""></asp:DropDownList>
            </div>
            <div class="form-group col-md-4">
                <label>Execução Manual ?</label>
                <asp:DropDownList ID="DropDownListExecucao" runat="server" class="form-control" required="">
                    <asp:ListItem Text="Selecione.." Value="Selecione"></asp:ListItem>
                    <asp:ListItem Text="Sim" Value="Sim"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="Não"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-2">
                <label>Projeto Data Quality</label>
                <asp:DropDownList ID="DropDownListProjeto" runat="server" class="form-control"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="form-group col-md-6">
                <label>Tem Parâmetros ?</label>
                <!--<asp:TextBox ID="txtTemParametros" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>-->
                
                 <asp:DropDownList ID="DropDownListParametros" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownListParametros_SelectedIndexChanged">
                    <asp:ListItem Text="Sim" Value="Sim"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="Não"></asp:ListItem>
                </asp:DropDownList>
                
                
            </div>
        </div>

        <div class="row">
             <div id="qtdParametros" runat="server">
                <div class="form-group col-md-6">
                    <label>Selecione a quantidade de parâmetros</label>
                    <!--<asp:TextBox ID="txtQtdParametros" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>-->
                   
                     <asp:DropDownList ID="DropDownListQtdParametros" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownListQtdParametros_SelectedIndexChanged">
                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    
                </div>
            </div>

        </div>

        
        <div id="param_1" runat="server">
            <div class="row">
                <div class="form-group col-md-3">
                    <label>1º Parâmetro</label>
                    <asp:TextBox ID="TextBoxParametro_1_nome" runat="server" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-3">
                    <label>Texto de ajuda para o usuário</label>
                    <asp:TextBox ID="TextBoxParametro_1_ajuda" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div id="param_2" runat="server">
             <div class="row">
                <div class="form-group col-md-3">
                    <label>2º Parâmetro</label>
                    <asp:TextBox ID="TextBoxParametro_2_nome" runat="server" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-3">
                    <label>Texto de ajuda para o usuário</label>
                    <asp:TextBox ID="TextBoxParametro_2_ajuda" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div id="param_3" runat="server">
            <div class="row">
                <div class="form-group col-md-3">
                    <label>3º Parâmetro</label>
                    <asp:TextBox ID="TextBoxParametro_3_nome" runat="server" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-3">
                    <label>Texto de ajuda para o usuário</label>
                    <asp:TextBox ID="TextBoxParametro_3_ajuda" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div id="param_4" runat="server">
            <div class="row">
                <div class="form-group col-md-3">
                    <label>4º Parâmetro</label>
                    <asp:TextBox ID="TextBoxParametro_4_nome" runat="server" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-3">
                    <label>Texto de ajuda para o usuário</label>
                    <asp:TextBox ID="TextBoxParametro_4_ajuda" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div id="param_5" runat="server">
            <div class="row">
                <div class="form-group col-md-3">
                    <label>5º Parâmetro</label>
                    <asp:TextBox ID="TextBoxParametro_5_nome" runat="server" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-3">
                    <label>Texto de ajuda para o usuário</label>
                    <asp:TextBox ID="TextBoxParametro_5_ajuda" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        </div>


        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Atualizar" OnClick="Button1_Click" />
        <br />
        <br />
        <small style="color: red;" id="projetoHelp" class="form-text text-muted">Para que o data owner consiga fazer upload no flat file, é necessário o pipeline está vinculado a um projeto data quality.<br />
            Caso não seja vinculado a um projeto, ele poderá apenas executar o pipeline atualizado</small>
        <br />
        <br />
        <div class="alert alert-success" role="alert" id="mensagemOk" runat="server">
            Sucesso - Pipeline atualizado com sucesso.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErro" runat="server">
            Erro - Pipeline não foi atualizado.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroConexao" runat="server">
            Erro - Falha na conexão com banco de dados.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroPerfil" runat="server">
            Erro - Selecione um perfil para pipeline.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
         <div class="alert alert-danger" role="alert" id="mensagemErroExecucao" runat="server">
            Erro - Informe se pipeline pode ser executado manualmente.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
         <div class="alert alert-danger" role="alert" id="mensagemErroExecucaoParametro" runat="server">
            Erro - Informe se pipeline tem parâmetros.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroPipelineCadastrado" runat="server">
            Erro - Pipeline já cadastrado.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
    </div>
</asp:Content>

