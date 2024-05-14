using Microsoft.Data.SqlClient;
using System.Data;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AcademyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

string commandString = "SELECT * FROM Department; SELECT * FROM Teacher;";
string commandStringDepartment = "SELECT * FROM Department";
string commandStringTeacher = "SELECT * FROM Teacher";
using (SqlConnection connection = new SqlConnection(connectionString))
{
    SqlDataAdapter adapter = new SqlDataAdapter(commandString, connection);

    //SqlDataAdapter adapterDepartment = new SqlDataAdapter(commandStringDepartment, connection);
    //SqlDataAdapter adapterTeacher = new SqlDataAdapter(commandStringTeacher, connection);

    DataSet dataSet = new DataSet();

    adapter.Fill(dataSet);

    //adapterDepartment.Fill(dataSet);
    //adapterTeacher.Fill(dataSet);

    //foreach(DataTable table in dataSet.Tables)
    //{
    //    foreach(DataColumn column in table.Columns)
    //        Console.Write($"| {column.ColumnName} |");
    //    Console.WriteLine($"\n{new string('-', 30)}");

    //    foreach (DataRow row in table.Rows)
    //    {
    //        var rowArray = row.ItemArray;
    //        foreach(object item in rowArray)
    //            Console.Write($"| {item} |");
    //        Console.WriteLine();
    //    }
    //    Console.WriteLine($"\n\n");
    //}



    var tableDeparts = dataSet.Tables[0];
    var tableTeachers = dataSet.Tables[1];

    foreach(DataRow d in tableDeparts.Rows)
    {
        Console.WriteLine($"{d["id"]}: {d["title"]}");
        foreach(DataRow t in tableTeachers.Rows)
            if ((int)t["department_id"] == (int)d["id"])
                Console.WriteLine($"\t{t["id"]}: {t["last_name"]} {t["first_name"]}");
        Console.Write($"Input id for boss department: ");
        int headId = Int32.Parse(Console.ReadLine());
        d["head_id"] = headId;
    }

    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
    adapter.Update(dataSet);
}