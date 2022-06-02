<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="editar-usuario.aspx.cs" Inherits="PortalOnDemand.editar_usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container body-content">
        <br />
        <h3>Atualizar Usuário</h3>
       <div class="row">
            <div class="form-group col-md-4">
                <label>E-mail</label>
                <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-md-2">
                <label>Perfil</label>
                <asp:DropDownList ID="DropDownListPerfil" runat="server" class="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Atualizar" OnClick="Button1_Click" />
        <br /><br />
        <div class="alert alert-success" role="alert" id="mensagemOk" runat="server">
            Sucesso - Usuário atualizado com sucesso.
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErro" runat="server">
            Erro - Usuário não foi atualizado.
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
        <div class="alert alert-danger" role="alert" id="mensagemErroEmail" runat="server">
            Erro - E-mail inválido, Insira um e-mail Nestlé.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
        <div class="alert alert-danger" role="alert" id="mensagemErroPerfil" runat="server">
            Erro - Selecione um perfil para usuário.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
         <!--  
         <div class="alert alert-danger" role="alert" id="mensagemErroUsuarioCadastrado" runat="server">
            Erro - Usuário já cadastrado.
             <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                 <span aria-hidden="true">&times;</span>
             </button>
        </div>
         -->
    </div>
</asp:Content>
