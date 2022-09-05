using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superkatten.Katministratie.Application.Reporting;

public class ReportBuilder : IReportBuilder
{
    public string BuildSuperkattenInventory(IReadOnlyCollection<Superkat> superkatten)
    {
        var result = "Location type;Location name;Total catched;Totaal poezen retour;Totaal katten retour;Totaal kittens retour;Totaal niet retour\n";

        var catchOriginTypes = (CatchOriginType[])Enum.GetValues(typeof(CatchOriginType));

        foreach (var catchOriginType in catchOriginTypes)
        {
            var superkattenAtCatchOriginType = superkatten
                .Where(o => o.CatchLocation.Type == catchOriginType)
                .ToList();

            var locationNames = superkattenAtCatchOriginType
                .DistinctBy(o => o.CatchLocation.Name)
                .Select(o => o.CatchLocation.Name)
                .ToList();

            foreach (var locationName in locationNames)
            {
                result += catchOriginType;
                result += ";";

                result += locationName;
                result += ";";

                var superkattenAtCatchOrigin = superkattenAtCatchOriginType
                    .Where(o => o.CatchLocation.Name == locationName)
                    .ToList();

                // Total cats catched in total
                result += superkattenAtCatchOrigin
                    .Count(o => o.CatchLocation.Name == locationName)
                    .ToString("00");
                result += ";";

                // total count of molly's returned
                result += superkattenAtCatchOrigin
                    .Where(o => o.AgeCategory == AgeCategory.Adult || o.AgeCategory == AgeCategory.Juvenile)
                    .Where(o => o.Gender == Gender.Molly)
                    .Count(o => o.Retour)
                    .ToString("00");
                result += ";";

                // total count of molly's returned
                result += superkattenAtCatchOrigin
                    .Where(o => o.AgeCategory == AgeCategory.Adult || o.AgeCategory == AgeCategory.Juvenile)
                    .Where(o => o.Gender == Gender.Tomcat)
                    .Count(o => o.Retour)
                    .ToString("00");
                result += ";";

                // total count of kittens returned
                result += superkattenAtCatchOrigin
                    .Where(o => o.AgeCategory == AgeCategory.Kitten)
                    .Count(o => o.Retour)
                    .ToString("00");
                result += ";";

                // Total count of cats socialized and not returned
                result += superkattenAtCatchOrigin
                    .Count(o => !o.Retour)
                    .ToString("00");
                result += "\n";
            }
        }

        return result;
    }
}
