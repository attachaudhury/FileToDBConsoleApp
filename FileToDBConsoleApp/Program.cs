//using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileToDBConsoleApp
{
    class Program
    {
        static int i = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("running");
            //var searchTerm = "";
            //var searchDirectory = new System.IO.DirectoryInfo(@"c:\Article fiesta");

            //var queryMatchingFiles =
            //        from file in searchDirectory.GetFiles()
            //        where file.Extension == ".txt"
            //        let fileContent = System.IO.File.ReadAllText(file.FullName)
            //        where fileContent.Contains(searchTerm)
            //        select file.FullName;

            //foreach (var fileName in queryMatchingFiles)
            //{
            //    Console.WriteLine(fileName);
            //}

            DirSearch(@"E:\dev\akbar\article utf");

        }
        static List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            string connetionString = null;
            SqlConnection cnn;
            connetionString =
            "Data Source=localhost;" +
            "Initial Catalog=FileToDB;" +
            "Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);




            SqlCommand sqlCmd3 = new SqlCommand();

            sqlCmd3.CommandText = "INSERT INTO cat (name) OUTPUT INSERTED.ID VALUES (@Name) ";
            sqlCmd3.Parameters.AddWithValue("@Name", Path.GetFileName(sDir));
            sqlCmd3.Connection = cnn;

            if (cnn != null && cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            //sqlCmd3.ExecuteNonQuery().ToString();
            Int32 newId = (Int32)sqlCmd3.ExecuteScalar();

            cnn.Close();




            foreach (string f in Directory.GetFiles(sDir))
            {
                Console.WriteLine(i++);
                string filename = Path.GetFileNameWithoutExtension(f);
                string fileType = Path.GetExtension(f);
                string content = "";
                if (fileType == ".txt")
                {
                    content = System.IO.File.ReadAllText(f);
                    SqlCommand sqlCmd2 = new SqlCommand();

                    sqlCmd2.CommandText = "INSERT INTO article (title,content,cat) VALUES (@TypeId, @ProductId, @Url) ";
                    sqlCmd2.Parameters.AddWithValue("@TypeId", filename);
                    sqlCmd2.Parameters.AddWithValue("@ProductId", content);
                    sqlCmd2.Parameters.AddWithValue("@Url", newId);
                    sqlCmd2.Connection = cnn;

                    if (cnn != null && cnn.State == ConnectionState.Closed)
                    {
                        cnn.Open();
                    }
                    sqlCmd2.ExecuteNonQuery().ToString();
                    cnn.Close();
                }

                //else if (fileType == ".doc" || fileType == ".docx")
                //{
                //    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                //    object miss = System.Reflection.Missing.Value;
                //    object path = f;
                //    object readOnly = true;
                //    Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                //    for (int i = 0; i < docs.Paragraphs.Count; i++)
                //    {
                //        content += " \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString();
                //    }
                //    //Console.WriteLine(totaltext);
                //    docs.Close();
                //    word.Quit();
                //}

            }
            foreach (string d in Directory.GetDirectories(sDir))
            {
                files.AddRange(DirSearch(d));
            }


            return files;




        }
        //static List<String> DirSearch1(string sDir)
        //{

        //    List<String> files = new List<String>();
        //    try
        //    {
        //        foreach (string f in Directory.GetFiles(sDir))
        //        {
        //            string content = System.IO.File.ReadAllText(f);

        //            files.Add(f);


        //        }
        //        foreach (string d in Directory.GetDirectories(sDir))
        //        {
        //            files.AddRange(DirSearch1(d));
        //        }
        //    }
        //    catch (System.Exception excpt)
        //    {

        //    }

        //    return files;
        //}

    }
}
