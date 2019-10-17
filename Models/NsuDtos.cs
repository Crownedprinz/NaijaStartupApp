using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NaijaStartupApp.Models
{
    public class NsuDtos
    {
        public class User : IdentityUser
        {
            public bool IsActive {get; set;}
            //public DateTime CreationTime { get; set; }
            //public string CreatorUserId { get; set; }
            //public DateTime ModificationTime { get; set; }
            //public string ModificationUserId { get; set; }
            //public DateTime DeletionTime { get; set; }
            //public string DeletionUserId { get; set; }
            //public bool IsDeleted { get; set; }
        }

        public class Package
        {
            public int Id { get; set; }
            public string PackageName { get; set; }
            public Decimal Price { get; set; }

            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class Company_Registration
        {
            [Key]
            public Guid Id { get; set; }
            public string CompanyName { get; set; }
            public string CompanyType { get; set; }
            public string AlternateCompanyName { get; set; }
            public string AlternateCompanyType { get; set; }
            public string FinancialYearEnd { get; set; }
            public Package Package { get; set; }
            public User User { get; set; }
            public string BusinessActivity { get; set; }
            public string SndBusinessActivity { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Postcode { get; set; }
            public bool LocalDirector { get; set; }
            public string CompanyCapitalCurrency { get; set; }
            public string ShareHolderName { get; set; }
            public int NoOfSharesIssue { get; set; }
            public Decimal SharePrice { get; set; }
            public Decimal SharesAllocated { get; set; }
            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class Company_Officers
        {
            [Key]
            public Guid Id { get; set; }
            public Company_Registration Registration { get; set; }
            public string FullName { get; set; }
            public string Gender { get; set; }
            public string Designation { get; set; }
            public string Id_Type { get; set; }
            public string Id_Number { get; set; }
            public string Nationality { get; set; }
            public string Birth_Country { get; set; }
            public string Phone_No { get; set; }
            public string Dob { get; set; }
            public string Email { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string PostalCode { get; set; }
            public string MobileNo { get; set; }
            public byte[] PassportFile { get; set; }
            public byte[] AddressFile { get; set; }
            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}
