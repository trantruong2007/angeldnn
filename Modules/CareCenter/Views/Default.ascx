<%@ Import namespace="Angel.DNN.CareCenter"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="Angel.DNN.CareCenter.Views.Default" %>

requests

<ul>
    <li><a href="<%=UrlHelper.UrlForView(ViewNames.Default) %>">Request</a></li>
    <li><a href="<%=UrlHelper.UrlForView(ViewNames.UnassignedRequests) %>">Unassigned Requests</a></li>
    <li><a href="<%=UrlHelper.UrlForView(ViewNames.CareAvailability) %>">Care Availability</a></li>
</ul>
