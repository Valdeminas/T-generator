using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;

namespace T_generator.Services.Amazon.EPPlusCore
{
    public class EPPLusCore
    {
        private IHostingEnvironment _environment;

        public static void test()
        {
            //IHostingEnvironment _environment;
            var templatePath = Path.Combine("D:\\Programavimas\\T-gen_new\\T-generator\\T-generator_Gedo\\src\\T-generator\\wwwroot\\uploads\\Templates", "Unisex.xlsx");
            //var fullTemplatePath = Path.Combine(_environment.WebRootPath, templatePath);
            FileInfo newFile = new FileInfo(templatePath);

            //ExcelPackage pck = new ExcelPackage(newFile);
            //Add the Content sheet
            //ExcelWorksheet ws = pck.Workbook.Worksheets.SingleOrDefault(a=>a.Name=="Template");
           

            //for (int i = 4; i < rows; i++)
            //{
            //    ws.SetValue(4,)
            //}

            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pck.Workbook.Worksheets.SingleOrDefault(a => a.Name == "Template");

                var temp= worksheet.GetValue(1, 1);
                var rows = worksheet.Dimension.Rows;
                var columns = worksheet.Dimension.Columns;

                // clear value
                for (int i = 4; i < rows+1; i++)
                {
                    for(int j = 1; j < columns+1; j++)
                    {
                        
                        worksheet.SetValue(i, j, "");
                    }
                }
                
                pck.Save();

            }
            //ws.View.ShowGridLines = false;
        }
    }
}
