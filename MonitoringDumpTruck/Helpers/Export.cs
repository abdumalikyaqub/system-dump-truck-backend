using MonitoringDumpTruck.Models.Entities;
using System.Globalization;
using System.Text;

namespace MonitoringDumpTruck.Helpers;

public class Export
{
    internal static string ConvertToCsv(IEnumerable<DumpTruck> dumpTrucks)
    {
        var sb = new StringBuilder();

        // Добавляем заголовок CSV файла
        sb.AppendLine("Id,Model,YearIssue,GosNumber,KPP,LoadCapacity,BodyVolume,TOIR,MaxSpeed,FullMass,Mileage,MaxFuel,EngineModel,TireModel");

        // Проходим по каждому объекту DumpTruck и добавляем его данные в CSV строку
        foreach (var dumpTruck in dumpTrucks)
        {
            sb.AppendLine($"{dumpTruck.Id},{EscapeCsvField(dumpTruck.Model)},{dumpTruck.YearIssue?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)},{EscapeCsvField(dumpTruck.GosNumber)},{EscapeCsvField(dumpTruck.KPP)},{dumpTruck.LoadCapacity},{dumpTruck.BodyVolume},{EscapeCsvField(dumpTruck.TOIR)},{dumpTruck.MaxSpeed},{dumpTruck.FullMass},{dumpTruck.Mileage},{dumpTruck.MaxFuel},{EscapeCsvField(dumpTruck.EngineModel)},{EscapeCsvField(dumpTruck.TireModel)}");
        }

        return sb.ToString();
    }

    // Дополнительная функция для экранирования полей CSV
    static string EscapeCsvField(string field)
    {
        // Если строка содержит запятую, кавычки или новую строку, оборачиваем ее в кавычки и экранируем кавычки внутри строки
        if (!string.IsNullOrEmpty(field) && (field.Contains(",") || field.Contains("\"") || field.Contains("\n")))
        {
            return "\"" + field.Replace("\"", "\"\"") + "\"";
        }
        else
        {
            return field;
        }
    }

}
