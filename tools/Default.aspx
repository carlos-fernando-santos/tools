<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Tools.Master" CodeBehind="Default.aspx.vb" Inherits="tools.main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">

    <div class="container">
        <h3>Encriptar e Desencriptar</h3>
        <div class="row">
            <div class="col">
                <asp:Label runat="server">Valor:</asp:Label>
            </div>
            <div class="col">
                <asp:TextBox ID="txtValor" runat="server" Width="218px"></asp:TextBox>
            </div>
            <div class="col">
                <asp:Button runat="server" ID="btEncrypt" Text="Encriptar" />
                <asp:Button runat="server" ID="btDecrfypt" Text="Descriptar"/>
            </div>
        </div>
        <div class="row">
            <div class="col"> 
                <asp:Label runat="server" ID="lblResultado">Resultado:</asp:Label>
            </div>
            <div class ="col">
                <asp:TextBox ID="txtResultado" runat="server" Width="223px"></asp:TextBox>
             </div>
        </div>
        <p style="height:10px"></p>
    
            <h3>Construção de queries</h3>

        <div class="form-group row">
            <label for="xdsn" class="col-sm-2 col-form-label">Instância SQL</label>
            <asp:DropDownList runat="server" ID="xdsn" class="form-control col-sm-3">
                <asp:ListItem Selected="True" Value="1">Testes</asp:ListItem>
                <asp:ListItem Value="2">Real</asp:ListItem>
                <asp:ListItem Value="3">Testes RH</asp:ListItem>
                <asp:ListItem Value="4">Real RH</asp:ListItem>
                <asp:ListItem Value="5">Testes Peru</asp:ListItem>
                <asp:ListItem Value="6">Real Peru</asp:ListItem>
            </asp:DropDownList>            
           <label for="xtype" class="col-sm-2 col-form-label">Tipo Resultado</label>
            <asp:DropDownList runat="server" ID="xtype" class="form-control col-sm-3">
                <asp:ListItem Selected="True" Value="1">Insert</asp:ListItem>
                <asp:ListItem Value="2">Update</asp:ListItem>
            </asp:DropDownList>
            <label for="xtable" class="col-sm-2 col-form-label">Tabela</label>
            <asp:TextBox runat="server" ID="xtable" class="form-control col-sm-3" AutoPostBack="True"></asp:TextBox>
        </div>

        <div class="form-group row">
            <label for="xqueryinput" class="col-sm-2 col-form-label" style="vertical-align:top;">Query Input</label>
            <asp:TextBox runat="server" class="form-control col-sm-4" ID="xqueryinput" TextMode="MultiLine" Width="80%" ></asp:TextBox>
        </div>

        <div class="form-group row">
            <asp:Button ID="xbuttonexec" runat="server" Text="Executar"  />
        </div>
        
        <div class="form-group row">
            <label for="xgrid" class="col--sm-2 col-form-control">Resultados</label>
            <asp:GridView id="xgrid" class="form-control col-sm-4"  runat ="server"></asp:GridView>
        </div>

        <div class="form-group row">
            <label for="xqueryoutput" class="col-sm-2 col-form-label" style="vertical-align:top;">Query Output</label>
            <asp:TextBox runat="server" class="form-control col-sm-4" ID="xqueryoutput" TextMode="MultiLine" Width="80%" Height="175px" ></asp:TextBox>
        </div>
   
                   
    </div>
</asp:Content>
