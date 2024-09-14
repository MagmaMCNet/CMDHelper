using Spectre.Console;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;

#pragma warning disable SYSLIB0014 // Type or member is obsolete
internal class Program
{
    static void Main(string[] args)
    {
        
        if (DoesThisList.Includes(args, "Function=BoldText"))
        {
            int color = 1; // Red
            if (DoesThisList.Includes(args, "Color=Green"))
                color = 2;
            if (DoesThisList.Includes(args, "Color=Yellow"))
                color = 3;
            else if(DoesThisList.Includes(args, "Color=Blue"))
                color = 4;
            else if(DoesThisList.Includes(args, "Color=Purple"))
                color = 5;
            else if(DoesThisList.Includes(args, "Color=Light Blue"))
                color = 6;
            else if (DoesThisList.Includes(args, "Color=White"))
                color = 7;

            AnsiConsole.Write(new FigletText(DoesThisList.Return.IncludesStartsWith(args, "Text=")).Centered().Color(Color.FromInt32(color)));
        }

        else if (DoesThisList.Includes(args, "Function=Text"))
        {
            if (DoesThisList.Includes(args, "Position=Center"))
                AnsiConsole.Write(new Markup(DoesThisList.Return.IncludesStartsWith(args, "Text=")).Centered());
            else if (DoesThisList.Includes(args, "Position=Right"))
                AnsiConsole.Write(new Markup(DoesThisList.Return.IncludesStartsWith(args, "Text=")).RightAligned());
            else
                AnsiConsole.Write(new Markup(DoesThisList.Return.IncludesStartsWith(args, "Text=")));

        }

        else if (DoesThisList.Includes(args, "Function=DownloadFile"))
        {
            using (var client = new WebClient())
            {
                string FileName = DoesThisList.Return.IncludesStartsWith(args, "FileName=");
                string Url = DoesThisList.Return.IncludesStartsWith(args, "Url=");

                if (DoesThisList.Includes(args, "Print=true"))
                    AnsiConsole.MarkupLine($"[blue]Downloading [cyan]{FileName}[/][/]");

                client.DownloadFile(Url, FileName);

                if (DoesThisList.Includes(args, "Print=true"))
                    AnsiConsole.MarkupLine($"[blue]Download [cyan]{FileName}[/] Has Completed.[/]");


                
            }
        }

        else if (DoesThisList.Includes(args, "Function=UnZip"))
        {
            if (DoesThisList.Includes(args, "Print=true"))
                AnsiConsole.MarkupLine($"[blue]Extracting [cyan]{DoesThisList.Return.IncludesStartsWith(args, "FileName=")}[/] To [cyan]{DoesThisList.Return.IncludesStartsWith(args, "Extract=")}[/][/]");
            ZipFile.ExtractToDirectory(
                    Path.GetFullPath(DoesThisList.Return.IncludesStartsWith(args, "FileName=")),
                    Path.GetFullPath(DoesThisList.Return.IncludesStartsWith(args, "Extract=")));
        }
        else
        {
            AnsiConsole.WriteLine("Functions:");
            AnsiConsole.WriteLine("    Text");
            AnsiConsole.WriteLine("        example: `call CMDHelper.exe \"Function=Text\" \"Text=[blue]Hello[/]\" \"Position=Center\" `");
            AnsiConsole.WriteLine("    BoltText");
            AnsiConsole.WriteLine("        example: `call CMDHelper.exe \"Function=BoldText\" \"Text=Big Text\" \"Color=red\" `");
            AnsiConsole.WriteLine("    DownloadFile");
            AnsiConsole.WriteLine("        example: `call CMDHelper.exe \"Function=DownloadFile\" \"Url=https://cdn.magma-mc.net/DownloadZip.zip\" \"FileName=DownloadedZip.zip\" \"Print=true\" `");
            AnsiConsole.WriteLine("    UnZip");
            AnsiConsole.WriteLine("        example: `call CMDHelper.exe \"Function=UnZip\" \"FileName=DownloadedZip.zip\" \"Extract=ExtractedZip\" \"Print=true\" `");
            Console.ReadKey();
        }

    }
    class DoesThisList
    {
        public static bool Includes(string[] List, string include)
        {
            bool result = false;
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].Contains(include))
                {
                    result = true;
                }
            }
            return result;
        }
        public static bool IncludesStartsWith(string[] List, string include)
        {
            bool result = false;
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].StartsWith(include))
                {
                    result = true;
                }
            }
            return result;
        }
        public class Return
        {
            public static string IncludesStartsWith(string[] List, string include)
            {
                string result = "";
                for (int i = 0; i < List.Length; i++)
                {
                    if (List[i].StartsWith(include))
                    {
                        result = List[i].Replace(include, "");
                    }
                }
                return result;
            }
        }

    }

}