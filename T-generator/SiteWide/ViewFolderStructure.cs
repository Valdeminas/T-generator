using System.Web.Mvc;

public class ViewFolderStructure : RazorViewEngine
{
    public ViewFolderStructure()
    {
        ViewLocationFormats = new[] 
        {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Amazon/{1}/{0}.cshtml", "~/Views/Amazon/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
            
        };

        MasterLocationFormats = new[] 
        {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Amazon/{1}/{0}.cshtml", "~/Views/Amazon/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
        };

        PartialViewLocationFormats = new[] 
        {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Amazon/{1}/{0}.cshtml", "~/Views/Amazon/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
        };
    }
}