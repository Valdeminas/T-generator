using System.Web.Mvc;

public class ViewFolderStructure : RazorViewEngine
{
    public ViewFolderStructure()
    {
        ViewLocationFormats = new[] 
        {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Basic/{1}/{0}.cshtml", "~/Views/Amazon/Data/Basic/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Dump/{1}/{0}.cshtml", "~/Views/Amazon/Data/Dump/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Intermediate/{1}/{0}.cshtml", "~/Views/Amazon/Data/Intermediate/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
            
        };

        MasterLocationFormats = new[] 
        {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Basic/{1}/{0}.cshtml", "~/Views/Amazon/Data/Basic/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Dump/{1}/{0}.cshtml", "~/Views/Amazon/Data/Dump/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Intermediate/{1}/{0}.cshtml", "~/Views/Amazon/Data/Intermediate/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
        };

        PartialViewLocationFormats = new[] 
        {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Basic/{1}/{0}.cshtml", "~/Views/Amazon/Data/Basic/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Dump/{1}/{0}.cshtml", "~/Views/Amazon/Data/Dump/{1}/{0}.vbhtml",
            "~/Views/Amazon/Data/Intermediate/{1}/{0}.cshtml", "~/Views/Amazon/Data/Intermediate/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
        };
    }
}