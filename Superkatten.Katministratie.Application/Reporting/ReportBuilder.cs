using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superkatten.Katministratie.Application.Reporting;

public class ReportBuilder : IReportBuilder
{
    public string BuildSuperkattenInventory(IReadOnlyCollection<Superkat> superkatten)
    {
        var sortedByLocation = superkatten
            .OrderBy(o => o.CatchDate)
            .ToList();

        var locations = sortedByLocation.DistinctBy(o => o.CatchLocation.Type).Select(o => o.CatchLocation).ToList();

        var result = "Location type;Total catched;Totaal poezen retour;Totaal katten retour;Totaal kittens retour;Totaal niet retour\n";
        foreach(var location in locations)
        {
            result += location.Type;
            result += ";";

            // Total cats catched in total
            result += superkatten
                .Count(o => o.CatchLocation.Type == location.Type)
                .ToString("00");
            result += ";";

            // total count of molly's returned
            result += superkatten
                .Where(o => o.CatchLocation.Type == location.Type)
                .Where(o => o.AgeCategory == AgeCategory.Adult || o.AgeCategory == AgeCategory.Juvenile)
                .Where(o => o.Gender == Gender.Molly)
                .Count(o => o.Retour)
                .ToString("00");
            result += ";";

            // total count of molly's returned
            result += superkatten
                .Where(o => o.CatchLocation.Type == location.Type)
                .Where(o => o.AgeCategory == AgeCategory.Adult || o.AgeCategory == AgeCategory.Juvenile)
                .Where(o => o.Gender == Gender.Tomcat)
                .Count(o => o.Retour)
                .ToString("00");
            result += ";";

            // total count of kittens returned
            result += superkatten
                .Where(o => o.CatchLocation.Type == location.Type)
                .Where(o => o.AgeCategory == AgeCategory.Kitten)
                .Count(o => o.Retour)
                .ToString("00");
            result += ";";

            // Total count of cats socialized and not returned
            result += superkatten
                .Where(o => o.CatchLocation.Type == location.Type)
                .Count(o => !o.Retour)
                .ToString("00");
            result += "\n";
        }

        return result;
    }
}
