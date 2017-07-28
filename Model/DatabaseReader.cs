using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

public class DatabaseReader
{
    public static List<DatabaseEntry> LoadFromText(string inText)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        List<DatabaseEntry> databaseEntryList = new List<DatabaseEntry>();
        CsvReader csvReader = new CsvReader((TextReader)new StringReader(inText));
        while (csvReader.Read())
        {
            if (csvReader.CurrentRecord.Length > csvReader.FieldHeaders.Length)
                throw new System.Exception(string.Format("Field with more entries than there are columns - some data will be ignored! Check your data! File: {0}, line: {1}, columns: {2}, values in row: {3}", "", (object)csvReader.Row, (object)csvReader.FieldHeaders.Length, (object)csvReader.CurrentRecord.Length));
            if (csvReader.CurrentRecord.Length < csvReader.FieldHeaders.Length)
            {
                throw new System.Exception(string.Format("Field with fewer entries than there are columns - data is missing and can't be read. This row is being skipped. Check your data! File: {0}, line: {1}, columns: {2}, values in row: {3}", "", (object)csvReader.Row, (object)csvReader.FieldHeaders.Length, (object)csvReader.CurrentRecord.Length));
            }
            else
            {
                Dictionary<string, object> inData = new Dictionary<string, object>();
                for (int index = 0; index < csvReader.FieldHeaders.Length; ++index)
                {
                    string field;
                    if (!csvReader.TryGetField<string>(index, out field))
                    {
                        throw new System.Exception("Bad field");
                    }
                    else
                    {
                        if (field == string.Empty)
                            field = "0";
                        object obj = (object)field;
                        int result1;
                        if (int.TryParse(field, out result1))
                        {
                            obj = (object)result1;
                        }
                        else
                        {
                            float result2;
                            if (float.TryParse(field, out result2))
                                obj = (object)result2;
                        }
                        inData[csvReader.FieldHeaders[index]] = obj;
                    }
                }
                DatabaseEntry databaseEntry = new DatabaseEntry(inData);
                databaseEntryList.Add(databaseEntry);
            }
        }
        return databaseEntryList;
    }
}
