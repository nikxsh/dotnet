<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Translator._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script src="Scripts/Translate.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        To Marathi Translator!
    </h2>
    <div style="height: 344px">
        <center>
            <textarea id="txtAreaFrom" name="txtAreaFrom" rows="8" cols="50" onfocus="javascript:print_many_words()" onkeyup="javascript:print_many_words()"></textarea>
            <textarea id="txtAreaTo" name="txtAreaTo" rows="8" cols="50"></textarea>
            <textarea id="txtAreaUnicode" name="txtAreaUnicode" rows="5" cols="103"></textarea>
        </center>
    </div>
</asp:Content>
