using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using System.Collections.Generic;
using Actris.Abstraction.Model.View;

namespace Actris.Infrastructure.Helpers
{
    public class PDFTableGenerator : IDocument
    {
        public GridListModel Model { get; set; }
        public string HeaderText { get; set; }
        public int[] TableHeaderSizes { get; set; }
        public static int NORMAL_FONT_SIZE = 10;

        public PDFTableGenerator(GridListModel model, string headerTitle, int[] tableHeaderSizes)
        {
            Model = model;
            HeaderText = headerTitle;
            TableHeaderSizes = tableHeaderSizes;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignRight().Text(x =>
                {
                    x.CurrentPageNumber().FontSize(NORMAL_FONT_SIZE-2);
                    x.Span(" / ").FontSize(NORMAL_FONT_SIZE-2);
                    x.TotalPages().FontSize(NORMAL_FONT_SIZE-2);
                });
            });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Grey.Darken3);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(HeaderText).Style(titleStyle);
                });

                row.ConstantItem(120).Height(10).Placeholder();
            });
        }

            void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    foreach (var size in TableHeaderSizes)
                    {
                        if (size != 0) columns.ConstantColumn(size);
                        else columns.RelativeColumn();
                    }
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCellStyle).Text("#").FontSize(NORMAL_FONT_SIZE);
                    foreach (var rowHead in Model.ColumnDefinitions)
                    {
                        if (rowHead.Type == ColumnType.Id) continue;
                        header.Cell().Element(HeaderCellStyle).Text(rowHead.Name).FontSize(NORMAL_FONT_SIZE);
                    }

                    
                });

                int rowNumber = 1;
                foreach (var row in Model.Rows)
                {
                    table.Cell().Element(BodyCellStyle).Text(rowNumber).FontSize(NORMAL_FONT_SIZE);
                    foreach (var col in row.Columns)
                    {
                        table.Cell().Element(BodyCellStyle).Text(col.Value).FontSize(NORMAL_FONT_SIZE);
                    }
                    rowNumber++;
                }
            });

        }

        static IContainer HeaderCellStyle(IContainer container)
        {
            return container.DefaultTextStyle(x => x.SemiBold()).BorderBottom(1).BorderColor(Colors.Black).PaddingVertical(2).PaddingHorizontal(5);
        }

        static IContainer BodyCellStyle(IContainer container)
        {
            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(5);
        }
    }
}
