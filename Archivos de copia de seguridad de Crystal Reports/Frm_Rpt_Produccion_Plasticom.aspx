﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Rpt_Produccion_Plasticom.aspx.cs" Inherits="web_trazabilidad.Paginas.Reporting.Frm_Rpt_Produccion_Plasticom" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=10.0.16.113, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:ReportViewer ID="RV_Produccion_Plasticom" runat="server" BackColor="White" Height="600px" Width="100%"></telerik:ReportViewer>
</asp:Content>
