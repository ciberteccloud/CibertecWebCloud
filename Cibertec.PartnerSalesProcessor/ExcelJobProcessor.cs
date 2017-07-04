using Cibertec.UnitOfWork;
using Quartz;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cibertec.PartnerSalesProcessor
{
    public class ExcelJobProcessor : IJob
    {
        private readonly string filePath;
        private readonly string historyPath;
        public ExcelJobProcessor()
        {
            filePath = Properties.Settings.Default.FolderToListen;
            historyPath = Properties.Settings.Default.HistoryFiles;
        }

        public void Execute(IJobExecutionContext context)
        {
            Task.Run(() =>
            {
                Console.WriteLine("Process Running");
                if (!Directory.Exists(filePath)) return;

                var files = Directory.GetFiles(filePath, "*.xlsx");
                if (!files.Any())
                {
                    Console.WriteLine("Nothing to process");
                    return;
                }

                foreach (var fileName in files)
                {
                    var processExcel = new ProcessSale(new CibertecUnitOfWork());
                    processExcel.ReadExcel(fileName);
                    File.Move(fileName, $"{historyPath}\\{Path.GetFileName(fileName)}");
                    Console.WriteLine($"File {Path.GetFileName(fileName)} completed");
                }
            });
        }
    }
}
