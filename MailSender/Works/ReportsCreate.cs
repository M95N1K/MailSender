using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media;
using Xceed.Document;
using Xceed.Document.NET;
using Xceed.Words.NET;
using MailSender.Infrastructure.Interfaces;
using System.Windows;
using System.Security.Policy;

namespace MailSender.Works
{
    class ReportsCreate
    {
		private DocX _document;
		private Font _fontFamily = new Font("Times New Roman");
		private double _fontSizeText = 12;
		private double _fontSizeTitle = 14;
		private double _spacing = 1.5;
		private string _filename;

        public ReportsCreate(string fileName)
        {
			_filename = Path.Combine(Directory.GetCurrentDirectory(), fileName);
			try
            {
				using (DocX doc = DocX.Create(_filename))
                {
					doc.Save();
                }
            }
			catch (Exception e)
            {
				AppErrors.AddError($"Ошибка создания файла отчета \n{e.Message}");
            }
        }

		public void Create()
        {
			using (_document = DocX.Load(_filename))
            {
                _document.MarginLeft = 85;
                _document.MarginRight = 28.3f;
                _document.MarginTop = 28.3f;
                _document.MarginBottom = 28.3f;

                AddTitle(_document, "Список адресов: ");
                IEmailData tmp = (IEmailData)UnionClass.MergClasses["EmailDate"];
                foreach (var item in tmp.EmailDate)
                {
                    AddText(_document, item.Value);
                }
                _document.Save();
            }
        }

        private void AddTitle(DocX doc,string text)
        {
            Paragraph title = CreateParagraph(doc);
            
            title.Append(text).FontSize(_fontSizeTitle).Bold().Alignment = Alignment.center;
        }


        private void AddText(DocX doc, string text)
        {
            Paragraph texts = CreateParagraph(doc);
            texts.Append(text).FontSize(_fontSizeText);
        }

        private Paragraph CreateParagraph(DocX doc)
        {
            if (doc == null) return null;
            Paragraph result = doc.InsertParagraph();
            result.Spacing(_spacing);
            result.Font(_fontFamily);
            return result;
        }
	}
}
