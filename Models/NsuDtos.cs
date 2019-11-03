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
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string Address { get; set; }
            public bool IsActive { get; set; }
            public string Role { get; set; }
            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public bool IsDeleted { get; set; }
            public string DeletionUserId { get; set; }
            public DateTime DeletionTime { get; set; }
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

        public class Incentives
        {
            public int Id { get; set; }
            public string IncentiveName { get; set; }
            public string Description { get; set; }

            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
        }
        public class Contact
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Message { get; set; }

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
            public string CacRegistrationNumber { get; set; }
            public string CompanyName { get; set; }
            public string CompanyType { get; set; }
            public string AlternateCompanyName { get; set; }

            public List<Payments> Payments { get; set; }
            public List<Comp_Incentives> Comp_Incentives { get; set; }
            public string AlternateCompanyType { get; set; }
            public string FinancialYearEnd { get; set; }
            public Package Package { get; set; }
            public User User { get; set; }
            public string BusinessActivity { get; set; }
            public string ApprovalStatus { get; set; }
            public string SndBusinessActivity { get; set; }
            public bool IsAddressRegistered { get; set; }
            public bool LocalResidentDirector { get; set; }
            public decimal LocalResidentDirectorPrice { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Postcode { get; set; }
            public bool LocalDirector { get; set; }
            public string CompanyCapitalCurrency { get; set; }
            public string ShareHolderName { get; set; }
            public int NoOfSharesIssue { get; set; }
            public bool RegCompleted { get; set; }
            public bool IsCacAvailable { get; set; }
            public Decimal SharePrice { get; set; }
            public Decimal SharesAllocated { get; set; }
            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
            public List<AddOnService> addOnServices { get; set; }
            public List<Company_Officers> company_Officers { get; set; }
        }

       
        public class AddOnService
        {
            [Key]
            public Guid Id { get; set; }
            public Company_Registration Registration { get; set; }
            public string ServiceName {get;set;}
            public decimal Price { get; set; }
            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
        }
        public class Payments
        {
            [Key]
            public Guid Id { get; set; }
            public string ApiRequest { get; set; }
            public string ApiResponse { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }
            public string PaymentType { get; set; }
            public decimal Tax { get; set; }
            public decimal Discount { get; set; }
            public decimal Total { get; set; }
            public decimal Amount { get; set; }
            public Company_Registration Registration { get; set; }
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
        public class Comp_Incentives
        {
            public int Id { get; set; }
            public Incentives Incentive { get; set; }
            public Company_Registration Registration { get; set; }
            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class ChatHeader
        {
            public int Id { get; set; }
            public User User { get; set; }
            public Company_Registration Company { get; set; }
            public string Group { get; set; }
            public string Subject { get; set; }
            public List<ChatThread> ChatThread { get; set; }
            public string Body { get; set; }
            public DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime ModificationTime { get; set; }
            public string ModificationUserId { get; set; }
            public DateTime DeletionTime { get; set; }
            public string DeletionUserId { get; set; }
            public bool IsDeleted { get; set; }
        }


        public class ChatThread
        {
            public int Id { get; set; }
            public User User { get; set; }
            public ChatHeader Chat { get; set; }
            public string Body { get; set; }
            public bool IsRead { get; set; }
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
