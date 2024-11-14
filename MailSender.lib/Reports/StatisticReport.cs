using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace MailSender.lib.Reports
{
    public class StatisticReport
    {
        int SentMailsCount { get; set; }

        public StatisticReport(int SentMails)
        {
             SentMailsCount = SentMails;
        }

        // Creates a WordprocessingDocument.
        public void CreatePackage(string filePath)
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                CreateParts(package);
            }
        }

        // Adds child parts and generates content of the specified part.
        private void CreateParts(WordprocessingDocument document)
        {
            MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
            GenerateMainDocumentPart1Content(mainDocumentPart1);

            NumberingDefinitionsPart numberingDefinitionsPart1 = mainDocumentPart1.AddNewPart<NumberingDefinitionsPart>("docRId0");
            GenerateNumberingDefinitionsPart1Content(numberingDefinitionsPart1);

            StyleDefinitionsPart styleDefinitionsPart1 = mainDocumentPart1.AddNewPart<StyleDefinitionsPart>("docRId1");
            GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);

            SetPackageProperties(document);
        }

        // Generates content of mainDocumentPart1.
        private void GenerateMainDocumentPart1Content(MainDocumentPart mainDocumentPart1)
        {
            Document document1 = new Document();
            document1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            Body body1 = new Body();

            Paragraph paragraph1 = new Paragraph();

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { Before = "0", After = "200", Line = "276" };
            Indentation indentation1 = new Indentation() { Start = "0", End = "0", FirstLine = "0" };
            Justification justification1 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
            RunFonts runFonts1 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Color color1 = new Color() { Val = "auto" };
            Spacing spacing1 = new Spacing() { Val = 0 };
            Position position1 = new Position() { Val = "0" };
            FontSize fontSize1 = new FontSize() { Val = "22" };
            Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "auto" };

            paragraphMarkRunProperties1.Append(runFonts1);
            paragraphMarkRunProperties1.Append(color1);
            paragraphMarkRunProperties1.Append(spacing1);
            paragraphMarkRunProperties1.Append(position1);
            paragraphMarkRunProperties1.Append(fontSize1);
            paragraphMarkRunProperties1.Append(shading1);

            paragraphProperties1.Append(spacingBetweenLines1);
            paragraphProperties1.Append(indentation1);
            paragraphProperties1.Append(justification1);
            paragraphProperties1.Append(paragraphMarkRunProperties1);

            Run run1 = new Run();

            RunProperties runProperties1 = new RunProperties();
            RunFonts runFonts2 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Bold bold1 = new Bold();
            Color color2 = new Color() { Val = "auto" };
            Spacing spacing2 = new Spacing() { Val = 0 };
            Position position2 = new Position() { Val = "0" };
            FontSize fontSize2 = new FontSize() { Val = "22" };
            Shading shading2 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "auto" };

            runProperties1.Append(runFonts2);
            runProperties1.Append(bold1);
            runProperties1.Append(color2);
            runProperties1.Append(spacing2);
            runProperties1.Append(position2);
            runProperties1.Append(fontSize2);
            runProperties1.Append(shading2);
            Text text1 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text1.Text = "Отчет приложения отправки почты - статистика";

            run1.Append(runProperties1);
            run1.Append(text1);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);

            Paragraph paragraph2 = new Paragraph();

            ParagraphProperties paragraphProperties2 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines() { Before = "0", After = "200", Line = "276" };
            Indentation indentation2 = new Indentation() { Start = "0", End = "0", FirstLine = "0" };
            Justification justification2 = new Justification() { Val = JustificationValues.Left };

            ParagraphMarkRunProperties paragraphMarkRunProperties2 = new ParagraphMarkRunProperties();
            RunFonts runFonts3 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Color color3 = new Color() { Val = "auto" };
            Spacing spacing3 = new Spacing() { Val = 0 };
            Position position3 = new Position() { Val = "0" };
            FontSize fontSize3 = new FontSize() { Val = "22" };
            Shading shading3 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "auto" };

            paragraphMarkRunProperties2.Append(runFonts3);
            paragraphMarkRunProperties2.Append(color3);
            paragraphMarkRunProperties2.Append(spacing3);
            paragraphMarkRunProperties2.Append(position3);
            paragraphMarkRunProperties2.Append(fontSize3);
            paragraphMarkRunProperties2.Append(shading3);

            paragraphProperties2.Append(spacingBetweenLines2);
            paragraphProperties2.Append(indentation2);
            paragraphProperties2.Append(justification2);
            paragraphProperties2.Append(paragraphMarkRunProperties2);

            paragraph2.Append(paragraphProperties2);

            Paragraph paragraph3 = new Paragraph();

            ParagraphProperties paragraphProperties3 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines3 = new SpacingBetweenLines() { Before = "0", After = "200", Line = "276" };
            Indentation indentation3 = new Indentation() { Start = "0", End = "0", FirstLine = "0" };
            Justification justification3 = new Justification() { Val = JustificationValues.Left };

            ParagraphMarkRunProperties paragraphMarkRunProperties3 = new ParagraphMarkRunProperties();
            RunFonts runFonts4 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Color color4 = new Color() { Val = "000000" };
            Spacing spacing4 = new Spacing() { Val = 0 };
            Position position4 = new Position() { Val = "0" };
            FontSize fontSize4 = new FontSize() { Val = "22" };
            Shading shading4 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "FFFFFF" };

            paragraphMarkRunProperties3.Append(runFonts4);
            paragraphMarkRunProperties3.Append(color4);
            paragraphMarkRunProperties3.Append(spacing4);
            paragraphMarkRunProperties3.Append(position4);
            paragraphMarkRunProperties3.Append(fontSize4);
            paragraphMarkRunProperties3.Append(shading4);

            paragraphProperties3.Append(spacingBetweenLines3);
            paragraphProperties3.Append(indentation3);
            paragraphProperties3.Append(justification3);
            paragraphProperties3.Append(paragraphMarkRunProperties3);

            Run run2 = new Run();

            RunProperties runProperties2 = new RunProperties();
            RunFonts runFonts5 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Color color5 = new Color() { Val = "FF0000" };
            Spacing spacing5 = new Spacing() { Val = 0 };
            Position position5 = new Position() { Val = "0" };
            FontSize fontSize5 = new FontSize() { Val = "22" };
            Shading shading5 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "auto" };

            runProperties2.Append(runFonts5);
            runProperties2.Append(color5);
            runProperties2.Append(spacing5);
            runProperties2.Append(position5);
            runProperties2.Append(fontSize5);
            runProperties2.Append(shading5);
            Text text2 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text2.Text = "Отправлено сообщений ";

            run2.Append(runProperties2);
            run2.Append(text2);

            Run run3 = new Run();

            RunProperties runProperties3 = new RunProperties();
            RunFonts runFonts6 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Color color6 = new Color() { Val = "auto" };
            Spacing spacing6 = new Spacing() { Val = 0 };
            Position position6 = new Position() { Val = "0" };
            FontSize fontSize6 = new FontSize() { Val = "22" };
            Shading shading6 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "auto" };

            runProperties3.Append(runFonts6);
            runProperties3.Append(color6);
            runProperties3.Append(spacing6);
            runProperties3.Append(position6);
            runProperties3.Append(fontSize6);
            runProperties3.Append(shading6);
            Text text3 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text3.Text = "- ";

            run3.Append(runProperties3);
            run3.Append(text3);

            Run run4 = new Run();

            RunProperties runProperties4 = new RunProperties();
            RunFonts runFonts7 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Color color7 = new Color() { Val = "000000" };
            Spacing spacing7 = new Spacing() { Val = 0 };
            Position position7 = new Position() { Val = "0" };
            FontSize fontSize7 = new FontSize() { Val = "22" };
            Shading shading7 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "FFFFFF" };

            runProperties4.Append(runFonts7);
            runProperties4.Append(color7);
            runProperties4.Append(spacing7);
            runProperties4.Append(position7);
            runProperties4.Append(fontSize7);
            runProperties4.Append(shading7);
            Text text4 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text4.Text = SentMailsCount.ToString();

            run4.Append(runProperties4);
            run4.Append(text4);

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(run2);
            paragraph3.Append(run3);
            paragraph3.Append(run4);

            Paragraph paragraph4 = new Paragraph();

            ParagraphProperties paragraphProperties4 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines4 = new SpacingBetweenLines() { Before = "0", After = "200", Line = "276" };
            Indentation indentation4 = new Indentation() { Start = "0", End = "0", FirstLine = "0" };
            Justification justification4 = new Justification() { Val = JustificationValues.Left };

            ParagraphMarkRunProperties paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();
            RunFonts runFonts8 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Color color8 = new Color() { Val = "000000" };
            Spacing spacing8 = new Spacing() { Val = 0 };
            Position position8 = new Position() { Val = "0" };
            FontSize fontSize8 = new FontSize() { Val = "22" };
            Shading shading8 = new Shading() { Val = ShadingPatternValues.Clear, Fill = "FFFFFF" };

            paragraphMarkRunProperties4.Append(runFonts8);
            paragraphMarkRunProperties4.Append(color8);
            paragraphMarkRunProperties4.Append(spacing8);
            paragraphMarkRunProperties4.Append(position8);
            paragraphMarkRunProperties4.Append(fontSize8);
            paragraphMarkRunProperties4.Append(shading8);

            paragraphProperties4.Append(spacingBetweenLines4);
            paragraphProperties4.Append(indentation4);
            paragraphProperties4.Append(justification4);
            paragraphProperties4.Append(paragraphMarkRunProperties4);

            paragraph4.Append(paragraphProperties4);

            body1.Append(paragraph1);
            body1.Append(paragraph2);
            body1.Append(paragraph3);
            body1.Append(paragraph4);

            document1.Append(body1);

            mainDocumentPart1.Document = document1;
        }

        // Generates content of numberingDefinitionsPart1.
        private void GenerateNumberingDefinitionsPart1Content(NumberingDefinitionsPart numberingDefinitionsPart1)
        {
            Numbering numbering1 = new Numbering();
            numbering1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            numberingDefinitionsPart1.Numbering = numbering1;
        }

        // Generates content of styleDefinitionsPart1.
        private void GenerateStyleDefinitionsPart1Content(StyleDefinitionsPart styleDefinitionsPart1)
        {
            Styles styles1 = new Styles();
            styles1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            styleDefinitionsPart1.Styles = styles1;
        }

        private void SetPackageProperties(OpenXmlPackage document) { }
    }
}
