﻿using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Services.Amazon
{
    public class ViewLocationExpander : IViewLocationExpander
    {

        /// <summary>
        /// Used to specify the locations that the view engine should search to 
        /// locate views.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //{2} is area, {1} is controller,{0} is the action
            string[] locations = new string[] {
                "/Views/{2}/{1}/{0}.cshtml",
                "/Views/Amazon/Data/Basic/{2}/{1}/{0}.cshtml",
                "/Views/Amazon/Data/Dump/{2}/{1}/{0}.cshtml",
                "/Views/Amazon/Data/Intermediate/{2}/{1}/{0}.cshtml" };
            return locations.Union(viewLocations);          //Add mvc default locations after ours
        }


        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }

    }
}
