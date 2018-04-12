using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ForestInteractiveTest.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public static List<Contact> GetContactList()
        {
            using (UsersContext dbContext = new UsersContext())
            {
                return dbContext.Contact.OrderBy(p => p.Id).ToList();
            }
        }

        public static void DoImport(string filePath)
        {
            HSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new HSSFWorkbook(file);
            }

            ISheet sheet = hssfwb.GetSheet("Sheet1");

            using (var db = new UsersContext())
            {
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null)
                    {
                        Contact model = new Contact
                        {
                            FirstName = sheet.GetRow(row).GetCell(0).StringCellValue,
                            LastName = sheet.GetRow(row).GetCell(1).StringCellValue,
                            PhoneNumber = sheet.GetRow(row).GetCell(2).StringCellValue,
                            Address = sheet.GetRow(row).GetCell(3).StringCellValue,
                            Gender = sheet.GetRow(row).GetCell(4).StringCellValue
                        };
                        db.Contact.Add(model);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}