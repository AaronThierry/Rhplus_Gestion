using System;
using System.Collections.Generic;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace RH_GRH
{
    /// <summary>
    /// Document PDF consolidé contenant plusieurs bulletins de paie
    /// </summary>
    public class BulletinConsolideDocument : IDocument
    {
        private readonly List<BulletinModel> bulletins;

        public BulletinConsolideDocument(List<BulletinModel> bulletins)
        {
            this.bulletins = bulletins ?? new List<BulletinModel>();
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0); // Pas de marge globale, chaque bulletin gère ses marges
                page.PageColor(Colors.White);

                page.Content().Column(column =>
                {
                    bool isFirst = true;
                    foreach (var bulletin in bulletins)
                    {
                        // Saut de page avant chaque bulletin (sauf le premier)
                        if (!isFirst)
                        {
                            column.Item().PageBreak();
                        }
                        isFirst = false;

                        // Ajouter le bulletin
                        column.Item().Component(new BulletinSingleComponent(bulletin));
                    }
                });
            });
        }
    }

    /// <summary>
    /// Composant pour un seul bulletin dans le PDF consolidé
    /// Reprend la structure de BulletinDocument mais comme composant
    /// </summary>
    public class BulletinSingleComponent : IComponent
    {
        private readonly BulletinModel model;

        public BulletinSingleComponent(BulletinModel model)
        {
            this.model = model;
        }

        public void Compose(IContainer container)
        {
            container.Padding(20).Column(column =>
            {
                // En-tête
                column.Item().Row(row =>
                {
                    // Logo entreprise (si disponible)
                    if (model.LogoEntreprise != null && model.LogoEntreprise.Length > 0)
                    {
                        row.RelativeItem(1).Height(80).AlignLeft().AlignMiddle()
                            .Image(model.LogoEntreprise);
                    }
                    else
                    {
                        row.RelativeItem(1).Height(80).AlignLeft().AlignMiddle()
                            .Text(model.Sigle ?? model.NomEntreprise)
                            .FontSize(24).Bold().FontColor(Colors.Blue.Darken2);
                    }

                    // Informations entreprise
                    row.RelativeItem(2).Column(col =>
                    {
                        col.Item().Text(model.NomEntreprise ?? "").FontSize(12).Bold();
                        col.Item().Text(model.AdressePhysiqueEntreprise ?? "").FontSize(9);
                        col.Item().Text($"Tél: {model.TelephoneEntreprise ?? ""}").FontSize(9);
                        col.Item().Text($"Email: {model.EmailEntreprise ?? ""}").FontSize(9);
                    });
                });

                column.Item().PaddingVertical(10).LineHorizontal(2).LineColor(Colors.Blue.Darken2);

                // Titre
                column.Item().PaddingVertical(10).AlignCenter()
                    .Text("BULLETIN DE PAIE")
                    .FontSize(16).Bold().FontColor(Colors.Blue.Darken2);

                // Période
                column.Item().AlignCenter().Text($"Période: {model.Periode}").FontSize(10);

                column.Item().PaddingVertical(5);

                // Informations employé
                column.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Row(row =>
                {
                    row.RelativeItem(1).Column(col =>
                    {
                        col.Item().Text(t =>
                        {
                            t.Span("Nom: ").Bold();
                            t.Span(model.NomEmploye ?? "").FontSize(10);
                        });
                        col.Item().Text(t =>
                        {
                            t.Span("Matricule: ").Bold();
                            t.Span(model.Matricule ?? "").FontSize(10);
                        });
                        col.Item().Text(t =>
                        {
                            t.Span("Poste: ").Bold();
                            t.Span(model.Poste ?? "").FontSize(10);
                        });
                        col.Item().Text(t =>
                        {
                            t.Span("Catégorie: ").Bold();
                            t.Span(model.Categorie ?? "").FontSize(10);
                        });
                    });

                    row.RelativeItem(1).Column(col =>
                    {
                        col.Item().Text(t =>
                        {
                            t.Span("Service: ").Bold();
                            t.Span(model.Service ?? "").FontSize(10);
                        });
                        col.Item().Text(t =>
                        {
                            t.Span("Direction: ").Bold();
                            t.Span(model.Direction ?? "").FontSize(10);
                        });
                        col.Item().Text(t =>
                        {
                            t.Span("N° CNSS: ").Bold();
                            t.Span(model.NumeroCNSSEmploye ?? "").FontSize(10);
                        });
                        col.Item().Text(t =>
                        {
                            t.Span("Contrat: ").Bold();
                            t.Span(model.Contrat ?? "").FontSize(10);
                        });
                    });
                });

                column.Item().PaddingVertical(10);

                // Tableau des éléments de paie
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                    });

                    // En-tête du tableau
                    table.Header(header =>
                    {
                        header.Cell().Background(Colors.Blue.Darken2).Padding(5)
                            .Text("Désignation").FontColor(Colors.White).Bold().FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken2).Padding(5)
                            .Text("Base").FontColor(Colors.White).Bold().FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken2).Padding(5)
                            .Text("Taux").FontColor(Colors.White).Bold().FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken2).Padding(5)
                            .Text("Montant").FontColor(Colors.White).Bold().FontSize(9);
                    });

                    // Lignes du tableau
                    int rowIndex = 0;

                    // Salaire de base
                    AjouterLigne(table, ref rowIndex, "Salaire de base",
                        model.NbJourHeure.ToString(), FormatMontant(model.TauxSalaireDeBase), FormatMontant(model.SalaireDeBase));

                    // Heures supplémentaires
                    if (model.HeuresSup > 0)
                    {
                        AjouterLigne(table, ref rowIndex, "Heures supplémentaires",
                            model.HeuresSup.ToString("F2"), FormatMontant(model.TauxHeureSupp), FormatMontant(model.PrimeHeureSupp));
                    }

                    // Prime d'ancienneté
                    if (model.PrimeAnciennete > 0)
                    {
                        AjouterLigne(table, ref rowIndex, $"Prime d'ancienneté ({model.Anciennete})",
                            "-", "-", FormatMontant((double)model.PrimeAnciennete));
                    }

                    // Ligne vide
                    AjouterLigneVide(table, ref rowIndex);

                    // Salaire brut
                    AjouterLigneTotale(table, ref rowIndex, "SALAIRE BRUT", FormatMontant(model.SalaireBrut));

                    // Ligne vide
                    AjouterLigneVide(table, ref rowIndex);

                    // Déductions
                    table.Cell().ColumnSpan(4).Background(Colors.Grey.Lighten3).Padding(5)
                        .Text("DÉDUCTIONS").Bold().FontSize(9);
                    rowIndex++;

                    AjouterLigne(table, ref rowIndex, "CNSS Employé (5.5%)",
                        FormatMontant(model.SalaireBrut), "5.5%", FormatMontant(model.CnssEmploye));

                    AjouterLigne(table, ref rowIndex, "IUTS",
                        FormatMontant(model.BaseIUTS), $"{model.Charges} ch.", FormatMontant(model.Iuts));

                    // Ligne vide
                    AjouterLigneVide(table, ref rowIndex);

                    // Salaire net
                    AjouterLigneTotale(table, ref rowIndex, "SALAIRE NET", FormatMontant((double)model.SalaireNet));

                    // Effort de paix
                    if (model.EffortDePaix > 0)
                    {
                        AjouterLigne(table, ref rowIndex, "Effort de paix (1%)",
                            "-", "-", FormatMontant((double)model.EffortDePaix));
                    }

                    // Dette
                    if (model.ValeurDette > 0)
                    {
                        AjouterLigne(table, ref rowIndex, "Dette",
                            "-", "-", FormatMontant((double)model.ValeurDette));
                    }

                    // Ligne vide
                    AjouterLigneVide(table, ref rowIndex);

                    // Net à payer
                    AjouterLigneTotale(table, ref rowIndex, "NET À PAYER",
                        FormatMontant((double)model.SalaireNetaPayerFinal), Colors.Green.Darken2);
                });

                // Cotisations patronales
                column.Item().PaddingVertical(10).Border(1).BorderColor(Colors.Grey.Lighten2)
                    .Padding(10).Column(col =>
                    {
                        col.Item().Text("Cotisations patronales").Bold().FontSize(10);
                        col.Item().PaddingVertical(2).Row(row =>
                        {
                            row.RelativeItem().Text($"• Pension (8,5%): {FormatMontant(model.CnssEmployeur)}").FontSize(8);
                            row.RelativeItem().Text($"• Risque pro. (1,5%): {FormatMontant(model.RisqueProfessionnel)}").FontSize(8);
                            row.RelativeItem().Text($"• Prestation fam. (6%): {FormatMontant(model.PrestationFamiliale)}").FontSize(8);
                        });
                    });

                // Pied de page
                column.Item().PaddingVertical(20).AlignCenter()
                    .Text($"Document généré le {DateTime.Now:dd/MM/yyyy à HH:mm}")
                    .FontSize(8).Italic().FontColor(Colors.Grey.Darken1);
            });
        }

        private void AjouterLigne(TableDescriptor table, ref int rowIndex, string designation,
            string baseVal, string taux, string montant)
        {
            var bgColor = rowIndex % 2 == 0 ? Colors.White : Colors.Grey.Lighten4;

            table.Cell().Background(bgColor).Padding(5).Text(designation).FontSize(9);
            table.Cell().Background(bgColor).Padding(5).AlignRight().Text(baseVal).FontSize(9);
            table.Cell().Background(bgColor).Padding(5).AlignRight().Text(taux).FontSize(9);
            table.Cell().Background(bgColor).Padding(5).AlignRight().Text(montant).FontSize(9).Bold();

            rowIndex++;
        }

        private void AjouterLigneVide(TableDescriptor table, ref int rowIndex)
        {
            table.Cell().ColumnSpan(4).PaddingVertical(3);
            rowIndex++;
        }

        private void AjouterLigneTotale(TableDescriptor table, ref int rowIndex, string label, string montant,
            string couleur = null)
        {
            var color = couleur ?? Colors.Blue.Darken2;

            table.Cell().ColumnSpan(3).Background(Colors.Grey.Lighten3).Padding(5)
                .Text(label).Bold().FontSize(10).FontColor(color);
            table.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignRight()
                .Text(montant).Bold().FontSize(10).FontColor(color);

            rowIndex++;
        }

        private string FormatMontant(double montant)
        {
            return $"{montant:N0} F";
        }

        private string FormatMontant(decimal montant)
        {
            return $"{montant:N0} F";
        }
    }
}
