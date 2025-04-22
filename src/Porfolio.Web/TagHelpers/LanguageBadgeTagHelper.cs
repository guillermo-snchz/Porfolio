using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Porfolio.Web.TagHelpers;

[HtmlTargetElement("language-badge")]
public class LanguageBadgeTagHelper : TagHelper
{
    public string Language { get; set; } = string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var color = GetColor(Language);

        output.TagName = "span";
        output.Attributes.SetAttribute("class", "badge rounded-pill");
        output.Attributes.SetAttribute("style", $"background-color:{color}; color:white;");
        output.Content.SetContent(Language);
    }

    private string GetColor(string language)
    {
        return language switch
        {
            "C#" => "#178600",
            "JavaScript" => "#f1e05a",
            "Python" => "#3572A5",
            "TypeScript" => "#2b7489",
            "Java" => "#b07219",
            "HTML" => "#e34c26",
            "CSS" => "#563d7c",
            "Go" => "#00ADD8",
            "Shell" => "#89e051",
            _ => "#6c757d"
        };
    }
}

