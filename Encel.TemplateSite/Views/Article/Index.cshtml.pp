@model $rootnamespace$.Models.Article

@{
    ViewBag.Title = Model.Title;
}

<h1>@Model.Title</h1>

<p style="font-size: large">@Model.Lead</p>

@Html.ContentFor(m => m.Content)
