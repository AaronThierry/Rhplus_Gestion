using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static FastReport.Utils.CompilerException;

namespace RH_GRH
{
    /// <summary>
    /// Générateur de bulletin de paie avec QR code
    /// </summary>
    public class BulletinDocument : IDocument
    {


        /// <summary>
        /// Génère un QR code PNG simple
        /// </summary>
        private static byte[] GenerateQrPngBytes(string payload, int pixelsPerModule = 8)
        {
            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var qr = new PngByteQRCode(data);
            return qr.GetGraphic(pixelsPerModule);
        }




    private readonly BulletinModel model;

        public BulletinDocument(BulletinModel model)
        {
            this.model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontFamily("Goudy Old Style").FontSize(10));

                /*// FILIGRANE
                page.Foreground().Element(watermark =>
                {
                    watermark
                        .AlignCenter()
                        .AlignMiddle()
                        .Rotate(-30)
                        .Text("Confidentiel", TextStyle.Default
                            .FontSize(35)
                            .FontColor(Colors.Grey.Lighten4)
                            .FontFamily("Montserrat")
                            .Italic()
                            .Bold()
                        );
                });
                */
                // EN-TÊTE
                page.Header().Element(header =>
                {
                    header.Column(col =>
                    {
                        // Ligne 1 : Logo | Infos entreprise | Période
                        col.Item().Row(row =>
                        {
                            if (model.LogoEntreprise != null)
                            {
                                var logoBytes = model.LogoEntreprise;
                                row.ConstantItem(60)
                                   .Height(60)
                                   .Image(new MemoryStream(logoBytes), ImageScaling.FitArea);
                            }

                            row.RelativeItem().Column(infoCol =>
                            {
                                infoCol.Item().PaddingLeft(3).Text(model.NomEntreprise).FontSize(9).FontFamily("Montserrat").Medium().Italic();
                                infoCol.Item().PaddingLeft(3).Text(model.Sigle).FontSize(10).Italic().Medium().FontFamily("Montserrat");
                                infoCol.Item().PaddingLeft(3).Text(model.TelephoneEntreprise).FontSize(9).FontFamily("Montserrat").Medium().Italic();
                                infoCol.Item().PaddingLeft(3).Text(model.EmailEntreprise).FontSize(9).FontFamily("Montserrat").Medium().Italic();
                                infoCol.Item().PaddingLeft(3).Text(model.AdressePhysiqueEntreprise + " / " +model.AdressePostaleEntreprise).FontSize(9).FontFamily("Montserrat").Medium().Italic();
                            });

                            row.ConstantItem(185).PaddingTop(-15).AlignMiddle().AlignRight().CornerRadius(10).Element(period =>
                            {
                                period
                                    .Background(Colors.Blue.Lighten5)
                                    .PaddingVertical(5)
                                    .PaddingHorizontal(15)
                                    .Border(1)
                                    .BorderColor(Colors.Transparent)
                                    .AlignCenter()
                                    .Text(model.Periode)
                                        .FontSize(12)
                                        .FontFamily("Montserrat")
                                        .FontColor(Colors.Black)
                                        .SemiBold();
                            });
                        });



                        // --- QR PAYLOAD SIMPLE (VERSION ORIGINALE) ---
                        var fr = System.Globalization.CultureInfo.GetCultureInfo("fr-FR");

                        // Données simples pour le QR code
                        var doc = "BULLETIN_PAIE";
                        var ent = model.NomEntreprise?.Trim() ?? "";
                        var emp = model.NomEmploye?.Trim() ?? "";
                        var mat = model.Matricule?.Trim() ?? "";
                        var per = model.Periode?.Trim() ?? "";
                        var net = Math.Round(model.SalaireNetaPayerFinal, 2);
                        var netInvariant = net.ToString("0.00", CultureInfo.InvariantCulture);
                        var currency = "XOF";
                        var issuedAt = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);

                        // Payload simple pipe-delimited
                        string qrPayload = $"{doc}|{ent}|{emp}|{mat}|{per}|{netInvariant}|{currency}|{issuedAt}";

                        // Génération du QR (PNG en mémoire) résolution standard
                        byte[] qrPng = GenerateQrPngBytes(qrPayload, pixelsPerModule: 8);



                        // Ligne 2 : QR + Infos employé
                        col.Item().Row(row =>
                        {
                            // --- QR à gauche (VERSION SIMPLE ORIGINALE) ---
                            row.AutoItem().Container()
                                .PaddingTop(10)
                                .Width(70).Height(70)
                                .Border(1).BorderColor(Colors.Grey.Lighten1)
                                .Padding(3)
                                .AlignCenter().AlignMiddle()
                                .Image(qrPng, ImageScaling.FitArea);

                            // 2) Spacer flexible qui pousse le reste à droite
                            row.RelativeItem().Element(_ => { }); // vide volontairement


                            // --- Ton bloc d’infos employé (inchangé) à droite ---
                            row.AutoItem().Container()
                                .AlignRight().Width(250)
                                .PaddingTop(10)
                                .Border(1).BorderColor(Colors.Black)
                                .Padding(6)
                                .Column(info =>
                                {
                                    info.Spacing(1);
                                    info.Item().Text(t => t.Span(model.Civilite + " " + model.NomEmploye)
                                        .FontColor(Colors.Blue.Medium).Bold().FontFamily("Montserrat").FontSize(10));
                                    info.Item().Text(t => t.Span(model.Matricule)
                                        .FontColor(Colors.Red.Medium).SemiBold().FontFamily("Montserrat").FontSize(10));
                                    info.Item().Text(t => t.Span(model.Poste)
                                        .FontColor(Colors.Blue.Medium).SemiBold().FontFamily("Montserrat").FontSize(10));
                                    info.Item().Text(t => t.Span(model.NumeroEmploye)
                                        .FontColor(Colors.Blue.Medium).SemiBold().FontFamily("Montserrat").FontSize(10));
                                });
                        });


                        // Ligne 3 : Détails contractuels
                        col.Item().PaddingTop(10).Border(1).BorderColor(Colors.Black).Padding(3).Row(row =>
                        {
                            row.RelativeItem().Column(col1 =>
                            {
                                col1.Spacing(2);
                                col1.Item().Text(b => { b.Span("Matricule : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Matricule.ToString()).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col1.Item().Text(b => { b.Span("Date naiss : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.DateNaissance.ToString("dd-MM-yyyy")).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col1.Item().Text(b => { b.Span("Date début : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.DateDebut.ToString("dd-MM-yyyy")).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col1.Item().Text(b => { b.Span("Date fin : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.DateFin?.ToString("dd-MM-yyyy")).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col1.Item().Text(b => { b.Span("Adresse : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.AdresseEmploye).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                            });

                            row.RelativeItem().Column(col2 =>
                            {
                                col2.Spacing(2);
                                col2.Item().Text(b => { b.Span("Statut : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.DureeContrat).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col2.Item().Text(b => { b.Span("Catégorie : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Categorie).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col2.Item().Text(b => { b.Span("Service : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Service).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col2.Item().Text(b => { b.Span("Direction : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Direction).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col2.Item().Text(b => { b.Span("Numéro cnss : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.NumeroCNSSEmploye).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                            });

                            row.RelativeItem().Column(col3 =>
                            {
                                col3.Spacing(2);
                                col3.Item().Text(b => { b.Span("Sexe : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Sexe).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); ; });
                                col3.Item().Text(b => { b.Span("H/Jr contrat : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.NbJourHeure.ToString()).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col3.Item().Text(b => { b.Span("Contrat : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Contrat).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col3.Item().Text(b => { b.Span("Charge(s) : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Charges.ToString()).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                                col3.Item().Text(b => { b.Span("Ancienneté : ").SemiBold().FontFamily("Montserrat").FontSize(10); b.Span(model.Anciennete).FontColor(Colors.Black).FontSize(9).Medium().FontFamily("Montserrat"); });
                            });
                        });
                    });
                });

                // CORPS DU DOCUMENT
                page.Content().PaddingVertical(10).Element(TablePart);


                // PIED DE PAGE
                page.Footer().PaddingHorizontal(20).Column(foot =>
                {
                    // Trait plein sur toute la largeur
                    foot.Item().Height(1).Background(Colors.Black);

                    foot.Item().PaddingTop(6).Row(row =>
                    {
                        var label = TextStyle.Default.FontFamily("Montserrat").FontSize(7).SemiBold().Underline();
                        var value = TextStyle.Default.FontFamily("Montserrat").FontSize(7);

                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("Registre de commerce :").Style(label).AlignLeft();
                            c.Item().Text("BF-BBD-01-2023").Style(value).AlignLeft();
                        });

                        row.RelativeItem().AlignCenter().Column(c =>
                        {
                            c.Item().Text("Numero ifu :").Style(label).AlignCenter();
                            c.Item().Text("002001190V").Style(value).AlignCenter();
                        });

                        row.RelativeItem().AlignRight().Column(c =>
                        {
                            c.Item().Text("Numero cnss :").Style(label).AlignRight();
                            c.Item().Text("32394L").Style(value).AlignRight();
                        });
                    });
                });


            });
        }

        // Helpers de style (optionnels)
        IContainer HeadCell(IContainer c) => c
            .Background(Colors.Grey.Lighten3)
            .Border(1)
            .BorderColor(Colors.Black)
            .PaddingVertical(4)
            .AlignMiddle()
            .PaddingHorizontal(6);

        IContainer BodyCell(IContainer c) => c
            .Border(1)
            .BorderColor(Colors.Black)
            .PaddingVertical(3)
            .AlignMiddle()
            .PaddingHorizontal(6);

        // Bandeau de section (ex. "Total brut")
        void SectionBar(TableDescriptor t, string title) =>
            t.Cell().ColumnSpan(COLS).Element(c =>
                c.Background(Colors.Grey.Lighten4).Height(25)
                 .PaddingVertical(4).PaddingHorizontal(5).AlignMiddle()
                 .Border(1).BorderColor(Colors.Transparent))
             .Text(title).SemiBold().FontFamily("Montserrat").FontSize(9);


        // 8 = nombre total de colonnes de ton tableau
        const int COLS = 8;



        // Une petite aide pour ajouter une ligne
        void AddBodyRow(TableDescriptor t, string no, string lib, string @base,
                        string nbreTxTrav, string gains, string retTrav,
                        string nbreTxPat, string retPat)
        {
            t.Cell().Element(BodyCell).AlignCenter().Text(no).FontSize(9);
            t.Cell().Element(BodyCell).Text(lib).FontSize(9);
            t.Cell().Element(BodyCell).AlignRight().Text(@base).FontSize(9);

            t.Cell().Element(BodyCell).AlignCenter().Text(nbreTxTrav).FontSize(9);
            t.Cell().Element(BodyCell).AlignRight().Text(gains).FontSize(9);
            t.Cell().Element(BodyCell).AlignRight().Text(retTrav).FontSize(9);

            t.Cell().Element(BodyCell).AlignCenter().Text(nbreTxPat).FontSize(9);
            t.Cell().Element(BodyCell).AlignRight().Text(retPat).FontSize(9);
        }

        void TablePart(IContainer container)
        {
            container.Column(col =>
            {

                col.Item().Table(table =>
                {

                    SectionBar(table, "Renumerations");
                    // Colonnes (8 au total)
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(25);  // N°
                        columns.ConstantColumn(155);   // Désignation
                        columns.ConstantColumn(60);  // Base
                        columns.ConstantColumn(55);   // Trav. Nbre/taux
                        columns.ConstantColumn(85);   // Trav. Gains
                        columns.ConstantColumn(60);   // Trav. Retenues
                        columns.ConstantColumn(55);   // Pat. Nbre/taux
                        columns.ConstantColumn(60);   // Pat. Retenues
                    });

                    // ---------- EN-TÊTE (2 lignes) ----------
                    table.Header(header =>
                    {


                        // Ligne 1
                        header.Cell().RowSpan(2).Element(HeadCell).AlignCenter().Text("N°").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().RowSpan(2).Element(HeadCell).AlignCenter().Text("Désignation").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().RowSpan(2).Element(HeadCell).AlignCenter().Text("Base").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().ColumnSpan(3).Element(HeadCell).AlignCenter().Text("Part Travailleur").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().ColumnSpan(2).Element(HeadCell).AlignCenter().Text("Part Patronale").SemiBold().FontSize(8).FontFamily("Montserrat");

                        // Ligne 2
                        header.Cell().Element(HeadCell).AlignCenter().Text("Nbre/taux").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().Element(HeadCell).AlignCenter().Text("Gains").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().Element(HeadCell).AlignCenter().Text("Retenues").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().Element(HeadCell).AlignCenter().Text("Nbre/taux").SemiBold().FontSize(8).FontFamily("Montserrat");
                        header.Cell().Element(HeadCell).AlignCenter().Text("Retenues").SemiBold().FontSize(8).FontFamily("Montserrat");
                    });


                    // ---------- LIGNES MAQUETTE (sans helper) ----------
                    // 01 Salaire de base
                    table.Cell().Element(BodyCell).AlignCenter().Text("01").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Salaire de base").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.baseUnitaire.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.TauxSalaireDeBase).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.SalaireDeBase).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                    // 02 Prime Anciennete
                    table.Cell().Element(BodyCell).AlignCenter().Text("02").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Prime d 'anciennete ").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.PrimeAnciennete).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("1").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.PrimeAnciennete).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                    // 03 Heures supplémentaires
                    table.Cell().Element(BodyCell).AlignCenter().Text("03").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Heures supplémentaires").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.baseUnitaire.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.TauxHeureSupp).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.PrimeHeureSupp).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                    // 04 Indemnite 1
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Numero_indemnite_1).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text(model.Nom_Indemnite_1).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Taux_Indemnite_1).AlignRight().FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Montant_Indemnite_1).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");



                    // 05 Indemnite 2
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Numero_indemnite_2).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text(model.Nom_Indemnite_2).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Taux_Indemnite_2).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Montant_Indemnite_2).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");



                    // 06 Indemnite 3
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Numero_indemnite_3).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text(model.Nom_Indemnite_3).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Taux_Indemnite_3).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Montant_Indemnite_3).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");



                    // 07 Indemnite 4
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Numero_indemnite_4).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text(model.Nom_Indemnite_4).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Taux_Indemnite_4).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Montant_Indemnite_4).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");




                    // 08 Indemnite 5
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Numero_indemnite_5).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text(model.Nom_Indemnite_5).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Taux_Indemnite_5).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Montant_Indemnite_5).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                    SectionBar(table, "Retenues");



                    // 18 Total brut
                    table.Cell().Element(BodyCell).AlignCenter().Text("18").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Total brut").FontSize(8).FontFamily("Montserrat").AlignRight().SemiBold();
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Background(Colors.Grey.Lighten4).AlignCenter().Text(model.SalaireBrut.ToString("N2")).FontSize(8).FontFamily("Montserrat").SemiBold();
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");

                    // 19 Base IUTS
                    table.Cell().Element(BodyCell).AlignCenter().Text("19").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Base IUTS").FontSize(8).FontFamily("Montserrat").AlignRight();
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.BaseIUTS.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                    // 20 IUTS
                    table.Cell().Element(BodyCell).AlignCenter().Text("20").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("IUTS").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.BaseIUTS.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("1").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Iuts.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                    // 21 TPA 
                    table.Cell().Element(BodyCell).AlignCenter().Text("21").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Tpa").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.SalaireBrut.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.TauxTpa.ToString()).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.Tpa.ToString("N2")).FontSize(8).FontFamily("Montserrat");


                    // 22 Pensions
                    table.Cell().Element(BodyCell).AlignCenter().Text("22").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Pensions").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.SalaireBrut.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("5.5").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.CnssEmploye.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("8.5").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.CnssEmployeur.ToString("N2")).FontSize(8).FontFamily("Montserrat");


                    // 23 Risque professionnel
                    table.Cell().Element(BodyCell).AlignCenter().Text("23").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Risque professionnel").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.SalaireBrut.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("1.5").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.RisqueProfessionnel.ToString("N2")).FontSize(8).FontFamily("Montserrat");


                    // 24 Prestation familliale
                    table.Cell().Element(BodyCell).AlignCenter().Text("24").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Prestation familliale").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.SalaireBrut.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("6").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.PrestationFamiliale.ToString("N2")).FontSize(8).FontFamily("Montserrat");


                    // 25 Avantages natures
                    table.Cell().Element(BodyCell).AlignCenter().Text("25").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Avantages natures").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.SalaireBrut.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.AvantageNature.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");


                    SectionBar(table, "Net a payer");


                    // 28 Salaire Net
                    table.Cell().Element(BodyCell).AlignCenter().Text("28").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Salaire net").FontSize(8).FontFamily("Montserrat").AlignRight().SemiBold();
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Background(Colors.Grey.Lighten4).AlignCenter().Text(model.SalaireNet.ToString("N2")).FontSize(8).FontFamily("Montserrat").SemiBold();
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                    // 29 Effort de paix
                    table.Cell().Element(BodyCell).AlignCenter().Text("20").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Effort de paix").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.SalaireNet.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("1").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.EffortDePaix.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");



                    // 30 Remboursement dette
                    table.Cell().Element(BodyCell).AlignCenter().Text("21").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Remboursement dette").FontSize(8).FontFamily("Montserrat").AlignCenter();
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.ValeurDette.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("1").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text(model.ValeurDette.ToString("N2")).FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");



                    // 28 Salaire net a payer
                    table.Cell().Element(BodyCell).AlignCenter().Text("28").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).Text("Salaire net a payer").FontSize(8).FontFamily("Montserrat").AlignRight().SemiBold();
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    var fr = System.Globalization.CultureInfo.GetCultureInfo("fr-FR");
                    table.Cell().Element(BodyCell).Background(Colors.Grey.Lighten4).AlignCenter().Text(b => b.Span($"{Convert.ToDecimal(model.SalaireNetaPayerFinal).ToString("N2", fr)} FCFA").FontFamily("Montserrat").SemiBold().FontSize(8));

                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignCenter().Text("").FontSize(8).FontFamily("Montserrat");
                    table.Cell().Element(BodyCell).AlignRight().Text("").FontSize(8).FontFamily("Montserrat");


                });

                // Pied de page du bloc tableau
                // ===== Bloc Règlement / Signatures (statique) =====
                col.Item().PaddingTop(16).Column(section =>
                {
                    // Ligne 1 : Mode de paiement (pointillés)
                    section.Item().Text(t => t
                        .Span("Mode de paiement : ").SemiBold().FontFamily("Montserrat").FontSize(9));
                    // Espacement
                    section.Item().Height(7);
                    // Ligne 2 : Payé le (pointillés)
                    section.Item().Text(t => t
                        .Span("Payé le : ").SemiBold().FontFamily("Montserrat").FontSize(9));

                    // Espacement
                    section.Item().Height(10);

                    // En-têtes des 3 colonnes
                    section.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Payement autorisé par :")
                            .FontFamily("Montserrat").FontSize(9).SemiBold();

                        r.RelativeItem().AlignCenter().Text("Payement effectué par :")
                            .FontFamily("Montserrat").FontSize(9).SemiBold();

                        r.RelativeItem().AlignRight().Text("Payement reçu par :")
                            .FontFamily("Montserrat").FontSize(9).SemiBold();
                    });

                    // Noms des responsables
                    section.Item().PaddingTop(5).Row(r =>
                    {
                        r.RelativeItem().Text(model.ResponsableEntreprise ?? "")
                            .FontFamily("Montserrat").FontSize(8);

                        r.RelativeItem().AlignCenter().Text("")
                            .FontFamily("Montserrat").FontSize(8);

                        r.RelativeItem().AlignRight().Text(model.NomEmploye ?? "")
                            .FontFamily("Montserrat").FontSize(8);
                    });

                    // Lignes de signature (à remplir à la main)
                    section.Item().PaddingTop(10).Row(r =>
                    {
                        r.RelativeItem().Container().PaddingRight(8)
                            .BorderTop(1).BorderColor(Colors.Grey.Medium).Height(1);
                        r.RelativeItem().Container().PaddingHorizontal(8)
                            .BorderTop(1).BorderColor(Colors.Grey.Medium).Height(1);
                        r.RelativeItem().Container().PaddingLeft(8)
                            .BorderTop(1).BorderColor(Colors.Grey.Medium).Height(1);
                    });
                });






            });

            // --- styles locaux (juste ici, pas de helpers de ligne) ---
            

        }



        private void AddRow(TableDescriptor table, string label, decimal value)
        {
            table.Cell().Element(CellStyle).Text(label);
            table.Cell().Element(CellStyle).AlignRight().Text($"{value:C0}");
        }

        private IContainer CellStyle(IContainer container) =>
            container.PaddingVertical(5)
                     .Border(1)
                     .BorderColor(Colors.Grey.Lighten2);
    }
}




