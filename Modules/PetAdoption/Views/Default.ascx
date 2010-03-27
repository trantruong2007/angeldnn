<%@ Import namespace="WebAscender.DNN.StarterModule"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="WebAscender.DNN.StarterModule.Views.Default" %>

<h1>The default view</h1>
<br />

<h2>Other Views:</h2>
<br />
<ul>
    <li><a href="<%=UrlHelper.UrlForView(ViewNames.BasicView) %>">Basic View</a></li>
    <li><a href="<%=UrlHelper.UrlForView(ViewNames.CustomTitles) %>">Custom Titles</a></li>
    <li><a href="<%= UrlHelper.UrlForViewFriendly(ViewNames.FriendlyPage,"","Custom-Page-Name") %>">A "Friendly" URL to a View with custom page name</a></li>
    <li><a href="<%= UrlHelper.UrlForTabFriendly(TabId,"","Custom-Page-Name") %>">A "Friendly" URL with custom page name</a></li>
    <li><a href="<%= UrlHelper.UrlForViewFriendly(ViewNames.FriendlyPage,"","Custom-Page-Name") %>">A "Friendly" URL to a View with custom page name</a></li>
    <li><a href="<%= UrlHelper.UrlForTabFriendly(TabId,"","/My-Custom-Path/Custom-Page-Name") %>">A "Friendly" URL with custom path and page name</a></li>
    <li><a href="<%= UrlHelper.UrlForTabFriendly(TabId,"myparam1=324,myparam2=kevin","/My-Custom-Path/Custom-Page-Name") %>">A "Friendly" URL with custom parameters, path, and page name</a></li>
</ul>
<br />

<h2>Accessing Module Settings:</h2>
<br />
<p>
    The value of the module setting <b><%=ModuleSettingNames.MySetting1 %></b> is: <b><%= Settings[ModuleSettingNames.MySetting1] %></b>
</p>