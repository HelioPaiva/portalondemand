<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="editar-acesso.aspx.cs" Inherits="PortalOnDemand.editar_acesso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container body-content">
        <br />
        <h3>Atualizar Acesso</h3>
        <div class="row">
            <div class="form-group col-md-4">
                <label>Usuário</label>
                <asp:DropDownList ID="DropDownListUsuario" runat="server" class="form-control" required=""></asp:DropDownList>
            </div>
            <div class="form-group col-md-2">
                <label>Perfil</label>
                <asp:DropDownList ID="DropDownListPerfil" runat="server" class="form-control" required=""></asp:DropDownList>
            </div>
        </div>
        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Atualizar" OnClick="Button1_Click" />
        <br />
        <br />
        <div class="alert alert-success" role="alert" id="mensagemOk" runat="server">
            Sucesso -  Acesso liberado com sucesso.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErro" runat="server">
            Erro - Acesso não foi atualizado.
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
            Erro - Selecione um perfil para liberar o acesso.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroAcessoCadastrado" runat="server">
            Erro - Usuário já com acesso para o perfil selecionado.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
         <div class="alert alert-danger" role="alert" id="mensagemErroUsuario" runat="server">
            Erro - Selecione um usuário para liberar o acesso.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
    </div>
</asp:Content>
