using MyBicycle.Model;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Globalization;
using System.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Collections.Generic;

namespace MyBicycle.Controllers
{
    class Builder
    {
        private readonly string DIRECTORY = "docs";
        private readonly string TEMP = "temp";
        private readonly string ASSETS = "assets";
        private readonly string TEMP_ORI = "temp_original.pdf";
        private readonly string TEMP_COP = "temp_copy.pdf";

        private string TEMP_PATH_ORI
        {
            get
            {
                return $"{AppDomain.CurrentDomain.BaseDirectory}/{TEMP}/{TEMP_ORI}";
            }
        }
        private string TEMP_PATH_COP
        {
            get
            {
                return $"{AppDomain.CurrentDomain.BaseDirectory}/{TEMP}/{TEMP_COP}";
            }
        }

        private void CheckIfDirectoryExists(string name)
        {
            if (!Directory.Exists(name))
            {
                Directory.CreateDirectory(name);
            }
        }

        private string CurrentPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            string generatedPath = path + $"/{DIRECTORY}/";

            return generatedPath;
        }

        private string Date(string seperator)
        {
            DateTime now = DateTime.Now;
            return String.Format($"{now.Year}{seperator}{now.Month}{seperator}{now.Day}");
        }

        private void RemoveFilesIfExists()
        {
            if(File.Exists(TEMP_PATH_ORI))
            {
                File.Delete(TEMP_PATH_ORI);
            }

            if (File.Exists(TEMP_PATH_COP))
            {
                File.Delete(TEMP_PATH_COP);
            }
        }

        public bool GeneratePdf(Customers obj)
        {
            Config cfg = new Config();
            CheckIfDirectoryExists(DIRECTORY);
            RemoveFilesIfExists();

            var HtmlTemplate = File.ReadAllText($@"{ASSETS}/template_original.html");
            HtmlTemplate = HtmlTemplate.Replace("{{PRIMARY}}", String.Format($"{Date("/")}/{obj.Id}"));
            HtmlTemplate = HtmlTemplate.Replace("{{TYPE}}", "ORYGINAŁ");
            HtmlTemplate = HtmlTemplate.Replace("{{COMPANY}}", cfg.Name);
            HtmlTemplate = HtmlTemplate.Replace("{{ADDRESS}}", cfg.Street);
            HtmlTemplate = HtmlTemplate.Replace("{{POST}}", cfg.Post);
            HtmlTemplate = HtmlTemplate.Replace("{{CITY}}", cfg.City);
            HtmlTemplate = HtmlTemplate.Replace("{{EMAIL}}", cfg.Email);
            HtmlTemplate = HtmlTemplate.Replace("{{PHONE_COMPANY}}", cfg.Phone);
            HtmlTemplate = HtmlTemplate.Replace("{{SURNAME}}", obj.Surname);
            HtmlTemplate = HtmlTemplate.Replace("{{NAME}}", obj.Name);
            HtmlTemplate = HtmlTemplate.Replace("{{PHONE}}", obj.Phone);
            HtmlTemplate = HtmlTemplate.Replace("{{MODEL}}", obj.Model);
            HtmlTemplate = HtmlTemplate.Replace("{{DESCRIPTION}}", obj.Description);

            PdfDocument original = PdfGenerator.GeneratePdf(HtmlTemplate, PageSize.A4);

            HtmlTemplate = HtmlTemplate.Replace("ORYGINAŁ", "KOPIA");
            PdfDocument copy = PdfGenerator.GeneratePdf(HtmlTemplate, PageSize.A4);

            CheckIfDirectoryExists(TEMP);

            original.Save(TEMP_PATH_ORI);
            copy.Save(TEMP_PATH_COP);

            string[] files = new[] { TEMP_ORI, TEMP_COP };

            PdfDocument pdf = new PdfDocument();

            foreach (string file in files)
            {
                PdfDocument inputDocument = PdfReader.Open($"{AppDomain.CurrentDomain.BaseDirectory}/temp/{file}", PdfDocumentOpenMode.Import);
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    PdfPage page = inputDocument.Pages[idx];
                    pdf.AddPage(page);
                }
            }

            try
            {
                pdf.Save($"{CurrentPath()}{Date("_")}_{obj.Id}.pdf");
                System.Diagnostics.Process.Start($"{CurrentPath()}{Date("_")}_{obj.Id}.pdf");
                RemoveFilesIfExists();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
