<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="mobile4.aspx.cs" Inherits="PortalOnDemand.mobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-responsive">
        <table class="table table-sm">
            <thead>
                <tr>
                    <th scope="col">Pipeline</th>
                    <th scope="col">Início</th>
                    <th scope="col">Duração</th>
                    <th scope="col">
                        Status
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">pl_master_broker_3_0-mi_sortimento_apuracao</th>
                    <td>13/04/2022 09:30</td>
                    <td>00:20</td>
                    <td>Em Andamento</td>
                </tr>
                <tr>
                    <th scope="row">pl_master_broker_3_0-mi_sortimento_apuracao</th>
                    <td>13/04/2022 09:30</td>
                    <td>00:20</td>
                    <td>
                        Em Processamento
                    </td>
                </tr>
                <tr>
                    <th scope="row">pl_master_broker_3_0-mi_sortimento_apuracao</th>
                    <td>13/04/2022 09:30</td>
                    <td>00:20</td>
                    <td>Em Andamento</td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
